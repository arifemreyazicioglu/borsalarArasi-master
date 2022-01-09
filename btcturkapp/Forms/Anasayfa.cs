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

            //int h = Screen.PrimaryScreen.WorkingArea.Height - 40;
            //int w = Screen.PrimaryScreen.WorkingArea.Width;
            //this.ClientSize = new Size(w, h);

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

            abritajGridView.Rows.Add();
            abritajGridView.Rows.Add();
            abritajGridView.Rows.Add();

            abritajGridView.Rows[0].Cells[0].Value = "USDTTRY";
            abritajGridView.Rows[0].Cells[1].Value = "Binance -> BTCTurk";
            abritajGridView.Rows[0].Cells[2].Value = 100000;
            abritajGridView.Rows[1].Cells[0].Value = "USDTTRY";
            abritajGridView.Rows[1].Cells[1].Value = "BTCTurk -> Binance";
            abritajGridView.Rows[1].Cells[2].Value = 100000;
            abritajGridView.Rows[2].Cells[0].Value = "BTCUSDT";
            abritajGridView.Rows[2].Cells[1].Value = "Binance -> BTCTurk";
            abritajGridView.Rows[2].Cells[2].Value = 10;
            abritajGridView.Rows[3].Cells[0].Value = "BTCUSDT";
            abritajGridView.Rows[3].Cells[1].Value = "BTCTurk -> Binance";
            abritajGridView.Rows[3].Cells[2].Value = 10;


            var timer = new Timer { Interval = 500 };
            timer.Tick += async (o, args) =>
            {
                //
                //
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
                //               
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
                //
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
                //
                //
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
                //
                //
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
                //
                //
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
                //
                //
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
                //
                //
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
                //
                //
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
                //
                //
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
                //
                //
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
                //
                //
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
                //
                //
                //BİNANCE SATİP BTCTURK DE ALMA USDT
                var usdtMiktarBinance = abritajGridView.Rows[0].Cells[2].Value;

                var kazancSellUsdtBinance = (double.Parse(binanceUsdtAskPrice, CultureInfo.InvariantCulture) * double.Parse(usdtMiktarBinance.ToString())) - ((double.Parse(usdtMiktarBinance.ToString()) * double.Parse("0.0002", CultureInfo.InvariantCulture)) * double.Parse(binanceUsdtAskPrice, CultureInfo.InvariantCulture));
                abritajGridView.Rows[0].Cells[3].Value = kazancSellUsdtBinance.ToString("0.####");

                var maliyetBuyUsdtBtcTurk = (double.Parse(btcTurkUsdtBidPrice, CultureInfo.InvariantCulture) * double.Parse(usdtMiktarBinance.ToString())) + ((double.Parse(usdtMiktarBinance.ToString()) * double.Parse("0.0001", CultureInfo.InvariantCulture)) * double.Parse(btcTurkUsdtAskPrice, CultureInfo.InvariantCulture));
                abritajGridView.Rows[0].Cells[4].Value = maliyetBuyUsdtBtcTurk.ToString("0.####");

                var karZararBinanceToBtcTurkUsdt = double.Parse((string)abritajGridView.Rows[0].Cells[3].Value) - double.Parse((string)abritajGridView.Rows[0].Cells[4].Value) - (double.Parse(btcTurkUsdtAskPrice, CultureInfo.InvariantCulture) / 4);
                abritajGridView.Rows[0].Cells[5].Value = karZararBinanceToBtcTurkUsdt.ToString("0.####") + " TL";
                //
                //
                //BTCTURK SATİP BİNANCE DE ALMA USDT
                var usdtMiktarBtcTurk = abritajGridView.Rows[1].Cells[2].Value;

                var kazancSellUsdtBtcTurk = (double.Parse(btcTurkUsdtAskPrice, CultureInfo.InvariantCulture) * double.Parse(usdtMiktarBtcTurk.ToString())) - ((double.Parse(usdtMiktarBtcTurk.ToString()) * double.Parse("0.0001", CultureInfo.InvariantCulture)) * double.Parse(btcTurkUsdtAskPrice, CultureInfo.InvariantCulture));
                abritajGridView.Rows[1].Cells[3].Value = kazancSellUsdtBtcTurk.ToString("0.####");

                var maliyetBuyUsdtBinance = (double.Parse(binanceUsdtBidPrice, CultureInfo.InvariantCulture) * double.Parse(usdtMiktarBtcTurk.ToString())) + ((double.Parse(usdtMiktarBtcTurk.ToString()) * double.Parse("0.0002", CultureInfo.InvariantCulture)) * double.Parse(binanceUsdtAskPrice, CultureInfo.InvariantCulture));
                abritajGridView.Rows[1].Cells[4].Value = maliyetBuyUsdtBinance.ToString("0.####");

                var karZararBtcTurkToBinanceUsdt = double.Parse((string)abritajGridView.Rows[1].Cells[3].Value) - double.Parse((string)abritajGridView.Rows[1].Cells[4].Value) - (double.Parse(binanceUsdtAskPrice, CultureInfo.InvariantCulture));
                abritajGridView.Rows[1].Cells[5].Value = karZararBtcTurkToBinanceUsdt.ToString("0.####") + " TL";               
                //
                //
                //BINANCE SATİP BTCTURK DE ALMA BTC
                var btcMiktarBinance = abritajGridView.Rows[2].Cells[2].Value;

                var kazancSellBtcBinance = (double.Parse(btcTurkBtcUsdtAskPrice, CultureInfo.InvariantCulture) * double.Parse(btcMiktarBinance.ToString())) - ((double.Parse(btcMiktarBinance.ToString()) * double.Parse("0.001", CultureInfo.InvariantCulture)) * double.Parse(btcTurkBtcUsdtAskPrice, CultureInfo.InvariantCulture));
                abritajGridView.Rows[2].Cells[3].Value = kazancSellBtcBinance.ToString("0.####");

                var maliyetBuyBtcBtcTurk = (double.Parse(binanceBtcUsdtBidPrice, CultureInfo.InvariantCulture) * double.Parse(btcMiktarBinance.ToString())) - ((double.Parse(btcMiktarBinance.ToString()) * double.Parse("0.0001", CultureInfo.InvariantCulture)) * double.Parse(binanceBtcUsdtAskPrice, CultureInfo.InvariantCulture));
                abritajGridView.Rows[2].Cells[4].Value = maliyetBuyBtcBtcTurk.ToString("0.####");

                var karZararBinanceToBtcTurkBtc = double.Parse((string)abritajGridView.Rows[2].Cells[3].Value) - double.Parse((string)abritajGridView.Rows[2].Cells[4].Value);
                abritajGridView.Rows[2].Cells[5].Value = karZararBinanceToBtcTurkBtc.ToString("0.####") + " TL";
                //
                //
                //BTCTURK SATIP BINANCE DE ALMA BTC
                var btcMiktarBtcTurk = abritajGridView.Rows[3].Cells[2].Value;   
                
                var kazancSellBtcBtcTurk = (double.Parse(btcTurkBtcUsdtAskPrice, CultureInfo.InvariantCulture) * double.Parse(btcMiktarBtcTurk.ToString())) - ((double.Parse(btcMiktarBtcTurk.ToString()) * double.Parse("0.0001", CultureInfo.InvariantCulture)) * double.Parse(btcTurkBtcUsdtAskPrice, CultureInfo.InvariantCulture));
                abritajGridView.Rows[3].Cells[3].Value = kazancSellBtcBtcTurk.ToString("0.####");

                var maliyetBuyBtcBinance = (double.Parse(binanceBtcUsdtBidPrice, CultureInfo.InvariantCulture) * double.Parse(btcMiktarBtcTurk.ToString())) - ((double.Parse(btcMiktarBtcTurk.ToString()) * double.Parse("0.001", CultureInfo.InvariantCulture)) * double.Parse(binanceBtcUsdtAskPrice, CultureInfo.InvariantCulture));
                abritajGridView.Rows[3].Cells[4].Value = maliyetBuyBtcBinance.ToString("0.####");

                var karZararBtcTurkToBinanceBtc = double.Parse((string)abritajGridView.Rows[3].Cells[3].Value) - double.Parse((string)abritajGridView.Rows[3].Cells[4].Value) - (double.Parse(binanceBtcUsdtAskPrice, CultureInfo.InvariantCulture) * 0.0005);
                abritajGridView.Rows[3].Cells[5].Value = karZararBtcTurkToBinanceBtc.ToString("0.####") + " TL";

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
