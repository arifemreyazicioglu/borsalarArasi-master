using APIClient.ApiClientV1;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using btcturkapp.BTCTurkFunction;
using System.Globalization;
using btcturkapp.BinanceFunctions;
using System.IO;
using Binance;
using System.Text.RegularExpressions;
using APIClient.Models;
using btcturkapp.Function;
using BinanceTR.Business.Concrete;
using BinanceTR.Models;

namespace btcturkapp
{ 
    public partial class Anasayfa : Form
    {
        //private string pattern = "^[0-9]{0,9}$";
        private string symbolForCancelBinance = "";

        public Anasayfa()
        {
            InitializeComponent();
            priceDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            priceDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            priceDifferenceGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            priceDifferenceGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            sellBuyBuySellDifferenceGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            sellBuyBuySellDifferenceGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            abritajGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            abritajGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            abritajGridView.RowTemplate.Height = 25;
            priceDataGridView.RowTemplate.Height = 25;
            priceDifferenceGridView.RowTemplate.Height = 25;
            sellBuyBuySellDifferenceGridView.RowTemplate.Height = 25;

            priceDataGridView.Rows.Add();
            priceDataGridView.Rows.Add();
            priceDataGridView.Rows.Add();

            priceDataGridView.Rows[0].Cells[0].Value = "Binance";
            priceDataGridView.Rows[0].Cells[1].Value = "USDTTRY";

            priceDataGridView.Rows[1].Cells[0].Value = "BTCTurk";
            priceDataGridView.Rows[1].Cells[1].Value = "USDTTRY";

            priceDataGridView.Rows[2].Cells[0].Value = "Binance";
            priceDataGridView.Rows[2].Cells[1].Value = "BTCUSDT";

            priceDataGridView.Rows[3].Cells[0].Value = "BTCTurk";
            priceDataGridView.Rows[3].Cells[1].Value = "BTCUSDT";

            abritajGridView.Rows.Add();
            abritajGridView.Rows.Add();
            abritajGridView.Rows.Add();

            abritajGridView.Rows[0].Cells[0].Value = "USDTTRY";
            abritajGridView.Rows[0].Cells[1].Value = "Binance -> BTCTurk";
            abritajGridView.Rows[0].Cells[2].Value = 100000.0;

            abritajGridView.Rows[1].Cells[0].Value = "USDTTRY";
            abritajGridView.Rows[1].Cells[1].Value = "BTCTurk -> Binance";
            abritajGridView.Rows[1].Cells[2].Value = 100000.0;

            abritajGridView.Rows[2].Cells[0].Value = "BTCUSDT";
            abritajGridView.Rows[2].Cells[1].Value = "Binance -> BTCTurk";
            abritajGridView.Rows[2].Cells[2].Value = 10.0;

            abritajGridView.Rows[3].Cells[0].Value = "BTCUSDT";
            abritajGridView.Rows[3].Cells[1].Value = "BTCTurk -> Binance";
            abritajGridView.Rows[3].Cells[2].Value = 10.0;

        }
        private void Anasayfa_Load(object sender, EventArgs e)
        {

            Control.CheckForIllegalCrossThreadCalls = false;

            FormLoadFunctions formFunc = new FormLoadFunctions();
            formFunc.formFunctions(label5,label6,btcTurkIdTextBox,btcTurkEmirIptalButton,binanceIdTextBox,binanceEmirIptalButton,assetGridView,priceDataGridView,priceDifferenceGridView,sellBuyBuySellDifferenceGridView,abritajGridView);
           
        }
        private async void usdtBinanceSellBtcTurkBuyButton_Click(object sender, EventArgs e)
        {
            string quantity = abritajGridView.Rows[0].Cells[2].Value.ToString();
            string btcTurkPrice = priceDataGridView.Rows[1].Cells[3].Value.ToString().Split(' ')[0];
            string binancePrice = priceDataGridView.Rows[0].Cells[2].Value.ToString().Split(' ')[0];
            symbolForCancelBinance = "USDT_TRY";
            OrderFunctions func = new OrderFunctions();
            await func.binanceSellBtcTurkBuyUsdtOrder(decimal.Parse(quantity),btcTurkPrice, binancePrice,listBox1, btcTurkIdTextBox, binanceIdTextBox, btcTurkEmirIptalButton, binanceEmirIptalButton);
        }
        private async void usdtBtcTurkSellBinanceBuyButton_Click(object sender, EventArgs e)
        {
            string quantity = abritajGridView.Rows[1].Cells[2].Value.ToString();
            string btcTurkPrice = priceDataGridView.Rows[1].Cells[2].Value.ToString().Split(' ')[0];
            string binancePrice = priceDataGridView.Rows[0].Cells[3].Value.ToString().Split(' ')[0];
            symbolForCancelBinance = "USDT_TRY";
            OrderFunctions func = new OrderFunctions();
            await func.btcTurkSellBinanceBuyUsdtOrder(decimal.Parse(quantity), btcTurkPrice, binancePrice, listBox1, btcTurkIdTextBox, binanceIdTextBox, btcTurkEmirIptalButton, binanceEmirIptalButton);                   
        }
        private async void btcBinanceSellBtcTurkBuyButton_Click(object sender, EventArgs e)
        {
            string quantity = abritajGridView.Rows[2].Cells[2].Value.ToString();
            string btcTurkPrice = priceDataGridView.Rows[3].Cells[3].Value.ToString().Split(' ')[0];
            string binancePrice = priceDataGridView.Rows[2].Cells[2].Value.ToString().Split(' ')[0];
            symbolForCancelBinance = "BTC_USDT";
            OrderFunctions func = new OrderFunctions();
            await func.binanceAskBtcTurkBuyBtcOrder(decimal.Parse(quantity),btcTurkPrice, binancePrice,listBox1, btcTurkIdTextBox, binanceIdTextBox, btcTurkEmirIptalButton, binanceEmirIptalButton);           
        }
        private async void btcBtcTurkSellBinanceBuyButton_Click(object sender, EventArgs e)
        {
            string quantity = abritajGridView.Rows[3].Cells[2].Value.ToString();
            string btcTurkPrice = priceDataGridView.Rows[3].Cells[2].Value.ToString().Split(' ')[0];
            string binancePrice = priceDataGridView.Rows[2].Cells[3].Value.ToString().Split(' ')[0];
            symbolForCancelBinance = "BTC_USDT";
            OrderFunctions func = new OrderFunctions();
            await func.btcTurkSellBinanceBuyBtcOrder(decimal.Parse(quantity),btcTurkPrice,binancePrice, listBox1, btcTurkIdTextBox, binanceIdTextBox, btcTurkEmirIptalButton, binanceEmirIptalButton);       
        }
        private async void btcTurkEmirIptalButton_Click(object sender, EventArgs e)
        {
            OrderFunctions func = new OrderFunctions();
            await func.btcTurkEmirIptal(listBox1, btcTurkIdTextBox, btcTurkEmirIptalButton);           
        }
        private async void binanceEmirIptalButton_Click(object sender, EventArgs e)
        {
            OrderFunctions func = new OrderFunctions();
            await func.binanceEmirIptal(listBox1, binanceIdTextBox, binanceEmirIptalButton,symbolForCancelBinance);
        }
        private void startBotButton_Click(object sender, EventArgs e)
        {
            BotFunctions botFunc = new BotFunctions();
            botFunc.startBotFunc(startBotButton,botMarjTextBox,comboBox1,sellBuyBuySellDifferenceGridView,abritajGridView,priceDataGridView,listBox1,btcTurkIdTextBox,binanceIdTextBox,btcTurkEmirIptalButton,binanceEmirIptalButton,symbolForCancelBinance,assetGridView);
        }
        
        private void abritajGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        private void Anasayfa_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void abritajGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            abritajGridView.EditingControl.KeyPress -= EditingControl_KeyPress;
            abritajGridView.EditingControl.KeyPress += EditingControl_KeyPress;
        }
        private void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        private void btcTurkIdTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void binanceIdTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }       
        private void botMarjTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}
