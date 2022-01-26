using APIClient.ApiClientV1;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using btcturkapp.BTCTurkFunction;
using System.Globalization;
using APIClient.Models;

namespace btcturkapp.BTCTurkFunction
{
    public class btcTurkFunction
    {

        public async Task<string> BTCTurkGetValueAsync(string curr)
        {
            var responseString = string.Empty;
            var configuration = new ConfigurationBuilder().AddJsonFile("btcTurkApiKeys.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];
            var apiClientV1 = new ApiClientV1(publicKey, privateKey, resourceUrl);
            try
            {
                var tickerList = await apiClientV1.GetTicker(curr);
                if (tickerList.Success)
                {
                    foreach (var ticker in tickerList.Data)
                    {
                        responseString = ticker.ToString();
                    }
                }
                else
                {
                    Console.WriteLine(tickerList.ToString());
                }
                return responseString;
            }
            catch
            {
                return "Server Error";
            }
            
        }
        public async Task<OpenOrderOutput> BTCTurkOpenOrders(string symbol)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("btcTurkApiKeys.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];
            var apiClientV1 = new ApiClientV1(publicKey, privateKey, resourceUrl);
            
            var openOrders = await apiClientV1.GetOpenOrders(symbol);

            return openOrders.Data;
        }

        public async Task<OrderBook> BTCTurkGetOrderBookAsync(string symbol)
        {
            OrderBook responseString = new OrderBook();
            var configuration = new ConfigurationBuilder().AddJsonFile("btcTurkApiKeys.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];
            var apiClientV1 = new ApiClientV1(publicKey, privateKey, resourceUrl);
           
                var tickerList = await apiClientV1.GetOrderBook(symbol);
                if (tickerList.Success)
                {
                    responseString = tickerList.Data;
                    return responseString;
                }
                else
                {
                    Console.WriteLine(tickerList.ToString());
                }
                return responseString;
           
        }

        public async Task<string> BTCTurkGetAccountBalance(string symbol)
        {
            var responseString = string.Empty;
            var configuration = new ConfigurationBuilder().AddJsonFile("btcTurkApiKeys.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];
            var apiClientV1 = new ApiClientV1(publicKey, privateKey, resourceUrl);

            try
            {
                var balances = await apiClientV1.GetBalances();

                if (balances.Data != null && balances.Success)
                {
                    foreach (var balance in balances.Data)
                    {
                        if (balance.Asset.Contains(symbol))
                        {
                            responseString = balance.Balance.ToString("0.####");
                            //Console.WriteLine(responseString);
                        }

                    }
                }
                return responseString;
            }
            catch
            {
                return "Server Error";
            }

        }

    }
}
