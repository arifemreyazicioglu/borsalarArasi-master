namespace btcturkapp
{
    partial class Anasayfa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.priceDataGridView = new System.Windows.Forms.DataGridView();
            this.exchangeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coinName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.askPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bidPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceDifferenceGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.abritajGridView = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.sellBuyBuySellDifferenceGridView = new System.Windows.Forms.DataGridView();
            this.A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.priceDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.priceDifferenceGridView)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.abritajGridView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sellBuyBuySellDifferenceGridView)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // priceDataGridView
            // 
            this.priceDataGridView.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.priceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.priceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.exchangeName,
            this.coinName,
            this.askPrice,
            this.bidPrice});
            this.priceDataGridView.Location = new System.Drawing.Point(14, 5);
            this.priceDataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.priceDataGridView.Name = "priceDataGridView";
            this.priceDataGridView.RowHeadersWidth = 62;
            this.priceDataGridView.Size = new System.Drawing.Size(782, 227);
            this.priceDataGridView.TabIndex = 11;
            // 
            // exchangeName
            // 
            this.exchangeName.HeaderText = "Borsa";
            this.exchangeName.MinimumWidth = 8;
            this.exchangeName.Name = "exchangeName";
            this.exchangeName.Width = 120;
            // 
            // coinName
            // 
            this.coinName.HeaderText = "Coin";
            this.coinName.MinimumWidth = 8;
            this.coinName.Name = "coinName";
            this.coinName.Width = 120;
            // 
            // askPrice
            // 
            this.askPrice.HeaderText = "Satış (Ask)";
            this.askPrice.MinimumWidth = 8;
            this.askPrice.Name = "askPrice";
            this.askPrice.Width = 120;
            // 
            // bidPrice
            // 
            this.bidPrice.HeaderText = "Alış (Bid)";
            this.bidPrice.MinimumWidth = 8;
            this.bidPrice.Name = "bidPrice";
            this.bidPrice.Width = 120;
            // 
            // priceDifferenceGridView
            // 
            this.priceDifferenceGridView.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.priceDifferenceGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.priceDifferenceGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.priceDifferenceGridView.Location = new System.Drawing.Point(12, 254);
            this.priceDifferenceGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.priceDifferenceGridView.Name = "priceDifferenceGridView";
            this.priceDifferenceGridView.RowHeadersWidth = 62;
            this.priceDifferenceGridView.Size = new System.Drawing.Size(786, 203);
            this.priceDifferenceGridView.TabIndex = 12;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "USDT Satış Farkı";
            this.Column1.MinimumWidth = 8;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 120;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "USDT Alış Farkı";
            this.Column2.MinimumWidth = 8;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 120;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "BTC Satış Farkı";
            this.Column3.MinimumWidth = 8;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 120;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "BTC Alış Farkı";
            this.Column4.MinimumWidth = 8;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 120;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox5.BackColor = System.Drawing.Color.White;
            this.groupBox5.Controls.Add(this.label26);
            this.groupBox5.Controls.Add(this.label25);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox5.ForeColor = System.Drawing.Color.Black;
            this.groupBox5.Location = new System.Drawing.Point(13, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(784, 409);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Komisyonlar";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label26.Location = new System.Drawing.Point(6, 117);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(491, 22);
            this.label26.TabIndex = 5;
            this.label26.Text = "BTCTurk\'te Tether alma ve satma komisyonu : 0.0001";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label25.Location = new System.Drawing.Point(6, 63);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(487, 22);
            this.label25.TabIndex = 4;
            this.label25.Text = "Binance\'de Tether alma ve satma komisyonu : 0.0002";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(6, 338);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(599, 22);
            this.label18.TabIndex = 3;
            this.label18.Text = "BTCTurk\'ten Binance\'e Tether gönderme komisyonu : 0.25 Tether";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(6, 286);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(576, 22);
            this.label17.TabIndex = 2;
            this.label17.Text = "Binance\'den BTCTurk\'e Tether gönderme komisyonu : 1 Tether";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(6, 229);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(512, 22);
            this.label16.TabIndex = 1;
            this.label16.Text = "BTC\'den Binance\'e BTC gönderme komisyonu : Ücretsiz";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(6, 172);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(588, 22);
            this.label14.TabIndex = 0;
            this.label14.Text = "Binance\'den BTCTurk\'e BTC gönderme komisyonu : 0.0005 BTC";
            // 
            // abritajGridView
            // 
            this.abritajGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.abritajGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
            this.abritajGridView.Location = new System.Drawing.Point(4, 5);
            this.abritajGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.abritajGridView.Name = "abritajGridView";
            this.abritajGridView.RowHeadersWidth = 62;
            this.abritajGridView.Size = new System.Drawing.Size(994, 452);
            this.abritajGridView.TabIndex = 13;
            this.abritajGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.abritajGridView_EditingControlShowing);
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Coin";
            this.Column5.MinimumWidth = 8;
            this.Column5.Name = "Column5";
            this.Column5.Width = 150;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Yön";
            this.Column6.MinimumWidth = 8;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 120;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Miktar";
            this.Column7.MinimumWidth = 8;
            this.Column7.Name = "Column7";
            this.Column7.Width = 150;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Kazanılan Para";
            this.Column8.MinimumWidth = 8;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 150;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Ödenecek Para";
            this.Column9.MinimumWidth = 8;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 150;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Kar - Zarar";
            this.Column10.MinimumWidth = 8;
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 150;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.priceDataGridView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.priceDifferenceGridView, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(18, 18);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(810, 475);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.abritajGridView, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(836, 18);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1002, 475);
            this.tableLayoutPanel2.TabIndex = 15;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox5, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(940, 501);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(810, 437);
            this.tableLayoutPanel3.TabIndex = 16;
            // 
            // sellBuyBuySellDifferenceGridView
            // 
            this.sellBuyBuySellDifferenceGridView.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.sellBuyBuySellDifferenceGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sellBuyBuySellDifferenceGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.A,
            this.Column11,
            this.Column12,
            this.Column13});
            this.sellBuyBuySellDifferenceGridView.Location = new System.Drawing.Point(8, 3);
            this.sellBuyBuySellDifferenceGridView.Name = "sellBuyBuySellDifferenceGridView";
            this.sellBuyBuySellDifferenceGridView.RowHeadersWidth = 62;
            this.sellBuyBuySellDifferenceGridView.RowTemplate.Height = 28;
            this.sellBuyBuySellDifferenceGridView.Size = new System.Drawing.Size(899, 193);
            this.sellBuyBuySellDifferenceGridView.TabIndex = 17;
            // 
            // A
            // 
            this.A.HeaderText = "USDT Binance Alış - BTCTurk Satış Fark";
            this.A.MinimumWidth = 8;
            this.A.Name = "A";
            this.A.ReadOnly = true;
            this.A.Width = 140;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "USDT Binance Satış - BTCTurk Alış Fark";
            this.Column11.MinimumWidth = 8;
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 140;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "BTC Binance Alış - BTCTurk Satış Fark";
            this.Column12.MinimumWidth = 8;
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 140;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "BTC Binance Satış - BTCTurk Alış Fark";
            this.Column13.MinimumWidth = 8;
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 140;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.sellBuyBuySellDifferenceGridView, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(18, 501);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(915, 230);
            this.tableLayoutPanel4.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(143, 808);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 26);
            this.label1.TabIndex = 19;
            this.label1.Text = "label1";
            // 
            // Anasayfa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1851, 1017);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Anasayfa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anasayfa";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.priceDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.priceDifferenceGridView)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.abritajGridView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sellBuyBuySellDifferenceGridView)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView priceDataGridView;
        private System.Windows.Forms.DataGridView priceDifferenceGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridView abritajGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.DataGridView sellBuyBuySellDifferenceGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DataGridViewTextBoxColumn A;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn exchangeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn coinName;
        private System.Windows.Forms.DataGridViewTextBoxColumn askPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn bidPrice;
        private System.Windows.Forms.Label label1;
    }
}