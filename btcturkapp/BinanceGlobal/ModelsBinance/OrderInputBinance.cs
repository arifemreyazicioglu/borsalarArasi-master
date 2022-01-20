using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.ModelsBinance
{
    public class OrderInputBinance
    {
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal StopPrice { get; set; }
        public string NewOrderClientId { get; set; }
        public OrderMethod OrderMethod { get; set; }
        public OrderType OrderType { get; set; }
        public string Symbol { get; set; }
    }

    public enum OrderType
    {
        Buy,
        Sell
    }

    public enum OrderMethod
    {
        Limit,
        Market,
        StopLimit,
        StopMarket
    }
}
