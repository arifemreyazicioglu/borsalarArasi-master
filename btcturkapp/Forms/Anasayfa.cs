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
using System.Text.RegularExpressions;
using btcturkapp.Forms;
using APIClient.Models;


namespace btcturkapp
{ 
    public partial class Anasayfa : Form
    {
        private string pattern = "^[0-9]{0,9}$";

        public Anasayfa()
        {
            
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {

            btcTurkFunction btcTurk = new btcTurkFunction();
            binanceFunctions binance = new binanceFunctions();

            priceDataGridView.ColumnCount = 4;
            abritajGridView.ColumnCount = 6;
            priceDifferenceGridView.ColumnCount = 4;
            sellBuyBuySellDifferenceGridView.ColumnCount = 4;

            priceDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            priceDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            priceDifferenceGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            priceDifferenceGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            sellBuyBuySellDifferenceGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            sellBuyBuySellDifferenceGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            abritajGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            abritajGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            abritajGridView.RowTemplate.Height = 25;
            priceDataGridView.RowTemplate.Height = 25;
            priceDifferenceGridView.RowTemplate.Height = 25;
            sellBuyBuySellDifferenceGridView.RowTemplate.Height = 25;


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
            abritajGridView.Rows[0].Cells[2].Value = 100000.0;  
            abritajGridView.Rows[1].Cells[0].Value = "USDTTRY";
            abritajGridView.Rows[1].Cells[1].Value = "BTCTurk -> Binance";
            abritajGridView.Rows[1].Cells[2].Value = 100000.0;

            abritajGridView.Rows[2].Cells[0].Value = "BTCUSDT";
            abritajGridView.Rows[2].Cells[1].Value = "Binance -> BTCTurk";
            abritajGridView.Rows[2].Cells[2].Value = 10.0;

            abritajGridView.Rows[3].Cells[0].Value = "BTCUSDT";
            abritajGridView.Rows[3].Cells[1].Value = "BTCTurk -> Binance";
            abritajGridView.Rows[3].Cells[2].Value = 10.0;


           
            var timer = new Timer { Interval = 1000 };
            timer.Tick += async (o, args) =>
            {
                //
                //
                //Binance ve BTCTurk Asset Miktarları
                binanceUsdtMiktarıLabel.Text = "Binance Usdt Miktarı : " + await binance.BinanceGetBalanceAsync("USDT");
                btcTurkUsdtMiktarıLabel.Text = "BTCTurk Usdt Miktarı : " + await btcTurk.BTCTurkGetAccountBalance("USDT");
                binanceBtcMiktarıLabel.Text = "Binance BTC Miktarı : " + await binance.BinanceGetBalanceAsync("BTC");
                btcTurkBtcMiktarıLabel.Text = "BTCTurk BTC Miktarı : " + await btcTurk.BTCTurkGetAccountBalance("BTC");
                //
                //
                //Binance Usdt Satış Fiyatı
                var binanceUsdtAskPrice = "Waiting";
                if (!string.IsNullOrEmpty(binanceUsdtAskPrice))
                {
                    try
                    {
                        binanceUsdtAskPrice = await binance.BinanceGetValueAsync("USDTTRY");
                        binanceUsdtAskPrice = binanceUsdtAskPrice.Split(' ')[3].Split(' ')[0];
                        priceDataGridView.Rows[0].Cells[2].Value = binanceUsdtAskPrice + " ₺";
                    }
                    catch
                    {
                        priceDataGridView.Rows[0].Cells[2].Value = "Server Error";
                    }
                }                
                //               
                //Binance Usdt Alış Fiyatı
                var binanceUsdtBidPrice = "Waiting";
                if (!string.IsNullOrEmpty(binanceUsdtBidPrice))
                {
                    try
                    {
                        binanceUsdtBidPrice = await binance.BinanceGetValueAsync("USDTTRY");
                        binanceUsdtBidPrice = binanceUsdtBidPrice.Split(' ')[1].Split(' ')[0];
                        priceDataGridView.Rows[0].Cells[3].Value = binanceUsdtBidPrice + " ₺";
                    }
                    catch
                    {
                        priceDataGridView.Rows[0].Cells[3].Value = "Server Error";
                    }
                }
                //
                //
                //BTCTurk Usdt Satış Fiyatı
                var btcTurkUsdtAskPrice = "Waiting";
                if (!string.IsNullOrEmpty(btcTurkUsdtAskPrice))
                {
                    try
                    {
                        btcTurkUsdtAskPrice = await btcTurk.BTCTurkGetValueAsync("USDTTRY");
                        btcTurkUsdtAskPrice = btcTurkUsdtAskPrice.Split(' ')[3].Split(' ')[0];
                        priceDataGridView.Rows[1].Cells[2].Value = btcTurkUsdtAskPrice + " ₺";
                    }
                    catch
                    {
                        priceDataGridView.Rows[1].Cells[2].Value = "Server Error";
                    }
                }              
                //
                //
                //BTCTurk Usdt Alış Fiyatı
                var btcTurkUsdtBidPrice = "Waiting";
                if (!string.IsNullOrEmpty(btcTurkUsdtBidPrice))
                {
                    try
                    {
                        btcTurkUsdtBidPrice = await btcTurk.BTCTurkGetValueAsync("USDTTRY");
                        btcTurkUsdtBidPrice = btcTurkUsdtBidPrice.Split(' ')[1].Split(' ')[0];
                        priceDataGridView.Rows[1].Cells[3].Value = btcTurkUsdtBidPrice + " ₺";
                    }
                    catch
                    {
                        priceDataGridView.Rows[1].Cells[3].Value = "Server Error";
                    }
                }              
                //
                //
                //Binance BTC Satış Fiyatı
                var binanceBtcUsdtAskPrice = "Waiting";
                if (!string.IsNullOrEmpty(binanceBtcUsdtAskPrice))
                {
                    try
                    {
                        binanceBtcUsdtAskPrice = await binance.BinanceGetValueAsync("BTCUSDT");
                        binanceBtcUsdtAskPrice = binanceBtcUsdtAskPrice.Split(' ')[3].Split(' ')[0];
                        priceDataGridView.Rows[2].Cells[2].Value = binanceBtcUsdtAskPrice + " $";
                    }
                    catch
                    {
                        priceDataGridView.Rows[2].Cells[2].Value = "Server Error";
                    }
                }              
                //
                //
                //Binance BTC Alış Fiyatı
                var binanceBtcUsdtBidPrice = "Waiting";
                if (!string.IsNullOrEmpty(binanceBtcUsdtBidPrice))
                {
                    try
                    {
                        binanceBtcUsdtBidPrice = await binance.BinanceGetValueAsync("BTCUSDT");
                        binanceBtcUsdtBidPrice = binanceBtcUsdtBidPrice.Split(' ')[1].Split(' ')[0];
                        priceDataGridView.Rows[2].Cells[3].Value = binanceBtcUsdtBidPrice + " $";
                    }
                    catch
                    {
                        priceDataGridView.Rows[2].Cells[3].Value = "Server Error";
                    }
                }                 
                //
                //
                //BTCTurk BTC Satış Fiyatı
                var btcTurkBtcUsdtAskPrice = "Waiting";
                if (!string.IsNullOrEmpty(btcTurkBtcUsdtAskPrice))
                {
                    try
                    {
                        btcTurkBtcUsdtAskPrice = await btcTurk.BTCTurkGetValueAsync("BTCUSDT");
                        btcTurkBtcUsdtAskPrice = btcTurkBtcUsdtAskPrice.Split(' ')[3].Split(' ')[0];
                        priceDataGridView.Rows[3].Cells[2].Value = btcTurkBtcUsdtAskPrice + " $";
                    }
                    catch
                    {
                        priceDataGridView.Rows[3].Cells[2].Value = "Server Error";
                    }
                }                
                //
                //
                //BTCTurk BTC Alış Fiyatı
                var btcTurkBtcUsdtBidPrice = "Waiting";
                if (!string.IsNullOrEmpty(btcTurkBtcUsdtBidPrice))
                {
                    try
                    {
                        btcTurkBtcUsdtBidPrice = await btcTurk.BTCTurkGetValueAsync("BTCUSDT");
                        btcTurkBtcUsdtBidPrice = btcTurkBtcUsdtBidPrice.Split(' ')[1].Split(' ')[0];
                        priceDataGridView.Rows[3].Cells[3].Value = btcTurkBtcUsdtBidPrice + " $";
                    }
                    catch
                    {
                        priceDataGridView.Rows[3].Cells[3].Value = "Server Error";
                    }
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
                        priceDifferenceGridView.Rows[0].Cells[0].Value = "BTCTurk'te +" + priceDifferenceUsdtAsk.ToString("0.####") + " ₺";
                    }
                    else
                    {
                        priceDifferenceGridView.Rows[0].Cells[0].Value = "Binance'de +" + priceDifferenceUsdtAsk.ToString("0.####") + " ₺";
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
                        priceDifferenceGridView.Rows[0].Cells[1].Value = "BTCTurk'te +" + priceDifferenceUsdtBid.ToString("0.####") + " ₺";
                    }
                    else
                    {
                        priceDifferenceGridView.Rows[0].Cells[1].Value = "Binance'de +" + priceDifferenceUsdtBid.ToString("0.####") + " ₺";
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
                        priceDifferenceGridView.Rows[0].Cells[2].Value = "BTCTurk'te +" + priceDifferenceBtcUsdtAsk.ToString("0.####") + " $";
                    }
                    else
                    {
                        priceDifferenceGridView.Rows[0].Cells[2].Value = "Binance'de +" + priceDifferenceBtcUsdtAsk.ToString("0.####") + " $";
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
                        priceDifferenceGridView.Rows[0].Cells[3].Value = "BTCTurk'te +" + priceDifferenceBtcUsdtBid.ToString("0.####") + " $";
                    }
                    else
                    {
                        priceDifferenceGridView.Rows[0].Cells[3].Value = "Binance'de +" + priceDifferenceBtcUsdtBid.ToString("0.####") + " $";
                    }
                }
                catch
                {
                    priceDifferenceGridView.Rows[0].Cells[3].Value = "Server Error";
                }
                //
                //
                // USDT Binance alis btcturk satis arasindaki fark
                double usdtPriceDifferenceBinanceBidBtcTurkAsk = 0;
                try
                {
                    usdtPriceDifferenceBinanceBidBtcTurkAsk = double.Parse(binanceUsdtBidPrice, CultureInfo.InvariantCulture) - double.Parse(btcTurkUsdtAskPrice, CultureInfo.InvariantCulture);
                    sellBuyBuySellDifferenceGridView.Rows[0].Cells[0].Value = usdtPriceDifferenceBinanceBidBtcTurkAsk.ToString("0.####") + " ₺";
                }
                catch
                {
                    sellBuyBuySellDifferenceGridView.Rows[0].Cells[0].Value = "Server Error";
                }
                //
                //
                // USDT Binance satis btcturk alis arasindaki fark
                double usdtPriceDifferenceBinanceAskBtcTurkABid= 0;
                try
                {
                    usdtPriceDifferenceBinanceAskBtcTurkABid = double.Parse(binanceUsdtAskPrice, CultureInfo.InvariantCulture) - double.Parse(btcTurkUsdtBidPrice, CultureInfo.InvariantCulture);
                    sellBuyBuySellDifferenceGridView.Rows[0].Cells[1].Value = usdtPriceDifferenceBinanceAskBtcTurkABid.ToString("0.####") + " ₺";
                }
                catch
                {
                    sellBuyBuySellDifferenceGridView.Rows[0].Cells[1].Value = "Server Error";
                }
                //
                //
                // BTC Binance alis btcturk satis arasindaki fark
                double btcPriceDifferenceBinanceBidBtcTurkAsk = 0;
                try
                {
                    btcPriceDifferenceBinanceBidBtcTurkAsk = double.Parse(binanceBtcUsdtBidPrice, CultureInfo.InvariantCulture) - double.Parse(btcTurkBtcUsdtAskPrice, CultureInfo.InvariantCulture);
                    sellBuyBuySellDifferenceGridView.Rows[0].Cells[2].Value = btcPriceDifferenceBinanceBidBtcTurkAsk.ToString("0.####") + " $";
                }
                catch
                {
                    sellBuyBuySellDifferenceGridView.Rows[0].Cells[2].Value = "Server Error";
                }
                //
                //
                // BTC Binance satis btcturk alis arasindaki fark
                double btcPriceDifferenceBinanceAskBtcTurkABid = 0;
                try
                {
                    btcPriceDifferenceBinanceAskBtcTurkABid = double.Parse(binanceBtcUsdtAskPrice, CultureInfo.InvariantCulture) - double.Parse(btcTurkBtcUsdtBidPrice, CultureInfo.InvariantCulture);
                    sellBuyBuySellDifferenceGridView.Rows[0].Cells[3].Value = btcPriceDifferenceBinanceAskBtcTurkABid.ToString("0.####") + " $";
                }
                catch
                {
                    sellBuyBuySellDifferenceGridView.Rows[0].Cells[3].Value = "Server Error";
                }
                //
                //
                //BİNANCE SATİP BTCTURK DE ALMA USDT
                var usdtMiktarBinance = abritajGridView.Rows[0].Cells[2].Value;
                try
                {
                    var kazancSellUsdtBinance = (double.Parse(binanceUsdtAskPrice, CultureInfo.InvariantCulture) * double.Parse(usdtMiktarBinance.ToString(), CultureInfo.InvariantCulture)) - ((double.Parse(usdtMiktarBinance.ToString(), CultureInfo.InvariantCulture) * double.Parse("0.0002", CultureInfo.InvariantCulture)) * double.Parse(binanceUsdtBidPrice, CultureInfo.InvariantCulture));
                    abritajGridView.Rows[0].Cells[3].Value = kazancSellUsdtBinance.ToString("0.####");

                    var maliyetBuyUsdtBtcTurk = (double.Parse(btcTurkUsdtBidPrice, CultureInfo.InvariantCulture) * double.Parse(usdtMiktarBinance.ToString(), CultureInfo.InvariantCulture)) + ((double.Parse(usdtMiktarBinance.ToString(), CultureInfo.InvariantCulture) * double.Parse("0.0001", CultureInfo.InvariantCulture)) * double.Parse(btcTurkUsdtBidPrice, CultureInfo.InvariantCulture));
                    abritajGridView.Rows[0].Cells[4].Value = maliyetBuyUsdtBtcTurk.ToString("0.####");

                    var karZararBinanceToBtcTurkUsdt = double.Parse((string)abritajGridView.Rows[0].Cells[3].Value) - double.Parse((string)abritajGridView.Rows[0].Cells[4].Value) - (double.Parse(btcTurkUsdtAskPrice, CultureInfo.InvariantCulture) / 4);
                    abritajGridView.Rows[0].Cells[5].Value = karZararBinanceToBtcTurkUsdt.ToString("0.####") + " TL";
                }
                catch
                {
                    abritajGridView.Rows[0].Cells[5].Value = "Server Error";
                }               
                //
                //
                //BTCTURK SATİP BİNANCE DE ALMA USDT
                var usdtMiktarBtcTurk = abritajGridView.Rows[1].Cells[2].Value;
                try
                {
                    var kazancSellUsdtBtcTurk = (double.Parse(btcTurkUsdtAskPrice, CultureInfo.InvariantCulture) * double.Parse(usdtMiktarBtcTurk.ToString(), CultureInfo.InvariantCulture)) - ((double.Parse(usdtMiktarBtcTurk.ToString()) * double.Parse("0.0001", CultureInfo.InvariantCulture)) * double.Parse(btcTurkUsdtBidPrice, CultureInfo.InvariantCulture));
                    abritajGridView.Rows[1].Cells[3].Value = kazancSellUsdtBtcTurk.ToString("0.####");

                    var maliyetBuyUsdtBinance = (double.Parse(binanceUsdtBidPrice, CultureInfo.InvariantCulture) * double.Parse(usdtMiktarBtcTurk.ToString(), CultureInfo.InvariantCulture)) + ((double.Parse(usdtMiktarBtcTurk.ToString()) * double.Parse("0.0002", CultureInfo.InvariantCulture)) * double.Parse(binanceUsdtBidPrice, CultureInfo.InvariantCulture));
                    abritajGridView.Rows[1].Cells[4].Value = maliyetBuyUsdtBinance.ToString("0.####");

                    var karZararBtcTurkToBinanceUsdt = double.Parse((string)abritajGridView.Rows[1].Cells[3].Value) - double.Parse((string)abritajGridView.Rows[1].Cells[4].Value) - (double.Parse(binanceUsdtAskPrice, CultureInfo.InvariantCulture));
                    abritajGridView.Rows[1].Cells[5].Value = karZararBtcTurkToBinanceUsdt.ToString("0.####") + " TL";
                }
                catch
                {
                    abritajGridView.Rows[1].Cells[5].Value = "Server Error";
                }                
                //
                //
                //BINANCE SATİP BTCTURK DE ALMA BTC
                var btcMiktarBinance = abritajGridView.Rows[2].Cells[2].Value;
                try
                {
                    var kazancSellBtcBinance = (double.Parse(binanceBtcUsdtAskPrice, CultureInfo.InvariantCulture) * double.Parse(btcMiktarBinance.ToString(), CultureInfo.InvariantCulture)) - ((double.Parse(btcMiktarBinance.ToString()) * double.Parse("0.001", CultureInfo.InvariantCulture)) * double.Parse(binanceBtcUsdtBidPrice, CultureInfo.InvariantCulture));
                    abritajGridView.Rows[2].Cells[3].Value = kazancSellBtcBinance.ToString("0.###");

                    var maliyetBuyBtcBtcTurk = (double.Parse(btcTurkBtcUsdtBidPrice, CultureInfo.InvariantCulture) * double.Parse(btcMiktarBinance.ToString(), CultureInfo.InvariantCulture)) + ((double.Parse(btcMiktarBinance.ToString()) * double.Parse("0.0001", CultureInfo.InvariantCulture)) * double.Parse(btcTurkBtcUsdtBidPrice, CultureInfo.InvariantCulture));
                    abritajGridView.Rows[2].Cells[4].Value = maliyetBuyBtcBtcTurk.ToString("0.####");

                    var karZararBinanceToBtcTurkBtc = double.Parse((string)abritajGridView.Rows[2].Cells[3].Value) - double.Parse((string)abritajGridView.Rows[2].Cells[4].Value);
                    abritajGridView.Rows[2].Cells[5].Value = karZararBinanceToBtcTurkBtc.ToString("0.####") + " $";
                }
                catch
                {
                    abritajGridView.Rows[2].Cells[5].Value = "Server Error";
                }                
                //
                //
                //BTCTURK SATIP BINANCE DE ALMA BTC
                var btcMiktarBtcTurk = abritajGridView.Rows[3].Cells[2].Value;
                try
                {
                    var kazancSellBtcBtcTurk = (double.Parse(btcTurkBtcUsdtAskPrice, CultureInfo.InvariantCulture) * double.Parse(btcMiktarBtcTurk.ToString(), CultureInfo.InvariantCulture)) - ((double.Parse(btcMiktarBtcTurk.ToString()) * double.Parse("0.0001", CultureInfo.InvariantCulture)) * double.Parse(btcTurkBtcUsdtBidPrice, CultureInfo.InvariantCulture));
                    abritajGridView.Rows[3].Cells[3].Value = kazancSellBtcBtcTurk.ToString("0.####");

                    var maliyetBuyBtcBinance = (double.Parse(binanceBtcUsdtBidPrice, CultureInfo.InvariantCulture) * double.Parse(btcMiktarBtcTurk.ToString(), CultureInfo.InvariantCulture)) + ((double.Parse(btcMiktarBtcTurk.ToString()) * double.Parse("0.001", CultureInfo.InvariantCulture)) * double.Parse(binanceBtcUsdtBidPrice, CultureInfo.InvariantCulture));
                    abritajGridView.Rows[3].Cells[4].Value = maliyetBuyBtcBinance.ToString("0.###");

                    var karZararBtcTurkToBinanceBtc = double.Parse((string)abritajGridView.Rows[3].Cells[3].Value) - double.Parse((string)abritajGridView.Rows[3].Cells[4].Value) - (double.Parse(binanceBtcUsdtAskPrice, CultureInfo.InvariantCulture) * 0.0005);
                    abritajGridView.Rows[3].Cells[5].Value = karZararBtcTurkToBinanceBtc.ToString("0.####") + " $";
                }
                catch
                {
                    abritajGridView.Rows[3].Cells[5].Value = "Server Error";
                }
                
            };
            timer.Start();
        }
        private void abritajGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            abritajGridView.EditingControl.KeyPress -= EditingControl_KeyPress;
            abritajGridView.EditingControl.KeyPress += EditingControl_KeyPress;
        }
        private void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)&& !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
       

        private async void button1_Click(object sender, EventArgs e)
        {
            btcTurkFunction btcTurk = new btcTurkFunction();
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];
            var apiClientV1 = new ApiClientV1(publicKey, privateKey, resourceUrl);

            var methodType = OrderMethod.Limit;
            var orderType = OrderType.Buy;

            var limitSellOrder = new OrderInput
            {
                Quantity = 136.90m,
                Price = 13.872m,
                OrderMethod = methodType,
                OrderType = orderType,
                PairSymbol = "USDT_TRY",
            };

            ////Create New Order
            var orderOutput = await apiClientV1.CreateOrder(limitSellOrder);

            string message = "";
            string title = "İşlem Durumu";

            if (!orderOutput.Success)
            {
                message = $"Code:{orderOutput.Code} , Message: {orderOutput.Message}";               
            }
            else
            {
                message = orderOutput.Data.ToString();
                listBox1.Items.Add(message);
                textBox1.Text = orderOutput.Data.Id.ToString();
            }


            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.OK)
            {
                this.Show();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];
            var apiClientV1 = new ApiClientV1(publicKey, privateKey, resourceUrl);

            var orderId = textBox1.Text;
            var cancelOrder = await apiClientV1.CancelOrder(long.Parse(orderId));

            string message = "";
            string title = "İşlem Durumu";

            if (!cancelOrder)
            {
                message = "Could not cancel order";               
            }
            else
            {
                message = $"Successfully canceled order {orderId}";
                listBox1.Items.Clear();
                textBox1.Clear();
            }
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.OK)
            {
                this.Show();
            }
        }
        private void abritajGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void Anasayfa_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

 
    }
}
