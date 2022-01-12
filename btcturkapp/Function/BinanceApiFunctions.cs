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
            var configuration = new ConfigurationBuilder().AddJsonFile("apikeys.json").Build();
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
            var configuration = new ConfigurationBuilder().AddJsonFile("apikeys.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];

            var binanceV1 = new BinanceV1(publicKey, privateKey, resourceUrl);
            try
            {
                var tickerList = await binanceV1.GetBalances();

                UserBalanceBinance Currency1 = (from coin in tickerList.Balances where coin.Asset == symbol select coin).FirstOrDefault();
                
                Console.WriteLine(Currency1.Free);
                return Currency1.Free.ToString();
            }
            catch
            {
                return "Server Error";
            }

        }
    }
}
