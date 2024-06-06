using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public void Koduj(string nazwa, string tresc)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudBlobClient client = account.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference("nonencryptet");
            container.CreateIfNotExists();


            var blob = container.GetBlockBlobReference(nazwa);

            var bytes = new System.Text.ASCIIEncoding().GetBytes(tresc);
            var s = new System.IO.MemoryStream(bytes);
            blob.UploadFromStream(s);

            CloudQueueClient clientKolejka = account.CreateCloudQueueClient();
            CloudQueue queue = clientKolejka.GetQueueReference("kolejka");
            queue.CreateIfNotExists();
            var msg = new CloudQueueMessage(nazwa+" "+tresc);
            queue.AddMessage(msg);
        }
        public string Pobierz(string nazwa)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudBlobClient client = account.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference("encryptet");
            container.CreateIfNotExists();

            var blob = container.GetBlockBlobReference(nazwa);
            if(!container.GetBlockBlobReference(nazwa).Exists())
            {
                return "blob not found";
            }
            
            var s2 = new System.IO.MemoryStream(); 
            blob.DownloadToStream(s2); 
            string content = System.Text.Encoding.UTF8.GetString(s2.ToArray());
            return content;

        }


    }
}
