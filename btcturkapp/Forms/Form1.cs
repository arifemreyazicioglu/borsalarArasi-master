using Binance.ModelsBinance;
using btcturkapp.BinanceFunctions;
using btcturkapp.BTCTurkFunction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingBot;
using TradingBot.Models;

namespace btcturkapp.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Anasayfa ans = new Anasayfa();
            ans.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btcTurkFunction btcTurk = new btcTurkFunction();
            binanceFunctions binance = new binanceFunctions();

            //var accInfo = API.GetAccountInformation();

            //Check account funds for both currencies
           

            var timer = new Timer { Interval = 1000 };
            timer.Tick += async (o, args) =>
            {
                //AssetBalances Currency1 = (from coin in accInfo.Balances where coin.Asset == "USDT" select coin).FirstOrDefault();
                //var freeCurrency1 = Currency1.Free;

                label1.Text = await btcTurk.BTCTurkGetAccountBalance("TRY");
                //label2.Text = freeCurrency1.ToString();
                label2.Text = await binance.BinanceGetBalanceAsync("USDT");
            };
            timer.Start();
        }
    }
}
