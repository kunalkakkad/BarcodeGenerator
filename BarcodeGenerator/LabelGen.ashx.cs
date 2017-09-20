using Neodynamic.SDK.Printing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace BarcodeGenerator
{
    /// <summary>
    /// Summary description for LabelGen
    /// </summary>
    public class LabelGen : IHttpHandler
    {
        byte[] _buffer = null;
        string _productId = "";
        string _productName = "";
        double _dpi = 203;

        public void ProcessRequest(HttpContext context)
        {
            //get data for label from the querystring params...
            _productId = context.Request["prodId"];
            _productName = context.Request["prodName"];
            _dpi = 203;
            try
            {
                _dpi = double.Parse(context.Request["dpi"]);
            }
            catch { }

            this.GenerateThermalLabel();

            context.Response.ContentType = "application/pdf";
            context.Response.BufferOutput = true;
            context.Response.BinaryWrite(_buffer);
            context.Response.Flush();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void GenerateThermalLabel()
        {
            Thread worker = new Thread(new ThreadStart(this.GenerateThermalLabelWorker));
            worker.SetApartmentState(ApartmentState.STA);
            worker.Name = "GenerateThermalLabelWorker";
            worker.Start();
            worker.Join();
        }

        private void GenerateThermalLabelWorker()
        {
            //Create the thermal label object
            ThermalLabel tLabel = new ThermalLabel(UnitType.Inch, 4, 3);

            //Define a TextItem object
            TextItem txtItem = new TextItem(0.2, 0.2, 3, 0.5, _productName);
            //font settings
            txtItem.Font.Name = "Arial";
            txtItem.Font.Unit = FontUnit.Point;
            txtItem.Font.Size = 20;

            //Define a BarcodeItem object
            BarcodeItem bcItem = new BarcodeItem(0.2, 1, 3.6, 1.25, BarcodeSymbology.Code128, _productId);
            //Set bars height to .75inch
            bcItem.BarHeight = 0.75;
            //Set bars width to 0.02inch
            bcItem.BarWidth = 0.02;
            //font settings
            bcItem.Font.Name = "Arial";
            bcItem.Font.Unit = FontUnit.Point;
            bcItem.Font.Size = 12;
            //border settings
            bcItem.BorderThickness = new FrameThickness(0.02);
            bcItem.CornerRadius = new RectangleCornerRadius(0.075);
            //center barcode inside its container
            bcItem.BarcodeAlignment = BarcodeAlignment.MiddleCenter;

            //Add items to ThermalLabel object...
            tLabel.Items.Add(txtItem);
            tLabel.Items.Add(bcItem);

            //generate a pdf doc of the label
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (PrintJob pj = new PrintJob())
                {
                    pj.ExportToPdf(tLabel, ms, _dpi);
                }

                _buffer = ms.ToArray();
            }
        }
    }
}