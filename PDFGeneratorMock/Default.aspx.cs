using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PDFGeneratorMock
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 遅延を発生させる
            System.Threading.Thread.Sleep(5000);

            Context.Response.Buffer = false;
            Response.ContentType = @"application/pdf";


            byte[] bs = Properties.Resources.SamplePdf;

            if (Context.Response.IsClientConnected)
            {
                Context.Response.OutputStream.Write(bs, 0, bs.Length);
                Context.Response.Flush();
            }
        }
    }
}