﻿using APIClient.ApiClientV1;
using APIClient.Models;
using BinanceTR.Business.Concrete;
using BinanceTR.Models.Enums;
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
        public async Task binanceAskBtcTurkSellUsdtOrder(decimal quantity,string btcTurkPrice,string binancePrice,ListBox listBox1,TextBox btcTurkIdTextBox,TextBox binanceIdTextBox,Button btcTurkEmirIptalButton,Button binanceEmirIptalButton)
        {
            string message = "";
            string title = "İşlem Durumu";

            var configuration = new ConfigurationBuilder().AddJsonFile("btcTurkApiKeys.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];
            var apiClientV1 = new ApiClientV1(publicKey, privateKey, resourceUrl);

            var limitSellOrder = new OrderInput
            {
                Quantity = quantity,
                Price = decimal.Parse(btcTurkPrice),
                OrderMethod = OrderMethod.Limit,
                OrderType = OrderType.Buy,
                PairSymbol = "USDT_TRY",
            };

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

            var configuration1 = new ConfigurationBuilder().AddJsonFile("binanceApiKeys.json").Build();
            var publicKey1 = configuration1["publicKey"];
            var privateKey1 = configuration1["privateKey"];
            var resourceUrlBinance = configuration1["resourceUrlBinance"];
            var resourceUrlBinanceTr = configuration1["resourceUrlBinanceTr"];

            var binanceTr = new BinanceTrManager(publicKey1, privateKey1, resourceUrlBinance, resourceUrlBinanceTr);

            var order = await binanceTr.PostNewLimitOrderAsync("USDT_TRY", OrderSideEnum.SELL, quantity, binancePrice).ConfigureAwait(false);

            if(!order.Success)
            {
                message = message + "\n" + $"Binance: Code:{order.Code} , Message: {order.Message}";
            }
            else
            {
                listBox1.Items.Add("Binance usdt Satım işlemi : " + order.Data.ToString());
                binanceIdTextBox.Text = order.Data.OrderId.ToString(); //order.ToString().Split(' ')[1].Split(',')[0];
                binanceEmirIptalButton.Enabled = true;
            }
            
            if (message != "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons);
            }

        }
        public async Task btcTurkSellBinanceBuyUsdtOrder(decimal quantity, string btcTurkPrice, string binancePrice, ListBox listBox1, TextBox btcTurkIdTextBox, TextBox binanceIdTextBox, Button btcTurkEmirIptalButton, Button binanceEmirIptalButton)
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
                Quantity = quantity,
                Price = decimal.Parse(btcTurkPrice),
                OrderMethod = OrderMethod.Limit,
                OrderType = OrderType.Sell,
                PairSymbol = "USDT_TRY",
            };
            ////Create New Order
            var orderOutput = await apiClientV1.CreateOrder(limitSellOrder);

            if (!orderOutput.Success)
            {
                message = $"BTCTurk: Code:{orderOutput.Code} , Message: {orderOutput.Message}";
            }
            else
            {
                listBox1.Items.Add("BTCTurk usdt satım işlemi : " + orderOutput.Data.ToString());
                btcTurkIdTextBox.Text = orderOutput.Data.Id.ToString();
                btcTurkEmirIptalButton.Enabled = true;
            }

            binanceFunctions binance = new binanceFunctions();

            var configuration1 = new ConfigurationBuilder().AddJsonFile("binanceApiKeys.json").Build();
            var publicKey1 = configuration1["publicKey"];
            var privateKey1 = configuration1["privateKey"];
            var resourceUrlBinance = configuration1["resourceUrlBinance"];
            var resourceUrlBinanceTr = configuration1["resourceUrlBinanceTr"];

            var binanceTr = new BinanceTrManager(publicKey1, privateKey1, resourceUrlBinance, resourceUrlBinanceTr);

            var order = await binanceTr.PostNewLimitOrderAsync("USDT_TRY", OrderSideEnum.BUY, quantity, binancePrice);

            if (!order.Success)
            {
                message = message + "\n" + $"Binance: Code:{order.Code} , Message: {order.Message}";
            }
            else
            {
                listBox1.Items.Add("Binance usdt Satım işlemi : " + order.Data.ToString());
                binanceIdTextBox.Text = order.Data.OrderId.ToString(); //order.ToString().Split(' ')[1].Split(',')[0];
                binanceEmirIptalButton.Enabled = true;
            }

            if (message != "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons);
            }
            
        }       
        public async Task binanceAskBtcTurkBuyBtcOrder(decimal quantity, string btcTurkPrice, string binancePrice, ListBox listBox1, TextBox btcTurkIdTextBox, TextBox binanceIdTextBox, Button btcTurkEmirIptalButton, Button binanceEmirIptalButton)
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
                Quantity = quantity,
                Price = decimal.Parse(btcTurkPrice),
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

            var configuration1 = new ConfigurationBuilder().AddJsonFile("binanceApiKeys.json").Build();
            var publicKey1 = configuration1["publicKey"];
            var privateKey1 = configuration1["privateKey"];
            var resourceUrlBinance = configuration1["resourceUrlBinance"];
            var resourceUrlBinanceTr = configuration1["resourceUrlBinanceTr"];

            var binanceTr = new BinanceTrManager(publicKey1, privateKey1, resourceUrlBinance, resourceUrlBinanceTr);

            var order = await binanceTr.PostNewLimitOrderAsync("BTC_USDT", OrderSideEnum.SELL, quantity, binancePrice);

            if (!order.Success)
            {
                message = message + "\n" + $"Binance: Code:{order.Code} , Message: {order.Message}";
            }
            else
            {
                listBox1.Items.Add("Binance usdt Satım işlemi : " + order.Data.ToString());
                binanceIdTextBox.Text = order.Data.OrderId.ToString(); 
                binanceEmirIptalButton.Enabled = true;
            }

            if (message != "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons);
            }

        }
        public async Task btcTurkSellBinanceBuyBtcOrder(decimal quantity, string btcTurkPrice, string binancePrice, ListBox listBox1, TextBox btcTurkIdTextBox, TextBox binanceIdTextBox, Button btcTurkEmirIptalButton, Button binanceEmirIptalButton)
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
                Quantity = quantity,
                Price = decimal.Parse(btcTurkPrice),
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


            var configuration1 = new ConfigurationBuilder().AddJsonFile("binanceApiKeys.json").Build();
            var publicKey1 = configuration1["publicKey"];
            var privateKey1 = configuration1["privateKey"];
            var resourceUrlBinance = configuration1["resourceUrlBinance"];
            var resourceUrlBinanceTr = configuration1["resourceUrlBinanceTr"];

            var binanceTr = new BinanceTrManager(publicKey1, privateKey1, resourceUrlBinance, resourceUrlBinanceTr);

            var order = await binanceTr.PostNewLimitOrderAsync("BTC_USDT", OrderSideEnum.BUY, quantity, binancePrice);

            if (!order.Success)
            {
                message = message + "\n" + $"Binance: Code:{order.Code} , Message: {order.Message}";
            }
            else
            {
                listBox1.Items.Add("Binance usdt Satım işlemi : " + order.Data.ToString());
                binanceIdTextBox.Text = order.Data.OrderId.ToString();
                binanceEmirIptalButton.Enabled = true;
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
