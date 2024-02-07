using System;
using System.IO;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Newtonsoft.Json.Linq;

namespace IIABackend
{
    /// <summary>
    /// Class to manange all Cosmos DB functions.
    /// </summary>
    public static class CreatePDF
    {
        // create a function to store a blob and return url

        // take a prefix as input in the function eg CreateNews and url would be blob url  + prefix + new hash()
        private static readonly string[] Units = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        private static readonly string[] Tens = { string.Empty, string.Empty, "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        /// <summary>
        /// Stores a file
        /// </summary>
        /// <param name="invoiceNumber">invoiceNumber</param>
        /// <param name="invoiceDate">invoiceDate</param>
        /// <param name="itemName">itemName</param>
        /// <param name="sac">sac</param>
        /// <param name="cgst">cgst</param>
        /// <param name="sgst">sgst</param>
        /// <param name="igst">igst</param>
        /// <param name="cgst_amount">cgst_amount</param>
        /// <param name="sgst_amount">sgst_amount</param>
        /// <param name="igst_amount">igst_amount</param>
        /// <param name="subtotal">subtotal</param>
        /// <param name="total">total</param>
        /// <param name="paymentMade">paymentMade</param>
        /// <param name="paymentMode">paymentMode</param>
        /// <param name="gstin1">gstin1</param>
        /// <param name="unitName">Unit Name</param>
        /// <param name="phoneNumber">Phone Number</param>
        /// <param name="chapterName">Chapter Name</param>
        /// <param name="memberId">MemberId</param>
        /// <param name="memberAddress">memberAddress</param>
        /// <param name="state">State</param>
        /// <param name="gstin">GSTIN</param>
        /// <param name="paymentReason">Payment Reason</param>
        /// <param name="currentDate">Current Date</param>
        /// <param name="startYear">Start Year</param>
        /// <param name="expiryYear">Expiry Year</param>
        /// <param name="chequeNumber">Cheque Number</param>
        /// <param name="admissionFee"> Admission Fee</param>
        /// <param name="sourceGst">Source GST</param>
        /// <returns>url of the file</returns>
        public static string CreateInvoice(string invoiceNumber, string invoiceDate, string itemName, string sac, string cgst, string sgst, string igst, string cgst_amount, string sgst_amount, string igst_amount, string subtotal, string total, string paymentMade, string paymentMode, string gstin1, string unitName, string phoneNumber, string chapterName, string memberId, string memberAddress, string state, string gstin, string paymentReason, DateTime currentDate, string startYear, string expiryYear, string chequeNumber, string admissionFee, string sourceGst)
        {
            /*var sourceGst = string.Empty;

            if (gstin != string.Empty)
            {
                if (gstin.Remove(2) == "07")
                {
                    sourceGst = "07AAATI4647K1ZF";
                }
                else if (gstin.Remove(2) == "05")
                {
                    sourceGst = "05AAATI4647K1ZJ";
                }
                else
                {
                    sourceGst = "09AAATI4647K1ZB";
                }
            }
            else
            {
                if (state == "Uttarakhand")
                {
                    sourceGst = "05AAATI4647K1ZJ";
                }
                else if (state == "Delhi (NCT)")
                {
                    sourceGst = "07AAATI4647K1ZF";
                }
                else
                {
                    sourceGst = "09AAATI4647K1ZB";
                }
            }*/

            total = Math.Round(double.Parse(total)).ToString();
            cgst_amount = Math.Round(double.Parse(cgst_amount), 2).ToString();
            sgst_amount = Math.Round(double.Parse(sgst_amount), 2).ToString();
            igst_amount = Math.Round(double.Parse(igst_amount), 2).ToString();
            subtotal = Math.Round(double.Parse(subtotal), 2).ToString();
            var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new iText.Layout.Document(pdf).SetFontSize(10);
            Image img = new Image(ImageDataFactory.Create(new Uri("https://iiaprodstorage.blob.core.windows.net/utils/IIALogo.png")));
            Paragraph address = new Paragraph(new Text("INDIAN INDUSTRIES ASSOCIATION\n").SetBold().SetUnderline().SetFontSize(20)).Add(new Text("AN APEX BODY OF MICRO, SMALL & MEDIUM ENTERPRISES\n").SetUnderline().SetFontSize(10).SetBold()).Add(new Text("(IN THE SERVICE OF MSME SINCE 1985)\n").SetBold().SetTextAlignment(TextAlignment.CENTER).SetFontSize(8));

            img.SetWidth(60);
            img.SetHeight(60);

            Table table1 = new Table(3);
            table1.SetWidth(UnitValue.CreatePercentValue(100));

            Cell cell1 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(img);
            Cell cell2 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(address);
            Cell cell3, cell4, cell5, cell6, cell7, cell8, cell9;
            table1.AddCell(cell1);
            table1.AddCell(cell2);

            document.Add(table1);
            document.Add(new Paragraph(new Text("IIA Bhawan, Vibhuti Khand, Phase II, Gomti Nagar, Lucknow\nPhone No : 0522 - 2720090, Email Id: admin@iiaonline.in").SetFontSize(8).SetBold()).SetTextAlignment(TextAlignment.CENTER));
            document.Add(new Paragraph(new Text("TAX INVOICE").SetFontSize(13).SetBold()).SetTextAlignment(TextAlignment.CENTER));
            document.Add(new Paragraph(new Text("GSTIN-" + sourceGst).SetBold().SetFontSize(10)).SetTextAlignment(TextAlignment.LEFT));
            cell1 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(new Text("Invoice / Bill No.:   \nMember Name:\nMember ID:").SetBold().SetFontSize(8)));
            cell2 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetFontSize(8).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(new Text(invoiceNumber + "\n" + unitName + "\n" + memberId)));
            Table table2 = new Table(2);
            table2.AddCell(cell1);
            table2.AddCell(cell2);
            document.Add(table2);
            Table table3 = new Table(2).SetWidth(UnitValue.CreatePercentValue(100)).SetBorder(Border.NO_BORDER);
            cell1 = new Cell(1, 2).SetBorder(Border.NO_BORDER).SetFontSize(8).SetBold().SetUnderline().Add(new Paragraph(new Text("Billing Address-")));
            cell2 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetBold().SetFontSize(8).Add(new Paragraph(new Text("Address:")));
            cell3 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetFontSize(8).Add(new Paragraph(new Text(memberAddress)));
            table3.AddCell(cell1);
            table3.AddCell(cell2);
            table3.AddCell(cell3);
            cell2 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetBold().SetFontSize(8).Add(new Paragraph(new Text("State:")));
            cell3 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetFontSize(8).Add(new Paragraph(new Text(state)));
            table3.AddCell(cell2);
            table3.AddCell(cell3);
            cell2 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetBold().SetFontSize(8).Add(new Paragraph(new Text("GSTIN:")));
            cell3 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetFontSize(8).Add(new Paragraph(new Text(gstin)));
            table3.AddCell(cell2);
            table3.AddCell(cell3);
            Table table4 = new Table(2).SetWidth(UnitValue.CreatePercentValue(100)).SetBorder(Border.NO_BORDER);
            cell1 = new Cell(1, 2).SetBorder(Border.NO_BORDER).SetFontSize(8).SetBold().SetUnderline().Add(new Paragraph(new Text("Delivery Address-")));
            cell2 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetBold().SetFontSize(8).Add(new Paragraph(new Text("Address:")));
            cell3 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetFontSize(8).Add(new Paragraph(new Text(memberAddress)));
            table4.AddCell(cell1);
            table4.AddCell(cell2);
            table4.AddCell(cell3);
            cell2 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetBold().SetFontSize(8).Add(new Paragraph(new Text("State:")));
            cell3 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetFontSize(8).Add(new Paragraph(new Text(state)));
            table4.AddCell(cell2);
            table4.AddCell(cell3);
            cell2 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetBold().SetFontSize(8).Add(new Paragraph(new Text("GSTIN:")));
            cell3 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetFontSize(8).Add(new Paragraph(new Text(gstin)));
            table4.AddCell(cell2);
            table4.AddCell(cell3);
            cell1 = new Cell(1, 1);
            cell1.Add(table3);
            cell2 = new Cell(1, 1);
            cell2.Add(table4);
            Table table5 = new Table(2).SetWidth(UnitValue.CreatePercentValue(100));
            table5.AddCell(cell1);
            table5.AddCell(cell2);
            document.Add(table5);
            document.Add(new Paragraph(new Text("Date: ").SetBold().SetFontSize(8)).Add(new Text(currentDate.ToString("dd/MM/yyyy")).SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
            /*change*/
            cell1 = new Cell(1, 1);
            cell1.Add(new Paragraph(new Text("Sr. No.").SetFontSize(8)));
            cell2 = new Cell(1, 1);
            cell2.Add(new Paragraph(new Text("Particulars").SetFontSize(8)));
            cell3 = new Cell(1, 1);
            cell3.Add(new Paragraph(new Text("Membership Period").SetFontSize(8)));
            cell4 = new Cell(1, 1);
            cell4.Add(new Paragraph(new Text("SAC").SetFontSize(8)));
            cell5 = new Cell(1, 1);
            cell5.Add(new Paragraph(new Text("Qty").SetFontSize(8)));
            cell6 = new Cell(1, 1);
            cell6.Add(new Paragraph(new Text("Unit Price(Rs.)").SetFontSize(8)));
            cell7 = new Cell(1, 1);
            cell7.Add(new Paragraph(new Text("Total Value(Rs.)").SetFontSize(8)));
            cell8 = new Cell(1, 1);
            cell8.Add(new Paragraph(new Text("Discount(Rs.)").SetFontSize(8)));
            cell9 = new Cell(1, 1);
            cell9.Add(new Paragraph(new Text("Taxable Value").SetFontSize(8)));
            Table table6 = new Table(9).SetWidth(UnitValue.CreatePercentValue(100));
            table6.AddCell(cell1);
            table6.AddCell(cell2);
            table6.AddCell(cell3);
            table6.AddCell(cell4);
            table6.AddCell(cell5);
            table6.AddCell(cell6);
            table6.AddCell(cell7);
            table6.AddCell(cell8);
            table6.AddCell(cell9);
            cell1 = new Cell(1, 1);
            cell1.Add(new Paragraph(new Text("1").SetFontSize(8)));
            cell2 = new Cell(1, 1);
            cell2.Add(new Paragraph(new Text(paymentReason).SetFontSize(8)));
            cell3 = new Cell(1, 1);
            cell3.Add(new Paragraph(new Text("01/04/" + startYear + " to 31/03/" + expiryYear).SetFontSize(8)));
            cell4 = new Cell(1, 1);
            cell4.Add(new Paragraph(new Text("999599").SetFontSize(8)));
            cell5 = new Cell(1, 1);
            cell5.Add(new Paragraph(new Text("1").SetFontSize(8)));
            cell6 = new Cell(1, 1);
            cell6.Add(new Paragraph(new Text(subtotal).SetFontSize(8)));
            cell7 = new Cell(1, 1);
            cell7.Add(new Paragraph(new Text(subtotal).SetFontSize(8)));
            cell8 = new Cell(1, 1);
            cell8.Add(new Paragraph(new Text("0.00").SetFontSize(8)));
            cell9 = new Cell(1, 1);
            cell9.Add(new Paragraph(new Text(subtotal).SetFontSize(8)));
            table6.AddCell(cell1);
            table6.AddCell(cell2);
            table6.AddCell(cell3);
            table6.AddCell(cell4);
            table6.AddCell(cell5);
            table6.AddCell(cell6);
            table6.AddCell(cell7);
            table6.AddCell(cell8);
            table6.AddCell(cell9);
            if (admissionFee != string.Empty)
            {
                cell1 = new Cell(1, 1);
                cell1.Add(new Paragraph(new Text("2").SetFontSize(8)));
                cell2 = new Cell(1, 1);
                cell2.Add(new Paragraph(new Text("Admission Fee").SetFontSize(8)));
                cell3 = new Cell(1, 1);
                cell3.Add(new Paragraph(new Text("01/04/" + startYear + " to 31/03/" + expiryYear).SetFontSize(8)));
                cell4 = new Cell(1, 1);
                cell4.Add(new Paragraph(new Text("999599").SetFontSize(8)));
                cell5 = new Cell(1, 1);
                cell5.Add(new Paragraph(new Text("1").SetFontSize(8)));
                cell6 = new Cell(1, 1);
                cell6.Add(new Paragraph(new Text(Math.Round(double.Parse(admissionFee), 2).ToString()).SetFontSize(8)));
                cell7 = new Cell(1, 1);
                cell7.Add(new Paragraph(new Text(Math.Round(double.Parse(admissionFee), 2).ToString()).SetFontSize(8)));
                cell8 = new Cell(1, 1);
                cell8.Add(new Paragraph(new Text("0.00").SetFontSize(8)));
                cell9 = new Cell(1, 1);
                cell9.Add(new Paragraph(new Text(Math.Round(double.Parse(admissionFee), 2).ToString()).SetFontSize(8)));
                table6.AddCell(cell1);
                table6.AddCell(cell2);
                table6.AddCell(cell3);
                table6.AddCell(cell4);
                table6.AddCell(cell5);
                table6.AddCell(cell6);
                table6.AddCell(cell7);
                table6.AddCell(cell8);
                table6.AddCell(cell9);
            }

            cell1 = new Cell(1, 1);
            cell1.Add(new Paragraph(new Text(string.Empty).SetFontSize(8)));
            cell2 = new Cell(1, 3);
            cell2.Add(new Paragraph(new Text("Total").SetFontSize(8)));
            cell3 = new Cell(1, 1);
            cell3.Add(new Paragraph(new Text(string.Empty).SetFontSize(8)));
            cell4 = new Cell(1, 1);
            cell4.Add(new Paragraph(new Text((double.Parse(subtotal) + double.Parse(admissionFee == string.Empty ? "0" : admissionFee)).ToString()).SetFontSize(8)));
            cell5 = new Cell(1, 1);
            cell5.Add(new Paragraph(new Text((double.Parse(subtotal) + double.Parse(admissionFee == string.Empty ? "0" : admissionFee)).ToString()).SetFontSize(8)));
            cell6 = new Cell(1, 1);
            cell6.Add(new Paragraph(new Text("0" + ".00").SetFontSize(8)));
            cell7 = new Cell(1, 1);
            cell7.Add(new Paragraph(new Text((double.Parse(subtotal) + double.Parse(admissionFee == string.Empty ? "0" : admissionFee)).ToString()).SetFontSize(8)));
            table6.AddCell(cell1);
            table6.AddCell(cell2);
            table6.AddCell(cell3);
            table6.AddCell(cell4);
            table6.AddCell(cell5);
            table6.AddCell(cell6);
            table6.AddCell(cell7);
            cell1 = new Cell(1, 8);
            cell1.Add(new Paragraph(new Text("CGST " + cgst + "%").SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
            cell2 = new Cell(1, 1);
            cell2.Add(new Paragraph(new Text(double.Parse(cgst_amount) == (int)double.Parse(cgst_amount) ? (cgst_amount + ".00") : cgst_amount).SetFontSize(8)));
            table6.AddCell(cell1);
            table6.AddCell(cell2);
            cell1 = new Cell(1, 8);
            cell1.Add(new Paragraph(new Text("SGST " + sgst + "%").SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
            cell2 = new Cell(1, 1);
            cell2.Add(new Paragraph(new Text(double.Parse(sgst_amount) == (int)double.Parse(sgst_amount) ? (sgst_amount + ".00") : sgst_amount).SetFontSize(8)));
            table6.AddCell(cell1);
            table6.AddCell(cell2);
            cell1 = new Cell(1, 8);
            cell1.Add(new Paragraph(new Text("IGST " + igst + "%").SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
            cell2 = new Cell(1, 1);
            cell2.Add(new Paragraph(new Text(double.Parse(igst_amount) == (int)double.Parse(igst_amount) ? (igst_amount + ".00") : igst_amount).SetFontSize(8)));
            table6.AddCell(cell1);
            table6.AddCell(cell2);
            cell1 = new Cell(1, 8);
            cell1.Add(new Paragraph(new Text("Total").SetFontSize(8).SetBold()).SetTextAlignment(TextAlignment.RIGHT));
            cell2 = new Cell(1, 1);
            cell2.Add(new Paragraph(new Text(double.Parse(total) == (int)double.Parse(total) ? (total + ".00") : total).SetFontSize(8)));
            table6.AddCell(cell1);
            table6.AddCell(cell2);
            /*table 6 insert*/
            document.Add(table6);
            document.Add(new Paragraph(new Text("For Indian Industries Association\nAuthorised Signatory").SetBold().SetFontSize(7)).SetTextAlignment(TextAlignment.RIGHT));
            document.Add(new Paragraph(new Text("*This invoice is computer generated and may not contain signature. For any queries, please call on the telephone no provided above.").SetBold().SetFontSize(7)));
            table1 = new Table(3);
            table1.SetWidth(UnitValue.CreatePercentValue(100));

            cell1 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(img);
            cell2 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(address);
            table1.AddCell(cell1);
            table1.AddCell(cell2);

            document.Add(table1);
            document.Add(new Paragraph(new Text("IIA Bhawan, Vibhuti Khand, Phase II, Gomti Nagar, Lucknow\nPhone No : 0522 - 2720090, Email Id: admin@iiaonline.in").SetFontSize(8).SetBold()).SetTextAlignment(TextAlignment.CENTER));
            document.Add(new Paragraph(new Text("TAX INVOICE").SetFontSize(13).SetBold()).SetTextAlignment(TextAlignment.CENTER));
            cell1 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text("GSTIN-" + sourceGst).SetBold().SetFontSize(10)).SetTextAlignment(TextAlignment.LEFT));
            cell2 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text("Date: ").SetBold().SetFontSize(7)).Add(new Text(currentDate.ToString("dd/MM/yyyy")).SetFontSize(7)).SetTextAlignment(TextAlignment.RIGHT));
            Table table8 = new Table(2).SetWidth(UnitValue.CreatePercentValue(100));
            table8.AddCell(cell1);
            table8.AddCell(cell2);
            document.Add(table8);
            document.Add(new Paragraph(new Text("Receipt No.: ").SetBold().SetFontSize(8)).Add(new Text("MR-" + invoiceNumber).SetFontSize(8)).SetTextAlignment(TextAlignment.LEFT));
            document.Add(new Paragraph(new Text("Received with thanks from " + unitName + " (Member ID : (" + memberId + ") the sum of Rupees " + ConvertAmount(double.Parse(total)) + " by " + paymentMode + " against Invoice No " + invoiceNumber + " Dated " + currentDate.ToString("dd/MM/yyyy") + " for Membership for the period from 01/04/" + startYear + " to 31/03/" + expiryYear + (paymentMode == "Cheque" ? " and Cheque Number - " + chequeNumber : paymentMode == "NEFT" ? " and NEFT Number - " + chequeNumber : string.Empty)).SetFontSize(8)));
            document.Add(new Paragraph(new Text("Rs. " + (double.Parse(total) == (int)double.Parse(total) ? (total + ".00") : total))).SetTextAlignment(TextAlignment.LEFT));
            document.Add(new Paragraph(new Text("For Indian Industries Association\nAuthorised Signatory").SetBold().SetFontSize(7)).SetTextAlignment(TextAlignment.RIGHT));
            document.Close();

            string url = BlobStorage.UploadInvoice(stream.ToArray());

            return url;
        }

        /// <summary>
        /// Stores a file
        /// </summary>
        /// <param name = "name" >Name</param>
        /// <param name="state">State</param>
        /// <param name="address">Address</param>
        /// <param name="gstin">GSTIN</param>
        /// <param name="paymentMode">paymentMode</param>
        /// <param name="checkNumber">Cheque Number</param>
        /// <param name="checkDate">Cheque Date</param>
        /// <param name="phoneNumber">Phone Number</param>
        /// <param name="itemList">ItemList</param>
        /// <param name="currentDate">Current Date</param>
        /// <param name="invoiceNumber">Invoice Number</param>
        /// <param name="sourceGst">Source GST as per admin</param>
        /// <returns>url of the file</returns>
        public static string CreateInvoiceForNonMember(string name, string state, string address, string gstin, string paymentMode, string checkNumber, string checkDate, string phoneNumber, JArray itemList, DateTime currentDate, string invoiceNumber, string sourceGst)
        {
            /*var sourceGst = string.Empty;

            if (gstin != string.Empty && gstin.Length == 15)
            {
                if (gstin.Remove(2) == "07")
                {
                    sourceGst = "07AAATI4647K1ZF";
                }
                else if (gstin.Remove(2) == "05")
                {
                    sourceGst = "05AAATI4647K1ZJ";
                }
                else
                {
                    sourceGst = "09AAATI4647K1ZB";
                }
            }
            else
            {
                if (state == "Uttarakhand")
                {
                    sourceGst = "05AAATI4647K1ZJ";
                }
                else if (state == "Delhi (NCT)")
                {
                    sourceGst = "07AAATI4647K1ZF";
                }
                else
                {
                    sourceGst = "09AAATI4647K1ZB";
                }
            }*/

            var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new iText.Layout.Document(pdf).SetFontSize(10);
            /*int number = 0;*/
            Image img = new Image(ImageDataFactory.Create(new Uri("https://iiaprodstorage.blob.core.windows.net/utils/IIALogo.png")));
            Paragraph constantaddress = new Paragraph(new Text("INDIAN INDUSTRIES ASSOCIATION\n").SetBold().SetUnderline().SetFontSize(20)).Add(new Text("AN APEX BODY OF MICRO, SMALL & MEDIUM ENTERPRISES\n").SetUnderline().SetFontSize(10).SetBold()).Add(new Text("(IN THE SERVICE OF MSME SINCE 1985)\n").SetBold().SetTextAlignment(TextAlignment.CENTER).SetFontSize(8));

            img.SetWidth(60);
            img.SetHeight(60);

            Table table1 = new Table(3);
            table1.SetWidth(UnitValue.CreatePercentValue(100));

            Cell cell1 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(img);
            Cell cell2 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(constantaddress);
            Cell cell3, cell4, cell5, cell6, cell7, cell8, cell9;
            table1.AddCell(cell1);
            table1.AddCell(cell2);

            document.Add(table1);
            document.Add(new Paragraph(new Text("IIA Bhawan, Vibhuti Khand, Phase II, Gomti Nagar, Lucknow\nPhone No : 0522 - 2720090, Email Id: admin@iiaonline.in").SetFontSize(8).SetBold()).SetTextAlignment(TextAlignment.CENTER));
            document.Add(new Paragraph(new Text("TAX INVOICE").SetFontSize(13).SetBold()).SetTextAlignment(TextAlignment.CENTER));
            document.Add(new Paragraph(new Text("GSTIN-" + sourceGst).SetBold().SetFontSize(10)).SetTextAlignment(TextAlignment.LEFT));
            cell1 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(new Text("Invoice / Bill No.:   \nCustomer Name:\nPhone Number:").SetBold().SetFontSize(8)));
            cell2 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetFontSize(8).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(new Text(invoiceNumber + "\n" + name + "\n" + phoneNumber)));
            Table table2 = new Table(2);
            table2.AddCell(cell1);
            table2.AddCell(cell2);
            document.Add(table2);
            document.Add(new Paragraph(new Text("Date: ").SetBold().SetFontSize(8)).Add(new Text(currentDate.ToString("dd/MM/yyyy")).SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
            Table table3 = new Table(2).SetWidth(UnitValue.CreatePercentValue(100)).SetBorder(Border.NO_BORDER);
            cell1 = new Cell(1, 2).SetBorder(Border.NO_BORDER).SetFontSize(8).SetBold().SetUnderline().Add(new Paragraph(new Text("Billing Address-")));
            cell2 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetBold().SetFontSize(8).Add(new Paragraph(new Text("Address")));
            cell3 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetFontSize(8).Add(new Paragraph(new Text(address)));
            table3.AddCell(cell1);
            table3.AddCell(cell2);
            table3.AddCell(cell3);
            cell2 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetBold().SetFontSize(8).Add(new Paragraph(new Text("State:")));
            cell3 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetFontSize(8).Add(new Paragraph(new Text(state)));
            table3.AddCell(cell2);
            table3.AddCell(cell3);
            cell2 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetBold().SetFontSize(8).Add(new Paragraph(new Text("GSTIN:")));
            cell3 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetFontSize(8).Add(new Paragraph(new Text(gstin)));
            table3.AddCell(cell2);
            table3.AddCell(cell3);
            string stateCode = string.IsNullOrEmpty(gstin) || gstin.Length < 15 ? string.Empty : gstin.Substring(0, 2);
            cell2 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetBold().SetFontSize(8).Add(new Paragraph(new Text("State Code: ")));
            cell3 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetFontSize(8).Add(new Paragraph(new Text(stateCode)));
            table3.AddCell(cell2);
            table3.AddCell(cell3);
            Table table4 = new Table(2).SetWidth(UnitValue.CreatePercentValue(100)).SetBorder(Border.NO_BORDER);
            cell1 = new Cell(1, 2).SetBorder(Border.NO_BORDER).SetFontSize(8).SetBold().SetUnderline().Add(new Paragraph(new Text("Delivery Address-")));
            cell2 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetBold().SetFontSize(8).Add(new Paragraph(new Text("Address:")));
            cell3 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetFontSize(8).Add(new Paragraph(new Text(address)));
            table4.AddCell(cell1);
            table4.AddCell(cell2);
            table4.AddCell(cell3);
            cell2 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetBold().SetFontSize(8).Add(new Paragraph(new Text("State:")));
            cell3 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetFontSize(8).Add(new Paragraph(new Text(state)));
            table4.AddCell(cell2);
            table4.AddCell(cell3);
            cell2 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetBold().SetFontSize(8).Add(new Paragraph(new Text("GSTIN:")));
            cell3 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetFontSize(8).Add(new Paragraph(new Text(gstin)));
            table4.AddCell(cell2);
            table4.AddCell(cell3);
            cell2 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetBold().SetFontSize(8).Add(new Paragraph(new Text("State Code: ")));
            cell3 = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetFontSize(8).Add(new Paragraph(new Text(stateCode)));
            table4.AddCell(cell2);
            table4.AddCell(cell3);
            cell1 = new Cell(1, 1);
            cell1.Add(table3);
            cell2 = new Cell(1, 1);
            cell2.Add(table4);
            Table table5 = new Table(2).SetWidth(UnitValue.CreatePercentValue(100));
            table5.AddCell(cell1);
            table5.AddCell(cell2);
            document.Add(table5);
  /*CHANGE*/
            cell1 = new Cell(1, 1);
            cell1.Add(new Paragraph(new Text("Sr. No.").SetFontSize(8)));
            cell2 = new Cell(1, 1);
            cell2.Add(new Paragraph(new Text("Particulars").SetFontSize(8)));
            cell3 = new Cell(1, 1);
            cell3.Add(new Paragraph(new Text("SAC").SetFontSize(8)));
            cell4 = new Cell(1, 1);
            cell4.Add(new Paragraph(new Text("Qty").SetFontSize(8)));
            cell5 = new Cell(1, 1);
            cell5.Add(new Paragraph(new Text("UnitPrice").SetFontSize(8)));
            cell6 = new Cell(1, 1);
            cell6.Add(new Paragraph(new Text("Total Value(Rs.)").SetFontSize(8)));
            cell7 = new Cell(1, 1);
            cell7.Add(new Paragraph(new Text("GST Rate(%)").SetFontSize(8)));
            cell8 = new Cell(1, 1);
            cell8.Add(new Paragraph(new Text("GST Value").SetFontSize(8)));
            cell9 = new Cell(1, 1);
            cell9.Add(new Paragraph(new Text("Discount(Rs.) ").SetFontSize(8)));
            Cell cell10 = new Cell(1, 1);
            cell10.Add(new Paragraph(new Text("Taxable Value").SetFontSize(8)));
            Table table6 = new Table(10).SetWidth(UnitValue.CreatePercentValue(100));
            table6.AddCell(cell1);
            table6.AddCell(cell2);
            table6.AddCell(cell3);
            table6.AddCell(cell4);
            table6.AddCell(cell5);
            table6.AddCell(cell6);
            table6.AddCell(cell7);
            table6.AddCell(cell8);
            table6.AddCell(cell9);
            table6.AddCell(cell10);
            double totalsum = 0.0;
            double totalgst = 0.0;
            for (int i = 0; i < itemList.Count; i++)
            {
                cell1 = new Cell(1, 1);
                cell1.Add(new Paragraph(new Text((i + 1).ToString()).SetFontSize(8)));
                cell2 = new Cell(1, 1);
                cell2.Add(new Paragraph(new Text((string)itemList[i]["ItemName"]).SetFontSize(8)));
                cell3 = new Cell(1, 1);
                cell3.Add(new Paragraph(new Text((string)itemList[i]["SAC"]).SetFontSize(8)));
                cell4 = new Cell(1, 1);
                cell4.Add(new Paragraph(new Text((string)itemList[i]["Quantity"]).SetFontSize(8)));
                cell5 = new Cell(1, 1);
                cell5.Add(new Paragraph(new Text((string)itemList[i]["UnitPrice"]).SetFontSize(8)));
                cell6 = new Cell(1, 1);
                cell6.Add(new Paragraph(new Text(Math.Round(double.Parse((string)itemList[i]["Quantity"]) * double.Parse((string)itemList[i]["UnitPrice"]), 2).ToString()).SetFontSize(8)));
                totalsum += double.Parse((string)itemList[i]["Quantity"]) * double.Parse((string)itemList[i]["UnitPrice"]);
                cell7 = new Cell(1, 1);
                cell7.Add(new Paragraph(new Text((string)itemList[i]["GSTRate"]).SetFontSize(8)));
                cell8 = new Cell(1, 1);
                string gstvalue = (string)itemList[i]["GSTRate"];
                gstvalue = gstvalue.Remove(gstvalue.Length - 1);
                double numericgstvalue = double.Parse((string)itemList[i]["Quantity"]) * double.Parse((string)itemList[i]["UnitPrice"]) * double.Parse(gstvalue);
                numericgstvalue = (double)numericgstvalue / 100;
                totalgst = (double)totalgst + numericgstvalue;
                cell8.Add(new Paragraph(new Text(Math.Round(numericgstvalue, 2).ToString()).SetFontSize(8)));
                cell9 = new Cell(1, 1);
                cell9.Add(new Paragraph(new Text("0.00").SetFontSize(8)));
                cell10 = new Cell(1, 1);
                cell10.Add(new Paragraph(new Text(Math.Round(double.Parse((string)itemList[i]["Quantity"]) * double.Parse((string)itemList[i]["UnitPrice"]), 2).ToString()).SetFontSize(8)));
                table6.AddCell(cell1);
                table6.AddCell(cell2);
                table6.AddCell(cell3);
                table6.AddCell(cell4);
                table6.AddCell(cell5);
                table6.AddCell(cell6);
                table6.AddCell(cell7);
                table6.AddCell(cell8);
                table6.AddCell(cell9);
                table6.AddCell(cell10);
            }

            totalgst = (double)(decimal)totalgst;
            totalsum = (double)(decimal)totalsum;
            cell1 = new Cell(1, 1);
            cell1.Add(new Paragraph(new Text(string.Empty).SetFontSize(8)));
            cell2 = new Cell(1, 6);
            cell2.Add(new Paragraph(new Text("Total").SetFontSize(8)));
            cell3 = new Cell(1, 1);
            cell3.Add(new Paragraph(new Text(Math.Round(totalgst, 2).ToString()).SetFontSize(8)));
            cell8 = new Cell(1, 1);
            cell8.Add(new Paragraph(new Text("0.00 ").SetFontSize(8)));
            cell9 = new Cell(1, 1);
            cell9.Add(new Paragraph(new Text(Math.Round(totalsum, 2).ToString()).SetFontSize(8)));
            table6.AddCell(cell1);
            table6.AddCell(cell2);
            table6.AddCell(cell3);
            table6.AddCell(cell8);
            table6.AddCell(cell9);
            string checkgst = string.Empty;
            string admingst = sourceGst.Remove(2);
            if (gstin != string.Empty && gstin.Length == 15)
            {
                checkgst = gstin.Remove(2);

                if ((checkgst == "07" && admingst == "07") || (checkgst == "05" && admingst == "05") || (checkgst == "09" && admingst == "09"))
                {
                    /*cell1 = new Cell(1, 1);
                    cell1.Add(new Paragraph(new Text(" - ").SetFontSize(8)));
                    cell2 = new Cell(1, 1);
                    cell2.Add(new Paragraph(new Text(string.Empty).SetFontSize(8)));
                    cell3 = new Cell(1, 1);
                    cell3.Add(new Paragraph(new Text(string.Empty).SetFontSize(8)));
                    cell4 = new Cell(1, 1);
                    cell4.Add(new Paragraph(new Text(string.Empty).SetFontSize(8)));
                    cell5 = new Cell(1, 1);
                    cell5.Add(new Paragraph(new Text(string.Empty).SetFontSize(8)));
                    cell6 = new Cell(1, 1);
                    cell6.Add(new Paragraph(new Text(string.Empty).SetFontSize(8)));
                    cell7 = new Cell(1, 1);
                    cell7.Add(new Paragraph(new Text("   ").SetFontSize(8)));
                    cell8 = new Cell(1, 1);
                    cell8.Add(new Paragraph(new Text(string.Empty).SetFontSize(8)));
                    cell9 = new Cell(1, 1);
                    cell9.Add(new Paragraph(new Text(string.Empty).SetFontSize(8)));
                    table6.AddCell(cell1);
                    table6.AddCell(cell2);
                    table6.AddCell(cell3);
                    table6.AddCell(cell4);
                    table6.AddCell(cell5);
                    table6.AddCell(cell6);
                    table6.AddCell(cell7);
                    table6.AddCell(cell8);
                    table6.AddCell(cell9);*/

                    cell8 = new Cell(1, 9);
                    cell8.Add(new Paragraph(new Text("CGST ").SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
                    cell9 = new Cell(1, 1);
                    cell9.Add(new Paragraph(new Text(Math.Round(totalgst / 2, 2).ToString()).SetFontSize(8)));
                    table6.AddCell(cell8);
                    table6.AddCell(cell9);
                    cell8 = new Cell(1, 9);
                    cell8.Add(new Paragraph(new Text("SGST ").SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
                    cell9 = new Cell(1, 1);
                    cell9.Add(new Paragraph(new Text(Math.Round(totalgst / 2, 2).ToString()).SetFontSize(8)));
                    table6.AddCell(cell8);
                    table6.AddCell(cell9);
                    cell8 = new Cell(1, 9);
                    cell8.Add(new Paragraph(new Text("IGST ").SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
                    cell9 = new Cell(1, 1);
                    cell9.Add(new Paragraph(new Text("0").SetFontSize(8)));
                    table6.AddCell(cell8);
                    table6.AddCell(cell9);
                    cell8 = new Cell(1, 9);
                    cell8.Add(new Paragraph(new Text("Grand Total ").SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
                    cell9 = new Cell(1, 1);
                    cell9.Add(new Paragraph(new Text(Math.Round(totalsum + totalgst).ToString()).SetFontSize(8)));
                    table6.AddCell(cell8);
                    table6.AddCell(cell9);
                }
                else
                {
                    cell8 = new Cell(1, 9);
                    cell8.Add(new Paragraph(new Text("CGST ").SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
                    cell9 = new Cell(1, 1);
                    cell9.Add(new Paragraph(new Text(((state == "Uttar Pradesh" && admingst == "09") || (state == "Uttarakhand" && admingst == "05") || (state == "Delhi (NCT)" && admingst == "07")) ? Math.Round(totalgst / 2, 2).ToString() : "0").SetFontSize(8)));
                    table6.AddCell(cell8);
                    table6.AddCell(cell9);
                    cell8 = new Cell(1, 9);
                    cell8.Add(new Paragraph(new Text("SGST ").SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
                    cell9 = new Cell(1, 1);
                    cell9.Add(new Paragraph(new Text(((state == "Uttar Pradesh" && admingst == "09") || (state == "Uttarakhand" && admingst == "05") || (state == "Delhi (NCT)" && admingst == "07")) ? Math.Round(totalgst / 2, 2).ToString() : "0").SetFontSize(8)));
                    table6.AddCell(cell8);
                    table6.AddCell(cell9);
                    cell8 = new Cell(1, 9);
                    cell8.Add(new Paragraph(new Text("IGST ").SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
                    cell9 = new Cell(1, 1);
                    cell9.Add(new Paragraph(new Text(((state == "Uttar Pradesh" && admingst == "09") || (state == "Uttarakhand" && admingst == "05") || (state == "Delhi (NCT)" && admingst == "07")) ? "0" : Math.Round(totalgst, 2).ToString()).SetFontSize(8)));
                    table6.AddCell(cell8);
                    table6.AddCell(cell9);
                    cell8 = new Cell(1, 9);
                    cell8.Add(new Paragraph(new Text("Grand Total ").SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
                    cell9 = new Cell(1, 1);
                    cell9.Add(new Paragraph(new Text(Math.Round(totalsum + totalgst).ToString()).SetFontSize(8)));
                    table6.AddCell(cell8);
                    table6.AddCell(cell9);
                }

            }
            else
            {
                cell8 = new Cell(1, 9);
                cell8.Add(new Paragraph(new Text("CGST ").SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
                cell9 = new Cell(1, 1);
                cell9.Add(new Paragraph(new Text(((state == "Uttar Pradesh" && admingst == "09") || (state == "Uttarakhand" && admingst == "05") || (state == "Delhi (NCT)" && admingst == "07")) ? Math.Round(totalgst / 2, 2).ToString() : "0").SetFontSize(8)));
                table6.AddCell(cell8);
                table6.AddCell(cell9);
                cell8 = new Cell(1, 9);
                cell8.Add(new Paragraph(new Text("SGST ").SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
                cell9 = new Cell(1, 1);
                cell9.Add(new Paragraph(new Text(((state == "Uttar Pradesh" && admingst == "09") || (state == "Uttarakhand" && admingst == "05") || (state == "Delhi (NCT)" && admingst == "07")) ? Math.Round(totalgst / 2, 2).ToString() : "0").SetFontSize(8)));
                table6.AddCell(cell8);
                table6.AddCell(cell9);
                cell8 = new Cell(1, 9);
                cell8.Add(new Paragraph(new Text("IGST ").SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
                cell9 = new Cell(1, 1);
                cell9.Add(new Paragraph(new Text(((state == "Uttar Pradesh" && admingst == "09") || (state == "Uttarakhand" && admingst == "05") || (state == "Delhi (NCT)" && admingst == "07")) ? "0" : Math.Round(totalgst, 2).ToString()).SetFontSize(8)));
                table6.AddCell(cell8);
                table6.AddCell(cell9);
                cell8 = new Cell(1, 9);
                cell8.Add(new Paragraph(new Text("Grand Total ").SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
                cell9 = new Cell(1, 1);
                cell9.Add(new Paragraph(new Text(Math.Round(totalsum + totalgst).ToString()).SetFontSize(8)));
                table6.AddCell(cell8);
                table6.AddCell(cell9);
            }

            /*table 6 insert*/
            document.Add(table6);
            document.Add(new Paragraph(new Text("Amount in rupees: " + ConvertAmount(Math.Round(totalsum + totalgst))).SetBold().SetFontSize(7)).SetTextAlignment(TextAlignment.LEFT));
            document.Add(new Paragraph(new Text("Whether Reverse Charge: No").SetBold().SetFontSize(7)).SetTextAlignment(TextAlignment.LEFT));
            document.Add(new Paragraph(new Text("For Indian Industries Association\nAuthorised Signatory").SetBold().SetFontSize(7)).SetTextAlignment(TextAlignment.RIGHT));
            document.Add(new Paragraph(new Text("*This invoice is computer generated and may not contain signature. For any queries, please call on the telephone no provided above.").SetBold().SetFontSize(7)));
            table1 = new Table(3);
            table1.SetWidth(UnitValue.CreatePercentValue(100));

            cell1 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(img);
            cell2 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(constantaddress);
            table1.AddCell(cell1);
            table1.AddCell(cell2);

            //document.Add(table1);
            //document.Add(new Paragraph(new Text("IIA Bhawan, Vibhuti Khand, Phase II, Gomti Nagar, Lucknow\nPhone No : 0522 - 2720090, Email Id: admin@iiaonline.in").SetFontSize(8).SetBold()).SetTextAlignment(TextAlignment.CENTER));
            //document.Add(new Paragraph(new Text("TAX INVOICE").SetFontSize(13).SetBold()).SetTextAlignment(TextAlignment.CENTER));
            cell1 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text("GSTIN-" + sourceGst).SetBold().SetFontSize(10)).SetTextAlignment(TextAlignment.LEFT));
            cell2 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text("Date: ").SetBold().SetFontSize(7)).Add(new Text(currentDate.ToString("dd/MM/yyyy")).SetFontSize(7)).SetTextAlignment(TextAlignment.RIGHT));
            Table table8 = new Table(2).SetWidth(UnitValue.CreatePercentValue(100));
            table8.AddCell(cell1);
            table8.AddCell(cell2);
            // document.Add(table8);
            decimal grandtotal = (decimal)(totalsum + totalgst);
            // document.Add(new Paragraph(new Text("Receipt No.: ").SetBold().SetFontSize(8)).Add(new Text("MR-" + invoiceNumber).SetFontSize(8)).SetTextAlignment(TextAlignment.LEFT));
            // document.Add(new Paragraph(new Text("Received with thanks from " + name + " the sum of Rupees " + ConvertAmount(Math.Round(totalsum + totalgst)) + " by " + paymentMode + " against Invoice No " + invoiceNumber + " Dated " + currentDate.ToString("dd/MM/yyyy") + (paymentMode == "Cheque" ? " and Cheque Number - " + checkNumber : paymentMode == "NEFT" ? " and NEFT Number - " + checkNumber : string.Empty)).SetFontSize(8)));
            // document.Add(new Paragraph(new Text("Rs. " + Math.Round(grandtotal).ToString() + ".00")).SetTextAlignment(TextAlignment.LEFT));
            // document.Add(new Paragraph(new Text("For Indian Industries Association\nAuthorised Signatory").SetBold().SetFontSize(7)).SetTextAlignment(TextAlignment.RIGHT));
            document.Close();
            string url = BlobStorage.UploadInvoice(stream.ToArray());

            return url;
        }

        /// <summary>
        /// Convert Amount to Words
        /// </summary>
        /// <param name="amount">amount int double</param>
        /// <returns>amount in words</returns>
        public static string ConvertAmount(double amount)
        {
            try
            {
                long amount_int = (long)amount;
                long amount_dec = (long)Math.Round((amount - amount_int) * 100);
                if (amount_dec == 0)
                {
                    return Convert(amount_int) + " Only";
                }
                else
                {
                    return Convert(amount_int) + " Point " + Convert(amount_dec) + " Only.";
                }
            }
            catch (Exception)
            {
                // TODO: handle exception
            }

            return string.Empty;
        }

        /// <summary>
        /// Convert
        /// </summary>
        /// <param name="i">number</param>
        /// <returns>Number</returns>
        public static string Convert(long i)
        {
            if (i < 20)
            {
                return Units[i];
            }

            if (i < 100)
            {
                return Tens[i / 10] + ((i % 10 > 0) ? " " + Convert(i % 10) : string.Empty);
            }

            if (i < 1000)
            {
                return Units[i / 100] + " Hundred"
                        + ((i % 100 > 0) ? " And " + Convert(i % 100) : string.Empty);
            }

            if (i < 100000)
            {
                return Convert(i / 1000) + " Thousand "
                + ((i % 1000 > 0) ? " " + Convert(i % 1000) : string.Empty);
            }

            if (i < 10000000)
            {
                return Convert(i / 100000) + " Lakh "
                        + ((i % 100000 > 0) ? " " + Convert(i % 100000) : string.Empty);
            }

            if (i < 1000000000)
            {
                return Convert(i / 10000000) + " Crore "
                        + ((i % 10000000 > 0) ? " " + Convert(i % 10000000) : string.Empty);
            }

            return Convert(i / 1000000000) + " Arab "
                    + ((i % 1000000000 > 0) ? " " + Convert(i % 1000000000) : string.Empty);
        }
    }
}
