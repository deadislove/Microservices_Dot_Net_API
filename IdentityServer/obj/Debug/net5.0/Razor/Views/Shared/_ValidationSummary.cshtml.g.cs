#pragma checksum "C:\Users\DavidLin\Documents\Projects_VisualStudio2019\Microservices.Api\Microservices.Api\IdentityServer\Views\Shared\_ValidationSummary.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2814345506ab4d26cef706375ef6228f6506cc86"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__ValidationSummary), @"mvc.1.0.view", @"/Views/Shared/_ValidationSummary.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2814345506ab4d26cef706375ef6228f6506cc86", @"/Views/Shared/_ValidationSummary.cshtml")]
    #nullable restore
    public class Views_Shared__ValidationSummary : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\DavidLin\Documents\Projects_VisualStudio2019\Microservices.Api\Microservices.Api\IdentityServer\Views\Shared\_ValidationSummary.cshtml"
 if (ViewContext.ModelState.IsValid == false)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"alert alert-danger\">\n    <strong>Error</strong>\n    <div asp-validation-summary=\"All\" class=\"danger\"></div>\n</div>");
#nullable restore
#line 6 "C:\Users\DavidLin\Documents\Projects_VisualStudio2019\Microservices.Api\Microservices.Api\IdentityServer\Views\Shared\_ValidationSummary.cshtml"
      }

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
