using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ClosedXML.Excel;

namespace IIABackend
{
    /// <summary>
    /// Read every excel
    /// </summary>
    public static class ExcelWriteForPayment
    {
        /// <summary>
        /// Generic Excel Reader
        /// </summary>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <param name="chapterId">Chapter Id</param>
        /// <param name="opration"> opration</param>
        /// <param name="reason">Membership or non-membership</param>
        /// <returns>url of the file</returns>
        public static string GenericExcelWriterForPayment(string startDate, string endDate, int chapterId,  string opration, string reason)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[]
            {
                                                     new DataColumn("MemberShipID"),
                                                     new DataColumn("FirstName"),
                                                     new DataColumn("LastName"),
                                                     new DataColumn("PhoneNumber"),
                                                     new DataColumn("Email"),
                                                     new DataColumn("UnitName"),
                                                     new DataColumn("ChapterName"),
                                                     new DataColumn("SubTotal"),
                                                     new DataColumn("IGSTPercent"),
                                                     new DataColumn("CGSTPercent"),
                                                     new DataColumn("SGSTPercent"),
                                                     new DataColumn("IGSTValue"),
                                                     new DataColumn("CGSTValue"),
                                                     new DataColumn("SGSTValue"),
                                                     new DataColumn("PaymentReason"),
                                                     new DataColumn("PaymentMode"),
                                                     new DataColumn("ChequeNumber"),
                                                     new DataColumn("OnlineTransactionID"),
                                                     new DataColumn("OrderID"),
                                                     new DataColumn("Total"),
                                                     new DataColumn("OnlineFees"),
                                                     new DataColumn("InvoicePath"),
                                                     new DataColumn("CreateDateTimeStamp"),
                                                     new DataColumn("Status"),
                                                     new DataColumn("GSTIN"),
                                                     new DataColumn("InvoiceNumber"),
                                                     new DataColumn("HOShare"),
                                                     new DataColumn("ChapterShare"),
            });
            var payments = new List<PaymentDetailsForExcel>();
            payments = opration == "failed" ? Database.GetPaymentDetailsForExcelFailed(startDate, endDate, chapterId) : Database.GetPaymentDetailsForExcel(startDate, endDate, chapterId);
            var nonMemberPayments = Database.GetPaymentDetailsForExcelNonMember(startDate, endDate);
            if (reason == "Membership")
            {
                foreach (var p in payments)
                {
                    var st = string.Empty;
                    if (p.Status == "0")
                    {
                        st = "Failed";
                    }
                    else if (p.Status == "1")
                    {
                        st = "Success";
                    }
                    else
                    {
                        st = "Tempered";
                    }

                    string chapterShare = Math.Round(Convert.ToDecimal(p.ChapterShare), 2).ToString();
                    dt.Rows.Add(p.UserId, p.FirstName, p.LastName, p.PhoneNumber, p.Email, p.InvoiceId, p.AdminId, p.SubTotal, p.IGSTPercent, p.CGSTPercent, p.SGSTPercent, p.IGSTValue, p.CGSTValue, p.SGSTValue, p.PaymentReason, p.PaymentMode, p.ChequeNumber.Split("$%#")[0], p.OnlineTransactionId, p.OrderId, p.Total, p.OnlineFees, p.InvoicePath, p.CreateDateTimeStamp, st, p.GSTIN, p.Status == "1" ? p.InvoiceNumber : string.Empty, p.Status == "1" ? p.HOShare : string.Empty, p.Status == "1" ? chapterShare : string.Empty);
                }
            }
            else
            {
                foreach (dynamic p in nonMemberPayments)
                {
                    dt.Rows.Add(p.UserId, p.FirstName, p.LastName, p.PhoneNumber, p.Email, p.InvoiceId, p.AdminId, p.SubTotal, p.IGSTPercent, p.CGSTPercent, p.SGSTPercent, p.IGSTValue, p.CGSTValue, p.SGSTValue, p.PaymentReason, p.PaymentMode, p.ChequeNumber.Split("$%#")[0], p.OnlineTransactionId, p.OrderId, p.Total, p.OnlineFees, p.InvoicePath, p.CreateDateTimeStamp, p.Status == "1" ? "Success" : "failed", p.GSTIN, p.Status == "1" ? p.InvoiceNumber : string.Empty, p.Status == "1" ? p.HOShare : string.Empty, p.Status == "1" ? p.ChapterShare : string.Empty);
                }
            }

            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            var workbookBytes = new byte[0];
            using (var ms = new MemoryStream())
            {
                wb.SaveAs(ms);
                workbookBytes = ms.ToArray();
            }

            return BlobStorage.UploadExcelFileForPayment(workbookBytes);
        }
    }
}