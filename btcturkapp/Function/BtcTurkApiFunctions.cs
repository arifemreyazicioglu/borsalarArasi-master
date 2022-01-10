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

namespace btcturkapp.BTCTurkFunction
{
    public class btcTurkFunction
    {
        public async Task<string> BTCTurkGetValueAsync(string curr)
        {
            var responseString = string.Empty;
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
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

        public async Task<string> BTCTurkGetAccountBalance(string symbol)
        {
            var responseString = string.Empty;
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];
            var apiClientV1 = new ApiClientV1(publicKey, privateKey, resourceUrl);

            var balances = await apiClientV1.GetBalances(symbol);

            if (balances != null && balances.Success)
            {
                foreach (var balance in balances.Data)
                {
                    if(balance.Asset.Contains(symbol))
                    {
                        Console.WriteLine(balance.ToString());
                        responseString = balance.ToString();
                    }
                    
                }
            }

            return responseString;

        }
      
    }
}
