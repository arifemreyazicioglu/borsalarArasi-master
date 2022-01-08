namespace Binance.ModelsBinance
{
    public class TickerBinance
    {
        //public string symbol { get; set; }       
        //public decimal bidPrice { get; set; }
        //public decimal askPrice { get; set; }

        public string Symbol { get; set; }

        //public string PriceChange { get; set; }

        //public string PriceChangePercent { get; set; }

        //public string WeightedAvgPrice { get; set; }


        //public string PrevClosePrice { get; set; }


        //public string LastPrice { get; set; }


        //public string LastQty { get; set; }


        public decimal bidPrice { get; set; }


        //public string BidQty { get; set; }


        public decimal askPrice { get; set; }


        //public string AskQty { get; set; }


        //public string OpenPrice { get; set; }


        //public string HighPrice { get; set; }


        //public string LowPrice { get; set; }


        //public string Volume { get; set; }


        //public string QuoteVolume { get; set; }


        //public long OpenTime { get; set; }


        //public long CloseTime { get; set; }


        //public long FirstId { get; set; }


        //public long LastId { get; set; }


        //public long Count { get; set; }
    

        public override string ToString()
        {
            return /*"Pair: " + Pair + "\n" + "PairNormalized: " + PairNormalized + "\n" +  "Last: " + Last.ToString();  + "\n" + "High: " + High + "\n" +
                   "Low: " + Low + "\n" + "Volume: " + Volume + "\n" +
                   */"Bid: " + bidPrice.ToString("0.####") + " \n\n" + "Ask: " + askPrice.ToString("0.####") + " ";/*"\n" +
                   "Open: " + Open + "\n" + "Average: " + Average + "\n" + "Daily: " + Daily + "\n" +
                   "DailyPercent: " + DailyPercent + "\n" + "DenominatorSymbol: " + DenominatorSymbol + "\n" +
                   "NumeratorSymbol: " + NumeratorSymbol + "\n" + "Timestamp: " + Timestamp; */
        }

      
    }
}
