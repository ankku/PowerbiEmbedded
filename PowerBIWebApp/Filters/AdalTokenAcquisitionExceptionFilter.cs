using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net;

namespace PowerBIWebApp.Filters
{
    /// <summary>
    /// Triggers authentication if access token cannot be acquired
    /// silently, i.e. from cache.
    /// </summary>
    public class AdalTokenAcquisitionExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //If ADAL failed to acquire access token
            if (context.Exception is AdalSilentTokenAcquisitionException)
            {
                //Send user to Azure AD to re-authenticate
                context.Result = new ChallengeResult();
            }

            if (context.Exception is WebException && ((WebException)context.Exception).Status == WebExceptionStatus.ProtocolError
                && ((HttpWebResponse)((WebException)context.Exception).Response).StatusCode == HttpStatusCode.Forbidden)
            {
                //Send user to Azure AD to re-authenticate
                context.Result = new ChallengeResult();
            }
        }
    }
}