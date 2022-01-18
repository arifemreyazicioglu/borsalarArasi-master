using APIClient.ApiClientV1;
using APIClient.Models;
using btcturkapp.BinanceFunctions;
using btcturkapp.BTCTurkFunction;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btcturkapp.Function
{
    public class OrderFunctions
    {
        public async Task binanceAskBtcTurkSellUsdtOrder(ListBox listBox1,TextBox btcTurkIdTextBox,TextBox binanceIdTextBox,Button btcTurkEmirIptalButton,Button binanceEmirIptalButton)
        {
            string message = "";
            string title = "İşlem Durumu";
            btcTurkFunction btcTurk = new btcTurkFunction();
            var configuration = new ConfigurationBuilder().AddJsonFile("btcTurkApiKeys.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];
            var apiClientV1 = new ApiClientV1(publicKey, privateKey, resourceUrl);

            var limitSellOrder = new OrderInput
            {
                Quantity = 7.50m,
                Price = 13.500m,
                OrderMethod = OrderMethod.Limit,
                OrderType = OrderType.Buy,
                PairSymbol = "USDT_TRY",
            };
            ////Create New Order
            ///
            var orderOutput = await apiClientV1.CreateOrder(limitSellOrder);

            if (!orderOutput.Success)
            {
                message = $"BTCTurk: Code:{orderOutput.Code} , Message: {orderOutput.Message}";
            }
            else
            {
             
                listBox1.Items.Add("BTCTurk usdt alım işlemi : " + orderOutput.Data.ToString());
                btcTurkIdTextBox.Text = orderOutput.Data.Id.ToString();
                btcTurkEmirIptalButton.Enabled = true;
           
            }
 
            binanceFunctions binance = new binanceFunctions();
            try
            {
                var order = await binance.BinanceCreateOrder("USDTTRY", "SELL", 5, 13.70m);           
                listBox1.Items.Add("Binance usdt Satım işlemi : " + order.ToString());
                binanceIdTextBox.Text = order.ToString().Split(' ')[1].Split(',')[0];
                binanceEmirIptalButton.Enabled = true;
                
            }
            catch(Exception e)
            {
                message = message + "\n" + "Binance :" + e.ToString().Split('}')[0];
            }

            if(message !="")
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons);              
            }

        }
        public async Task btcTurkSellBinanceBuyUsdtOrder(ListBox listBox1, TextBox btcTurkIdTextBox, TextBox binanceIdTextBox, Button btcTurkEmirIptalButton, Button binanceEmirIptalButton)
        {
            string message = "";
            string title = "İşlem Durumu";
            btcTurkFunction btcTurk = new btcTurkFunction();
            var configuration = new ConfigurationBuilder().AddJsonFile("btcTurkApiKeys.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];
            var apiClientV1 = new ApiClientV1(publicKey, privateKey, resourceUrl);

            var limitSellOrder = new OrderInput
            {
                Quantity = 7.50m,
                Price = 13.500m,
                OrderMethod = OrderMethod.Limit,
                OrderType = OrderType.Sell,
                PairSymbol = "USDT_TRY",
            };
            ////Create New Order
            var orderOutput = await apiClientV1.CreateOrder(limitSellOrder);

            if (!orderOutput.Success)
            {
                message = $"Code:{orderOutput.Code} , Message: {orderOutput.Message}";
            }
            else
            {               
                listBox1.Items.Add("BTCTurk usdt satım işlemi : " + orderOutput.Data.ToString());
                btcTurkIdTextBox.Text = orderOutput.Data.Id.ToString();
                btcTurkEmirIptalButton.Enabled = true;
            }

            binanceFunctions binance = new binanceFunctions();
            try
            {
                var order = await binance.BinanceCreateOrder("USDTTRY", "BUY", 5, 13.60m);

                listBox1.Items.Add("Binance usdt alım işlemi : " + order.ToString());
                binanceIdTextBox.Text = order.ToString().Split(' ')[1].Split(',')[0];
                binanceEmirIptalButton.Enabled = true;

            }
            catch (Exception e)
            {
                message = message + "\n" + e.ToString();
            }

            if (message != "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons);
            }
        }       
        public async Task binanceAskBtcTurkSellBtcOrder(ListBox listBox1, TextBox btcTurkIdTextBox, TextBox binanceIdTextBox, Button btcTurkEmirIptalButton, Button binanceEmirIptalButton)
        {
            string message = "";
            string title = "İşlem Durumu";
            btcTurkFunction btcTurk = new btcTurkFunction();
            var configuration = new ConfigurationBuilder().AddJsonFile("btcTurkApiKeys.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];
            var apiClientV1 = new ApiClientV1(publicKey, privateKey, resourceUrl);

            var limitSellOrder = new OrderInput
            {
                Quantity = 0.0001m,
                Price = 50000m,
                OrderMethod = OrderMethod.Limit,
                OrderType = OrderType.Buy,
                PairSymbol = "BTC_USDT",
            };
            ////Create New Order
            ///
            var orderOutput = await apiClientV1.CreateOrder(limitSellOrder);

            if (!orderOutput.Success)
            {
                message = $"BTCTurk: Code:{orderOutput.Code} , Message: {orderOutput.Message}";
            }
            else
            {
                listBox1.Items.Add("BTCTurk usdt alım işlemi : " + orderOutput.Data.ToString());
                btcTurkIdTextBox.Text = orderOutput.Data.Id.ToString();
                btcTurkEmirIptalButton.Enabled = true;
            }
            
            binanceFunctions binance = new binanceFunctions();
            try
            {
                var order = await binance.BinanceCreateOrder("BTCUSDT", "SELL", 0.001m, 50000);
                listBox1.Items.Add("Binance usdt Satım işlemi : " + order.ToString());
                binanceIdTextBox.Text = order.ToString().Split(' ')[1].Split(',')[0];
                binanceEmirIptalButton.Enabled = true;

            }
            catch (Exception e)
            {
                message = message + "\n" + "Binance :" + e.ToString().Split('}')[0];
            }

            if (message != "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons);
            }

        }
        public async Task btcTurkSellBinanceBuyBtcOrder(ListBox listBox1, TextBox btcTurkIdTextBox, TextBox binanceIdTextBox, Button btcTurkEmirIptalButton, Button binanceEmirIptalButton)
        {
            string message = "";
            string title = "İşlem Durumu";
            btcTurkFunction btcTurk = new btcTurkFunction();
            var configuration = new ConfigurationBuilder().AddJsonFile("btcTurkApiKeys.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];
            var apiClientV1 = new ApiClientV1(publicKey, privateKey, resourceUrl);

            var limitSellOrder = new OrderInput
            {
                Quantity = 0.0001m,
                Price = 50000m,
                OrderMethod = OrderMethod.Limit,
                OrderType = OrderType.Sell,
                PairSymbol = "BTC_USDT",
            };
            ////Create New Order
            ///
            var orderOutput = await apiClientV1.CreateOrder(limitSellOrder);

            if (!orderOutput.Success)
            {
                message = $"BTCTurk: Code:{orderOutput.Code} , Message: {orderOutput.Message}";
            }
            else
            {
                listBox1.Items.Add("BTCTurk usdt alım işlemi : " + orderOutput.Data.ToString());
                btcTurkIdTextBox.Text = orderOutput.Data.Id.ToString();
                btcTurkEmirIptalButton.Enabled = true;
            }

            binanceFunctions binance = new binanceFunctions();
            try
            {
                var order = await binance.BinanceCreateOrder("BTCUSDT", "BUY", 0.0001m, 50000m);
                listBox1.Items.Add("Binance usdt Satım işlemi : " + order.ToString());
                binanceIdTextBox.Text = order.ToString().Split(' ')[1].Split(',')[0];
                binanceEmirIptalButton.Enabled = true;

            }
            catch (Exception e)
            {
                message = message + "\n" + "Binance :" + e.ToString().Split('}')[0];
            }

            if (message != "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons);
            }
        }
        public async Task btcTurkEmirIptal(ListBox listBox1,TextBox btcTurkIdTextBox,Button btcTurkEmirIptalButton)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("btcTurkApiKeys.json").Build();

            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];
            var apiClientV1 = new ApiClientV1(publicKey, privateKey, resourceUrl);

            var orderId = btcTurkIdTextBox.Text;
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
                btcTurkIdTextBox.Clear();
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    if (listBox1.Items[i].ToString().Contains(orderId) == true)
                    {
                        listBox1.Items.RemoveAt(i);
                    }

                }
                btcTurkEmirIptalButton.Enabled = false;
            }
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons);         
        }
        public async Task binanceEmirIptal(ListBox listBox1, TextBox binanceIdTextBox, Button binanceEmirIptalButton,string symbolForCancelBinance)
        {
            string message = "";
            string title = "İşlem Durumu";
            long id = long.Parse(binanceIdTextBox.Text);

            binanceFunctions binance = new binanceFunctions();

            try
            {
                var order = await binance.BinanceCancelOrder(id, symbolForCancelBinance);
                binanceIdTextBox.Clear();
                message = $"Successfully canceled order {id}";

                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    if (listBox1.Items[i].ToString().Contains(id.ToString()) == true)
                    {
                        listBox1.Items.RemoveAt(i);
                    }
                }

                binanceEmirIptalButton.Enabled = false;

            }
            catch(Exception e)
            {
                message = e.ToString().Split('}')[0];
            }
            
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons);

        }
        
    }
}
