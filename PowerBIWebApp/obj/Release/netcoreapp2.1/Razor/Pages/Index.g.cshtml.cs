#pragma checksum "C:\Users\mlenart\Documents\GitHub\PowerbiEmbedded\PowerBIWebApp\Pages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d8a736f7614566042d0f7387951cd74e7e9bede3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(PowerBIWebApp.Pages.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/Index.cshtml", typeof(PowerBIWebApp.Pages.Pages_Index), null)]
namespace PowerBIWebApp.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\mlenart\Documents\GitHub\PowerbiEmbedded\PowerBIWebApp\Pages\_ViewImports.cshtml"
using PowerBIWebApp;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d8a736f7614566042d0f7387951cd74e7e9bede3", @"/Pages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e45369a425d9263f210647fe29697c314ac7948b", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("Select1"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page-handler", "data", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(7, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 4 "C:\Users\mlenart\Documents\GitHub\PowerbiEmbedded\PowerBIWebApp\Pages\Index.cshtml"
  
    ViewData["Title"] = "Home page";

#line default
#line hidden
            BeginContext(73, 15, true);
            WriteLiteral("\r\n<div>\r\n\r\n    ");
            EndContext();
            BeginContext(88, 436, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f574811612694cb7a61ebe8dd67509f6", async() => {
                BeginContext(132, 61, true);
                WriteLiteral("\r\n        https://analysis.windows.net/powerbi/api/\r\n        ");
                EndContext();
                BeginContext(193, 218, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "77a1efa12582409f91537dea404660f7", async() => {
                    BeginContext(230, 14, true);
                    WriteLiteral("\r\n            ");
                    EndContext();
                    BeginContext(244, 23, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a5999ad7196846b8b22bfd60d0cd3226", async() => {
                        BeginContext(252, 6, true);
                        WriteLiteral("groups");
                        EndContext();
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(267, 14, true);
                    WriteLiteral("\r\n            ");
                    EndContext();
                    BeginContext(281, 24, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ed82bc7c01bd494293f2279988b306c2", async() => {
                        BeginContext(289, 7, true);
                        WriteLiteral("reports");
                        EndContext();
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(305, 14, true);
                    WriteLiteral("\r\n            ");
                    EndContext();
                    BeginContext(319, 25, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "23c4ca50ea2845a3a539a5595e361c6e", async() => {
                        BeginContext(327, 8, true);
                        WriteLiteral("datasets");
                        EndContext();
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(344, 58, true);
                    WriteLiteral("\r\n            <!--option>admin/groups</option-->\r\n        ");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#line 12 "C:\Users\mlenart\Documents\GitHub\PowerbiEmbedded\PowerBIWebApp\Pages\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Query);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(411, 106, true);
                WriteLiteral("\r\n        <!--       <input type=\"search\" asp-for=\"Query\" />     -->\r\n        <button>Query</button>\r\n    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.PageHandler = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(524, 43, true);
            WriteLiteral("\r\n</div>\r\n\r\n<br />\r\n<table class=\"table\">\r\n");
            EndContext();
#line 25 "C:\Users\mlenart\Documents\GitHub\PowerbiEmbedded\PowerBIWebApp\Pages\Index.cshtml"
     if (Model.TableContent != null)
    {
        foreach (System.Data.DataColumn col in Model.TableContent.Columns)
        {

#line default
#line hidden
            BeginContext(699, 16, true);
            WriteLiteral("            <th>");
            EndContext();
            BeginContext(716, 14, false);
#line 29 "C:\Users\mlenart\Documents\GitHub\PowerbiEmbedded\PowerBIWebApp\Pages\Index.cshtml"
           Write(col.ColumnName);

#line default
#line hidden
            EndContext();
            BeginContext(730, 7, true);
            WriteLiteral("</th>\r\n");
            EndContext();
#line 30 "C:\Users\mlenart\Documents\GitHub\PowerbiEmbedded\PowerBIWebApp\Pages\Index.cshtml"
        }

        foreach (System.Data.DataRow row in Model.TableContent.Rows)
        {

#line default
#line hidden
            BeginContext(831, 18, true);
            WriteLiteral("            <tr>\r\n");
            EndContext();
#line 35 "C:\Users\mlenart\Documents\GitHub\PowerbiEmbedded\PowerBIWebApp\Pages\Index.cshtml"
                 foreach (System.Data.DataColumn col in Model.TableContent.Columns)
                {

#line default
#line hidden
            BeginContext(953, 25, true);
            WriteLiteral("                    <td> ");
            EndContext();
            BeginContext(979, 19, false);
#line 37 "C:\Users\mlenart\Documents\GitHub\PowerbiEmbedded\PowerBIWebApp\Pages\Index.cshtml"
                    Write(row[col].ToString());

#line default
#line hidden
            EndContext();
            BeginContext(998, 8, true);
            WriteLiteral(" </td>\r\n");
            EndContext();
#line 38 "C:\Users\mlenart\Documents\GitHub\PowerbiEmbedded\PowerBIWebApp\Pages\Index.cshtml"
                }

#line default
#line hidden
            BeginContext(1025, 19, true);
            WriteLiteral("            </tr>\r\n");
            EndContext();
#line 40 "C:\Users\mlenart\Documents\GitHub\PowerbiEmbedded\PowerBIWebApp\Pages\Index.cshtml"
        }
    }

#line default
#line hidden
            BeginContext(1062, 5390, true);
            WriteLiteral(@"</table>

<meta charset=""utf-8"">
<title>jQuery UI Datepicker functionality</title>
<link href=""https://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css""
      rel=""stylesheet"">
<script src=""https://code.jquery.com/jquery-1.10.2.js""></script>
<script src=""https://code.jquery.com/ui/1.10.4/jquery-ui.js""></script>

<!-- Javascript -->
<script>
    $(function () {
        $(""#datepicker-13"").datepicker();
        $(""#datepicker-13"").datepicker(""show"");
    });
</script>

<!-- HTML -->
<p>Enter Date: <input type=""text"" id=""datepicker-13""></p>



<!--
<div id=""myCarousel"" class=""carousel slide"" data-ride=""carousel"" data-interval=""6000"">
    <ol class=""carousel-indicators"">
        <li data-target=""#myCarousel"" data-slide-to=""0"" class=""active""></li>
        <li data-target=""#myCarousel"" data-slide-to=""1""></li>
        <li data-target=""#myCarousel"" data-slide-to=""2""></li>
    </ol>
    <div class=""carousel-inner"" role=""listbox"">
        <div class=""item active"">
            <");
            WriteLiteral(@"img src=""~/images/banner1.svg"" alt=""ASP.NET"" class=""img-responsive"" />
            <div class=""carousel-caption"" role=""option"">
                <p>
                    Learn how to build ASP.NET apps that can run anywhere.
                    <a class=""btn btn-default"" href=""https://go.microsoft.com/fwlink/?LinkID=525028&clcid=0x409"">
                        Learn More
                    </a>
                </p>
            </div>
        </div>
        <div class=""item"">
            <img src=""~/images/banner2.svg"" alt=""Visual Studio"" class=""img-responsive"" />
            <div class=""carousel-caption"" role=""option"">
                <p>
                    There are powerful new features in Visual Studio for building modern web apps.
                    <a class=""btn btn-default"" href=""https://go.microsoft.com/fwlink/?LinkID=525030&clcid=0x409"">
                        Learn More
                    </a>
                </p>
            </div>
        </div>
        <div class=""item"">
 ");
            WriteLiteral(@"           <img src=""~/images/banner3.svg"" alt=""Microsoft Azure"" class=""img-responsive"" />
            <div class=""carousel-caption"" role=""option"">
                <p>
                    Learn how Microsoft's Azure cloud platform allows you to build, deploy, and scale web apps.
                    <a class=""btn btn-default"" href=""https://go.microsoft.com/fwlink/?LinkID=525027&clcid=0x409"">
                        Learn More
                    </a>
                </p>
            </div>
        </div>
    </div>
    <a class=""left carousel-control"" href=""#myCarousel"" role=""button"" data-slide=""prev"">
        <span class=""glyphicon glyphicon-chevron-left"" aria-hidden=""true""></span>
        <span class=""sr-only"">Previous</span>
    </a>
    <a class=""right carousel-control"" href=""#myCarousel"" role=""button"" data-slide=""next"">
        <span class=""glyphicon glyphicon-chevron-right"" aria-hidden=""true""></span>
        <span class=""sr-only"">Next</span>
    </a>
</div>

<div class=""row"">
    <d");
            WriteLiteral(@"iv class=""col-md-3"">
        <h2>Application uses</h2>
        <ul>
            <li>Sample pages using ASP.NET Core Razor Pages</li>
            <li>Theming using <a href=""https://go.microsoft.com/fwlink/?LinkID=398939"">Bootstrap</a></li>
        </ul>
    </div>
    <div class=""col-md-3"">
        <h2>How to</h2>
        <ul>
            <li><a href=""https://go.microsoft.com/fwlink/?linkid=852130"">Working with Razor Pages.</a></li>
            <li><a href=""https://go.microsoft.com/fwlink/?LinkId=699315"">Manage User Secrets using Secret Manager.</a></li>
            <li><a href=""https://go.microsoft.com/fwlink/?LinkId=699316"">Use logging to log a message.</a></li>
            <li><a href=""https://go.microsoft.com/fwlink/?LinkId=699317"">Add packages using NuGet.</a></li>
            <li><a href=""https://go.microsoft.com/fwlink/?LinkId=699319"">Target development, staging or production environment.</a></li>
        </ul>
    </div>
    <div class=""col-md-3"">
        <h2>Overview</h2>
        <u");
            WriteLiteral(@"l>
            <li><a href=""https://go.microsoft.com/fwlink/?LinkId=518008"">Conceptual overview of what is ASP.NET Core</a></li>
            <li><a href=""https://go.microsoft.com/fwlink/?LinkId=699320"">Fundamentals of ASP.NET Core such as Startup and middleware.</a></li>
            <li><a href=""https://go.microsoft.com/fwlink/?LinkId=398602"">Working with Data</a></li>
            <li><a href=""https://go.microsoft.com/fwlink/?LinkId=398603"">Security</a></li>
            <li><a href=""https://go.microsoft.com/fwlink/?LinkID=699321"">Client side development</a></li>
            <li><a href=""https://go.microsoft.com/fwlink/?LinkID=699322"">Develop on different platforms</a></li>
            <li><a href=""https://go.microsoft.com/fwlink/?LinkID=699323"">Read more on the documentation site</a></li>
        </ul>
    </div>
    <div class=""col-md-3"">
        <h2>Run &amp; Deploy</h2>
        <ul>
            <li><a href=""https://go.microsoft.com/fwlink/?LinkID=517851"">Run your app</a></li>
            <li>");
            WriteLiteral(@"<a href=""https://go.microsoft.com/fwlink/?LinkID=517853"">Run tools such as EF migrations and more</a></li>
            <li><a href=""https://go.microsoft.com/fwlink/?LinkID=398609"">Publish to Microsoft Azure App Service</a></li>
        </ul>
    </div>
</div>
-->
");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel>)PageContext?.ViewData;
        public IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
