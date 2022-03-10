#pragma checksum "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceHistory.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9291e704341e116caf4ab2c00140475e99038304"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ServiceProvider_ServiceHistory), @"mvc.1.0.view", @"/Views/ServiceProvider/ServiceHistory.cshtml")]
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
#nullable restore
#line 2 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\_ViewImports.cshtml"
using Helperland.Models.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceHistory.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9291e704341e116caf4ab2c00140475e99038304", @"/Views/ServiceProvider/ServiceHistory.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8436988cf6f16b66288afee37d077d69a30fa435", @"/Views/_ViewImports.cshtml")]
    public class Views_ServiceProvider_ServiceHistory : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Helperland.Models.ViewModel.ServiceProvider.ServiceProviderServiceHistoryViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ServiceProviderMenuPartialView", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "/image/calendar2.png", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "/image/layer-712.png", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ImageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceHistory.cshtml"
  
    Layout = "_Layout";
    ViewBag.Title = "Services History ";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"container-fluid\">\r\n    <div class=\"row justify-content-center gray-bachgroud\" style=\"margin-top: 100px; padding-top: 2rem; padding-bottom: 2rem;\">\r\n        <h1 class=\"text-center\"> Welcome, ");
#nullable restore
#line 9 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceHistory.cshtml"
                                     Write(Context.Session.GetString("username"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h1>\r\n    </div>\r\n    <div class=\"row justify-content-center mt-2\">\r\n        <div class=\"col-md-2\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "9291e704341e116caf4ab2c00140475e990383045207", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
        </div>
        <div class=""col-md-9"">
            <div class=""row p-3"">
                <div class=""col-md-12 table-responsive"">
                    <table id=""tbleServiceRequestHistory"" class=""table table-striped"" >
                        <thead>
                            <tr>
                                <td>Service Id </td>
                                <td>Service Date </td>
                                <td>Customer Details </td>
                            </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 27 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceHistory.cshtml"
                             foreach (var item in Model)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr");
            BeginWriteAttribute("onclick", " onclick=\"", 1305, "\"", 1348, 3);
            WriteAttributeValue("", 1315, "OpenModel(", 1315, 10, true);
#nullable restore
#line 29 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceHistory.cshtml"
WriteAttributeValue("", 1325, item.ServiceRequestId, 1325, 22, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1347, ")", 1347, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    <td style=\"width:30%\"> ");
#nullable restore
#line 30 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceHistory.cshtml"
                                                      Write(item.ServiceRequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                                    <td class=\"flex\" style=\"width:30%\">\r\n                                        <div>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "9291e704341e116caf4ab2c00140475e990383048253", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ImageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.Src = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 32 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceHistory.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("<b> ");
#nullable restore
#line 32 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceHistory.cshtml"
                                                                                                      Write(item.ServiceDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b> </div>\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "9291e704341e116caf4ab2c00140475e9903830410235", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ImageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.Src = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
#nullable restore
#line 33 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceHistory.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@" 8.:00 - 12:00
                                    </td>
                                    <td style=""width:38%"">

                                        <div class=""d-flex"">
                                            <div class=""center""><img src=""/image/layer-15.png""></div>
                                            <div>
                                                <span class=""d-block""> ");
#nullable restore
#line 40 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceHistory.cshtml"
                                                                  Write(item.CustomerName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </span>\r\n                                                <span class=\"d-block\"> ");
#nullable restore
#line 41 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceHistory.cshtml"
                                                                  Write(item.AddressLine1);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </span>\r\n                                                <span class=\"d-block\"> ");
#nullable restore
#line 42 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceHistory.cshtml"
                                                                  Write(item.AddressLine2);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </span>\r\n\r\n                                            </div>\r\n\r\n                                        </div>\r\n\r\n\r\n                                    </td>\r\n                                </tr>\r\n");
#nullable restore
#line 51 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceHistory.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        </tbody>
                    </table>
                </div>
            </div>

        </div>
    </div>
</div>

<!-- Modal  Service Details -->
<div class=""modal fade"" id=""ServiceDeatils"" tabindex=""-1"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog modal-dialog-centered "">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"">Service Details</h5>
                <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
            </div>
            <div class=""modal-body"">
                <div class=""row"">
                    <div class=""col-md-12"" id=""ModelMainDiv"">
                        <div>
                            <span id=""ModelServiceDate"" class=""font-bold h5""></span> 8:00 - 13:00<br />
                            <span class=""font-bold""> Duration </span><span id=""ModelDuration""> </span> Hrs
                        ");
            WriteLiteral(@"</div>
                        <hr />
                        <div>
                            <span class=""font-bold""> Service Id: </span><span id=""ModelServiceRequestId""> </span>.<br />
                            <span class=""font-bold""> Extras </span> <span id=""ModelExtra""></span><br />
                            <span class=""font-bold""> Net Amount: </span> <span id=""ModelTotalCost"" class=""font-blue font-bold""></span> €<br />
                        </div>
                        <hr />
                        <div>
                            <span class=""font-bold"">Customer Name: </span><span id=""ModelCustomerName""> </span><br />
                            <span class=""font-bold""> Service Address :-  </span><span id=""ModelServiceAddress""> </span>.

                        </div>
                        <hr />
                        <div>
                            <span class=""font-bold""> Comments </span> <br /> <span id=""ModelComments""> </span>
                        </div>
     ");
            WriteLiteral(@"                   <hr />
                        <span> <img src=""/image/not-included.png"" id=""ModelHasPatImage"" /> I <span id=""ModelHasPat"">  don't </span>  have Pate at Home</span>


                    </div>
                </div>
            </div>

        </div>
    </div>
</div>


");
            DefineSection("Script", async() => {
                WriteLiteral(@"
    <link href=""https://cdn.datatables.net/1.11.3/css/jquery.dataTables.min.css"" rel=""stylesheet"">
    <script src=""https://cdn.datatables.net/1.11.3/js/jquery.dataTables.min.js""></script>
    <script>
                /*   Open Service Details Model on Click of Table Row*/
        function OpenModel(para) {
            $(""#ServiceDeatils"").modal('show');
            $(""#ModelServiceRequestId"").text(para);
            $.ajax({
                 type: ""POST"",
                 url: """);
#nullable restore
#line 115 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceHistory.cshtml"
                  Write(Url.Action("GetServiceRequestData"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""",
                 data: { ""ServiceReqestId"": para },
                 success: function (responce) {
                     var obj = JSON.parse(responce);
                     $(""#ModelDuration"").text(obj.TotalHour);
                     $(""#ModelServiceDate"").text(obj.ServiceDate);
                     $(""#ModelExtra"").text(obj.Extra);
                     $(""#ModelTotalCost"").text(obj.TotalCost);
                     $(""#ModelServiceAddress"").text(obj.Address);
                     $(""#ModelCustomerName"").text(obj.CustomerName);
                     $(""#ModelComments"").text(obj.Comments);
                     if (obj.Haspet) {
                         $(""#ModelHasPatImage"").attr(""src"", ""/image/included.png"");
                         $(""#ModelHasPat"").hide();
                     } else {
                         $(""#ModelHasPatImage"").attr(""src"", ""/image/not-included.png"");
                         $(""#ModelHasPat"").show();
                     }


                 },
                ");
                WriteLiteral(@" failure: function (response) {
                        alert(""failure"");
                 },
                 error: function (response) {
                        alert(""Something went Worng"");
                 }

              });
        }

        /* Apply Data Table*/
        $('#tbleServiceRequestHistory').dataTable({
            ""bFilter"": false,
            ""bInfo"": false,
            'aoColumnDefs': [{
                'bSortable': false,
                'aTargets': [-1] /* 1st one, start by the right */
            }]
        });
    </script>
");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Helperland.Models.ViewModel.ServiceProvider.ServiceProviderServiceHistoryViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
