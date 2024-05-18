using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services.Description;
using System.Xml;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebGet(UriTemplate = "index.html")]
        [XmlSerializerFormat]
        XmlDocument GetHtml();

        [OperationContract]
        [WebInvoke(UriTemplate = "CreateUser/{a}/{b}")]
        string CreateUser(string a, string b);

        [OperationContract]
        [WebInvoke(UriTemplate = "LoginUser/{a}/{b}")]
        Guid LoginUser(string a, string b);

        [OperationContract]
        [WebInvoke(UriTemplate = "LogOut/{id}")]
        string LogOut(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "Put/{nazwa}/{tresc}/{id}")]
        string Put(string nazwa,string tresc,string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "Get/{nazwa}/{id}")]
        string Get(string nazwa, string id);

        [OperationContract]
        [WebGet(UriTemplate = "scripts.js")]
        Stream GetScript();

        // TODO: Add your service operations here
    }
}
