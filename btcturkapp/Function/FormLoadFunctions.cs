using APIClient.ApiClientV1;
using BinanceTR.Business.Concrete;
using btcturkapp.BinanceFunctions;
using btcturkapp.BTCTurkFunction;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btcturkapp.Function
{
    public class FormLoadFunctions
    {
        public  void formFunctions(Label label5,Label label6,TextBox btcTurkIdTextBox,
            Button btcTurkEmirIptalButton,TextBox binanceIdTextBox, Button binanceEmirIptalButton,
            DataGridView assetGridView,DataGridView priceDataGridView,DataGridView priceDifferenceGridView,
            DataGridView sellBuyBuySellDifferenceGridView,DataGridView abritajGridView,ListBox listbox1)
        {
            btcTurkFunction btcTurk = new btcTurkFunction();
            binanceFunctions binance = new binanceFunctions();

            //var configuration = new ConfigurationBuilder().AddJsonFile("binanceApiKeys.json").Build();
            //var publicKey = configuration["publicKey"];
            //var privateKey = configuration["privateKey"];
            //var resourceUrlBinance = configuration["resourceUrlBinance"];
            //var resourceUrlBinanceTr = configuration["resourceUrlBinanceTr"];

            //var binanceTr = new BinanceTrManager(publicKey, privateKey, resourceUrlBinance, resourceUrlBinanceTr);

            //var ihsan = await binanceTr.GetAllOpenOrdersAsync("USDT_TRY");

            //foreach (var ayse in ihsan.Data)
            //{
            //    listbox1.Items.Add(ayse.Price + " " + ayse.Status);
            //}

            var timer = new Timer { Interval = 1500 };
            timer.Tick += async (o, args) =>
            {
               
                var arif = await binance.BinanceGetOrderBookAsync("USDTTRY");
                label5.Text = arif.Bids[0][0].ToString("0.####") + "  " + arif.Bids[0][1].ToString("0.####");

                var emre = await btcTurk.BTCTurkGetOrderBookAsync("USDTTRY");
                label6.Text = emre.Bids[0][0].ToString() + "  " + emre.Bids[0][1];

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
                //Binance Usdt Satış Fiyatı
                var binanceUsdtAskPrice = "Waiting";
                if (!string.IsNullOrEmpty(binanceUsdtAskPrice))
                {
                    try
                    {
                        var response = await binance.BinanceGetOrderBookAsync("USDTTRY");
                        binanceUsdtAskPrice = response.Asks[0][0].ToString("0.####").Replace(",", ".");
                        priceDataGridView.Rows[0].Cells[2].Value = binanceUsdtAskPrice + " ₺";
                    }
                    catch
                    {
                        priceDataGridView.Rows[0].Cells[2].Value = "Server Error";
                    }
                }
                //
                //
                //Binance Usdt Alış Fiyatı
                var binanceUsdtBidPrice = "Waiting";
                if (!string.IsNullOrEmpty(binanceUsdtBidPrice))
                {
                    try
                    {
                        var response = await binance.BinanceGetOrderBookAsync("USDTTRY");
                        binanceUsdtBidPrice = response.Bids[0][0].ToString("0.####").Replace(",", ".");
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
                //
                //
                //Binance ve BTCTurk Asset Miktarları
                try
                {
                    assetGridView.Rows[0].Cells[0].Value = await binance.BinanceGetBalanceAsync("USDT") + " $";
                }
                catch
                {
                    assetGridView.Rows[0].Cells[0].Value = "Server Error";
                }
                try
                {
                    assetGridView.Rows[0].Cells[1].Value = await btcTurk.BTCTurkGetAccountBalance("USDT") + " $";
                }
                catch
                {
                    assetGridView.Rows[0].Cells[1].Value = "Server Error";
                }
                try
                {
                    assetGridView.Rows[0].Cells[2].Value = await binance.BinanceGetBalanceAsync("BTC") + " ₿";
                }
                catch
                {
                    assetGridView.Rows[0].Cells[2].Value = "Server Error";
                }
                try
                {
                    assetGridView.Rows[0].Cells[3].Value = await btcTurk.BTCTurkGetAccountBalance("BTC") + " ₿";
                }
                catch
                {
                    assetGridView.Rows[0].Cells[3].Value = "Server Error";
                }
                try
                {
                    assetGridView.Rows[0].Cells[4].Value = await binance.BinanceGetBalanceAsync("TRY") + " ₺";
                }
                catch
                {
                    assetGridView.Rows[0].Cells[4].Value = "Server Error";
                }
                try
                {
                    assetGridView.Rows[0].Cells[5].Value = await btcTurk.BTCTurkGetAccountBalance("TRY") + " ₺";
                }
                catch
                {
                    assetGridView.Rows[0].Cells[5].Value = "Server Error";
                }

            };
            timer.Start();
        }
    }
}
