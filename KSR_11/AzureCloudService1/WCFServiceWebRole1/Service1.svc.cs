using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
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
using Microsoft.Azure.KeyVault.Core;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;
using System.Security.Policy;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json.Linq;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        string indexFile = "C:\\Users\\chudy\\source\\repos\\KSR_11\\AzureCloudService1\\index.xhtml";
        string scriptFile = "C:\\Users\\chudy\\source\\repos\\KSR_11\\AzureCloudService1\\scripts.js";
        public XmlDocument GetHtml()
        {
            var html = new XmlDocument();
            html.Load(indexFile);
            return html;
        }

        public string CreateUser(string login, string haslo)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudTableClient cl = account.CreateCloudTableClient();
            var table = cl.GetTableReference("Tabelaraza");
            table.CreateIfNotExists();
            var eee = new TestEntity(login, "zbiórHaseł");
            eee.login = login;
            eee.haslo = haslo;
            TableOperation op = TableOperation.Insert(eee);


            TableOperation opCheck = TableOperation.Retrieve<TestEntity>(rowkey : login, partitionKey : "zbiórHaseł");
            var res = table.Execute(opCheck);
            TestEntity e = (TestEntity)res.Result;

            if (e == null)
            {
                table.Execute(op);
                return "registered sucsesfull";
            }
            else{
                return "login present ERRROR";
            }
        }

        public Stream GetScript()
        {
            if (File.Exists(scriptFile))
            {
                return new FileStream(scriptFile, FileMode.Open);
            }
            return null;
        }

        public Guid LoginUser(string login, string haslo)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudTableClient cl = account.CreateCloudTableClient();
            var table = cl.GetTableReference("Tabelaraza");
            table.CreateIfNotExists();
            TableOperation opCheck = TableOperation.Retrieve<TestEntity>(rowkey: login, partitionKey: "zbiórHaseł");
            var res = table.Execute(opCheck);
            TestEntity e = (TestEntity)res.Result;
            if(e != null && e.haslo == haslo)
            {
                var accountSession = CloudStorageAccount.DevelopmentStorageAccount;
                CloudTableClient clS = accountSession.CreateCloudTableClient();
                var tableS = clS.GetTableReference("Sesje");
                tableS.CreateIfNotExists();

                Guid sesGuid = Guid.NewGuid();
                var newSession = new TestEntity(sesGuid.ToString(), "zbiórSesji");
                newSession.login = login;
                newSession.haslo = haslo;
                TableOperation op = TableOperation.Insert(newSession);
                tableS.Execute(op);
                return sesGuid;
            }
            return Guid.Empty;
        }
        private TestEntity ifSessiaPResent(string guid)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudTableClient cl = account.CreateCloudTableClient();
            var table = cl.GetTableReference("Sesje");
            table.CreateIfNotExists();

            TableOperation opCheck = TableOperation.Retrieve<TestEntity>(rowkey: guid, partitionKey: "zbiórSesji");
            var res = table.Execute(opCheck);
            return (TestEntity)res.Result;
        }
        public string LogOut(string a)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudTableClient cl = account.CreateCloudTableClient();
            var table = cl.GetTableReference("Sesje");
            table.CreateIfNotExists();
            TestEntity e = ifSessiaPResent(a);
            if (e == null)
            {
                
                return "Log OUT FAILED FOR SOMEA RESAON";
            }
            else
            {
                TableOperation op = TableOperation.Delete(e);
                table.Execute(op);
                return "LOG OUT SUCCSESSS";
            }
        }

        public string Put(string nazwa, string tresc, string id)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudBlobClient client = account.CreateCloudBlobClient();


            TestEntity e = ifSessiaPResent(id);
            if (e == null)
            {
                return "no seesion present";
            }
            CloudBlobContainer container = client.GetContainerReference(e.login);
            container.CreateIfNotExists();
            var blob = container.GetBlockBlobReference(nazwa);

            var bytes = new System.Text.ASCIIEncoding().GetBytes(tresc);
            var s = new System.IO.MemoryStream(bytes);
            blob.UploadFromStream(s);



            return "tresc uploaded";


        }

        public string Get(string nazwa, string id)
        {
            TestEntity e = ifSessiaPResent(id);
            if (e == null)
            {
                return "no seesion present";
            }

            var account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudBlobClient client = account.CreateCloudBlobClient();
            if(!client.GetContainerReference(e.login).Exists())
            {
                return "bruh how? to nie powinno byc mozliwe ten error wywolac";
            }
            CloudBlobContainer container = client.GetContainerReference(e.login);
            container.CreateIfNotExists();
            if (!container.GetBlockBlobReference(nazwa).Exists())
            {
                return "ni ma bloba";
            }
            var blob = container.GetBlockBlobReference(nazwa);
            var s2 = new System.IO.MemoryStream();
            blob.DownloadToStream(s2); 
            string content = System.Text.Encoding.UTF8.GetString(s2.ToArray());
            return content;

        }
    }
}
