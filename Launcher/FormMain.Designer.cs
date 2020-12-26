namespace MapEditor
{
    partial class FormMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPlayGame = new System.Windows.Forms.Button();
            this.btnMapEditor = new System.Windows.Forms.Button();
            this.lblLogo = new System.Windows.Forms.Label();
            this.listBoxMaps = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnPlayGame
            // 
            this.btnPlayGame.Location = new System.Drawing.Point(91, 233);
            this.btnPlayGame.Margin = new System.Windows.Forms.Padding(4);
            this.btnPlayGame.Name = "btnPlayGame";
            this.btnPlayGame.Size = new System.Drawing.Size(213, 64);
            this.btnPlayGame.TabIndex = 0;
            this.btnPlayGame.Text = "Play Game";
            this.btnPlayGame.UseVisualStyleBackColor = true;
            this.btnPlayGame.Click += new System.EventHandler(this.btnPlayGame_Click);
            // 
            // btnMapEditor
            // 
            this.btnMapEditor.Location = new System.Drawing.Point(91, 321);
            this.btnMapEditor.Margin = new System.Windows.Forms.Padding(4);
            this.btnMapEditor.Name = "btnMapEditor";
            this.btnMapEditor.Size = new System.Drawing.Size(213, 62);
            this.btnMapEditor.TabIndex = 1;
            this.btnMapEditor.Text = "Map Editor";
            this.btnMapEditor.UseVisualStyleBackColor = true;
            this.btnMapEditor.Click += new System.EventHandler(this.btnMapEditor_Click);
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.BackColor = System.Drawing.Color.Black;
            this.lblLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(76, 79);
            this.lblLogo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(526, 76);
            this.lblLogo.TabIndex = 2;
            this.lblLogo.Text = "Krasses Spiel :D";
            // 
            // listBoxMaps
            // 
            this.listBoxMaps.BackColor = System.Drawing.SystemColors.Window;
            this.listBoxMaps.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.listBoxMaps.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxMaps.FormattingEnabled = true;
            this.listBoxMaps.ItemHeight = 69;
            this.listBoxMaps.Location = new System.Drawing.Point(720, 233);
            this.listBoxMaps.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxMaps.Name = "listBoxMaps";
            this.listBoxMaps.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBoxMaps.Size = new System.Drawing.Size(596, 211);
            this.listBoxMaps.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(335, 361);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 4;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Launcher.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(1564, 926);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listBoxMaps);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.btnMapEditor);
            this.Controls.Add(this.btnPlayGame);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.Text = "Launcher";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPlayGame;
        private System.Windows.Forms.Button btnMapEditor;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.ListBox listBoxMaps;
        private System.Windows.Forms.TextBox textBox1;
    }
}

