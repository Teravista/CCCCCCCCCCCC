using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Platformy_Lab_5_CS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TasksClick(object sender, RoutedEventArgs e)
        {
            int N;
            int K;
            try
            {
                N = int.Parse(NBox.Text);
                K = int.Parse(KBox.Text);
            }
            catch
            {
                System.Windows.MessageBox.Show("Input K or N wrong try again");
                return;
            }
            var myTuple = Tuple.Create(N, K);
            Task<int> dzielna = Task.Factory.StartNew<int>(
                (obj) =>
                {
                    var myTuple = (Tuple<int, int>)obj;
                    int N = myTuple.Item1;
                    int K = myTuple.Item2;
                    int sum = 1;
                    for (int i = 0; i < K; i++)
                        sum = sum * (N - i);
                    return sum;
                }, myTuple
             );

            Task<int> dzielnik = Task.Factory.StartNew<int>(
                (obj) =>
                {
                    var myTuple = (Tuple<int, int>)obj;
                    int N = myTuple.Item1;
                    int K = myTuple.Item2;
                    int sum = 1;
                    for (int i = 0; i < K; i++)
                        sum = sum * (i + 1);
                    return sum;
                }, myTuple
             );
            dzielna.Wait();
            dzielnik.Wait();
            double result = dzielna.Result / dzielnik.Result;
            TasksResBox.Text = result.ToString();
        }
        private void DelegatesClick(object sender, RoutedEventArgs e)
        {
            /*int N;
            int K;
            try
            {
                N = int.Parse(NBox.Text);
                K = int.Parse(KBox.Text);
            }
            catch
            {
                MessageBox.Show("Input K or N wrong try again");
                return;
            }
            Func<double> dzielnaFunc = Dzielna;
            Func<double> dzielnikFunc = Dzielnik;
            var resUp = dzielnaFunc.BeginInvoke(null, null);
            var resDown = dzielnikFunc.BeginInvoke(null, null);
            double dzielnaInt = dzielnaFunc.EndInvoke(resUp);
            double dzielnikInt = dzielnikFunc.EndInvoke(resDown);
            double finalResult = dzielnaInt / dzielnikInt;
            DelegatesResBox.Text = finalResult.ToString();*/
        }

        public async Task<double> DzielnaAsync(int N, int K)
        {
            int sum = 1;
            for (int i = 0; i < K; i++)
                sum = sum * (N - i);
            return sum;
        }

        public async Task<double> DzielnikAsync(int N, int K)
        {
            int sum = 1;
            for (int i = 0; i < K; i++)
                sum = sum * (i + 1);
            return sum;
        }

        public async void AsyncFunc()
        {
            int N;
            int K;
            try
            {
                N = int.Parse(NBox.Text);
                K = int.Parse(KBox.Text);
            }
            catch
            {
                System.Windows.MessageBox.Show("Input K or N wrong try again");
                return;
            }
            Task<double>[] tasks = new Task<double>[2];
            tasks[0] = Task.Run(() => DzielnaAsync(N, K));
            tasks[1] = Task.Run(() => DzielnikAsync(N, K));
            await Task.WhenAll(tasks);
            AsyncResBox.Text = (tasks[0].Result / tasks[1].Result).ToString();
        }

        private void AsyncClick(object sender, RoutedEventArgs e)
        {
            AsyncFunc();
        }

        private void BackgroundWorkerClick(object sender, RoutedEventArgs e)
        {
            int i;
            try
            {
                i = int.Parse(IinFib.Text);
                if(i<=2||i>103)
                {
                    System.Windows.MessageBox.Show("Input i for Fibbonaci is wrong max number is 103");
                    return;
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Input i for Fibbonaci is wrong");
                return;
            }
            FibStarter.IsEnabled = false; 
            BackgroundWorker MyBackground = new BackgroundWorker();
            MyBackground.DoWork += MyBackground_DoWork;
            MyBackground.ProgressChanged += MyBackground_ProgressChanged;
            MyBackground.WorkerReportsProgress = true;
            MyBackground.RunWorkerCompleted += MyBackground_Complteted;
            MyBackground.RunWorkerAsync(i);
        }
        private void MyBackground_Complteted(object sender, RunWorkerCompletedEventArgs e)
        {
            FibResult.Text = e.Result.ToString();
            FibStarter.IsEnabled = true;
        }
        private void MyBackground_DoWork(object? sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int iterations = (int)e.Argument;
            long[] fib = new long[iterations+1];
            fib[0] = 0;
            fib[1] = 1;
            worker.ReportProgress(0);
            for (int i = 2; i < iterations+1; ++i)
            {
                fib[i]= fib[i-1]+fib[i-2];
                if(worker.CancellationPending)
                {
                    return;
                }
                worker.ReportProgress((int)((float)i/(float)iterations*100));
                Thread.Sleep(20);
            }
            worker.ReportProgress(100);
            e.Result = fib[iterations ];
        }
        private void MyBackground_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
        }

        private void ResponseCheck(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();

            ResponseRes.Text = (rnd.Next()%10000000000000).ToString();
        }

        private void DnsClick(object sender, RoutedEventArgs e)
        {
            string[] hostNames = { "www.microsoft.com", "www.apple.com",
                "www.google.com", "www.ibm.com", "cisco.netacad.net",
                "www.oracle.com", "www.nokia.com", "www.hp.com", "www.dell.com",
                "www.samsung.com", "www.toshiba.com", "www.siemens.com",
                "www.amazon.com", "www.sony.com", "www.canon.com", "www.alcatel-lucent.com", "www.acer.com", "www.motorola.com" };

            DNSResult.Text = null;
            var HostIPNames = from a in hostNames.AsParallel()
                            select new { hostName = a, IP= Dns.GetHostAddresses(a).First() };
            foreach( var IPName in HostIPNames) {
                DNSResult.Text += IPName.hostName+"=> \n"+IPName.IP +"\n";
            }
        }
        private static void CompressFile(string OriginalFiledir, string name, string destination)
        {
            using FileStream originalFileStream = File.Open(OriginalFiledir + $"\\" + name, FileMode.Open);
            using FileStream compressedFileStream = File.Create(destination + $"\\" + name + ".gz");
            using var compressor = new GZipStream(compressedFileStream, CompressionMode.Compress);
            originalFileStream.CopyTo(compressor);

        }

        private static void DecompressFile(string OriginalFiledir, string name, string destination)
        {
            using FileStream compressedFileStream = File.Open(OriginalFiledir + $"\\" + name, FileMode.Open);
            string str = name.Substring(0, name.Length - 3);
            using FileStream outputFileStream = File.Create(destination + $"\\" + str);
            using var decompressor = new GZipStream(compressedFileStream, CompressionMode.Decompress);
            decompressor.CopyTo(outputFileStream);
        }

        private void DecompressClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialogResultC = new FolderBrowserDialog() { Description = "Select directory to open" };
            System.Windows.Forms.DialogResult result = dialogResultC.ShowDialog();
            if (dialogResultC.SelectedPath == "")
                return;
            DirectoryInfo info = new DirectoryInfo(dialogResultC.SelectedPath);
            FileInfo[] files = info.GetFiles();
            DirectoryInfo parentInfo = info.Parent;
            String decompressFolder = parentInfo.ToString() + $"\\decompressed";
            if (!Directory.Exists(decompressFolder))
            {
                Directory.CreateDirectory(decompressFolder);
            }
            foreach (FileInfo file in files)
            {
                var extension = file.Name.Substring(file.Name.Length - 3);
                if (extension==".gz")
                    DecompressFile(file.Directory.ToString(), file.Name.ToString(), decompressFolder);
            }
        }
        private void CompressClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialogResultC = new FolderBrowserDialog() { Description = "Select directory to open" };
            System.Windows.Forms.DialogResult result = dialogResultC.ShowDialog();
            if (dialogResultC.SelectedPath == "")
                return;
            DirectoryInfo info = new DirectoryInfo(dialogResultC.SelectedPath);
            //DirectoryInfo[] dir = info.GetDirectories();
            FileInfo[] files = info.GetFiles();
            DirectoryInfo parentInfo = info.Parent;
            String archiwumfolder = parentInfo.ToString() + $"\\archiwum";
            if (!Directory.Exists(archiwumfolder))
            {
                Directory.CreateDirectory(archiwumfolder);
            }
            foreach (FileInfo file in files)
            {
                CompressFile(file.Directory.ToString(),file.Name.ToString(), archiwumfolder);
            }
        }
    }
}

