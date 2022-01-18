using Binance.BinanceV1;
using Binance.ModelsBinance;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace btcturkapp.BinanceFunctions
{

    public class binanceFunctions
    {     
        public async Task<string> BinanceGetValueAsync(string curr)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("binanceApiKeys.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];

            var binanceV1 = new BinanceV1(publicKey, privateKey, resourceUrl);
            try
            {
                var tickerList = await binanceV1.GetTicker(curr);
                return tickerList.ToString();
            }
            catch
            {
                return "Server Error";
            }           
            
        }
        public async Task<string> BinanceGetBalanceAsync(string symbol)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("binanceApiKeys.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];

            var binanceV1 = new BinanceV1(publicKey, privateKey, resourceUrl);
            try
            {
                var tickerList = await binanceV1.GetBalances();

                UserBalanceBinance Currency1 = (from coin in tickerList.Balances where coin.Asset == symbol select coin).FirstOrDefault();

                return Currency1.Free.ToString("0.#########");

            }
            catch
            {
                return "Server Error";
            }

        }
        public async Task<string> BinanceCreateOrder(string symbol, string side, decimal quantity, decimal price)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("binanceApiKeys.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];

            var binanceV1 = new BinanceV1(publicKey, privateKey, resourceUrl);
                      
            var order =  await binanceV1.CreateOrderBinance(symbol,side,quantity,price);
            return order.ToString();
            
            
        }
        public async Task<string> BinanceCancelOrder(long id, string symbol)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("binanceApiKeys.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];

            var binanceV1 = new BinanceV1(publicKey, privateKey, resourceUrl);
           
            var order = await binanceV1.CancelOrderBinance(id,symbol);
            return order.ToString();
                     
        }
    }
}
