using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerRole2
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);
        static Random rnd = new Random();
        public override void Run()
        {
            Trace.TraceInformation("WorkerRole2 is running");

            try
            {
                var account = CloudStorageAccount.DevelopmentStorageAccount;
                CloudQueueClient client1 = account.CreateCloudQueueClient();
                CloudQueue queue = client1.GetQueueReference("kolejka");

                CloudBlobClient client = account.CreateCloudBlobClient();
                CloudBlobContainer container = client.GetContainerReference("encryptet");

                container.CreateIfNotExists();
                queue.CreateIfNotExists();

                var msg3 = queue.GetMessage();
                if(msg3 != null)
                {
                    var msgString = msg3.AsString;
                    string[] words = msgString.Split(' ');

                    string enryptedMEssage = EncryptMEssayge(words[1]);

                    
                    var blob = container.GetBlockBlobReference(words[0]);

                    var bytes = new ASCIIEncoding().GetBytes(enryptedMEssage);
                    var s = new System.IO.MemoryStream(bytes);
                    blob.UploadFromStream(s);

                    queue.DeleteMessage(msg3);//dobrze sobs³u¿ona wiadomosc
                }

            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        private string EncryptMEssayge(string message)
        {
            
            var randomNum = rnd.Next(0,3);
            if (randomNum == 0)
            {
                throw new Exception("my Exception");
            }
            var charArr = message.ToCharArray();
            for (int counter = 0; counter < message.Length; counter++)
            {
                int numeric_rep = charArr[counter];
                if (charArr[counter] >= 97 && charArr[counter] <123)
                {
                    charArr[counter] = (char)(numeric_rep + 13);
                    if ((charArr[counter]) >= 123)
                    {
                        charArr[counter] = (char)(97 + charArr[counter] % 123);
                    }
                }
                else if (charArr[counter] >= 65 && charArr[counter] <91)
                {
                    charArr[counter] = (char)(numeric_rep + 13);
                    if ((charArr[counter]) >= 91)
                    {
                        charArr[counter] = (char)(65 + charArr[counter] % 91);
                    }
                }
            }
            return new string(charArr);
        }
        public override bool OnStart()
        {
            // Use TLS 1.2 for Service Bus connections
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Trace.TraceInformation("WorkerRole2 has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("WorkerRole2 is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("WorkerRole2 has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Working");
                await Task.Delay(1000);
            }
        }
    }
}
