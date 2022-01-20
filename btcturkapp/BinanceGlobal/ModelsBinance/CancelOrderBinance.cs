using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace btcturkapp.Binance.ModelsBinance
{
    public  class CancelOrderBinance
    {

        public string Symbol { get; set; }

        public string OrigClientOrderId { get; set; }

        public long OrderId { get; set; }

        public long OrderListId { get; set; }

        public string ClientOrderId { get; set; }

        public string Price { get; set; }

        public string OrigQty { get; set; }

        public string ExecutedQty { get; set; }

        public string CummulativeQuoteQty { get; set; }

        public string Status { get; set; }

        public string TimeInForce { get; set; }

        public string Type { get; set; }

        public string Side { get; set; }

        public override string ToString()
        {
            return $"Id: {OrderId} , Symbol: {Symbol} , Side: {Side} , Price: {Price} , Quantity: {OrigQty} , Status: {Status}";
        }
    }
}
