using System;
using System.IO;
using System.Threading.Tasks;
using iText.IO.Image;
using iText.IO.Source;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// InvoicePage Object
    /// </summary>
    public static class InvoicePage
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("InvoicePage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get rates request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string invoiceNumber = data?.invoiceNumber;
            string invoiceDate = data?.invoiceDate;
            string subject = data?.subject;
            string itemName = data?.itemName;
            string sac = data?.sac;
            string cgst = data?.cgst;
            string sgst = data?.sgst;
            string total = data?.total;
            string sgst_amount = data?.sgst_amount;
            string cgst_amount = data?.cgst_amount;
            string paymentMade = data?.paymentMade;
            string paymentMode = data?.paymentMode;
            string igst = data?.igst;
            string gstin1 = data?.gstin1;
            string igst_amount = data?.igst_amount;
            string unitName = data?.unitName;
            string phoneNumber = data?.phoneNumber;
            string chapterName = data?.chapterName;

            string url = CreatePDF.CreateInvoice(invoiceNumber, invoiceDate, subject, itemName, sac, cgst, sgst, igst, cgst_amount, sgst_amount, igst_amount, total, paymentMade, paymentMode, gstin1, unitName, phoneNumber, chapterName);

            return new OkObjectResult(url);
        }
    }
}