#pragma checksum "C:\Users\mlenart\Documents\GitHub\PowerbiEmbedded\PowerBIWebApp\Pages\Admin.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1344694e041d8904b81928314591deccdbc170f3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(PowerBIWebApp.Pages.Pages_Admin), @"mvc.1.0.razor-page", @"/Pages/Admin.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/Admin.cshtml", typeof(PowerBIWebApp.Pages.Pages_Admin), null)]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1344694e041d8904b81928314591deccdbc170f3", @"/Pages/Admin.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e45369a425d9263f210647fe29697c314ac7948b", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Admin : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(7, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 4 "C:\Users\mlenart\Documents\GitHub\PowerbiEmbedded\PowerBIWebApp\Pages\Admin.cshtml"
  
    ViewData["Title"] = "Admin";

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AdminModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<AdminModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<AdminModel>)PageContext?.ViewData;
        public AdminModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
