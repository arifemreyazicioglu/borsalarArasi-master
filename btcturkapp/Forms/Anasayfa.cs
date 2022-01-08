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
                var binanceUsdtAskPrice = "";
                try
                {
                   binanceUsdtAskPrice = await binance.BinanceGetValueAsync("USDTTRY");
                   binanceUsdtAskPrice = binanceUsdtAskPrice.Split(' ')[3].Split(' ')[0];
                   priceDataGridView.Rows[0].Cells[2].Value = binanceUsdtAskPrice;
                }
                catch
                {
                    priceDataGridView.Rows[0].Cells[2].Value = "Server Error";
                }                
               
                //Binance Usdt Alış Fiyatı
                var binanceUsdtBidPrice = "";
                try
                {
                    binanceUsdtBidPrice = await binance.BinanceGetValueAsync("USDTTRY");
                    binanceUsdtBidPrice = binanceUsdtBidPrice.Split(' ')[1].Split(' ')[0];
                    priceDataGridView.Rows[0].Cells[3].Value = binanceUsdtBidPrice;
                }
                catch
                {
                    priceDataGridView.Rows[0].Cells[3].Value = "Server Error";
                }

                //BTCTurk Usdt Satış Fiyatı
                var btcTurkUsdtAskPrice = "";
                try
                {
                    btcTurkUsdtAskPrice = await btcTurk.BTCTurkGetValueAsync("USDTTRY");
                    btcTurkUsdtAskPrice = btcTurkUsdtAskPrice.Split(' ')[3].Split(' ')[0];
                    priceDataGridView.Rows[1].Cells[2].Value = btcTurkUsdtAskPrice;
                }
                catch
                {
                    priceDataGridView.Rows[1].Cells[2].Value = "Server Error";
                }

                //BTCTurk Usdt Alış Fiyatı
                var btcTurkUsdtBidPrice = "";
                try
                {
                    btcTurkUsdtBidPrice = await btcTurk.BTCTurkGetValueAsync("USDTTRY");
                    btcTurkUsdtBidPrice = btcTurkUsdtBidPrice.Split(' ')[1].Split(' ')[0];
                    priceDataGridView.Rows[1].Cells[3].Value = btcTurkUsdtBidPrice;
                }
                catch
                {
                    priceDataGridView.Rows[1].Cells[3].Value = "Server Error";
                }
                //Binance BTC Satış Fiyatı
                var binanceBtcUsdtAskPrice = "";
                try
                {
                    binanceBtcUsdtAskPrice = await binance.BinanceGetValueAsync("BTCUSDT");
                    binanceBtcUsdtAskPrice = binanceBtcUsdtAskPrice.Split(' ')[3].Split(' ')[0];
                    priceDataGridView.Rows[2].Cells[2].Value = binanceBtcUsdtAskPrice;
                }
                catch
                {
                    priceDataGridView.Rows[2].Cells[2].Value = "Server Error";
                }

                //Binance BTC Alış Fiyatı
                var binanceBtcUsdtBidPrice = "";
                try
                {
                    binanceBtcUsdtBidPrice = await binance.BinanceGetValueAsync("BTCUSDT");
                    binanceBtcUsdtBidPrice = binanceBtcUsdtBidPrice.Split(' ')[1].Split(' ')[0];
                    priceDataGridView.Rows[2].Cells[3].Value = binanceBtcUsdtBidPrice;
                }
                catch
                {
                    priceDataGridView.Rows[2].Cells[3].Value = "Server Error";
                }

                //BTCTurk BTC Satış Fiyatı
                var btcTurkBtcUsdtAskPrice = "";
                try
                {
                    btcTurkBtcUsdtAskPrice = await btcTurk.BTCTurkGetValueAsync("BTCUSDT");
                    btcTurkBtcUsdtAskPrice = btcTurkBtcUsdtAskPrice.Split(' ')[3].Split(' ')[0];
                    priceDataGridView.Rows[3].Cells[2].Value = btcTurkBtcUsdtAskPrice;
                }
                catch
                {
                    priceDataGridView.Rows[3].Cells[2].Value = "Server Error";
                }

                //BTCTurk BTC Alış Fiyatı
                var btcTurkBtcUsdtBidPrice = "";
                try
                {
                    btcTurkBtcUsdtBidPrice = await btcTurk.BTCTurkGetValueAsync("BTCUSDT");
                    btcTurkBtcUsdtBidPrice = btcTurkBtcUsdtBidPrice.Split(' ')[1].Split(' ')[0];
                    priceDataGridView.Rows[3].Cells[3].Value = btcTurkBtcUsdtBidPrice;
                }
                catch
                {
                    priceDataGridView.Rows[3].Cells[3].Value = "Server Error";
                }

                //Binance ve BTCTurk arasındaki USDT satış(ask) fiyatı farkı
                double priceDifferenceUsdtAsk = 0;
                try
                {
                    priceDifferenceUsdtAsk = double.Parse(binanceUsdtAskPrice, CultureInfo.InvariantCulture) - double.Parse(btcTurkUsdtAskPrice, CultureInfo.InvariantCulture);

                    if (priceDifferenceUsdtAsk < 0)
                    {
                        priceDifferenceUsdtAsk = Math.Abs(priceDifferenceUsdtAsk);
                        priceDifferenceGridView.Rows[0].Cells[0].Value = "BTCTurk'te +" + priceDifferenceUsdtAsk.ToString("0.####");
                    }
                    else
                    {
                        priceDifferenceGridView.Rows[0].Cells[0].Value = "Binance'de +" + priceDifferenceUsdtAsk.ToString("0.####");
                    }
                }
                catch
                {
                    priceDifferenceGridView.Rows[0].Cells[0].Value = "Server Error";
                }

                //Binance ve BTCTurk arasındaki USDT alış(bid) fiyatı farkı
                double priceDifferenceUsdtBid = 0;
                try
                {
                    priceDifferenceUsdtBid = double.Parse(binanceUsdtBidPrice, CultureInfo.InvariantCulture) - double.Parse(btcTurkUsdtBidPrice, CultureInfo.InvariantCulture);

                    if (priceDifferenceUsdtBid < 0)
                    {
                        priceDifferenceUsdtBid = Math.Abs(priceDifferenceUsdtBid);
                        priceDifferenceGridView.Rows[0].Cells[1].Value = "BTCTurk'te +" + priceDifferenceUsdtBid.ToString("0.####");
                    }
                    else
                    {
                        priceDifferenceGridView.Rows[0].Cells[1].Value = "Binance'de +" + priceDifferenceUsdtBid.ToString("0.####");
                    }
                }
                catch
                {
                    priceDifferenceGridView.Rows[0].Cells[1].Value = "Server Error";
                }

                //Binance ve BTCTurk arasındaki BTCUSDT satış(ask) fiyatı farkı
                double priceDifferenceBtcUsdtAsk = 0;
                try
                {
                    priceDifferenceBtcUsdtAsk = double.Parse(binanceBtcUsdtAskPrice, CultureInfo.InvariantCulture) - double.Parse(btcTurkBtcUsdtAskPrice, CultureInfo.InvariantCulture);

                    if (priceDifferenceBtcUsdtAsk < 0)
                    {
                        priceDifferenceBtcUsdtAsk = Math.Abs(priceDifferenceBtcUsdtAsk);
                        priceDifferenceGridView.Rows[0].Cells[2].Value = "BTCTurk'te +" + priceDifferenceBtcUsdtAsk.ToString("0.####");
                    }
                    else
                    {
                        priceDifferenceGridView.Rows[0].Cells[2].Value = "Binance'de +" + priceDifferenceBtcUsdtAsk.ToString("0.####");
                    }
                }
                catch
                {
                    priceDifferenceGridView.Rows[0].Cells[2].Value = "Server Error";
                }

                //Binance ve BTCTurk arasındaki BTCUSDT alış(bid) fiyatı farkı
                double priceDifferenceBtcUsdtBid = 0;
                try
                {
                    priceDifferenceBtcUsdtBid = double.Parse(binanceBtcUsdtBidPrice, CultureInfo.InvariantCulture) - double.Parse(btcTurkBtcUsdtBidPrice, CultureInfo.InvariantCulture);

                    if (priceDifferenceBtcUsdtBid < 0)
                    {
                        priceDifferenceBtcUsdtBid = Math.Abs(priceDifferenceBtcUsdtBid);
                        priceDifferenceGridView.Rows[0].Cells[3].Value = "BTCTurk'te +" + priceDifferenceBtcUsdtBid.ToString("0.####");
                    }
                    else
                    {
                        priceDifferenceGridView.Rows[0].Cells[3].Value = "Binance'de +" + priceDifferenceBtcUsdtBid.ToString("0.####");
                    }
                }
                catch
                {
                    priceDifferenceGridView.Rows[0].Cells[3].Value = "Server Error";
                }

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
