using System.Collections.Generic;

namespace Binance.ModelsBinance
{
    public class AccountInformation
    {
        public double MakerCommission { get; set; }
        public double TakerCommission { get; set; }
        public double BuyerCommission { get; set; }
        public double SellerCommission { get; set; }
        public bool CanTrade { get; set; }
        public bool CanWithdraw { get; set; } 
        public bool CanDeposit { get; set; }
        public List<UserBalanceBinance> Balances { get; set; }
    }

    public class UserBalanceBinance
    {
        public string Asset { get; set; }
        public double Free { get; set; }
        public double Locked { get; set; }
        public UserBalanceBinance Balances { get; internal set; }

        //public override string ToString()
        //    {
        //        return /*$"Asset: {Asset}, AssetName: {AssetName}, Balance: */MakerCommission.ToString()/*, Locked: {Locked}, Free: {Free}"*/;
        //    }
    }
   

}
