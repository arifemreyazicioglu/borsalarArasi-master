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
using APIClient.Models;
using btcturkapp.Function;

namespace btcturkapp
{ 
    public partial class Anasayfa : Form
    {
        //private string pattern = "^[0-9]{0,9}$";
        private string symbolForCancelBinance = "";

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


            var timer = new Timer { Interval = 1500 };
            timer.Tick += async (o, args) =>
            {

                if (btcTurkIdTextBox.Text == "")
                {
                    btcTurkEmirIptalButton.Enabled = false;
                }
                if (binanceIdTextBox.Text == "")
                {
                    binanceEmirIptalButton.Enabled = false;
                }
                //
                //
                //Binance ve BTCTurk Asset Miktarları
                try
                {
                    binanceUsdtMiktarıLabel.Text = "Binance Usdt Miktarı : \n" + await binance.BinanceGetBalanceAsync("USDT") + " $";
                    btcTurkUsdtMiktarıLabel.Text = "BTCTurk Usdt Miktarı : \n" + await btcTurk.BTCTurkGetAccountBalance("USDT") + " $";
                    binanceBtcMiktarıLabel.Text = "Binance BTC Miktarı : \n" + await binance.BinanceGetBalanceAsync("BTC") + " ₿";
                    btcTurkBtcMiktarıLabel.Text = "BTCTurk BTC Miktarı : \n" + await btcTurk.BTCTurkGetAccountBalance("BTC") + " ₿";
                    binanceTryMiktarıLabel.Text = "Binance Try Miktarı : \n" + await binance.BinanceGetBalanceAsync("TRY") + " ₺";
                    btcTurkTryMiktarıLabel.Text = "Binance Try Miktarı : \n" + await btcTurk.BTCTurkGetAccountBalance("TRY") + " ₺";
                }
                catch
                {
                    binanceUsdtMiktarıLabel.Text = "Server Error";
                    btcTurkUsdtMiktarıLabel.Text = "Server Error";
                    binanceBtcMiktarıLabel.Text = "Server Error";
                    btcTurkBtcMiktarıLabel.Text = "Server Error";
                    binanceTryMiktarıLabel.Text = "Server Error";
                    btcTurkTryMiktarıLabel.Text = "Server Error";
                }
                
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
                double usdtPriceDifferenceBtcTurkAskBinanceBid = 0;
                try
                {
                    usdtPriceDifferenceBtcTurkAskBinanceBid = double.Parse(btcTurkUsdtAskPrice, CultureInfo.InvariantCulture) - double.Parse(binanceUsdtBidPrice, CultureInfo.InvariantCulture);
                    sellBuyBuySellDifferenceGridView.Rows[0].Cells[0].Value = usdtPriceDifferenceBtcTurkAskBinanceBid.ToString("0.####") + " ₺";
                }
                catch
                {
                    sellBuyBuySellDifferenceGridView.Rows[0].Cells[0].Value = "Server Error";
                }
                //
                //
                // USDT Binance satis btcturk alis arasindaki fark
                double usdtPriceDifferenceBinanceAskBtcTurkABid = 0;
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
                double btcPriceDifferenceBtcTurkAskBinanceBid = 0;
                try
                {
                    btcPriceDifferenceBtcTurkAskBinanceBid = double.Parse(btcTurkBtcUsdtAskPrice, CultureInfo.InvariantCulture) - double.Parse(binanceBtcUsdtBidPrice, CultureInfo.InvariantCulture);
                    sellBuyBuySellDifferenceGridView.Rows[0].Cells[2].Value = btcPriceDifferenceBtcTurkAskBinanceBid.ToString("0.####") + " $";
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
        private async void usdtBinanceSellBtcTurkBuyButton_Click(object sender, EventArgs e)
        {
            string quantity = abritajGridView.Rows[0].Cells[2].Value.ToString();
            string btcTurkPrice = priceDataGridView.Rows[1].Cells[3].Value.ToString().Split(' ')[0];
            string binancePrice = priceDataGridView.Rows[0].Cells[2].Value.ToString().Split(' ')[0];
            symbolForCancelBinance = "USDTTRY";
            OrderFunctions func = new OrderFunctions();
            await func.binanceAskBtcTurkSellUsdtOrder(quantity,btcTurkPrice,binancePrice,listBox1, btcTurkIdTextBox, binanceIdTextBox, btcTurkEmirIptalButton, binanceEmirIptalButton);
        }
        private async void usdtBtcTurkSellBinanceBuyButton_Click(object sender, EventArgs e)
        {
            string quantity = abritajGridView.Rows[1].Cells[2].Value.ToString();
            string btcTurkPrice = priceDataGridView.Rows[1].Cells[2].Value.ToString().Split(' ')[0];
            string binancePrice = priceDataGridView.Rows[0].Cells[3].Value.ToString().Split(' ')[0];
            symbolForCancelBinance = "USDTTRY";
            OrderFunctions func = new OrderFunctions();
            await func.btcTurkSellBinanceBuyUsdtOrder(quantity, btcTurkPrice, binancePrice, listBox1, btcTurkIdTextBox, binanceIdTextBox, btcTurkEmirIptalButton, binanceEmirIptalButton);                   
        }
        private async void btcBinanceSellBtcTurkBuyButton_Click(object sender, EventArgs e)
        {
            symbolForCancelBinance = "BTCUSDT";
            OrderFunctions func = new OrderFunctions();
            await func.binanceAskBtcTurkSellBtcOrder(listBox1, btcTurkIdTextBox, binanceIdTextBox, btcTurkEmirIptalButton, binanceEmirIptalButton);           
        }
        private async void btcBtcTurkSellBinanceBuyButton_Click(object sender, EventArgs e)
        {
            symbolForCancelBinance = "BTCUSDT";
            OrderFunctions func = new OrderFunctions();
            await func.btcTurkSellBinanceBuyBtcOrder(listBox1, btcTurkIdTextBox, binanceIdTextBox, btcTurkEmirIptalButton, binanceEmirIptalButton);       
        }
        private async void btcTurkEmirIptalButton_Click(object sender, EventArgs e)
        {
            OrderFunctions func = new OrderFunctions();
            await func.btcTurkEmirIptal(listBox1, btcTurkIdTextBox, btcTurkEmirIptalButton);           
        }
        private async void binanceEmirIptalButton_Click(object sender, EventArgs e)
        {
            OrderFunctions func = new OrderFunctions();
            await func.binanceEmirIptal(listBox1, binanceIdTextBox, binanceEmirIptalButton,symbolForCancelBinance);
        }
        private void startBotButton_Click(object sender, EventArgs e)
        {
            startBotButton.Enabled = false;
            if (botMarjTextBox.Text == "" || comboBox1.SelectedIndex == -1)
            {
                string message = "İşlem yönünü veya marj aralığını eksik doldurdunuz!";
                string title = "UYARI";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons);
                startBotButton.Enabled = true;
            }
            else
            {
                double marj = 0.0;
                var secim = 0;
                if (comboBox1.SelectedItem.ToString() == "USDT Binance Sat - BTCTurk Al")
                {
                    marj = double.Parse(sellBuyBuySellDifferenceGridView.Rows[0].Cells[1].Value.ToString().Split(' ')[0]);
                    marj = marj * 1000;
                    secim = 1;
                }
                else if (comboBox1.SelectedItem.ToString() == "USDT BTCTurk Sat - Binance Al")
                {
                    marj = double.Parse(sellBuyBuySellDifferenceGridView.Rows[0].Cells[0].Value.ToString().Split(' ')[0]);
                    marj = marj * 1000;
                    secim = 2;
                }
                else if (comboBox1.SelectedItem.ToString() == "BTC Binance Sat - BTCTurk Al")
                {
                    marj = double.Parse(sellBuyBuySellDifferenceGridView.Rows[0].Cells[3].Value.ToString().Split(' ')[0]);
                    secim = 3;
                }
                else if (comboBox1.SelectedItem.ToString() == "BTC BTCTurk Sat - Binance Al")
                {
                    marj = double.Parse(sellBuyBuySellDifferenceGridView.Rows[0].Cells[2].Value.ToString().Split(' ')[0]);
                    secim = 4;
                }
                else
                {
                    marj = 0.0;
                    secim = 0;
                }

                var timer1 = new Timer { Interval = 10000 };
                timer1.Tick += async (o, args) =>
                {
                    if (botMarjTextBox.Text != "" && comboBox1.SelectedIndex != -1)
                    {
                        if (secim == 1 && marj >= double.Parse(botMarjTextBox.Text))
                        {
                            string quantity = abritajGridView.Rows[0].Cells[2].Value.ToString();
                            string btcTurkPrice = priceDataGridView.Rows[1].Cells[3].Value.ToString().Split(' ')[0];
                            string binancePrice = priceDataGridView.Rows[0].Cells[2].Value.ToString().Split(' ')[0];
                            symbolForCancelBinance = "USDTTRY";
                            OrderFunctions func = new OrderFunctions();
                            await func.binanceAskBtcTurkSellUsdtOrder(quantity, btcTurkPrice, binancePrice, listBox1, btcTurkIdTextBox, binanceIdTextBox, btcTurkEmirIptalButton, binanceEmirIptalButton);
                        }
                        else if (secim == 2 && marj >= double.Parse(botMarjTextBox.Text))
                        {
                            string quantity = abritajGridView.Rows[1].Cells[2].Value.ToString();
                            string btcTurkPrice = priceDataGridView.Rows[1].Cells[2].Value.ToString().Split(' ')[0];
                            string binancePrice = priceDataGridView.Rows[0].Cells[3].Value.ToString().Split(' ')[0];
                            symbolForCancelBinance = "USDTTRY";
                            OrderFunctions func = new OrderFunctions();
                            await func.btcTurkSellBinanceBuyUsdtOrder(quantity, btcTurkPrice, binancePrice, listBox1, btcTurkIdTextBox, binanceIdTextBox, btcTurkEmirIptalButton, binanceEmirIptalButton);
                        }
                        else if (secim == 3 && marj >= double.Parse(botMarjTextBox.Text))
                        {
                            symbolForCancelBinance = "BTCUSDT";
                            OrderFunctions func = new OrderFunctions();
                            await func.binanceAskBtcTurkSellBtcOrder(listBox1, btcTurkIdTextBox, binanceIdTextBox, btcTurkEmirIptalButton, binanceEmirIptalButton);
                        }
                        else if (secim == 4 && marj >= double.Parse(botMarjTextBox.Text))
                        {
                            symbolForCancelBinance = "BTCUSDT";
                            OrderFunctions func = new OrderFunctions();
                            await func.btcTurkSellBinanceBuyBtcOrder(listBox1, btcTurkIdTextBox, binanceIdTextBox, btcTurkEmirIptalButton, binanceEmirIptalButton);
                        }                     
                    }
                    else
                    {
                        string message = "İşlem yönünü veya marj aralığı eksik! Bot durduruldu!";
                        string title = "UYARI";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result = MessageBox.Show(message, title, buttons);
                        startBotButton.Enabled = true;
                        timer1.Stop();
                    }
                };
                timer1.Start();
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
        private void abritajGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            abritajGridView.EditingControl.KeyPress -= EditingControl_KeyPress;
            abritajGridView.EditingControl.KeyPress += EditingControl_KeyPress;
        }
        private void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btcTurkIdTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void binanceIdTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
       
        private void botMarjTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    
    }
}
