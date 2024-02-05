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
    public static class ExcelWriteForMembers
    {
        /// <summary>
        /// Generic Excel Reader
        /// </summary>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <returns>url of the file</returns>
        public static string GenericExcelWriterForMembers()
        {
            DataTable dt = new DataTable("Grid");
            int srNo = 1;
            dt.Columns.AddRange(new DataColumn[]
            {
                                                     new DataColumn("Sr No."),
                                                     new DataColumn("Membership Id"),
                                                     new DataColumn("Unit Name"),
                                                     new DataColumn("Owner Name"),
                                                     new DataColumn("Membership Joining Date"),
                                                     new DataColumn("Membership Current Expiry Year"),
                                                     new DataColumn("Chapter Name"),
                                                     new DataColumn("Category"),
                                                     new DataColumn("Membership Status"),
                                                     new DataColumn("GSTIN"),
                                                     new DataColumn("Exporter"),
                                                     new DataColumn("Phone Number"),
                                                     new DataColumn("Email"),
                                                     new DataColumn("DateOfBirth"),
                                                     new DataColumn("DateOfMarriage"),
                                                     new DataColumn("Address"),
                                                     new DataColumn("Classification"),
                                                     new DataColumn("Enterprise Type"),
                                                     new DataColumn("MembershipFees"),
            });
            dynamic members = Database.GetMemberDetailsForExcel();

            foreach (dynamic p in members)
            {
               DateTime? dob = p["DateOfBirth"];
               DateTime? dom = p["DateOfMarriage"];
               string dob2 = string.Format("{0:d}", dob);
               string dom2 = string.Format("{0:d}", dom);
               dt.Rows.Add(srNo, p["MembershipId"], p["UnitName"], p["FirstName"] + p["LastName"], p["MembershipJoinDate"], p["MembershipCurrentExpiryYear"], p["ChapterName"], p["ProductCategory"], p["ProfileStatus"] == 5 ? "Active" : "Inactive", p["GSTIN"], p["Exporter"], p["PhoneNumber"], p["Email"], dob2, dom2, p["Address"], p["Classification"], p["EnterpriseType"], p["MembershipFees"]);
               srNo++;
            }

            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            var workbookBytes = new byte[0];
            using (var ms = new MemoryStream())
            {
                wb.SaveAs(ms);
                workbookBytes = ms.ToArray();
            }

            return BlobStorage.UploadExcelFileForMembers(workbookBytes);
        }
    }
}