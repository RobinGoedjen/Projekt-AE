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
            this.btnSaveMap = new System.Windows.Forms.Button();
            this.btnChangeDim = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownMapDimX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMapDimY = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.txtMapName = new System.Windows.Forms.TextBox();
            this.pictureBoxMap = new System.Windows.Forms.PictureBox();
            this.btnSetPlayerPosition = new System.Windows.Forms.Button();
            this.labelPlayerOrientation = new System.Windows.Forms.Label();
            this.trackBarPlayerOrientation = new System.Windows.Forms.TrackBar();
            this.listBoxSprites = new System.Windows.Forms.ListBox();
            this.radioButtonSpriteIgnore = new System.Windows.Forms.RadioButton();
            this.groupBoxRadioSprite = new System.Windows.Forms.GroupBox();
            this.radioButtonSpriteDelete = new System.Windows.Forms.RadioButton();
            this.radioButtonSpriteDraw = new System.Windows.Forms.RadioButton();
            this.listBoxGameTexture = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMapDimX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMapDimY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPlayerOrientation)).BeginInit();
            this.groupBoxRadioSprite.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSaveMap
            // 
            this.btnSaveMap.Location = new System.Drawing.Point(33, 46);
            this.btnSaveMap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveMap.Name = "btnSaveMap";
            this.btnSaveMap.Size = new System.Drawing.Size(75, 23);
            this.btnSaveMap.TabIndex = 3;
            this.btnSaveMap.Text = "Save Map";
            this.btnSaveMap.UseVisualStyleBackColor = true;
            this.btnSaveMap.Click += new System.EventHandler(this.saveMap);
            // 
            // btnChangeDim
            // 
            this.btnChangeDim.Location = new System.Drawing.Point(584, 12);
            this.btnChangeDim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChangeDim.Name = "btnChangeDim";
            this.btnChangeDim.Size = new System.Drawing.Size(149, 30);
            this.btnChangeDim.TabIndex = 4;
            this.btnChangeDim.Text = "Change dimensions";
            this.btnChangeDim.UseVisualStyleBackColor = true;
            this.btnChangeDim.Click += new System.EventHandler(this.btnChangeDim_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(299, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Map dimensions";
            // 
            // numericUpDownMapDimX
            // 
            this.numericUpDownMapDimX.Location = new System.Drawing.Point(432, 17);
            this.numericUpDownMapDimX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDownMapDimX.Maximum = new decimal(new int[] {
            175,
            0,
            0,
            0});
            this.numericUpDownMapDimX.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownMapDimX.Name = "numericUpDownMapDimX";
            this.numericUpDownMapDimX.Size = new System.Drawing.Size(48, 22);
            this.numericUpDownMapDimX.TabIndex = 8;
            this.numericUpDownMapDimX.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericUpDownMapDimY
            // 
            this.numericUpDownMapDimY.Location = new System.Drawing.Point(508, 17);
            this.numericUpDownMapDimY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDownMapDimY.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownMapDimY.Name = "numericUpDownMapDimY";
            this.numericUpDownMapDimY.Size = new System.Drawing.Size(48, 22);
            this.numericUpDownMapDimY.TabIndex = 9;
            this.numericUpDownMapDimY.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(485, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "x";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(29, 18);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(76, 17);
            this.labelName.TabIndex = 11;
            this.labelName.Text = "Map Name";
            // 
            // txtMapName
            // 
            this.txtMapName.Location = new System.Drawing.Point(111, 16);
            this.txtMapName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMapName.Name = "txtMapName";
            this.txtMapName.Size = new System.Drawing.Size(100, 22);
            this.txtMapName.TabIndex = 12;
            // 
            // pictureBoxMap
            // 
            this.pictureBoxMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxMap.Location = new System.Drawing.Point(303, 86);
            this.pictureBoxMap.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxMap.Name = "pictureBoxMap";
            this.pictureBoxMap.Size = new System.Drawing.Size(525, 525);
            this.pictureBoxMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxMap.TabIndex = 13;
            this.pictureBoxMap.TabStop = false;
            this.pictureBoxMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMap_Mouse);
            this.pictureBoxMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMap_Mouse);
            this.pictureBoxMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMap_MouseUp);
            // 
            // btnSetPlayerPosition
            // 
            this.btnSetPlayerPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetPlayerPosition.Location = new System.Drawing.Point(32, 261);
            this.btnSetPlayerPosition.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSetPlayerPosition.Name = "btnSetPlayerPosition";
            this.btnSetPlayerPosition.Size = new System.Drawing.Size(219, 55);
            this.btnSetPlayerPosition.TabIndex = 16;
            this.btnSetPlayerPosition.Text = "Set Player Position";
            this.btnSetPlayerPosition.UseVisualStyleBackColor = true;
            this.btnSetPlayerPosition.Click += new System.EventHandler(this.btnSetPlayerPosition_Click);
            // 
            // labelPlayerOrientation
            // 
            this.labelPlayerOrientation.AutoSize = true;
            this.labelPlayerOrientation.Location = new System.Drawing.Point(33, 325);
            this.labelPlayerOrientation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPlayerOrientation.Name = "labelPlayerOrientation";
            this.labelPlayerOrientation.Size = new System.Drawing.Size(119, 17);
            this.labelPlayerOrientation.TabIndex = 17;
            this.labelPlayerOrientation.Text = "Player orientation";
            // 
            // trackBarPlayerOrientation
            // 
            this.trackBarPlayerOrientation.Location = new System.Drawing.Point(37, 345);
            this.trackBarPlayerOrientation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBarPlayerOrientation.Maximum = 360;
            this.trackBarPlayerOrientation.Name = "trackBarPlayerOrientation";
            this.trackBarPlayerOrientation.Size = new System.Drawing.Size(213, 56);
            this.trackBarPlayerOrientation.TabIndex = 18;
            this.trackBarPlayerOrientation.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarPlayerOrientation.ValueChanged += new System.EventHandler(this.trackBarPlayerOrientation_ValueChanged);
            // 
            // listBoxSprites
            // 
            this.listBoxSprites.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxSprites.FormattingEnabled = true;
            this.listBoxSprites.ItemHeight = 24;
            this.listBoxSprites.Location = new System.Drawing.Point(17, 384);
            this.listBoxSprites.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listBoxSprites.Name = "listBoxSprites";
            this.listBoxSprites.Size = new System.Drawing.Size(232, 148);
            this.listBoxSprites.TabIndex = 22;
            this.listBoxSprites.Click += new System.EventHandler(this.listBoxSprites_Click);
            // 
            // radioButtonSpriteIgnore
            // 
            this.radioButtonSpriteIgnore.AutoSize = true;
            this.radioButtonSpriteIgnore.Checked = true;
            this.radioButtonSpriteIgnore.Location = new System.Drawing.Point(8, 23);
            this.radioButtonSpriteIgnore.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonSpriteIgnore.Name = "radioButtonSpriteIgnore";
            this.radioButtonSpriteIgnore.Size = new System.Drawing.Size(76, 24);
            this.radioButtonSpriteIgnore.TabIndex = 23;
            this.radioButtonSpriteIgnore.TabStop = true;
            this.radioButtonSpriteIgnore.Text = "ignore";
            this.radioButtonSpriteIgnore.UseVisualStyleBackColor = true;
            // 
            // groupBoxRadioSprite
            // 
            this.groupBoxRadioSprite.Controls.Add(this.radioButtonSpriteDelete);
            this.groupBoxRadioSprite.Controls.Add(this.radioButtonSpriteDraw);
            this.groupBoxRadioSprite.Controls.Add(this.radioButtonSpriteIgnore);
            this.groupBoxRadioSprite.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxRadioSprite.Location = new System.Drawing.Point(17, 550);
            this.groupBoxRadioSprite.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxRadioSprite.Name = "groupBoxRadioSprite";
            this.groupBoxRadioSprite.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxRadioSprite.Size = new System.Drawing.Size(233, 122);
            this.groupBoxRadioSprite.TabIndex = 24;
            this.groupBoxRadioSprite.TabStop = false;
            this.groupBoxRadioSprite.Text = "Sprite Actions";
            // 
            // radioButtonSpriteDelete
            // 
            this.radioButtonSpriteDelete.AutoSize = true;
            this.radioButtonSpriteDelete.Location = new System.Drawing.Point(8, 87);
            this.radioButtonSpriteDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonSpriteDelete.Name = "radioButtonSpriteDelete";
            this.radioButtonSpriteDelete.Size = new System.Drawing.Size(75, 24);
            this.radioButtonSpriteDelete.TabIndex = 25;
            this.radioButtonSpriteDelete.Text = "delete";
            this.radioButtonSpriteDelete.UseVisualStyleBackColor = true;
            // 
            // radioButtonSpriteDraw
            // 
            this.radioButtonSpriteDraw.AutoSize = true;
            this.radioButtonSpriteDraw.Location = new System.Drawing.Point(8, 55);
            this.radioButtonSpriteDraw.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonSpriteDraw.Name = "radioButtonSpriteDraw";
            this.radioButtonSpriteDraw.Size = new System.Drawing.Size(66, 24);
            this.radioButtonSpriteDraw.TabIndex = 24;
            this.radioButtonSpriteDraw.Text = "draw";
            this.radioButtonSpriteDraw.UseVisualStyleBackColor = true;
            // 
            // listBoxGameTexture
            // 
            this.listBoxGameTexture.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxGameTexture.FormattingEnabled = true;
            this.listBoxGameTexture.ItemHeight = 24;
            this.listBoxGameTexture.Location = new System.Drawing.Point(17, 86);
            this.listBoxGameTexture.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listBoxGameTexture.Name = "listBoxGameTexture";
            this.listBoxGameTexture.Size = new System.Drawing.Size(232, 148);
            this.listBoxGameTexture.TabIndex = 25;
            this.listBoxGameTexture.Click += new System.EventHandler(this.listBoxTextures_Click);
            // 
            // FormMapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1569, 800);
            this.Controls.Add(this.listBoxGameTexture);
            this.Controls.Add(this.groupBoxRadioSprite);
            this.Controls.Add(this.listBoxSprites);
            this.Controls.Add(this.trackBarPlayerOrientation);
            this.Controls.Add(this.labelPlayerOrientation);
            this.Controls.Add(this.btnSetPlayerPosition);
            this.Controls.Add(this.pictureBoxMap);
            this.Controls.Add(this.txtMapName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownMapDimY);
            this.Controls.Add(this.numericUpDownMapDimX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChangeDim);
            this.Controls.Add(this.btnSaveMap);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormMapEditor";
            this.Text = "Map Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.FormMapEditor_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMapDimX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMapDimY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPlayerOrientation)).EndInit();
            this.groupBoxRadioSprite.ResumeLayout(false);
            this.groupBoxRadioSprite.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSaveMap;
        private System.Windows.Forms.Button btnChangeDim;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownMapDimX;
        private System.Windows.Forms.NumericUpDown numericUpDownMapDimY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox txtMapName;
        private System.Windows.Forms.PictureBox pictureBoxMap;
        private System.Windows.Forms.Button btnSetPlayerPosition;
        private System.Windows.Forms.Label labelPlayerOrientation;
        private System.Windows.Forms.TrackBar trackBarPlayerOrientation;
        private System.Windows.Forms.ListBox listBoxSprites;
        private System.Windows.Forms.RadioButton radioButtonSpriteIgnore;
        private System.Windows.Forms.GroupBox groupBoxRadioSprite;
        private System.Windows.Forms.RadioButton radioButtonSpriteDelete;
        private System.Windows.Forms.RadioButton radioButtonSpriteDraw;
        private System.Windows.Forms.ListBox listBoxGameTexture;
    }
}