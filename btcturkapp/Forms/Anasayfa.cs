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

            var timer = new Timer { Interval = 500 };
            timer.Tick += async (o, args) =>
            {
                //BTCTurk BTC Alış(Bid) fiyatı
      
                string btcturkbtcusdtbidprice = btcTurkBtcUsdtBidPrice.Text;
                if (!string.IsNullOrEmpty(btcturkbtcusdtbidprice))
                {

                    var res = await btcTurk.BTCTurkGetValueAsync("BTCUSDT");
                    res = res.Split(' ')[1].Split(' ')[0];
                    btcTurkBtcUsdtBidPrice.Text = res;
                }
                //BTCTurk BTC Satış(Ask) fiyatı
                string btcturkbtcusdtaskprice = btcTurkBtcUsdtAskPrice.Text;
                if (!string.IsNullOrEmpty(btcturkbtcusdtaskprice))
                {
                    var res = await btcTurk.BTCTurkGetValueAsync("BTCUSDT");
                    res = res.Split(' ')[3].Split(' ')[0];
                    btcTurkBtcUsdtAskPrice.Text = res;
                }
                //Binance BTC Alış(Bid) fiyatı
                string binancebtcusdtbidprice = binanceBtcUsdtBidPrice.Text;
                if (!string.IsNullOrEmpty(binancebtcusdtbidprice))
                {
           
                    var res = await binance.BinanceGetValueAsync("BTCUSDT");
                    res = res.Split(' ')[1].Split(' ')[0];
                    binanceBtcUsdtBidPrice.Text = res;
                }
                //Binance BTC Satış(Ask) fiyatı
                string binancebtcusdtaskprice = binanceBtcUsdtAskPrice.Text;
                if (!string.IsNullOrEmpty(binancebtcusdtaskprice))
                {
                    var res = await binance.BinanceGetValueAsync("BTCUSDT");
                    res = res.Split(' ')[3].Split(' ')[0];
                    binanceBtcUsdtAskPrice.Text = res;
                }
                //BTCTurk Usdt Alış(Bid) fiyatı
                string btcturkusdtbidprice = btcTurkUsdtBidPrice.Text;
                if (!string.IsNullOrEmpty(btcturkusdtbidprice))
                {
                  var res = await btcTurk.BTCTurkGetValueAsync("USDTTRY");
                  res = res.Split(' ')[1].Split(' ')[0];
                  btcTurkUsdtBidPrice.Text = res;

                }
                //BTCTurk Usdt Satş(Ask) fiyatı
                string btcturkusdtaskprice = btcTurkUsdtAskPrice.Text;
                if (!string.IsNullOrEmpty(btcturkusdtaskprice))
                {
                  var res = await btcTurk.BTCTurkGetValueAsync("USDTTRY");
                  res = res.Split(' ')[3].Split(' ')[0];
                  btcTurkUsdtAskPrice.Text = res;
                }
                //Binance USDT Alış(Bid) fiyatı
                string binanceusdtbidprice = binanceUsdtBidPrice.Text;
                if (!string.IsNullOrEmpty(binanceusdtbidprice))
                {
                    var res = await binance.BinanceGetValueAsync("USDTTRY");
                    res = res.Split(' ')[1].Split(' ')[0];
                    binanceUsdtBidPrice.Text = res;
                }
                //Binance USDT Satış(Ask) fiyatı
                string binanceusdtaskprice = binanceUsdtAskPrice.Text;
                if (!string.IsNullOrEmpty(binanceusdtaskprice))
                {
                    var res = await binance.BinanceGetValueAsync("USDTTRY");
                    res = res.Split(' ')[3].Split(' ')[0];              
                    binanceUsdtAskPrice.Text = res;
                }

                // Binance ve BTCTurk arasındaki Btc alış(bid) fiyatı farkı
                string farkbinancebtcturkbidbtc = farkBinancetoBtcTurkBidBtc.Text;
                if (!string.IsNullOrEmpty(farkbinancebtcturkbidbtc))
                {
                    var bid = double.Parse(binanceBtcUsdtBidPrice.Text, CultureInfo.InvariantCulture) - double.Parse(btcTurkBtcUsdtBidPrice.Text, CultureInfo.InvariantCulture);
                    if (bid < 0)
                    {
                        bid = Math.Abs(bid);
                        farkBinancetoBtcTurkBidBtc.Text = "BTCTurk'te " + bid.ToString("0.####") + " $ daha pahalı";
                        farkBinancetoBtcTurkBidBtc.ForeColor = Color.Black;
                    }
                    else
                    {
                        farkBinancetoBtcTurkBidBtc.Text = "Binance'de " + bid.ToString("0.####") + " $ daha pahalı";
                        farkBinancetoBtcTurkBidBtc.ForeColor = Color.Black;
                    }

                }

                //Binance ve BTCTurk arasındaki Btc satış(ask) fiyatı farkı
                string farkbinancebtcturkaskbtc = farkBinancetoBtcTurkAskBtc.Text;
                if (!string.IsNullOrEmpty(farkbinancebtcturkaskbtc))
                {

                    var ask = double.Parse(binanceBtcUsdtAskPrice.Text, CultureInfo.InvariantCulture) - double.Parse(btcTurkBtcUsdtAskPrice.Text, CultureInfo.InvariantCulture);

                    if (ask < 0)
                    {
                        ask = Math.Abs(ask);
                        farkBinancetoBtcTurkAskBtc.Text = "BTCTurk'te " + ask.ToString("0.####") + " $ daha pahalı";
                        farkBinancetoBtcTurkAskBtc.ForeColor = Color.Black;
                    }
                    else
                    {
                        farkBinancetoBtcTurkAskBtc.Text = "Binance'de " + ask.ToString("0.####") + " $ daha pahalı";
                        farkBinancetoBtcTurkAskBtc.ForeColor = Color.Black;
                    }

                }

                // Binance ve BTCTurk arasındaki Usdt alış(bid) fiyatı farkı
                string farkbinancebtcturkbidusdt = farkBinancetoBtcTurkBidUsdt.Text;
                if (!string.IsNullOrEmpty(farkbinancebtcturkbidusdt))
                {
                    var bid = double.Parse(binanceUsdtBidPrice.Text, CultureInfo.InvariantCulture) - double.Parse(btcTurkUsdtBidPrice.Text, CultureInfo.InvariantCulture);
                    if (bid < 0)
                    {
                        bid = Math.Abs(bid);
                        farkBinancetoBtcTurkBidUsdt.Text = "BTCTurk'te " + bid.ToString("0.####") + " TL daha pahalı";
                        farkBinancetoBtcTurkBidUsdt.ForeColor = Color.Black;
                    }
                    else
                    {
                        farkBinancetoBtcTurkBidUsdt.Text = "Binance'de " + bid.ToString("0.####") + " TL daha pahalı";
                        farkBinancetoBtcTurkBidUsdt.ForeColor = Color.Black;
                    }

                }

                // Binance ve BTCTurk arasındaki Usdt satış(ask) fiyatı farkı
                string farkbinancebtcturkaskusdt = farkBinancetoBtcTurkAskUsdt.Text;
                if (!string.IsNullOrEmpty(farkbinancebtcturkaskusdt))
                {
                    var bid = double.Parse(binanceUsdtAskPrice.Text, CultureInfo.InvariantCulture) - double.Parse(btcTurkUsdtAskPrice.Text, CultureInfo.InvariantCulture);
                    if (bid < 0)
                    {
                        bid = Math.Abs(bid);
                        farkBinancetoBtcTurkAskUsdt.Text = "BTCTurk'te " + bid.ToString("0.####") + " TL daha pahalı";
                        farkBinancetoBtcTurkAskUsdt.ForeColor = Color.Black;
                    }
                    else
                    {
                        farkBinancetoBtcTurkAskUsdt.Text = "Binance'de " + bid.ToString("0.####") + " TL daha pahalı";
                        farkBinancetoBtcTurkAskUsdt.ForeColor = Color.Black;
                    }

                }

                //Binance satip BTCTurk de alma Usdt
                string sellusdtonbinance = sellUsdtOnBinance.Text;
                if (!string.IsNullOrEmpty(sellusdtonbinance))
                {
                    //binance ask btc bid

                    if (binanceUsdtTextBox.Text == "")
                    {
                        binanceUsdtTextBox.Text = "0";
                    }

                    var res = (double.Parse(binanceUsdtAskPrice.Text, CultureInfo.InvariantCulture) * double.Parse(binanceUsdtTextBox.Text, CultureInfo.InvariantCulture)) - ((double.Parse(binanceUsdtTextBox.Text, CultureInfo.InvariantCulture) * double.Parse("0.0001", CultureInfo.InvariantCulture)) * double.Parse(binanceUsdtAskPrice.Text, CultureInfo.InvariantCulture)) ;

                    sellUsdtOnBinance.Text = res.ToString("0.####");

                }

                string buyusdtonbtcturk = buyUsdtOnBtcTurk.Text;
                if (!string.IsNullOrEmpty(buyusdtonbtcturk))
                {
                    //binance bid btc ask

                    if (binanceUsdtTextBox.Text == "")
                    {
                        binanceUsdtTextBox.Text = "0";
                    }

                    var res = (double.Parse(btcTurkUsdtBidPrice.Text, CultureInfo.InvariantCulture) * double.Parse(binanceUsdtTextBox.Text, CultureInfo.InvariantCulture)) + ((double.Parse(binanceUsdtTextBox.Text, CultureInfo.InvariantCulture) * double.Parse("0.0001", CultureInfo.InvariantCulture)) * double.Parse(btcTurkUsdtAskPrice.Text, CultureInfo.InvariantCulture));

                    buyUsdtOnBtcTurk.Text = res.ToString("0.####");

                }

                string karzarardurumu = karZararDurumu.Text;
                if (!string.IsNullOrEmpty(karzarardurumu))
                {

                    var res = double.Parse(sellUsdtOnBinance.Text) - double.Parse(buyUsdtOnBtcTurk.Text) - (double.Parse(btcTurkUsdtAskPrice.Text, CultureInfo.InvariantCulture) / 4);
                    karZararDurumu.Text = res.ToString("0.####") + " TL";

                }

                //BTCTurk satip Binance de alma Usdt

                string sellusdtonbtcturk = sellUsdtOnBtcTurk.Text;
                if (!string.IsNullOrEmpty(sellusdtonbtcturk))
                {
                    //binance ask btc bid

                    if (btcTurkUsdtTextBox.Text == "")
                    {
                        btcTurkUsdtTextBox.Text = "0";
                    }

                    var res = (double.Parse(btcTurkUsdtAskPrice.Text, CultureInfo.InvariantCulture) * double.Parse(btcTurkUsdtTextBox.Text, CultureInfo.InvariantCulture)) -((double.Parse(btcTurkUsdtTextBox.Text, CultureInfo.InvariantCulture) * double.Parse("0.0001", CultureInfo.InvariantCulture)) * double.Parse(btcTurkUsdtAskPrice.Text, CultureInfo.InvariantCulture));

                    //- 
                    sellUsdtOnBtcTurk.Text = res.ToString("0.####");


                }

                string buyusdtonbinance = buyUsdtOnBinance.Text;
                if (!string.IsNullOrEmpty(buyusdtonbinance))
                {
                    //binance bid btc ask

                    if (btcTurkUsdtTextBox.Text == "")
                    {
                        btcTurkUsdtTextBox.Text = "0";
                    }

                    var res = (double.Parse(btcTurkUsdtTextBox.Text, CultureInfo.InvariantCulture) * double.Parse(binanceUsdtBidPrice.Text, CultureInfo.InvariantCulture)) + ((double.Parse(btcTurkUsdtTextBox.Text, CultureInfo.InvariantCulture) * double.Parse("0.0001", CultureInfo.InvariantCulture)) * double.Parse(binanceUsdtAskPrice.Text, CultureInfo.InvariantCulture));

                    buyUsdtOnBinance.Text = res.ToString("0.####");


                }

                string karzarardurumu2 = karZararDurumu2.Text;
                if (!string.IsNullOrEmpty(karzarardurumu2))
                {

                    var res = double.Parse(sellUsdtOnBtcTurk.Text) - double.Parse(buyUsdtOnBinance.Text) - double.Parse(binanceUsdtAskPrice.Text, CultureInfo.InvariantCulture);

                    karZararDurumu2.Text = res.ToString("0.####") + " TL";

                    // (double.Parse(btcTurkBtcUsdtPrice.Text.Split(' ')[3].Split(' ')[0]) * double.Parse(btcTextBox.Text)) - (double.Parse(binanceBtcUsdtPrice.Text.Split(' ')[1].Split(' ')[0]) * double.Parse(btcTextBox.Text));

                }

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
