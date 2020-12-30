namespace MapEditor
{
    partial class FormMapEditor
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
            this.buttonWhite = new System.Windows.Forms.Button();
            this.buttonRed = new System.Windows.Forms.Button();
            this.buttonGreen = new System.Windows.Forms.Button();
            this.btnSaveMap = new System.Windows.Forms.Button();
            this.btnChangeDim = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownMapDimX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMapDimY = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.txtMapName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMapDimX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMapDimY)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonWhite
            // 
            this.buttonWhite.BackColor = System.Drawing.Color.White;
            this.buttonWhite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWhite.ForeColor = System.Drawing.Color.White;
            this.buttonWhite.Location = new System.Drawing.Point(24, 70);
            this.buttonWhite.Margin = new System.Windows.Forms.Padding(2);
            this.buttonWhite.Name = "buttonWhite";
            this.buttonWhite.Size = new System.Drawing.Size(38, 41);
            this.buttonWhite.TabIndex = 0;
            this.buttonWhite.Text = "0";
            this.buttonWhite.UseVisualStyleBackColor = false;
            this.buttonWhite.Click += new System.EventHandler(this.selectButton_click);
            // 
            // buttonRed
            // 
            this.buttonRed.BackColor = System.Drawing.Color.Red;
            this.buttonRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRed.ForeColor = System.Drawing.Color.Red;
            this.buttonRed.Location = new System.Drawing.Point(24, 161);
            this.buttonRed.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRed.Name = "buttonRed";
            this.buttonRed.Size = new System.Drawing.Size(38, 41);
            this.buttonRed.TabIndex = 1;
            this.buttonRed.Text = "2";
            this.buttonRed.UseVisualStyleBackColor = false;
            this.buttonRed.Click += new System.EventHandler(this.selectButton_click);
            // 
            // buttonGreen
            // 
            this.buttonGreen.BackColor = System.Drawing.Color.Green;
            this.buttonGreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGreen.ForeColor = System.Drawing.Color.Green;
            this.buttonGreen.Location = new System.Drawing.Point(24, 115);
            this.buttonGreen.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGreen.Name = "buttonGreen";
            this.buttonGreen.Size = new System.Drawing.Size(38, 41);
            this.buttonGreen.TabIndex = 2;
            this.buttonGreen.Text = "1";
            this.buttonGreen.UseVisualStyleBackColor = false;
            this.buttonGreen.Click += new System.EventHandler(this.selectButton_click);
            // 
            // btnSaveMap
            // 
            this.btnSaveMap.Location = new System.Drawing.Point(60, 536);
            this.btnSaveMap.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveMap.Name = "btnSaveMap";
            this.btnSaveMap.Size = new System.Drawing.Size(56, 19);
            this.btnSaveMap.TabIndex = 3;
            this.btnSaveMap.Text = "Save Map";
            this.btnSaveMap.UseVisualStyleBackColor = true;
            this.btnSaveMap.Click += new System.EventHandler(this.saveMap);
            // 
            // btnChangeDim
            // 
            this.btnChangeDim.Location = new System.Drawing.Point(438, 10);
            this.btnChangeDim.Margin = new System.Windows.Forms.Padding(2);
            this.btnChangeDim.Name = "btnChangeDim";
            this.btnChangeDim.Size = new System.Drawing.Size(112, 24);
            this.btnChangeDim.TabIndex = 4;
            this.btnChangeDim.Text = "Change dimensions";
            this.btnChangeDim.UseVisualStyleBackColor = true;
            this.btnChangeDim.Click += new System.EventHandler(this.btnChangeDim_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Map dimensions";
            // 
            // numericUpDownMapDimX
            // 
            this.numericUpDownMapDimX.Location = new System.Drawing.Point(324, 14);
            this.numericUpDownMapDimX.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownMapDimX.Name = "numericUpDownMapDimX";
            this.numericUpDownMapDimX.Size = new System.Drawing.Size(36, 20);
            this.numericUpDownMapDimX.TabIndex = 8;
            this.numericUpDownMapDimX.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // numericUpDownMapDimY
            // 
            this.numericUpDownMapDimY.Location = new System.Drawing.Point(381, 14);
            this.numericUpDownMapDimY.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownMapDimY.Name = "numericUpDownMapDimY";
            this.numericUpDownMapDimY.Size = new System.Drawing.Size(36, 20);
            this.numericUpDownMapDimY.TabIndex = 9;
            this.numericUpDownMapDimY.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(364, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "x";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(22, 15);
            this.labelName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(59, 13);
            this.labelName.TabIndex = 11;
            this.labelName.Text = "Map Name";
            // 
            // txtMapName
            // 
            this.txtMapName.Location = new System.Drawing.Point(83, 13);
            this.txtMapName.Margin = new System.Windows.Forms.Padding(2);
            this.txtMapName.Name = "txtMapName";
            this.txtMapName.Size = new System.Drawing.Size(76, 20);
            this.txtMapName.TabIndex = 12;
            // 
            // FormMapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1177, 650);
            this.Controls.Add(this.txtMapName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownMapDimY);
            this.Controls.Add(this.numericUpDownMapDimX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChangeDim);
            this.Controls.Add(this.btnSaveMap);
            this.Controls.Add(this.buttonGreen);
            this.Controls.Add(this.buttonRed);
            this.Controls.Add(this.buttonWhite);
            this.Name = "FormMapEditor";
            this.Text = "Map Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMapDimX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMapDimY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonWhite;
        private System.Windows.Forms.Button buttonRed;
        private System.Windows.Forms.Button buttonGreen;
        private System.Windows.Forms.Button btnSaveMap;
        private System.Windows.Forms.Button btnChangeDim;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownMapDimX;
        private System.Windows.Forms.NumericUpDown numericUpDownMapDimY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox txtMapName;
    }
}