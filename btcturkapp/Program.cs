using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using APIClient.ApiClientV1;
using APIClient.Helpers;
using Microsoft.Extensions.Configuration;

namespace btcturkapp
{
    static class Program
    {

        [STAThread]
        static void Main()
        {    
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Anasayfa());
        }
    }
}
