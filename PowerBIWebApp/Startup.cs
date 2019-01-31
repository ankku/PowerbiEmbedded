using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using System.IdentityModel.Tokens.Jwt;

namespace PowerBIWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*
                https://joonasw.net/view/aspnet-core-2-azure-ad-authentication
                https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-protocols-oidc
                https://dzimchuk.net/adal-distributed-token-cache-in-asp-net-core/
            */

            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()

            .AddOpenIdConnect(options =>
            {
                Configuration.GetSection("Authentication").Bind(options);
                options.ClientSecret = Configuration["statoil-powerbiapp-secret"];


                options.Events = new OpenIdConnectEvents()
                {
                    OnAuthorizationCodeReceived = AuthorizationCodeReceived,
                    OnAuthenticationFailed = AuthenticationFailed
                };

                options.SaveTokens = true;
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(Filters.AdalTokenAcquisitionExceptionFilter));

                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));

                //                options.Filters.Add(typeof(Filters.UnhandledExceptionFilter));
            })
            .AddRazorPagesOptions(options =>
            {
                options.Conventions.AllowAnonymousToFolder("/Account");
            });

            //            services.AddDistributedMemoryCache();
        }

        private async Task AuthorizationCodeReceived(AuthorizationCodeReceivedContext context)
        {
            try
            {
                context.HandleCodeRedemption();

                var request = context.HttpContext.Request;
                var currentUri = UriHelper.BuildAbsolute(request.Scheme, request.Host, request.PathBase, request.Path);
                var credential = new ClientCredential(context.Options.ClientId, context.Options.ClientSecret);
                var code = context.ProtocolMessage.Code;
                string userId = context.Principal.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

                var distributedCache = context.HttpContext.RequestServices.GetRequiredService<IDistributedCache>();
                var authContext = new AuthenticationContext(context.Options.Authority, new Utils.DistributedTokenCache(distributedCache, userId));
                AuthenticationResult result = await authContext.AcquireTokenByAuthorizationCodeAsync(code, new Uri(currentUri), credential, context.Options.Resource);
//                AuthenticationResult result = await authContext.AcquireTokenByAuthorizationCodeAsync(code, scopes);

                context.HandleCodeRedemption(result.AccessToken, result.IdToken);

                //https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/Adal-to-Msal
                /*
                                
                                var userTokenCache = new Utils.MSALSessionCache(userId, context.HttpContext).GetMsalCacheInstance();

                                Microsoft.Identity.Client.ConfidentialClientApplication app = new Microsoft.Identity.Client.ConfidentialClientApplication(context.Options.ClientId
                                                                                                                                                                , currentUri
                                                                                                                                                                , new Microsoft.Identity.Client.ClientCredential(context.Options.ClientSecret)
                                                                                                                                                                , userTokenCache
                                                                                                                                                                , null);


                                context.HandleCodeRedemption();
                                Microsoft.Identity.Client.AuthenticationResult tokenResult = await app.AcquireTokenByAuthorizationCodeAsync(code, scopes);
                                context.HandleCodeRedemption(null, tokenResult.IdToken);
                */
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            return Task.FromResult(0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
