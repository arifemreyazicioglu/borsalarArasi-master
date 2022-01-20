using Binance.BinanceV1;
using Binance.ModelsBinance;
using BinanceTR.Business.Concrete;
using BinanceTR.Models;
using BinanceTR.Models.Enums;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            var resourceUrlBinance = configuration["resourceUrlBinance"];
            var resourceUrlBinanceTr = configuration["resourceUrlBinanceTr"];

            var binanceV1 = new BinanceV1(publicKey, privateKey, resourceUrlBinance,resourceUrlBinanceTr);
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
            
            var responseString = string.Empty;
            var configuration = new ConfigurationBuilder().AddJsonFile("binanceApiKeys.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrlBinance = configuration["resourceUrlBinance"];
            var resourceUrlBinanceTr = configuration["resourceUrlBinanceTr"];

            var binanceTr = new BinanceTrManager(publicKey, privateKey, resourceUrlBinance, resourceUrlBinanceTr);
            try
            {
                var balances = await binanceTr.GetAssetIformationAsync(symbol);

                if (balances.Data != null)
                {
                    
                 responseString = balances.Data.Free.ToString();
                  //Console.WriteLine(responseString);
                                
                }
                return responseString;
            }
            catch
            {
                return "Server Error";
            }

        }        
        public async Task<string> BinanceCancelOrder(long id, string symbol)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("binanceApiKeys.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrlBinance = configuration["resourceUrlBinance"];
            var resourceUrlBinanceTr = configuration["resourceUrlBinanceTr"];

            var binanceTr = new BinanceTrManager(publicKey, privateKey, resourceUrlBinance, resourceUrlBinanceTr);

            var order = await binanceTr.CancelOrderByIdAsync(id);
            return order.ToString();
                     
        }
    }
}
