using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;

namespace ZAD3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    public class Service1 : IService1
    {
        string indexFile = "C:\\Users\\Robert\\source\\repos\\_WCF_3_Robert_Chudy\\index.xhtml";
        string scriptFile = "C:\\Users\\Robert\\source\\repos\\_WCF_3_Robert_Chudy\\scripts.js";
        public XmlDocument GetHtml()
        {
            var html = new XmlDocument();
            html.Load(indexFile);
            return html;
        }

        public int Dodaj(string a, string b)
        {
            return Int32.Parse(a) + Int32.Parse(b);
        }

        public Stream GetScript()
        {
            if (File.Exists(scriptFile))
            {
                return new FileStream(scriptFile, FileMode.Open);
            }
            return null;
        }
    }
}
