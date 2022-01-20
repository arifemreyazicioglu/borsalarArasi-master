using ImageProcessor.Imaging.Quantizers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.ModelsBinance
{
    public class ReturnModelBinance<T> where T : class
    {
            public bool Timestamp { get; set; }

            public string Msg { get; set; }

            public string Code { get; set; }

            public T Data { get; set; }

            public override string ToString()
            {
                return $"Code {Code}: , Message : {Msg}";
            }
        
        
    }
}