using btcturkapp.BinanceFunctions;
using btcturkapp.BTCTurkFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btcturkapp.Function
{
    public class BotFunctions
    {
        public void startBotFunc(Button startBotButton,TextBox botMarjTextBox,ComboBox comboBox1,DataGridView sellBuyBuySellDifferenceGridView,DataGridView abritajGridView,DataGridView priceDataGridView, ListBox listBox1, TextBox btcTurkIdTextBox, TextBox binanceIdTextBox, Button btcTurkEmirIptalButton, Button binanceEmirIptalButton,string symbolForCancelBinance,DataGridView assetGridView)
        {
            btcTurkFunction btcTurk = new btcTurkFunction();
            binanceFunctions binance = new binanceFunctions();

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

                var timer1 = new Timer { Interval = 5000 };
                timer1.Tick += async (o, args) =>
                {

                    var responseBtcTurkUsdtBids= await btcTurk.BTCTurkGetOrderBookAsync("USDTTRY");
                    var hacimBtcTurkUsdtBids= responseBtcTurkUsdtBids.Bids[0][1].ToString("0");

                    var responseBtcTurkUsdtAsks = await btcTurk.BTCTurkGetOrderBookAsync("USDTTRY");
                    var hacimBtcTurkUsdtAsks = responseBtcTurkUsdtAsks.Asks[0][1].ToString("0");

                    //var responseBtcTurkBtcBids = await btcTurk.BTCTurkGetOrderBookAsync("BTCUSDT");
                    //var hacimBtcTurkBtcBids = responseBtcTurkBtcBids.Bids[0][1].ToString("0.######");

                    //var responseBtcTurkBtcAsks = await btcTurk.BTCTurkGetOrderBookAsync("BTCUSDT");
                    //var hacimBtcTurkBtcAsks = responseBtcTurkBtcAsks.Asks[0][1].ToString("0.######");



                    Console.WriteLine(double.Parse(hacimBtcTurkUsdtBids));

                    if (botMarjTextBox.Text != "" && comboBox1.SelectedIndex != -1)
                    {
                        if (secim == 1 && marj >= double.Parse(botMarjTextBox.Text) && double.Parse(hacimBtcTurkUsdtBids) < 5000 && double.Parse(assetGridView.Rows[0].Cells[1].Value.ToString()) >= double.Parse(abritajGridView.Rows[0].Cells[2].Value.ToString()))
                        {
                            string quantity = abritajGridView.Rows[0].Cells[2].Value.ToString();
                            string btcTurkPrice = priceDataGridView.Rows[1].Cells[3].Value.ToString().Split(' ')[0];
                            string binancePrice = priceDataGridView.Rows[0].Cells[2].Value.ToString().Split(' ')[0];
                            symbolForCancelBinance = "USDT_TRY";
                            OrderFunctions func = new OrderFunctions();
                            await func.binanceSellBtcTurkBuyUsdtOrder(decimal.Parse(quantity), btcTurkPrice, binancePrice, listBox1, btcTurkIdTextBox, binanceIdTextBox, btcTurkEmirIptalButton, binanceEmirIptalButton);

                        }
                        else if (secim == 2 && marj >= double.Parse(botMarjTextBox.Text) && double.Parse(hacimBtcTurkUsdtAsks) < 5000 && double.Parse(assetGridView.Rows[0].Cells[1].Value.ToString()) >= double.Parse(abritajGridView.Rows[1].Cells[2].Value.ToString()))
                        {
                            string quantity = abritajGridView.Rows[1].Cells[2].Value.ToString();
                            string btcTurkPrice = priceDataGridView.Rows[1].Cells[2].Value.ToString().Split(' ')[0];
                            string binancePrice = priceDataGridView.Rows[0].Cells[3].Value.ToString().Split(' ')[0];
                            symbolForCancelBinance = "USDT_TRY";
                            OrderFunctions func = new OrderFunctions();
                            await func.btcTurkSellBinanceBuyUsdtOrder(decimal.Parse(quantity), btcTurkPrice, binancePrice, listBox1, btcTurkIdTextBox, binanceIdTextBox, btcTurkEmirIptalButton, binanceEmirIptalButton);

                        }
                        //else if (secim == 3 && marj >= double.Parse(botMarjTextBox.Text) && double.Parse(hacimBtcTurkBtcBids) < 5000 && double.Parse(assetGridView.Rows[0].Cells[3].Value.ToString()) >= double.Parse(abritajGridView.Rows[2].Cells[2].Value.ToString()))
                        //{
                        //    string quantity = abritajGridView.Rows[2].Cells[2].Value.ToString();
                        //    string btcTurkPrice = priceDataGridView.Rows[3].Cells[3].Value.ToString().Split(' ')[0];
                        //    string binancePrice = priceDataGridView.Rows[2].Cells[2].Value.ToString().Split(' ')[0];
                        //    symbolForCancelBinance = "BTC_USDT";
                        //    OrderFunctions func = new OrderFunctions();
                        //    await func.binanceSellBtcTurkBuyBtcOrder(decimal.Parse(quantity), btcTurkPrice, binancePrice, listBox1, btcTurkIdTextBox, binanceIdTextBox, btcTurkEmirIptalButton, binanceEmirIptalButton);
                        //}
                        //else if (secim == 4 && marj >= double.Parse(botMarjTextBox.Text) && double.Parse(hacimBtcTurkBtcAsks) < 5000 && double.Parse(assetGridView.Rows[0].Cells[3].Value.ToString()) >= double.Parse(abritajGridView.Rows[3].Cells[2].Value.ToString()))
                        //{
                        //    string quantity = abritajGridView.Rows[3].Cells[2].Value.ToString();
                        //    string btcTurkPrice = priceDataGridView.Rows[3].Cells[2].Value.ToString().Split(' ')[0];
                        //    string binancePrice = priceDataGridView.Rows[2].Cells[3].Value.ToString().Split(' ')[0];
                        //    symbolForCancelBinance = "BTC_USDT";
                        //    OrderFunctions func = new OrderFunctions();
                        //    await func.btcTurkSellBinanceBuyBtcOrder(decimal.Parse(quantity), btcTurkPrice, binancePrice, listBox1, btcTurkIdTextBox, binanceIdTextBox, btcTurkEmirIptalButton, binanceEmirIptalButton);
                        //}
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
    }
}
