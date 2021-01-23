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
            this.btnNewMap = new System.Windows.Forms.Button();
            this.lblLogo = new System.Windows.Forms.Label();
            this.listBoxMaps = new System.Windows.Forms.ListBox();
            this.btnEditMap = new System.Windows.Forms.Button();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.btnDeleteMap = new System.Windows.Forms.Button();
            this.checkBoxUseWallTextures = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPlayGame
            // 
            this.btnPlayGame.Location = new System.Drawing.Point(68, 189);
            this.btnPlayGame.Name = "btnPlayGame";
            this.btnPlayGame.Size = new System.Drawing.Size(160, 52);
            this.btnPlayGame.TabIndex = 0;
            this.btnPlayGame.Text = "Play Game";
            this.btnPlayGame.UseVisualStyleBackColor = true;
            this.btnPlayGame.Click += new System.EventHandler(this.btnPlayGame_Click);
            // 
            // btnNewMap
            // 
            this.btnNewMap.Location = new System.Drawing.Point(68, 261);
            this.btnNewMap.Name = "btnNewMap";
            this.btnNewMap.Size = new System.Drawing.Size(160, 50);
            this.btnNewMap.TabIndex = 1;
            this.btnNewMap.Text = "New Map";
            this.btnNewMap.UseVisualStyleBackColor = true;
            this.btnNewMap.Click += new System.EventHandler(this.btnMapEditor_Click);
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.BackColor = System.Drawing.Color.Black;
            this.lblLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(57, 64);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(426, 63);
            this.lblLogo.TabIndex = 2;
            this.lblLogo.Text = "Krasses Spiel :D";
            // 
            // listBoxMaps
            // 
            this.listBoxMaps.BackColor = System.Drawing.SystemColors.Window;
            this.listBoxMaps.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.listBoxMaps.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxMaps.FormattingEnabled = true;
            this.listBoxMaps.ItemHeight = 55;
            this.listBoxMaps.Location = new System.Drawing.Point(540, 64);
            this.listBoxMaps.Name = "listBoxMaps";
            this.listBoxMaps.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBoxMaps.Size = new System.Drawing.Size(448, 279);
            this.listBoxMaps.TabIndex = 3;
            this.listBoxMaps.Click += new System.EventHandler(this.listBoxMaps_Click);
            // 
            // btnEditMap
            // 
            this.btnEditMap.Location = new System.Drawing.Point(68, 327);
            this.btnEditMap.Name = "btnEditMap";
            this.btnEditMap.Size = new System.Drawing.Size(160, 50);
            this.btnEditMap.TabIndex = 4;
            this.btnEditMap.Text = "Edit Map";
            this.btnEditMap.UseVisualStyleBackColor = true;
            this.btnEditMap.Click += new System.EventHandler(this.btnEditMap_Click);
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.Location = new System.Drawing.Point(540, 374);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(448, 323);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxPreview.TabIndex = 5;
            this.pictureBoxPreview.TabStop = false;
            this.pictureBoxPreview.Visible = false;
            // 
            // btnDeleteMap
            // 
            this.btnDeleteMap.Location = new System.Drawing.Point(68, 392);
            this.btnDeleteMap.Name = "btnDeleteMap";
            this.btnDeleteMap.Size = new System.Drawing.Size(160, 50);
            this.btnDeleteMap.TabIndex = 6;
            this.btnDeleteMap.Text = "Delete Map";
            this.btnDeleteMap.UseVisualStyleBackColor = true;
            this.btnDeleteMap.Click += new System.EventHandler(this.btnDeleteMap_Click);
            // 
            // checkBoxUseWallTextures
            // 
            this.checkBoxUseWallTextures.AutoSize = true;
            this.checkBoxUseWallTextures.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxUseWallTextures.Location = new System.Drawing.Point(68, 468);
            this.checkBoxUseWallTextures.Name = "checkBoxUseWallTextures";
            this.checkBoxUseWallTextures.Size = new System.Drawing.Size(235, 22);
            this.checkBoxUseWallTextures.TabIndex = 7;
            this.checkBoxUseWallTextures.Text = "Use Wall Textures (visual bugs)";
            this.checkBoxUseWallTextures.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Launcher.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(1066, 752);
            this.Controls.Add(this.checkBoxUseWallTextures);
            this.Controls.Add(this.btnDeleteMap);
            this.Controls.Add(this.pictureBoxPreview);
            this.Controls.Add(this.btnEditMap);
            this.Controls.Add(this.listBoxMaps);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.btnNewMap);
            this.Controls.Add(this.btnPlayGame);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Launcher";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPlayGame;
        private System.Windows.Forms.Button btnNewMap;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.ListBox listBoxMaps;
        private System.Windows.Forms.Button btnEditMap;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Button btnDeleteMap;
        private System.Windows.Forms.CheckBox checkBoxUseWallTextures;
    }
}

