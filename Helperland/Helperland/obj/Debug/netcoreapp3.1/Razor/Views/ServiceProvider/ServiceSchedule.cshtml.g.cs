#pragma checksum "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceSchedule.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4efabe0fae52535609eae4c8caaa703c691d481c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ServiceProvider_ServiceSchedule), @"mvc.1.0.view", @"/Views/ServiceProvider/ServiceSchedule.cshtml")]
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
#line 5 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceSchedule.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4efabe0fae52535609eae4c8caaa703c691d481c", @"/Views/ServiceProvider/ServiceSchedule.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8436988cf6f16b66288afee37d077d69a30fa435", @"/Views/_ViewImports.cshtml")]
    public class Views_ServiceProvider_ServiceSchedule : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ServiceProviderMenuPartialView", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceSchedule.cshtml"
  
    Layout = "_Layout";
    ViewBag.Title = "Service Schedule";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"container-fluid\">\r\n    <div class=\"row justify-content-center gray-bachgroud\" style=\"margin-top: 100px; padding-top: 2rem; padding-bottom: 2rem;\">\r\n        <h1 class=\"text-center\"> Welcome, ");
#nullable restore
#line 8 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceSchedule.cshtml"
                                     Write(Context.Session.GetString("username"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h1>\r\n    </div>\r\n    <div class=\"row justify-content-center mt-2\">\r\n        <div class=\"col-md-2\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4efabe0fae52535609eae4c8caaa703c691d481c4387", async() => {
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
        <div class=""col-md-9 p-3"">
            <div class=""float-right"">
                <div style=""background-color: #1d7a8c; height: 15px; width: 15px; display: inline-block""></div>  Upcoming
                <div style=""background-color:grey; height: 15px; width: 15px; display: inline-block; margin-left: 15px;""></div> Completed
            </div>
            <div id=""calender"">

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
         ");
            WriteLiteral(@"           <div class=""col-md-12"" id=""ModelMainDiv"">
                        <div>
                            <span id=""ModelServiceDate"" class=""font-bold h5""></span> 8:00 - 13:00<br />
                            <span class=""font-bold""> Duration </span><span id=""ModelDuration""> </span> Hrs
                        </div>
                        <hr />
                        <div>
                            <span class=""font-bold""> Service Id: </span><span id=""ModelServiceRequestId""> </span>.<br />
                            <span class=""font-bold""> Extras </span> <span id=""ModelExtra""></span><br />
                            <span class=""font-bold""> Net Amount: </span> <span id=""ModelTotalCost"" class=""font-blue font-bold""></span> €<br />
                        </div>
                        <hr />
                        <div>
                            <span class=""font-bold"">Customer Name: </span><span id=""ModelCustomerName""> </span><br />
                            <span class=""font-");
            WriteLiteral(@"bold""> Service Address :-  </span><span id=""ModelServiceAddress""> </span>.

                        </div>
                        <hr />
                        <div>
                            <span class=""font-bold""> Comments </span> <br /> <span id=""ModelComments""> </span>
                        </div>
                        <hr />
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

    <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.4.0/fullcalendar.min.css"" />
    <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.4.0/fullcalendar.print.min.css"" media='print' />
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.4.0/fullcalendar.min.js""></script>
    <script>
        $(document).ready(function () {

            var ev = [];
            $.ajax({
                type: ""GET"",
                url: """);
#nullable restore
#line 82 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceSchedule.cshtml"
                 Write(Url.Action("GetUpcomingServiceRequest", "ServiceProvider"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""",
                success: function (data) {
                    $.each(data, function (i, v) {
                        ev.push({
                            title: v.id,
                            start: moment(v.start, ""DD-MM-YYYY""),
                            color: v.color,
                            allDay: true
                        });
                    })
                    GenerateCalender(ev);
                },
                error: function () {
                    alert(""fail"");
                }

            })





            function GenerateCalender(events) {
                $(""#calender"").fullCalendar('destroy');
                $(""#calender"").fullCalendar({
                    contentHeight: 450,
                    timeFormat: 'h(:mm)a',
                    defaultDate: new Date(),
                    header: {
                        left: 'prev,next,today',
                        center: 'title',
                        right : ''
             ");
                WriteLiteral(@"       },
                    eventLimit: true,
                    eventColor: ""#378006"",
                    events: events,
                    eventClick: function (calEvent, jsEvent, view) {
                        $(""#ServiceDeatils"").modal('show');
                        $(""#ModelServiceRequestId"").text(calEvent.title);
                            $.ajax({
                                 type: ""POST"",
                                url: """);
#nullable restore
#line 123 "G:\Tatvasoft\Tatvasoft_work\Helperland\Helperland\Views\ServiceProvider\ServiceSchedule.cshtml"
                                 Write(Url.Action("GetServiceRequestData"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""",
                                data: { ""ServiceReqestId"": calEvent.title },
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
                    ");
                WriteLiteral(@"                 } else {
                                         $(""#ModelHasPatImage"").attr(""src"", ""/image/not-included.png"");
                                         $(""#ModelHasPat"").show();
                                     }


                                 },
                                 failure: function (response) {
                                        alert(""failure"");
                                 },
                                 error: function (response) {
                                        alert(""Something went Worng"");
                                 }

                              });
                    }
                })
            }
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591