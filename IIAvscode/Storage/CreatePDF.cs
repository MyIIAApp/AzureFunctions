using System;
using System.IO;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace IIABackend
{
    /// <summary>
    /// Class to manange all Cosmos DB functions.
    /// </summary>
    public static class CreatePDF
    {
        // create a function to store a blob and return url

        // take a prefix as input in the function eg CreateNews and url would be blob url  + prefix + new hash()

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
        /// <returns>url of the file</returns>
        public static string CreateInvoice(string invoiceNumber, string invoiceDate, string itemName, string sac, string cgst, string sgst, string igst, string cgst_amount, string sgst_amount, string igst_amount, string subtotal, string total, string paymentMade, string paymentMode, string gstin1, string unitName, string phoneNumber, string chapterName)
        {
            var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new iText.Layout.Document(pdf).SetFontSize(10);

            // Image img = new Image(ImageDataFactory
            //    .Create("Files/IIA.jpeg"))
            //    .SetBackgroundColor(ColorConstants.LIGHT_GRAY);

            Paragraph header = new Paragraph("TAX INVOICE").SetFontSize(16).SetBold();

            Paragraph address = new Paragraph(new Text("Indian Industries Association\n").SetBold()).Add(new Text(Environment.GetEnvironmentVariable("IIAAddress"))).SetMultipliedLeading(1.0f);

            Paragraph newline = new Paragraph(new Text("\n"));

            Paragraph gstin = new Paragraph(new Text("gstin1").SetBold());

            // img.SetWidth(100);

            Table table1 = new Table(3).SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            table1.SetWidth(UnitValue.CreatePercentValue(100));

            // Cell cell1 = new Cell(1, 1)
            //     .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
            //     .Add(img);
            Cell cell2 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(header);
            Cell cell3 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text("Indian Industries Association\n").SetBold()).Add(new Text(Environment.GetEnvironmentVariable("IIAAddress")))
                .Add(new Text(Environment.GetEnvironmentVariable("IIAGSTIN")).SetBold()));

            // table1.AddCell(cell1);
            table1.AddCell(cell2);
            table1.AddCell(cell3);
            document.Add(table1);

            document.Add(newline);

            Table table2 = new Table(2);
            table2.SetWidth(UnitValue.CreatePercentValue(100));
            // cell1 = new Cell(1, 1)
            //     .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
            //     .Add(address);
            cell2 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text("Invoice# ").SetBold()).Add(new Text(invoiceNumber)).Add(new Text("\nInvoice Date: ").SetBold()).Add(new Text(invoiceDate)));
            // table2.AddCell(cell1);
            table2.AddCell(cell2);
            document.Add(table2);

            document.Add(gstin);

            document.Add(newline);

            // document.Add(new Paragraph(new Text("Subject- ").SetBold()).Add(new Text(subject)).SetMultipliedLeading(1.0f));
            Table table = new Table(7, false).SetFontSize(10).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell11 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(new Paragraph("Sr.No"));
            Cell cell12 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(new Paragraph("Item"));
            Cell cell13 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(new Paragraph("SAC"));
            Cell cell14 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(new Paragraph("CGST (in Rs.)"));
            Cell cell15 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(new Paragraph("SGST (in Rs.)"));
            Cell cell16 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(new Paragraph("IGST (in Rs.)"));
            Cell cell17 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(new Paragraph("AMOUNT (in Rs.)"));

            Cell cell21 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(new Paragraph("1"));
            Cell cell22 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(new Paragraph(itemName));
            Cell cell23 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(new Paragraph(sac));
            Cell cell24 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(new Paragraph(cgst_amount));
            Cell cell25 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(new Paragraph(sgst_amount));
            Cell cell26 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(new Paragraph(igst_amount));
            Cell cell27 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(new Paragraph(total));

            table.AddCell(cell11);
            table.AddCell(cell12);
            table.AddCell(cell13);
            table.AddCell(cell14);
            table.AddCell(cell15);
            table.AddCell(cell16);
            table.AddCell(cell17);
            table.AddCell(cell21);
            table.AddCell(cell22);
            table.AddCell(cell23);
            table.AddCell(cell24);
            table.AddCell(cell25);
            table.AddCell(cell26);
            table.AddCell(cell27);

            document.Add(table);

            Table table3 = new Table(3);
            table3.SetWidth(UnitValue.CreatePercentValue(100)).SetBackgroundColor(ColorConstants.LIGHT_GRAY);

            // cell1 = new Cell(7, 1)
            //     .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.TOP)
            //     .Add(new Paragraph(new Text("Thank you for shopping with us.")));
            cell21 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text("Sub Total(in Rs.)")));
            cell22 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text("CGST (")).Add(new Text(cgst)).Add(new Text("%)")));
            cell23 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text("SGST (")).Add(new Text(sgst)).Add(new Text("%)")));
            cell24 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text("IGST (")).Add(new Text(igst)).Add(new Text("%)")));
            cell25 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text("Total (in Rs.)").SetBold()));
            cell26 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text("Payment Made (in Rs.)")));
            cell27 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text("Payment Mode").SetBold()));

            Cell cell31 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text(subtotal)));
            Cell cell32 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text(cgst_amount)));
            Cell cell33 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text(sgst_amount)));
            Cell cell34 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text(igst_amount)));
            Cell cell35 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text(total)));
            Cell cell36 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text(paymentMade)));
            Cell cell37 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Paragraph(new Text(paymentMode)));

            // table3.AddCell(cell1);
            table3.AddCell(cell21);
            table3.AddCell(cell31);
            table3.AddCell(cell22);
            table3.AddCell(cell32);
            table3.AddCell(cell23);
            table3.AddCell(cell33);
            table3.AddCell(cell24);
            table3.AddCell(cell34);
            table3.AddCell(cell25);
            table3.AddCell(cell35);
            table3.AddCell(cell26);
            table3.AddCell(cell36);
            table3.AddCell(cell27);
            table3.AddCell(cell37);

            document.Add(newline);
            document.Add(table3);
            document.Add(newline);

            document.Add(new Paragraph(new Text("Terms And Conditions").SetBold()).SetTextAlignment(TextAlignment.LEFT));
            document.Add(new Paragraph(new Text("Your Company's Terms and Conditions will be displayed here. You can add it in the Invoice Preferences page under Settings").SetBold()).SetTextAlignment(TextAlignment.LEFT));

            document.Close();

            string url = BlobStorage.UploadInvoice(stream.ToArray());

            return url;
        }
    }
}