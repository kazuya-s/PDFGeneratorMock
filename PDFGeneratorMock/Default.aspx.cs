using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing.Layout;

namespace PDFGeneratorMock
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            byte[] bs = null;

            if (Context.Request["mode"] == "dynamic")
            {
                // PDFを動的に作る
                using (MemoryStream stream = new MemoryStream())
                {
                    PdfDocument document = new PdfDocument();
                    PdfPage page = document.AddPage();

                    // アクセスのあった文字列を表示
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    XFont font = new XFont("Times New Roman", 10, XFontStyle.Regular);
                    XTextFormatter tf = new XTextFormatter(gfx);
                    XRect rect = new XRect(40, 100, 500, 200);
                    tf.DrawString("URL = " + Context.Request.Url.ToString(), font, XBrushes.Black, rect, XStringFormats.TopLeft);

                    document.Save(stream, true);
                    bs = stream.ToArray();
                }
            }
            else
            {
                // 静的なPDFを返す
                System.Threading.Thread.Sleep(5000);

                Context.Response.Buffer = false;
                Response.ContentType = @"application/pdf";


                bs = Properties.Resources.SamplePdf;
            }

            if (Context.Response.IsClientConnected)
            {
                Context.Response.OutputStream.Write(bs, 0, bs.Length);
                Context.Response.Flush();
            }
        }
    }
}