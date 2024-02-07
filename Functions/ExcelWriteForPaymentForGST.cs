using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using ClosedXML.Excel;

namespace IIABackend
{
    /// <summary>
    /// Read every excel
    /// </summary>
    public static class ExcelWriteForPaymentForGST
    {
        /// <summary>
        /// Generic Excel Reader
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End Date</param>
        /// <param name="chapterId">CHapter Id</param>
        /// <param name="reason">Type of Membership report</param>
        /// <returns>url of the file</returns>
        public static string GenericExcelWriterForPaymentForGST(string startDate, string endDate, int chapterId, string reason)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[]
            {
                                                     new DataColumn("Membership Id"),
                                                     new DataColumn("GSTIN/UIN of Recipient"),
                                                     new DataColumn("Receiver Name"),
                                                     new DataColumn("Invoice Number"),
                                                     new DataColumn("Invoice date"),
                                                     new DataColumn("Invoice Value"),
                                                     new DataColumn("Place of Supply"),
                                                     new DataColumn("Reverse Charge"),
                                                     new DataColumn("Applicable % of Tax Rate"),
                                                     new DataColumn("Invoice Type"),
                                                     new DataColumn("E-Commerce GSTIN"),
                                                     new DataColumn("Rate"),
                                                     new DataColumn("CGST"),
                                                     new DataColumn("SGST"),
                                                     new DataColumn("IGST"),
                                                     new DataColumn("Taxable Value"),
                                                     new DataColumn("Chapter Name"),
                                                     new DataColumn("Source GSTIN"),
            });
            List<Dictionary<string, dynamic>> payments = Database.GetPaymentDetailsForExcelForGST(startDate, endDate, chapterId);
            List<Dictionary<string, dynamic>> nonMemberPayments = Database.GetPaymentDetailsForExcelForGSTNonMember2(startDate, endDate);
            if (reason == "Membership")
            {
                foreach (dynamic p in payments)
                {
                    dt.Rows.Add(p["MemberId"], p["GSTIN"], p["UnitName"].Replace(",", string.Empty), p["InvoiceNumber"], p["InvoiceDate"], p["InvoiceValue"], FindState(p["GSTIN"]) != string.Empty ? FindState(p["GSTIN"]) : FindStateByName(p["State"]), "N", string.Empty, "Regular", string.Empty, p["CGSTPercent"] != 0 ? (p["CGSTPercent"] * 2) : p["IGSTPercent"], p["CGST"], p["SGST"], p["IGST"], p["TaxableAmount"], p["ChapterName"], p["sourceGST"]);
                }
            }
            else
            {
                foreach (dynamic p in nonMemberPayments)
                {
                    dt.Rows.Add(p["MemberId"], (string)p["GSTIN"], p["UnitName"].Replace(",", string.Empty), p["InvoiceNumber"], p["InvoiceDate"], p["InvoiceValue"], FindState(p["GSTIN"]) != string.Empty ? FindState(p["GSTIN"]) : FindStateByName(p["State"]), "N", string.Empty, "Regular", string.Empty, p["CGST"] != 0 ? (p["CGST"] * 200 / p["TaxableAmount"]) : (p["IGST"] * 100 / p["TaxableAmount"]), p["CGST"], p["SGST"], p["IGST"], p["TaxableAmount"], p["ChapterName"], p["sourceGST"]);
                }
            }

            StringBuilder sb = new StringBuilder();
            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
             Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));
            /*Console.WriteLine(dt.Rows.Count);*/
            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            byte[] buffer = Encoding.ASCII.GetBytes(sb.ToString());
            return BlobStorage.UploadExcelFileForPayment(buffer);
        }

        /// <summary>
        /// State Finder From GSTIN
        /// </summary>
        /// <param name="gstin">GSTIN</param>
        /// <returns>url of the file</returns>
        public static string FindState(string gstin)
        {
            string output = string.Empty;
            if (gstin.Length == 15)
            {
                if (gstin.Trim().StartsWith("01"))
                {
                    output = "01-Jammu And Kashmir";
                }
                else if (gstin.Trim().StartsWith("02"))
                {
                    output = "02-Himachal Pradesh";
                }
                else if (gstin.Trim().StartsWith("03"))
                {
                    output = "03-Punjab";
                }
                else if (gstin.Trim().StartsWith("04"))
                {
                    output = "04-Chandigarh";
                }
                else if (gstin.Trim().StartsWith("05"))
                {
                    output = "05-Uttarakhand";
                }
                else if (gstin.Trim().StartsWith("06"))
                {
                    output = "06-Haryana";
                }
                else if (gstin.Trim().StartsWith("07"))
                {
                    output = "07-Delhi";
                }
                else if (gstin.Trim().StartsWith("08"))
                {
                    output = "08-Rajasthan";
                }
                else if (gstin.Trim().StartsWith("09"))
                {
                    output = "09-Uttar Pradesh";
                }
                else if (gstin.Trim().StartsWith("10"))
                {
                    output = "10-Bihar";
                }
                else if (gstin.Trim().StartsWith("11"))
                {
                    output = "11-Sikkim";
                }
                else if (gstin.Trim().StartsWith("12"))
                {
                    output = "12-Arunachal Pradesh";
                }
                else if (gstin.Trim().StartsWith("13"))
                {
                    output = "13-Nagaland";
                }
                else if (gstin.Trim().StartsWith("14"))
                {
                    output = "14-Manipur";
                }
                else if (gstin.Trim().StartsWith("15"))
                {
                    output = "15-Mizoram";
                }
                else if (gstin.Trim().StartsWith("16"))
                {
                    output = "16-Tripura";
                }
                else if (gstin.Trim().StartsWith("17"))
                {
                    output = "17-Meghalaya";
                }
                else if (gstin.Trim().StartsWith("18"))
                {
                    output = "18-Assam";
                }
                else if (gstin.Trim().StartsWith("19"))
                {
                    output = "19-West Bengal";
                }
                else if (gstin.Trim().StartsWith("20"))
                {
                    output = "20-Jharkhand";
                }
                else if (gstin.Trim().StartsWith("21"))
                {
                    output = "21-Odisha";
                }
                else if (gstin.Trim().StartsWith("22"))
                {
                    output = "22-Chattisgarh";
                }
                else if (gstin.Trim().StartsWith("23"))
                {
                    output = "23-Madhya Pradesh";
                }
                else if (gstin.Trim().StartsWith("24"))
                {
                    output = "24-Gujarat";
                }
                else if (gstin.Trim().StartsWith("26"))
                {
                    output = "26-Dadra and Nagar Haveli And Daman and Diu";
                }
                else if (gstin.Trim().StartsWith("27"))
                {
                    output = "27-Maharashtra";
                }
                else if (gstin.Trim().StartsWith("28"))
                {
                    output = "28-Andhra Pradesh";
                }
                else if (gstin.Trim().StartsWith("29"))
                {
                    output = "29-Karnataka";
                }
                else if (gstin.Trim().StartsWith("30"))
                {
                    output = "30-Goa";
                }
                else if (gstin.Trim().StartsWith("31"))
                {
                    output = "31-Lakshadweep";
                }
                else if (gstin.Trim().StartsWith("32"))
                {
                    output = "32-Kerala";
                }
                else if (gstin.Trim().StartsWith("33"))
                {
                    output = "33-Tamil Nadu";
                }
                else if (gstin.Trim().StartsWith("34"))
                {
                    output = "34-Puducherry";
                }
                else if (gstin.Trim().StartsWith("35"))
                {
                    output = "35-Andaman and Nicobar Islands";
                }
                else if (gstin.Trim().StartsWith("36"))
                {
                    output = "36-Telangana";
                }
                else if (gstin.Trim().StartsWith("37"))
                {
                    output = "37-Andhra Pradesh";
                }
                else if (gstin.Trim().StartsWith("38"))
                {
                    output = "38-Ladakh";
                }
                else if (gstin.Trim().StartsWith("97"))
                {
                    output = "97-Other Territories";
                }
                else if (gstin.Trim().StartsWith("99"))
                {
                    output = "99-Center Jurisdiction";
                }
            }

            return output;
        }

        /// <summary>
        /// State Finder From state name
        /// </summary>
        /// <param name="stateName">GSTIN</param>
        /// <returns>url of the file</returns>
        public static string FindStateByName(string stateName)
        {
            string output = string.Empty;
            if (stateName == "Jammu And Kashmir")
            {
                output = "01-Jammu And Kashmir";
            }
            else if (stateName == "Himachal Pradesh")
            {
                output = "02-Himachal Pradesh";
            }
            else if (stateName == "Punjab")
            {
                output = "03-Punjab";
            }
            else if (stateName == "Chandigarh")
            {
                output = "04-Chandigarh";
            }
            else if (stateName == "Uttarakhand")
            {
                output = "05-Uttarakhand";
            }
            else if (stateName == "Haryana")
            {
                output = "06-Haryana";
            }
            else if (stateName == "Delhi" || stateName == "New Delhi")
            {
                output = "07-Delhi";
            }
            else if (stateName == "Rajasthan")
            {
                output = "08-Rajasthan";
            }
            else if (stateName == "Uttar Pradesh")
            {
                output = "09-Uttar Pradesh";
            }
            else if (stateName == "Bihar")
            {
                output = "10-Bihar";
            }
            else if (stateName == "Sikkim")
            {
                output = "11-Sikkim";
            }
            else if (stateName == "Arunachal Pradesh")
            {
                output = "12-Arunachal Pradesh";
            }
            else if (stateName == "Nagaland")
            {
                output = "13-Nagaland";
            }
            else if (stateName == "Manipur")
            {
                output = "14-Manipur";
            }
            else if (stateName == "Mizoram")
            {
                output = "15-Mizoram";
            }
            else if (stateName == "Tripura")
            {
                output = "16-Tripura";
            }
            else if (stateName == "Meghalaya")
            {
                output = "17-Meghalaya";
            }
            else if (stateName == "Assam")
            {
                output = "18-Assam";
            }
            else if (stateName == "West Bengal")
            {
                output = "19-West Bengal";
            }
            else if (stateName == "Jharkhand")
            {
                output = "20-Jharkhand";
            }
            else if (stateName == "Odisha")
            {
                output = "21-Odisha";
            }
            else if (stateName == "Chattisgarh")
            {
                output = "22-Chattisgarh";
            }
            else if (stateName == "Madhya Pradesh")
            {
                output = "23-Madhya Pradesh";
            }
            else if (stateName == "Gujarat")
            {
                output = "24-Gujarat";
            }
            else if (stateName == "Dadra and Nagar Haveli And Daman and Diu")
            {
                output = "26-Dadra and Nagar Haveli And Daman and Diu";
            }
            else if (stateName == "Maharashtra")
            {
                output = "27-Maharashtra";
            }
            else if (stateName == "Andhra Pradesh")
            {
                output = "28-Andhra Pradesh";
            }
            else if (stateName == "Karnataka")
            {
                output = "29-Karnataka";
            }
            else if (stateName == "Goa")
            {
                output = "30-Goa";
            }
            else if (stateName == "Lakshadweep")
            {
                output = "31-Lakshadweep";
            }
            else if (stateName == "Kerala")
            {
                output = "32-Kerala";
            }
            else if (stateName == "Tamil Nadu")
            {
                output = "33-Tamil Nadu";
            }
            else if (stateName == "Puducherry")
            {
                output = "34-Puducherry";
            }
            else if (stateName == "Andaman and Nicobar Islands")
            {
                output = "35-Andaman and Nicobar Islands";
            }
            else if (stateName == "Telangana")
            {
                output = "36-Telangana";
            }
            else if (stateName == "Andhra Pradesh")
            {
                output = "37-Andhra Pradesh";
            }
            else if (stateName == "Ladakh")
            {
                output = "38-Ladakh";
            }

            return output;
        }
    }
}