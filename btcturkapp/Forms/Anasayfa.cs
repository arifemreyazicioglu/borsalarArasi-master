using APIClient.ApiClientV1;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using btcturkapp.BTCTurkFunction;
using System.Globalization;
using btcturkapp.BinanceFunctions;
using System.IO;
using Binance;

namespace btcturkapp
{
    
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();          
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            int h = Screen.PrimaryScreen.WorkingArea.Height - 40;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);

            btcTurkFunction btcTurk = new btcTurkFunction();
            binanceFunctions binance = new binanceFunctions();

            priceDataGridView.Rows.Add();
            priceDataGridView.Rows.Add();
            priceDataGridView.Rows.Add();
            priceDataGridView.Rows.Add();

            priceDataGridView.Rows[0].Cells[0].Value = "Binance";
            priceDataGridView.Rows[0].Cells[1].Value = "USDTTRY";
            priceDataGridView.Rows[1].Cells[0].Value = "BTCTurk";
            priceDataGridView.Rows[1].Cells[1].Value = "USDTTRY";
            priceDataGridView.Rows[2].Cells[0].Value = "Binance";
            priceDataGridView.Rows[2].Cells[1].Value = "BTCUSDT";
            priceDataGridView.Rows[3].Cells[0].Value = "BTCTurk";
            priceDataGridView.Rows[3].Cells[1].Value = "BTCUSDT";




            var timer = new Timer { Interval = 500 };
            timer.Tick += async (o, args) =>
            {

                //Binance Usdt Satış Fiyatı             
                var res1 = "";
                try
                {
                   res1 = await binance.BinanceGetValueAsync("USDTTRY");
                   res1 = res1.Split(' ')[3].Split(' ')[0];
                   priceDataGridView.Rows[0].Cells[2].Value = res1;
                }
                catch
                {
                    res1 = "Server Error";
                }                
               
                //Binance Usdt Alış Fiyatı
                var res2 = "";
                try
                {
                    res2 = await binance.BinanceGetValueAsync("USDTTRY");
                    res2 = res2.Split(' ')[1].Split(' ')[0];
                    priceDataGridView.Rows[0].Cells[3].Value = res2;
                }
                catch
                {
                    res2 = "Server Error";
                }

                //BTCTurk Usdt Satış Fiyatı
                var res3 = "";
                try
                {
                    res3 = await btcTurk.BTCTurkGetValueAsync("USDTTRY");
                    res3 = res3.Split(' ')[3].Split(' ')[0];
                    priceDataGridView.Rows[1].Cells[2].Value = res3;
                }
                catch
                {
                    res3 = "Server Error";
                }

                //BTCTurk Usdt Alış Fiyatı
                var res4 = "";
                try
                {
                    res4 = await btcTurk.BTCTurkGetValueAsync("USDTTRY");
                    res4 = res4.Split(' ')[1].Split(' ')[0];
                    priceDataGridView.Rows[1].Cells[3].Value = res4;
                }
                catch
                {
                    res4 = "Server Error";
                }
                //Binance BTC Satış Fiyatı
                var res5 = "";
                try
                {
                    res5 = await binance.BinanceGetValueAsync("BTCUSDT");
                    res5 = res5.Split(' ')[3].Split(' ')[0];
                    priceDataGridView.Rows[2].Cells[2].Value = res5;
                }
                catch
                {
                    res5 = "Server Error";
                }

                //Binance BTC Alış Fiyatı
                var res6 = "";
                try
                {
                    res6 = await binance.BinanceGetValueAsync("BTCUSDT");
                    res6 = res6.Split(' ')[1].Split(' ')[0];
                    priceDataGridView.Rows[2].Cells[3].Value = res6;
                }
                catch
                {
                    res6 = "Server Error";
                }

                //BTCTurk BTC Satış Fiyatı
                var res7 = "";
                try
                {
                    res7 = await btcTurk.BTCTurkGetValueAsync("BTCUSDT");
                    res7 = res7.Split(' ')[3].Split(' ')[0];
                    priceDataGridView.Rows[3].Cells[2].Value = res7;
                }
                catch
                {
                    res7 = "Server Error";
                }

                //BTCTurk BTC Alış Fiyatı
                var res8 = "";
                try
                {
                    res8 = await btcTurk.BTCTurkGetValueAsync("BTCUSDT");
                    res8 = res8.Split(' ')[1].Split(' ')[0];
                    priceDataGridView.Rows[3].Cells[3].Value = res8;
                }
                catch
                {
                    res8 = "Server Error";
                }

                //// Binance ve BTCTurk arasındaki Btc alış(bid) fiyatı farkı
                //string farkbinancebtcturkbidbtc = farkBinancetoBtcTurkBidBtc.Text;
                //if (!string.IsNullOrEmpty(farkbinancebtcturkbidbtc))
                //{
                //    var bid = double.Parse(binanceBtcUsdtBidPrice.Text, CultureInfo.InvariantCulture) - double.Parse(btcTurkBtcUsdtBidPrice.Text, CultureInfo.InvariantCulture);
                //    if (bid < 0)
                //    {
                //        bid = Math.Abs(bid);
                //        farkBinancetoBtcTurkBidBtc.Text = "BTCTurk'te " + bid.ToString("0.####") + " $ daha pahalı";
                //        farkBinancetoBtcTurkBidBtc.ForeColor = Color.Black;
                //    }
                //    else
                //    {
                //        farkBinancetoBtcTurkBidBtc.Text = "Binance'de " + bid.ToString("0.####") + " $ daha pahalı";
                //        farkBinancetoBtcTurkBidBtc.ForeColor = Color.Black;
                //    }

                //}

                ////Binance ve BTCTurk arasındaki Btc satış(ask) fiyatı farkı
                //string farkbinancebtcturkaskbtc = farkBinancetoBtcTurkAskBtc.Text;
                //if (!string.IsNullOrEmpty(farkbinancebtcturkaskbtc))
                //{

                //    var ask = double.Parse(binanceBtcUsdtAskPrice.Text, CultureInfo.InvariantCulture) - double.Parse(btcTurkBtcUsdtAskPrice.Text, CultureInfo.InvariantCulture);

                //    if (ask < 0)
                //    {
                //        ask = Math.Abs(ask);
                //        farkBinancetoBtcTurkAskBtc.Text = "BTCTurk'te " + ask.ToString("0.####") + " $ daha pahalı";
                //        farkBinancetoBtcTurkAskBtc.ForeColor = Color.Black;
                //    }
                //    else
                //    {
                //        farkBinancetoBtcTurkAskBtc.Text = "Binance'de " + ask.ToString("0.####") + " $ daha pahalı";
                //        farkBinancetoBtcTurkAskBtc.ForeColor = Color.Black;
                //    }

                //}

                //// Binance ve BTCTurk arasındaki Usdt alış(bid) fiyatı farkı
                //string farkbinancebtcturkbidusdt = farkBinancetoBtcTurkBidUsdt.Text;
                //if (!string.IsNullOrEmpty(farkbinancebtcturkbidusdt))
                //{
                //    var bid = double.Parse(binanceUsdtBidPrice.Text, CultureInfo.InvariantCulture) - double.Parse(btcTurkUsdtBidPrice.Text, CultureInfo.InvariantCulture);
                //    if (bid < 0)
                //    {
                //        bid = Math.Abs(bid);
                //        farkBinancetoBtcTurkBidUsdt.Text = "BTCTurk'te " + bid.ToString("0.####") + " TL daha pahalı";
                //        farkBinancetoBtcTurkBidUsdt.ForeColor = Color.Black;
                //    }
                //    else
                //    {
                //        farkBinancetoBtcTurkBidUsdt.Text = "Binance'de " + bid.ToString("0.####") + " TL daha pahalı";
                //        farkBinancetoBtcTurkBidUsdt.ForeColor = Color.Black;
                //    }

                //}

                //// Binance ve BTCTurk arasındaki Usdt satış(ask) fiyatı farkı
                //string farkbinancebtcturkaskusdt = farkBinancetoBtcTurkAskUsdt.Text;
                //if (!string.IsNullOrEmpty(farkbinancebtcturkaskusdt))
                //{
                //    var bid = double.Parse(binanceUsdtAskPrice.Text, CultureInfo.InvariantCulture) - double.Parse(btcTurkUsdtAskPrice.Text, CultureInfo.InvariantCulture);
                //    if (bid < 0)
                //    {
                //        bid = Math.Abs(bid);
                //        farkBinancetoBtcTurkAskUsdt.Text = "BTCTurk'te " + bid.ToString("0.####") + " TL daha pahalı";
                //        farkBinancetoBtcTurkAskUsdt.ForeColor = Color.Black;
                //    }
                //    else
                //    {
                //        farkBinancetoBtcTurkAskUsdt.Text = "Binance'de " + bid.ToString("0.####") + " TL daha pahalı";
                //        farkBinancetoBtcTurkAskUsdt.ForeColor = Color.Black;
                //    }

                //}

                ////Binance satip BTCTurk de alma Usdt
                //string sellusdtonbinance = sellUsdtOnBinance.Text;
                //if (!string.IsNullOrEmpty(sellusdtonbinance))
                //{
                //    //binance ask btc bid

                //    if (binanceUsdtTextBox.Text == "")
                //    {
                //        binanceUsdtTextBox.Text = "0";
                //    }

                //    var res = (double.Parse(binanceUsdtAskPrice.Text, CultureInfo.InvariantCulture) * double.Parse(binanceUsdtTextBox.Text, CultureInfo.InvariantCulture)) - ((double.Parse(binanceUsdtTextBox.Text, CultureInfo.InvariantCulture) * double.Parse("0.0001", CultureInfo.InvariantCulture)) * double.Parse(binanceUsdtAskPrice.Text, CultureInfo.InvariantCulture)) ;

                //    sellUsdtOnBinance.Text = res.ToString("0.####");

                //}

                //string buyusdtonbtcturk = buyUsdtOnBtcTurk.Text;
                //if (!string.IsNullOrEmpty(buyusdtonbtcturk))
                //{
                //    //binance bid btc ask

                //    if (binanceUsdtTextBox.Text == "")
                //    {
                //        binanceUsdtTextBox.Text = "0";
                //    }

                //    var res = (double.Parse(btcTurkUsdtBidPrice.Text, CultureInfo.InvariantCulture) * double.Parse(binanceUsdtTextBox.Text, CultureInfo.InvariantCulture)) + ((double.Parse(binanceUsdtTextBox.Text, CultureInfo.InvariantCulture) * double.Parse("0.0001", CultureInfo.InvariantCulture)) * double.Parse(btcTurkUsdtAskPrice.Text, CultureInfo.InvariantCulture));

                //    buyUsdtOnBtcTurk.Text = res.ToString("0.####");

                //}

                //string karzarardurumu = karZararDurumu.Text;
                //if (!string.IsNullOrEmpty(karzarardurumu))
                //{

                //    var res = double.Parse(sellUsdtOnBinance.Text) - double.Parse(buyUsdtOnBtcTurk.Text) - (double.Parse(btcTurkUsdtAskPrice.Text, CultureInfo.InvariantCulture) / 4);
                //    karZararDurumu.Text = res.ToString("0.####") + " TL";

                //}

                ////BTCTurk satip Binance de alma Usdt

                //string sellusdtonbtcturk = sellUsdtOnBtcTurk.Text;
                //if (!string.IsNullOrEmpty(sellusdtonbtcturk))
                //{
                //    //binance ask btc bid

                //    if (btcTurkUsdtTextBox.Text == "")
                //    {
                //        btcTurkUsdtTextBox.Text = "0";
                //    }

                //    var res = (double.Parse(btcTurkUsdtAskPrice.Text, CultureInfo.InvariantCulture) * double.Parse(btcTurkUsdtTextBox.Text, CultureInfo.InvariantCulture)) -((double.Parse(btcTurkUsdtTextBox.Text, CultureInfo.InvariantCulture) * double.Parse("0.0001", CultureInfo.InvariantCulture)) * double.Parse(btcTurkUsdtAskPrice.Text, CultureInfo.InvariantCulture));

                //    //- 
                //    sellUsdtOnBtcTurk.Text = res.ToString("0.####");


                //}

                //string buyusdtonbinance = buyUsdtOnBinance.Text;
                //if (!string.IsNullOrEmpty(buyusdtonbinance))
                //{
                //    //binance bid btc ask

                //    if (btcTurkUsdtTextBox.Text == "")
                //    {
                //        btcTurkUsdtTextBox.Text = "0";
                //    }

                //    var res = (double.Parse(btcTurkUsdtTextBox.Text, CultureInfo.InvariantCulture) * double.Parse(binanceUsdtBidPrice.Text, CultureInfo.InvariantCulture)) + ((double.Parse(btcTurkUsdtTextBox.Text, CultureInfo.InvariantCulture) * double.Parse("0.0001", CultureInfo.InvariantCulture)) * double.Parse(binanceUsdtAskPrice.Text, CultureInfo.InvariantCulture));

                //    buyUsdtOnBinance.Text = res.ToString("0.####");


                //}

                //string karzarardurumu2 = karZararDurumu2.Text;
                //if (!string.IsNullOrEmpty(karzarardurumu2))
                //{

                //    var res = double.Parse(sellUsdtOnBtcTurk.Text) - double.Parse(buyUsdtOnBinance.Text) - double.Parse(binanceUsdtAskPrice.Text, CultureInfo.InvariantCulture);

                //    karZararDurumu2.Text = res.ToString("0.####") + " TL";

                //    // (double.Parse(btcTurkBtcUsdtPrice.Text.Split(' ')[3].Split(' ')[0]) * double.Parse(btcTextBox.Text)) - (double.Parse(binanceBtcUsdtPrice.Text.Split(' ')[1].Split(' ')[0]) * double.Parse(btcTextBox.Text));

                //}

                //BTCTurk satip Binance de alma Btc
                //string sellbtconbtcturk = sellBtcOnBtcTurk.Text;
                //if (!string.IsNullOrEmpty(sellbtconbtcturk))
                //{
                //    //binance ask btc bid

                //    if (btcTurkBtcTextBox.Text == "")
                //    {
                //        btcTurkBtcTextBox.Text = "0";
                //    }

                //    var res = (double.Parse(btcTurkBtcUsdtAskPrice.Text, CultureInfo.InvariantCulture) * double.Parse(btcTurkBtcTextBox.Text, CultureInfo.InvariantCulture)) - (double.Parse(btcTurkBtcUsdtAskPrice.Text, CultureInfo.InvariantCulture) * 0.0005);

                //    //- 
                //    sellBtcOnBtcTurk.Text = res.ToString("0.####");


                //}

                //string buybtconbinance = buyBtcOnBinance.Text;
                //if (!string.IsNullOrEmpty(buyusdtonbinance))
                //{
                //    //binance bid btc ask

                //    if (btcTurkBtcTextBox.Text == "")
                //    {
                //        btcTurkBtcTextBox.Text = "0";
                //    }

                //    var res = (double.Parse(btcTurkBtcTextBox.Text, CultureInfo.InvariantCulture) * (double.Parse(binanceBtcUsdtBidPrice.Text, CultureInfo.InvariantCulture))) - (double.Parse(binanceUsdtBidPrice.Text, CultureInfo.InvariantCulture) * 0.0005);

                //    buyUsdtOnBinance.Text = res.ToString("0.####");


                //}

                //string karzarardurumu2 = karZararDurumu2.Text;
                //if (!string.IsNullOrEmpty(karzarardurumu2))
                //{

                //    var res = double.Parse(sellUsdtOnBtcTurk.Text) - double.Parse(buyUsdtOnBinance.Text) - double.Parse(binanceUsdtAskPrice.Text, CultureInfo.InvariantCulture);

                //    karZararDurumu2.Text = res.ToString("0.####") + " TL";

                //    // (double.Parse(btcTurkBtcUsdtPrice.Text.Split(' ')[3].Split(' ')[0]) * double.Parse(btcTextBox.Text)) - (double.Parse(binanceBtcUsdtPrice.Text.Split(' ')[1].Split(' ')[0]) * double.Parse(btcTextBox.Text));

                //}

                

                

               




            };

            timer.Start();

            

        }

        private void btcTurkUsdtTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            
        }

        private void binanceUsdtTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

       
    }
}
