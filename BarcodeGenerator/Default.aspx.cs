using Spire.Barcode;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BarcodeGenerator
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PrintBtn.Click += new EventHandler(this.PrintBtn_Click);
            //pdfViewer.Text = CreatePdfObjectTag("LabelGen.ashx?dpi=" + DropDownList1.SelectedItem.ToString() + "&prodId=" + TextBox2.Text + "&prodName=" + TextBox1.Text + "&out=PDF");
        }

        //private string CreatePdfObjectTag(string pdfUrl)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<object type=\"application/pdf\" ");
        //    sb.Append("data=\"");
        //    sb.Append(pdfUrl);
        //    sb.Append("#toolbar=1&navpanes=0&scrollbar=1&page=1&zoom=100\" width=\"600px\" height=\"400px\" VIEWASTEXT><p>It appears you don't have a PDF plugin for your browser. <a target=\"_blank\" href=\"");
        //    sb.Append(pdfUrl);
        //    sb.Append("\">Click here to download the PDF file.</a></p></object>");
        //    return sb.ToString();
        //}

        protected void PrintBtn_Click(object sender, EventArgs e)
        {
            SendToPrinter();
        }

        private void SendToPrinter()
        {
            //ProcessStartInfo info = new ProcessStartInfo();
            //info.Verb = "print";
            //info.FileName = @"D:\test.pdf";
            //info.CreateNoWindow = true;
            //info.WindowStyle = ProcessWindowStyle.Hidden;

            //Process p = new Process();
            //p.StartInfo = info;
            //p.Start();

            ////p.WaitForInputIdle();
            //System.Threading.Thread.Sleep(3000);
            //if (false == p.CloseMainWindow())
            //    p.Kill();

            BarcodeSettings bs = new BarcodeSettings();
            bs.Data = TextBox1.Text;
            bs.Data2D = TextBox1.Text;
            bs.ShowText = true;
            //bs.BarHeight = 1;
            //bs.X = 1;
            //bs.Y = 1;
            BarCodeGenerator generator = new BarCodeGenerator(bs);
            System.Drawing.Image barcode = generator.GenerateImage();

            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var guidNumber = Guid.NewGuid().ToString().Substring(0, 6);
            var barcodeGeneratePath = basePath + "BarcodePdfs\\" + guidNumber;
            //save the barcode as an image
            barcode.Save(barcodeGeneratePath + ".png");


            PdfDocument doc = new PdfDocument();
            PdfSection section = doc.Sections.Add();
            PdfPageBase page = doc.Pages.Add();

            PdfImage image = PdfImage.FromFile(barcodeGeneratePath + ".png");
            //Set image display location and size in PDF

            //float widthFitRate = image.PhysicalDimension.Width / page.Canvas.ClientSize.Width;
            //float heightFitRate = image.PhysicalDimension.Height / page.Canvas.ClientSize.Height;
            //float fitRate = Math.Max(widthFitRate, heightFitRate);
            //float fitWidth = image.PhysicalDimension.Width / fitRate;
            //float fitHeight = image.PhysicalDimension.Height / fitRate;

            var barcodeImageWidth = 100;
            var barcodeImageHeigth = 40;

            page.Canvas.DrawImage(image, 30, 30, barcodeImageWidth, barcodeImageHeigth);

            doc.SaveToFile(barcodeGeneratePath + ".pdf");
            doc.Close();

            doc.LoadFromFile(barcodeGeneratePath + ".pdf");
            doc.PrintDocument.Print();

        }
    }
}