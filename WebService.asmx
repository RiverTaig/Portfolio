<%@ WebService Language="C#" Class="WebService" %>

using System;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService   {

    [WebMethod]
    public string MakeTemplates(int pages, int startNumber, int endNumber, int rows, int columns, int colorCount1, int colorCount2, int colorCount3, int colorCount4,
        string color1, string color2, string color3, string color4) {
        try
        {
            Random rnd = new Random();
            int rNumber = rnd.Next(0, int.MaxValue);
            string fileName = HttpContext.Current.Request.MapPath("Templates/MyLogs" + rNumber.ToString() + ".txt");
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                for (int page = 0; page < pages; page++)
                {
                    for (int r = 0; r < rows; r++)
                    {
                        for (int c = 0; c < columns; c++)
                        {

                        }
                    }
                }
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(startNumber);
                sw.Close();
            }


            return fileName;
        }
        catch (Exception ex) {
            return ex.ToString();
        }

    }

}