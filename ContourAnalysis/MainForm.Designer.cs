namespace ContourAnalysis
{
    partial class MainForm
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
            this.ImageBox = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.BlobsCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogithechToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFamiliarContoursSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFindedContoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ShowAngleCheckBox = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.TextColorSelector = new System.Windows.Forms.ComboBox();
            this.ShowStageSelector = new System.Windows.Forms.ComboBox();
            this.MinMatchNSPSelector = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.ContoursLenghtSelector = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FilterBigCoupledCheckBox = new System.Windows.Forms.CheckBox();
            this.FilterSmallCoupledCheckBox = new System.Windows.Forms.CheckBox();
            this.MaxHeightSelector = new System.Windows.Forms.NumericUpDown();
            this.MaxWidthSelector = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.FilterBigBlobsCheckBox = new System.Windows.Forms.CheckBox();
            this.MinHeightSelector = new System.Windows.Forms.NumericUpDown();
            this.MinWidthSelector = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.FilterSmallBlobsCheckBox = new System.Windows.Forms.CheckBox();
            this.WindowSizeSelector = new System.Windows.Forms.NumericUpDown();
            this.PixelBrightnessLimitSelector = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ShowBoundingRectCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinMatchNSPSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContoursLenghtSelector)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxHeightSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxWidthSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinHeightSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinWidthSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindowSizeSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PixelBrightnessLimitSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageBox
            // 
            this.ImageBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImageBox.Location = new System.Drawing.Point(12, 27);
            this.ImageBox.Name = "ImageBox";
            this.ImageBox.Size = new System.Drawing.Size(640, 480);
            this.ImageBox.TabIndex = 1;
            this.ImageBox.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BlobsCountLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 521);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(914, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // BlobsCountLabel
            // 
            this.BlobsCountLabel.Name = "BlobsCountLabel";
            this.BlobsCountLabel.Size = new System.Drawing.Size(69, 17);
            this.BlobsCountLabel.Text = "BlobsCount";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.contoursToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(914, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDeviceToolStripMenuItem,
            this.openLogithechToolStripMenuItem,
            this.openPictureToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(81, 20);
            this.toolStripMenuItem1.Text = "Data source";
            // 
            // openDeviceToolStripMenuItem
            // 
            this.openDeviceToolStripMenuItem.Name = "openDeviceToolStripMenuItem";
            this.openDeviceToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.openDeviceToolStripMenuItem.Text = "Device ...";
            this.openDeviceToolStripMenuItem.Click += new System.EventHandler(this.OpenDeviceButton_Click);
            // 
            // openLogithechToolStripMenuItem
            // 
            this.openLogithechToolStripMenuItem.Name = "openLogithechToolStripMenuItem";
            this.openLogithechToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.openLogithechToolStripMenuItem.Text = "Picture ...";
            this.openLogithechToolStripMenuItem.Click += new System.EventHandler(this.OpenPictureButton_Click);
            // 
            // openPictureToolStripMenuItem
            // 
            this.openPictureToolStripMenuItem.Name = "openPictureToolStripMenuItem";
            this.openPictureToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.openPictureToolStripMenuItem.Text = "Logitech camera";
            this.openPictureToolStripMenuItem.Click += new System.EventHandler(this.OpenLogithechButton_Click);
            // 
            // contoursToolStripMenuItem
            // 
            this.contoursToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFamiliarContoursSetToolStripMenuItem,
            this.saveFindedContoursToolStripMenuItem});
            this.contoursToolStripMenuItem.Name = "contoursToolStripMenuItem";
            this.contoursToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.contoursToolStripMenuItem.Text = "Contours";
            // 
            // loadFamiliarContoursSetToolStripMenuItem
            // 
            this.loadFamiliarContoursSetToolStripMenuItem.Name = "loadFamiliarContoursSetToolStripMenuItem";
            this.loadFamiliarContoursSetToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.loadFamiliarContoursSetToolStripMenuItem.Text = "Load familiar";
            this.loadFamiliarContoursSetToolStripMenuItem.Click += new System.EventHandler(this.LoadFamiliarButton_Click);
            // 
            // saveFindedContoursToolStripMenuItem
            // 
            this.saveFindedContoursToolStripMenuItem.Name = "saveFindedContoursToolStripMenuItem";
            this.saveFindedContoursToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.saveFindedContoursToolStripMenuItem.Text = "Save finded";
            this.saveFindedContoursToolStripMenuItem.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ShowBoundingRectCheckBox);
            this.groupBox1.Controls.Add(this.ShowAngleCheckBox);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.TextColorSelector);
            this.groupBox1.Controls.Add(this.ShowStageSelector);
            this.groupBox1.Controls.Add(this.MinMatchNSPSelector);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.ContoursLenghtSelector);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.WindowSizeSelector);
            this.groupBox1.Controls.Add(this.PixelBrightnessLimitSelector);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(659, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 479);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // ShowAngleCheckBox
            // 
            this.ShowAngleCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ShowAngleCheckBox.Location = new System.Drawing.Point(6, 344);
            this.ShowAngleCheckBox.Name = "ShowAngleCheckBox";
            this.ShowAngleCheckBox.Size = new System.Drawing.Size(231, 17);
            this.ShowAngleCheckBox.TabIndex = 14;
            this.ShowAngleCheckBox.Text = "Show angle";
            this.ShowAngleCheckBox.UseVisualStyleBackColor = true;
            this.ShowAngleCheckBox.CheckedChanged += new System.EventHandler(this.CommonParams_Changed);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 322);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Text color";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 296);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Show";
            // 
            // TextColorSelector
            // 
            this.TextColorSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TextColorSelector.FormattingEnabled = true;
            this.TextColorSelector.Location = new System.Drawing.Point(83, 317);
            this.TextColorSelector.Name = "TextColorSelector";
            this.TextColorSelector.Size = new System.Drawing.Size(154, 21);
            this.TextColorSelector.TabIndex = 11;
            this.TextColorSelector.SelectedIndexChanged += new System.EventHandler(this.CommonParams_Changed);
            // 
            // ShowStageSelector
            // 
            this.ShowStageSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ShowStageSelector.FormattingEnabled = true;
            this.ShowStageSelector.Location = new System.Drawing.Point(61, 293);
            this.ShowStageSelector.Name = "ShowStageSelector";
            this.ShowStageSelector.Size = new System.Drawing.Size(176, 21);
            this.ShowStageSelector.TabIndex = 11;
            this.ShowStageSelector.SelectedIndexChanged += new System.EventHandler(this.CommonParams_Changed);
            // 
            // MinMatchNSPSelector
            // 
            this.MinMatchNSPSelector.DecimalPlaces = 2;
            this.MinMatchNSPSelector.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.MinMatchNSPSelector.Location = new System.Drawing.Point(176, 84);
            this.MinMatchNSPSelector.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MinMatchNSPSelector.Name = "MinMatchNSPSelector";
            this.MinMatchNSPSelector.Size = new System.Drawing.Size(61, 21);
            this.MinMatchNSPSelector.TabIndex = 10;
            this.MinMatchNSPSelector.Value = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            this.MinMatchNSPSelector.ValueChanged += new System.EventHandler(this.CommonParams_Changed);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(9, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Match NSP limit";
            // 
            // ContoursLenghtSelector
            // 
            this.ContoursLenghtSelector.Location = new System.Drawing.Point(176, 62);
            this.ContoursLenghtSelector.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.ContoursLenghtSelector.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.ContoursLenghtSelector.Name = "ContoursLenghtSelector";
            this.ContoursLenghtSelector.Size = new System.Drawing.Size(61, 21);
            this.ContoursLenghtSelector.TabIndex = 8;
            this.ContoursLenghtSelector.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.ContoursLenghtSelector.ValueChanged += new System.EventHandler(this.CommonParams_Changed);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 14);
            this.label7.TabIndex = 7;
            this.label7.Text = "Contours lenght";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.FilterBigCoupledCheckBox);
            this.groupBox2.Controls.Add(this.FilterSmallCoupledCheckBox);
            this.groupBox2.Controls.Add(this.MaxHeightSelector);
            this.groupBox2.Controls.Add(this.MaxWidthSelector);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.FilterBigBlobsCheckBox);
            this.groupBox2.Controls.Add(this.MinHeightSelector);
            this.groupBox2.Controls.Add(this.MinWidthSelector);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.FilterSmallBlobsCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(6, 116);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(231, 171);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Blob filter";
            // 
            // FilterBigCoupledCheckBox
            // 
            this.FilterBigCoupledCheckBox.AutoSize = true;
            this.FilterBigCoupledCheckBox.Location = new System.Drawing.Point(139, 120);
            this.FilterBigCoupledCheckBox.Name = "FilterBigCoupledCheckBox";
            this.FilterBigCoupledCheckBox.Size = new System.Drawing.Size(65, 17);
            this.FilterBigCoupledCheckBox.TabIndex = 10;
            this.FilterBigCoupledCheckBox.Text = "Coupled";
            this.FilterBigCoupledCheckBox.UseVisualStyleBackColor = true;
            this.FilterBigCoupledCheckBox.CheckedChanged += new System.EventHandler(this.PreprocessParams_Changed);
            // 
            // FilterSmallCoupledCheckBox
            // 
            this.FilterSmallCoupledCheckBox.AutoSize = true;
            this.FilterSmallCoupledCheckBox.Checked = true;
            this.FilterSmallCoupledCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FilterSmallCoupledCheckBox.Location = new System.Drawing.Point(139, 53);
            this.FilterSmallCoupledCheckBox.Name = "FilterSmallCoupledCheckBox";
            this.FilterSmallCoupledCheckBox.Size = new System.Drawing.Size(65, 17);
            this.FilterSmallCoupledCheckBox.TabIndex = 9;
            this.FilterSmallCoupledCheckBox.Text = "Coupled";
            this.FilterSmallCoupledCheckBox.UseVisualStyleBackColor = true;
            this.FilterSmallCoupledCheckBox.CheckedChanged += new System.EventHandler(this.PreprocessParams_Changed);
            // 
            // MaxHeightSelector
            // 
            this.MaxHeightSelector.Location = new System.Drawing.Point(67, 129);
            this.MaxHeightSelector.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.MaxHeightSelector.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.MaxHeightSelector.Name = "MaxHeightSelector";
            this.MaxHeightSelector.Size = new System.Drawing.Size(68, 21);
            this.MaxHeightSelector.TabIndex = 7;
            this.MaxHeightSelector.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.MaxHeightSelector.ValueChanged += new System.EventHandler(this.PreprocessParams_Changed);
            // 
            // MaxWidthSelector
            // 
            this.MaxWidthSelector.Location = new System.Drawing.Point(67, 106);
            this.MaxWidthSelector.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.MaxWidthSelector.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.MaxWidthSelector.Name = "MaxWidthSelector";
            this.MaxWidthSelector.Size = new System.Drawing.Size(68, 21);
            this.MaxWidthSelector.TabIndex = 8;
            this.MaxWidthSelector.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.MaxWidthSelector.ValueChanged += new System.EventHandler(this.PreprocessParams_Changed);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Max height";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Max width";
            // 
            // FilterBigBlobsCheckBox
            // 
            this.FilterBigBlobsCheckBox.AutoSize = true;
            this.FilterBigBlobsCheckBox.Location = new System.Drawing.Point(6, 88);
            this.FilterBigBlobsCheckBox.Name = "FilterBigBlobsCheckBox";
            this.FilterBigBlobsCheckBox.Size = new System.Drawing.Size(93, 17);
            this.FilterBigBlobsCheckBox.TabIndex = 4;
            this.FilterBigBlobsCheckBox.Text = "Filter big blobs";
            this.FilterBigBlobsCheckBox.UseVisualStyleBackColor = true;
            this.FilterBigBlobsCheckBox.CheckedChanged += new System.EventHandler(this.PreprocessParams_Changed);
            // 
            // MinHeightSelector
            // 
            this.MinHeightSelector.Location = new System.Drawing.Point(68, 62);
            this.MinHeightSelector.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MinHeightSelector.Name = "MinHeightSelector";
            this.MinHeightSelector.Size = new System.Drawing.Size(67, 21);
            this.MinHeightSelector.TabIndex = 3;
            this.MinHeightSelector.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.MinHeightSelector.ValueChanged += new System.EventHandler(this.PreprocessParams_Changed);
            // 
            // MinWidthSelector
            // 
            this.MinWidthSelector.Location = new System.Drawing.Point(68, 39);
            this.MinWidthSelector.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MinWidthSelector.Name = "MinWidthSelector";
            this.MinWidthSelector.Size = new System.Drawing.Size(67, 21);
            this.MinWidthSelector.TabIndex = 3;
            this.MinWidthSelector.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.MinWidthSelector.ValueChanged += new System.EventHandler(this.PreprocessParams_Changed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Min height";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Min width";
            // 
            // FilterSmallBlobsCheckBox
            // 
            this.FilterSmallBlobsCheckBox.AutoSize = true;
            this.FilterSmallBlobsCheckBox.Checked = true;
            this.FilterSmallBlobsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FilterSmallBlobsCheckBox.Location = new System.Drawing.Point(7, 21);
            this.FilterSmallBlobsCheckBox.Name = "FilterSmallBlobsCheckBox";
            this.FilterSmallBlobsCheckBox.Size = new System.Drawing.Size(102, 17);
            this.FilterSmallBlobsCheckBox.TabIndex = 0;
            this.FilterSmallBlobsCheckBox.Text = "Filter small blobs";
            this.FilterSmallBlobsCheckBox.UseVisualStyleBackColor = true;
            this.FilterSmallBlobsCheckBox.CheckedChanged += new System.EventHandler(this.PreprocessParams_Changed);
            // 
            // WindowSizeSelector
            // 
            this.WindowSizeSelector.Location = new System.Drawing.Point(176, 17);
            this.WindowSizeSelector.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.WindowSizeSelector.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.WindowSizeSelector.Name = "WindowSizeSelector";
            this.WindowSizeSelector.Size = new System.Drawing.Size(61, 21);
            this.WindowSizeSelector.TabIndex = 4;
            this.WindowSizeSelector.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.WindowSizeSelector.ValueChanged += new System.EventHandler(this.PreprocessParams_Changed);
            // 
            // PixelBrightnessLimitSelector
            // 
            this.PixelBrightnessLimitSelector.DecimalPlaces = 2;
            this.PixelBrightnessLimitSelector.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.PixelBrightnessLimitSelector.Location = new System.Drawing.Point(176, 40);
            this.PixelBrightnessLimitSelector.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.PixelBrightnessLimitSelector.Name = "PixelBrightnessLimitSelector";
            this.PixelBrightnessLimitSelector.Size = new System.Drawing.Size(61, 21);
            this.PixelBrightnessLimitSelector.TabIndex = 4;
            this.PixelBrightnessLimitSelector.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.PixelBrightnessLimitSelector.ValueChanged += new System.EventHandler(this.PreprocessParams_Changed);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "AT pixel brightness diff. limit";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Adaptive threshold (AT) window size";
            // 
            // ShowBoundingRectCheckBox
            // 
            this.ShowBoundingRectCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ShowBoundingRectCheckBox.Location = new System.Drawing.Point(6, 364);
            this.ShowBoundingRectCheckBox.Name = "ShowBoundingRectCheckBox";
            this.ShowBoundingRectCheckBox.Size = new System.Drawing.Size(231, 17);
            this.ShowBoundingRectCheckBox.TabIndex = 15;
            this.ShowBoundingRectCheckBox.Text = "Show bounding rectangle";
            this.ShowBoundingRectCheckBox.UseVisualStyleBackColor = true;
            this.ShowBoundingRectCheckBox.CheckedChanged += new System.EventHandler(this.CommonParams_Changed);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 543);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.ImageBox);
            this.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Contours analyzer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinMatchNSPSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContoursLenghtSelector)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxHeightSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxWidthSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinHeightSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinWidthSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindowSizeSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PixelBrightnessLimitSelector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ImageBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel BlobsCountLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLogithechToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPictureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFamiliarContoursSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFindedContoursToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown PixelBrightnessLimitSelector;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox FilterSmallCoupledCheckBox;
        private System.Windows.Forms.NumericUpDown MaxHeightSelector;
        private System.Windows.Forms.NumericUpDown MaxWidthSelector;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox FilterBigBlobsCheckBox;
        private System.Windows.Forms.NumericUpDown MinHeightSelector;
        private System.Windows.Forms.NumericUpDown MinWidthSelector;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox FilterSmallBlobsCheckBox;
        private System.Windows.Forms.CheckBox FilterBigCoupledCheckBox;
        private System.Windows.Forms.NumericUpDown WindowSizeSelector;
        private System.Windows.Forms.NumericUpDown ContoursLenghtSelector;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown MinMatchNSPSelector;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox ShowStageSelector;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox TextColorSelector;
        private System.Windows.Forms.CheckBox ShowAngleCheckBox;
        private System.Windows.Forms.CheckBox ShowBoundingRectCheckBox;
    }
}

