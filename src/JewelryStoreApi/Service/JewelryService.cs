using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStoreApi.Model
{
    public class JewelryService : IJewelryService
    {
        public double Calculate(JewelryDetail jewelryDetail)
        {
            double actualPrice = (jewelryDetail.PricePerGram * jewelryDetail.Weight);

            double discountedAmount = jewelryDetail.Discount.HasValue ? (actualPrice * jewelryDetail.Discount.Value) / 100 : 0;

            double totalPrice = actualPrice - discountedAmount;

            return totalPrice;
        }

        public byte[] CreateByteArray(JewelryDetail jewelryDetail)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append($"Gold Price per gram : {jewelryDetail.PricePerGram} \n");
            builder.Append($"Gold Weight : {jewelryDetail.Weight} grams \n");

            if (jewelryDetail.Discount.HasValue)
            {
                builder.Append($"Discount : {jewelryDetail.Discount} % \n");
            }
            builder.Append("_______________________________\n\n");
            builder.Append($"Total Price: {Calculate(jewelryDetail)}");

            string data = builder.ToString();

            MemoryStream stream = new MemoryStream();

            PdfDocument pdf = new PdfDocument(new PdfWriter(stream));

            Document document = new Document(pdf);

            Paragraph title = new Paragraph("Jewel Recipt");
            title.SetBold();
            title.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);


            Paragraph para = new Paragraph().Add(title).Add("\n_______________________________\n\n").Add(data);
            para.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

            document.Add(para);

            document.Close();

            return stream.ToArray();
        }
    }
}
