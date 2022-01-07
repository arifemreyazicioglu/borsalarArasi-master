using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.ModelsBinance;

namespace Binance
{
    public interface IBinanceClient
    {
        //Task<ReturnModelBinance<OrderBookBinance>> GetOrderBook(string pairSymbol, int limit = 30);

        //Task<ReturnModelBinance<IList<TradesBinance>>> GetLastTrades(string pairSymbol, int numberOfTrades);

        //Task<ReturnModelBinance<OrderOutputBinance>> CreateOrder(OrderInputBinance orderInput);

        //Task<bool> CancelOrder(long id);

        Task<TickerBinance> GetTicker(string pairSymbol);

        //Task<ReturnModelBinance<IList<TickerBinance>>> GetTicker();

        //Task<ReturnModelBinance<IList<UserBalanceBinance>>> GetBalances();

        //Task<ReturnModelBinance<OpenOrderOutputBinance>> GetOpenOrders(string pairSymbol = null);

        //Task<ReturnModelBinance<IList<OHLCBinance>>> GetDailyOhlc(string pairSymbol, int last);

        //Task<ReturnModelBinance<IList<UserTradeBinance>>> GetUserTrades(string[] type, string[] symbol, long startDate, long endDate);

        //Task<ReturnModelBinance<IList<UserTransactionBinance>>> GetUserFiatTransactions(string[] type, string[] symbol, long startDate, long endDate);

        //Task<ReturnModelBinance<IList<UserTransactionBinance>>> GetUserCryptoTransactions(string[] type, string[] symbol, long startDate, long endDate);
    }
}
