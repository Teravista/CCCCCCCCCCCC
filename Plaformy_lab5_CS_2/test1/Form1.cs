using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static double Dzielna(int N,int K)
        {
            int sum = 1;
            for (int i = 0; i < K; i++)
                sum = sum * (N - i);
            return sum;
        }

        public static double Dzielnik(int N, int K)
        {
            int sum = 1;
            for (int i = 0; i < K; i++)
                sum = sum * (i + 1);
            return sum;
        }
        private void DelegatesClick()
        {
            int N;
            int K;
            try
            {
                N = int.Parse(textBox2.Text);
                K = int.Parse(textBox3.Text);
            }
            catch
            {
                MessageBox.Show("Input K or N wrong try again");
                return;
            }
            Func<int,int,double> dzielnaFunc = Dzielna;
            Func<int,int,double> dzielnikFunc = Dzielnik;
            var resUp = dzielnaFunc.BeginInvoke(N,K,null, null);
            var resDown = dzielnikFunc.BeginInvoke(N, K, null, null);
            double dzielnaInt = dzielnaFunc.EndInvoke(resUp);
            double dzielnikInt = dzielnikFunc.EndInvoke(resDown);
            double finalResult = dzielnaInt / dzielnikInt;
            label3.Text=finalResult.ToString();
            return;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DelegatesClick();
        }
    }
}
