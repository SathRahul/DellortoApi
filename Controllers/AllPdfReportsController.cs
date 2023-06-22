using DellortoApi.Modals;
using DellortoApi.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;
using System.Reflection;
using DevExpress.XtraReports.UI;
using reportProjext.Reports;

namespace DellortoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AllPdfReportsController : ControllerBase
    {

        private readonly IAllPdfReportsLogic _allPdfReportsLogic;
        public AllPdfReportsController(IConfiguration configuration)
        {
            _allPdfReportsLogic = new AllPdfReportsLogic(configuration);
        }


        [HttpPost]
        public async Task<IActionResult> GetGRNGoodBadReport([FromBody] List<GrnGoodBadReportModel> model)
        {
            string fileUrl = "", full_file_url = "";
            XtraReport report = null;
            try
            {

         
                DataTable dt = _allPdfReportsLogic.ToDataTable(model);

                report = new GrnGoodBadReport();
                report.ExportOptions.Pdf.Compressed = true;
                report.DataSource = dt;
                report.DataMember = "GrnReport";
                await report.CreateDocumentAsync().ConfigureAwait(false);

                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Reports", "GRN Report")))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Reports", "GRN Report"));
                }
                string document_file_name = Guid.NewGuid().ToString();
                document_file_name = Regex.Replace(document_file_name, @"[^0-9a-zA-Z\.]", "");

                fileUrl = "Reports/GRN Report/" + document_file_name + ".pdf";

                full_file_url = Directory.GetCurrentDirectory() + "/wwwroot/" + fileUrl;
                if (System.IO.File.Exists(full_file_url))
                {
                    System.IO.File.Delete(full_file_url);
                }

                new PdfStreamingExporter(report, true).Export(Directory.GetCurrentDirectory() + "/wwwroot/" + fileUrl);
                dt.Dispose();
                Response.Headers.Add("filename", document_file_name + ".pdf");
                return File(System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/wwwroot/" + fileUrl), "application/pdf", document_file_name + ".pdf");

            }
            catch (Exception ex)
            {
                return new  JsonResult(ex.Message);
            }
            finally
            {
                if (System.IO.File.Exists(full_file_url))
                {
                    System.IO.File.Delete(full_file_url);
                }
                if (report != null)
                    report.Dispose();

            }
        }



    }

   
}



