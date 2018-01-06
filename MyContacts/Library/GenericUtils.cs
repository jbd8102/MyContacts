using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyContacts.Library
{
    public static class GenericUtils
    {
        public static void WriteToCSV(string pContenuExport)
        {
            WriteToCSV(pContenuExport, string.Empty);
        }

        public static void WriteToCSV(string pContenuExport, string fileName)
        {
            string attachment;
            string v_date;

            if (string.IsNullOrEmpty(fileName))
            {
                attachment = "attachment; filename=Export.csv";
            }
            else
            {
                // just in case, remove the apostrophes and hyphens
                fileName = fileName.Replace("'", " ");
                fileName = fileName.Replace("-", " ");
                fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
                fileName = fileName.Replace("+", " ");
                v_date = DateTime.Now.ToString("yyyyMMdd");

                attachment = string.Format("attachment; filename={0}_{1}.csv", v_date, fileName);
            }

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            HttpContext.Current.Response.AddHeader("Pragma", "public");
            HttpContext.Current.Response.Write(pContenuExport);
            HttpContext.Current.Response.End();
        }
    }
}