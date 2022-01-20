using System.Collections.Generic;

namespace Binance.ModelsBinance
{
   
    public class UserBalanceBinance
    {
        public string Asset { get; set; }
        public double Free { get; set; }
        public double Locked { get; set; }

        //public override string ToString()
        //    {
        //        return /*$"Asset: {Asset}, AssetName: {AssetName}, Balance: */MakerCommission.ToString()/*, Locked: {Locked}, Free: {Free}"*/;
        //    }
    }
   

}
