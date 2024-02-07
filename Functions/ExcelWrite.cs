using System.Collections.Generic;
using System.Data;
using System.IO;
using ClosedXML.Excel;

namespace IIABackend
{
    /// <summary>
    /// Read every excel
    /// </summary>
    public static class ExcelWrite
    {
        /// <summary>
        /// Generic Excel Reader
        /// </summary>
        /// <param name="chapterId">chapterId</param>
        /// <returns>url of the file</returns>
        public static string GenericExcelWriter(int chapterId)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[14]
            {
                                                     new DataColumn("TicketNumber"),
                                                     new DataColumn("Title"),
                                                     new DataColumn("Description"),
                                                     new DataColumn("Category"),
                                                     new DataColumn("ChapterId"),
                                                     new DataColumn("Status"),
                                                     new DataColumn("TicketCreationTime"),
                                                     new DataColumn("UserId"),
                                                     new DataColumn("Comment"),
                                                     new DataColumn("Attachment"),
                                                     new DataColumn("ClosedTicketTime"),
                                                     new DataColumn("UserName"),
                                                     new DataColumn("PhoneNumber"),
                                                     new DataColumn("ChapterName"),
            });
            var tickets = new List<Tickets>();
            if (chapterId == 82)
            {
                tickets = Database.GetSummaryForAllChapters();
            }
            else
            {
                tickets = Database.GetSummaryForChapters(chapterId);
            }

            foreach (var ticket in tickets)
            {
                var comments = Database.GetComment(ticket.TicketNumber);
                var comment = string.Empty;
                foreach (var c in comments)
                {
                    comment = comment + c.Comments + ";";
                }

                var attachments = Database.GetAttachment(ticket.TicketNumber);
                var attachment = string.Empty;
                foreach (var a in attachments)
                {
                    attachment = attachment + a.AttachmentURL + ";";
                }

                dt.Rows.Add(ticket.TicketNumber, ticket.Title, ticket.Description, ticket.Category, ticket.ChapterId, ticket.Status, ticket.TicketCreationTime, ticket.UserId, comment, attachment, ticket.ClosedTicketTime, ticket.UserName, ticket.PhoneNumber, ticket.ChapterName);
            }

            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            var workbookBytes = new byte[0];
            using (var ms = new MemoryStream())
            {
                wb.SaveAs(ms);
                workbookBytes = ms.ToArray();
            }

            return BlobStorage.UploadExcelFile(workbookBytes);
        }
    }
}