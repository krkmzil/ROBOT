namespace ROBOT
{
    partial class CameraSettingsPanel
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            labelTitle = new Label();
            labelCameraCaption = new Label();
            comboBoxCamera = new ComboBox();
            buttonConnect = new Button();
            buttonDisconnect = new Button();
            labelStatus = new Label();
            pictureBoxPreview = new PictureBox();
            labelBrightnessCaption = new Label();
            trackBarBrightness = new TrackBar();
            labelBrightnessValue = new Label();
            labelContrastCaption = new Label();
            trackBarContrast = new TrackBar();
            labelContrastValue = new Label();
            labelSharpnessCaption = new Label();
            trackBarSharpness = new TrackBar();
            labelSharpnessValue = new Label();
            labelSaturationCaption = new Label();
            trackBarSaturation = new TrackBar();
            labelSaturationValue = new Label();
            buttonResetDefaults = new Button();
            buttonSaveSettings = new Button();
            labelRoiTitle = new Label();
            buttonRoiUp = new Button();
            buttonRoiLeft = new Button();
            buttonRoiRight = new Button();
            buttonRoiDown = new Button();
            labelRoiWidthCaption = new Label();
            numericRoiWidth = new NumericUpDown();
            labelRoiHeightCaption = new Label();
            numericRoiHeight = new NumericUpDown();
            labelRoiPosition = new Label();
            labelObjectTitle = new Label();
            labelClassNameCaption = new Label();
            textBoxClassName = new TextBox();
            buttonAddSample = new Button();
            labelClassListCaption = new Label();
            listBoxClasses = new ListBox();
            buttonDeleteClass = new Button();
            labelShapeCaption = new Label();
            comboBoxShape = new ComboBox();
            labelThumbCaption = new Label();
            flowLayoutPanelSamples = new FlowLayoutPanel();
            buttonOpenDataset = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarSharpness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarSaturation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericRoiWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericRoiHeight).BeginInit();
            SuspendLayout();
            //
            // labelTitle
            //
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelTitle.ForeColor = Color.FromArgb(230, 232, 240);
            labelTitle.Location = new Point(20, 20);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(200, 30);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Kamera Ayarları";
            //
            // labelCameraCaption
            //
            labelCameraCaption.AutoSize = true;
            labelCameraCaption.ForeColor = Color.FromArgb(230, 232, 240);
            labelCameraCaption.Location = new Point(20, 68);
            labelCameraCaption.Name = "labelCameraCaption";
            labelCameraCaption.Size = new Size(52, 15);
            labelCameraCaption.TabIndex = 1;
            labelCameraCaption.Text = "Kamera:";
            //
            // comboBoxCamera
            //
            comboBoxCamera.BackColor = Color.FromArgb(38, 42, 61);
            comboBoxCamera.ForeColor = Color.FromArgb(230, 232, 240);
            comboBoxCamera.FlatStyle = FlatStyle.Flat;
            comboBoxCamera.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCamera.Location = new Point(80, 64);
            comboBoxCamera.Name = "comboBoxCamera";
            comboBoxCamera.Size = new Size(320, 23);
            comboBoxCamera.TabIndex = 2;
            //
            // buttonConnect
            //
            buttonConnect.BackColor = Color.FromArgb(76, 175, 80);
            buttonConnect.Cursor = Cursors.Hand;
            buttonConnect.FlatStyle = FlatStyle.Flat;
            buttonConnect.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonConnect.ForeColor = Color.White;
            buttonConnect.Location = new Point(410, 63);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(90, 25);
            buttonConnect.TabIndex = 3;
            buttonConnect.Text = "Bağlan";
            buttonConnect.UseVisualStyleBackColor = false;
            buttonConnect.FlatAppearance.BorderSize = 0;
            buttonConnect.Click += buttonConnect_Click;
            //
            // buttonDisconnect
            //
            buttonDisconnect.BackColor = Color.FromArgb(239, 83, 80);
            buttonDisconnect.Cursor = Cursors.Hand;
            buttonDisconnect.Enabled = false;
            buttonDisconnect.FlatStyle = FlatStyle.Flat;
            buttonDisconnect.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonDisconnect.ForeColor = Color.White;
            buttonDisconnect.Location = new Point(510, 63);
            buttonDisconnect.Name = "buttonDisconnect";
            buttonDisconnect.Size = new Size(120, 25);
            buttonDisconnect.TabIndex = 4;
            buttonDisconnect.Text = "Bağlantıyı Kes";
            buttonDisconnect.UseVisualStyleBackColor = false;
            buttonDisconnect.FlatAppearance.BorderSize = 0;
            buttonDisconnect.Click += buttonDisconnect_Click;
            //
            // labelStatus
            //
            labelStatus.AutoSize = true;
            labelStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelStatus.ForeColor = Color.FromArgb(239, 83, 80);
            labelStatus.Location = new Point(650, 68);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(70, 15);
            labelStatus.TabIndex = 5;
            labelStatus.Text = "Bağlı Değil";
            //
            // pictureBoxPreview
            //
            pictureBoxPreview.BackColor = Color.FromArgb(18, 19, 28);
            pictureBoxPreview.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxPreview.Location = new Point(20, 104);
            pictureBoxPreview.Name = "pictureBoxPreview";
            pictureBoxPreview.Size = new Size(640, 360);
            pictureBoxPreview.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPreview.TabIndex = 6;
            pictureBoxPreview.TabStop = false;
            //
            // labelBrightnessCaption
            //
            labelBrightnessCaption.AutoSize = true;
            labelBrightnessCaption.ForeColor = Color.FromArgb(230, 232, 240);
            labelBrightnessCaption.Location = new Point(20, 485);
            labelBrightnessCaption.Name = "labelBrightnessCaption";
            labelBrightnessCaption.Size = new Size(60, 15);
            labelBrightnessCaption.TabIndex = 7;
            labelBrightnessCaption.Text = "Parlaklık";
            //
            // trackBarBrightness
            //
            trackBarBrightness.BackColor = Color.FromArgb(27, 29, 42);
            trackBarBrightness.Enabled = false;
            trackBarBrightness.Location = new Point(160, 478);
            trackBarBrightness.Name = "trackBarBrightness";
            trackBarBrightness.Size = new Size(560, 45);
            trackBarBrightness.TabIndex = 8;
            trackBarBrightness.TickStyle = TickStyle.None;
            trackBarBrightness.Scroll += trackBarBrightness_Scroll;
            //
            // labelBrightnessValue
            //
            labelBrightnessValue.AutoSize = true;
            labelBrightnessValue.ForeColor = Color.FromArgb(0, 229, 255);
            labelBrightnessValue.Location = new Point(730, 485);
            labelBrightnessValue.Name = "labelBrightnessValue";
            labelBrightnessValue.Size = new Size(20, 15);
            labelBrightnessValue.TabIndex = 9;
            labelBrightnessValue.Text = "-";
            //
            // labelContrastCaption
            //
            labelContrastCaption.AutoSize = true;
            labelContrastCaption.ForeColor = Color.FromArgb(230, 232, 240);
            labelContrastCaption.Location = new Point(20, 540);
            labelContrastCaption.Name = "labelContrastCaption";
            labelContrastCaption.Size = new Size(58, 15);
            labelContrastCaption.TabIndex = 10;
            labelContrastCaption.Text = "Karşıtlık";
            //
            // trackBarContrast
            //
            trackBarContrast.BackColor = Color.FromArgb(27, 29, 42);
            trackBarContrast.Enabled = false;
            trackBarContrast.Location = new Point(160, 533);
            trackBarContrast.Name = "trackBarContrast";
            trackBarContrast.Size = new Size(560, 45);
            trackBarContrast.TabIndex = 11;
            trackBarContrast.TickStyle = TickStyle.None;
            trackBarContrast.Scroll += trackBarContrast_Scroll;
            //
            // labelContrastValue
            //
            labelContrastValue.AutoSize = true;
            labelContrastValue.ForeColor = Color.FromArgb(0, 229, 255);
            labelContrastValue.Location = new Point(730, 540);
            labelContrastValue.Name = "labelContrastValue";
            labelContrastValue.Size = new Size(20, 15);
            labelContrastValue.TabIndex = 12;
            labelContrastValue.Text = "-";
            //
            // labelSharpnessCaption
            //
            labelSharpnessCaption.AutoSize = true;
            labelSharpnessCaption.ForeColor = Color.FromArgb(230, 232, 240);
            labelSharpnessCaption.Location = new Point(20, 595);
            labelSharpnessCaption.Name = "labelSharpnessCaption";
            labelSharpnessCaption.Size = new Size(58, 15);
            labelSharpnessCaption.TabIndex = 13;
            labelSharpnessCaption.Text = "Keskinlik";
            //
            // trackBarSharpness
            //
            trackBarSharpness.BackColor = Color.FromArgb(27, 29, 42);
            trackBarSharpness.Enabled = false;
            trackBarSharpness.Location = new Point(160, 588);
            trackBarSharpness.Name = "trackBarSharpness";
            trackBarSharpness.Size = new Size(560, 45);
            trackBarSharpness.TabIndex = 14;
            trackBarSharpness.TickStyle = TickStyle.None;
            trackBarSharpness.Scroll += trackBarSharpness_Scroll;
            //
            // labelSharpnessValue
            //
            labelSharpnessValue.AutoSize = true;
            labelSharpnessValue.ForeColor = Color.FromArgb(0, 229, 255);
            labelSharpnessValue.Location = new Point(730, 595);
            labelSharpnessValue.Name = "labelSharpnessValue";
            labelSharpnessValue.Size = new Size(20, 15);
            labelSharpnessValue.TabIndex = 15;
            labelSharpnessValue.Text = "-";
            //
            // labelSaturationCaption
            //
            labelSaturationCaption.AutoSize = true;
            labelSaturationCaption.ForeColor = Color.FromArgb(230, 232, 240);
            labelSaturationCaption.Location = new Point(20, 650);
            labelSaturationCaption.Name = "labelSaturationCaption";
            labelSaturationCaption.Size = new Size(64, 15);
            labelSaturationCaption.TabIndex = 16;
            labelSaturationCaption.Text = "Doygunluk";
            //
            // trackBarSaturation
            //
            trackBarSaturation.BackColor = Color.FromArgb(27, 29, 42);
            trackBarSaturation.Enabled = false;
            trackBarSaturation.Location = new Point(160, 643);
            trackBarSaturation.Name = "trackBarSaturation";
            trackBarSaturation.Size = new Size(560, 45);
            trackBarSaturation.TabIndex = 17;
            trackBarSaturation.TickStyle = TickStyle.None;
            trackBarSaturation.Scroll += trackBarSaturation_Scroll;
            //
            // labelSaturationValue
            //
            labelSaturationValue.AutoSize = true;
            labelSaturationValue.ForeColor = Color.FromArgb(0, 229, 255);
            labelSaturationValue.Location = new Point(730, 650);
            labelSaturationValue.Name = "labelSaturationValue";
            labelSaturationValue.Size = new Size(20, 15);
            labelSaturationValue.TabIndex = 18;
            labelSaturationValue.Text = "-";
            //
            // buttonResetDefaults
            //
            buttonResetDefaults.BackColor = Color.FromArgb(91, 110, 245);
            buttonResetDefaults.Cursor = Cursors.Hand;
            buttonResetDefaults.Enabled = false;
            buttonResetDefaults.FlatStyle = FlatStyle.Flat;
            buttonResetDefaults.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonResetDefaults.ForeColor = Color.White;
            buttonResetDefaults.Location = new Point(160, 700);
            buttonResetDefaults.Name = "buttonResetDefaults";
            buttonResetDefaults.Size = new Size(200, 28);
            buttonResetDefaults.TabIndex = 19;
            buttonResetDefaults.Text = "Varsayılan Ayarlara Sıfırla";
            buttonResetDefaults.UseVisualStyleBackColor = false;
            buttonResetDefaults.FlatAppearance.BorderSize = 0;
            buttonResetDefaults.Click += buttonResetDefaults_Click;
            //
            // buttonSaveSettings
            //
            buttonSaveSettings.BackColor = Color.FromArgb(76, 175, 80);
            buttonSaveSettings.Cursor = Cursors.Hand;
            buttonSaveSettings.Enabled = false;
            buttonSaveSettings.FlatStyle = FlatStyle.Flat;
            buttonSaveSettings.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonSaveSettings.ForeColor = Color.White;
            buttonSaveSettings.Location = new Point(370, 700);
            buttonSaveSettings.Name = "buttonSaveSettings";
            buttonSaveSettings.Size = new Size(160, 28);
            buttonSaveSettings.TabIndex = 20;
            buttonSaveSettings.Text = "Ayarları Kaydet";
            buttonSaveSettings.UseVisualStyleBackColor = false;
            buttonSaveSettings.FlatAppearance.BorderSize = 0;
            buttonSaveSettings.Click += buttonSaveSettings_Click;
            //
            // labelRoiTitle
            //
            labelRoiTitle.AutoSize = true;
            labelRoiTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelRoiTitle.ForeColor = Color.FromArgb(230, 232, 240);
            labelRoiTitle.Location = new Point(680, 104);
            labelRoiTitle.Name = "labelRoiTitle";
            labelRoiTitle.Size = new Size(110, 19);
            labelRoiTitle.TabIndex = 21;
            labelRoiTitle.Text = "İlgi Alanı (ROI)";
            //
            // buttonRoiUp
            //
            buttonRoiUp.BackColor = Color.FromArgb(38, 42, 61);
            buttonRoiUp.Cursor = Cursors.Hand;
            buttonRoiUp.FlatStyle = FlatStyle.Flat;
            buttonRoiUp.ForeColor = Color.FromArgb(230, 232, 240);
            buttonRoiUp.Location = new Point(740, 135);
            buttonRoiUp.Name = "buttonRoiUp";
            buttonRoiUp.Size = new Size(40, 30);
            buttonRoiUp.TabIndex = 22;
            buttonRoiUp.Text = "▲";
            buttonRoiUp.UseVisualStyleBackColor = false;
            buttonRoiUp.FlatAppearance.BorderSize = 0;
            buttonRoiUp.Click += buttonRoiUp_Click;
            //
            // buttonRoiLeft
            //
            buttonRoiLeft.BackColor = Color.FromArgb(38, 42, 61);
            buttonRoiLeft.Cursor = Cursors.Hand;
            buttonRoiLeft.FlatStyle = FlatStyle.Flat;
            buttonRoiLeft.ForeColor = Color.FromArgb(230, 232, 240);
            buttonRoiLeft.Location = new Point(695, 170);
            buttonRoiLeft.Name = "buttonRoiLeft";
            buttonRoiLeft.Size = new Size(40, 30);
            buttonRoiLeft.TabIndex = 23;
            buttonRoiLeft.Text = "◀";
            buttonRoiLeft.UseVisualStyleBackColor = false;
            buttonRoiLeft.FlatAppearance.BorderSize = 0;
            buttonRoiLeft.Click += buttonRoiLeft_Click;
            //
            // buttonRoiRight
            //
            buttonRoiRight.BackColor = Color.FromArgb(38, 42, 61);
            buttonRoiRight.Cursor = Cursors.Hand;
            buttonRoiRight.FlatStyle = FlatStyle.Flat;
            buttonRoiRight.ForeColor = Color.FromArgb(230, 232, 240);
            buttonRoiRight.Location = new Point(785, 170);
            buttonRoiRight.Name = "buttonRoiRight";
            buttonRoiRight.Size = new Size(40, 30);
            buttonRoiRight.TabIndex = 24;
            buttonRoiRight.Text = "▶";
            buttonRoiRight.UseVisualStyleBackColor = false;
            buttonRoiRight.FlatAppearance.BorderSize = 0;
            buttonRoiRight.Click += buttonRoiRight_Click;
            //
            // buttonRoiDown
            //
            buttonRoiDown.BackColor = Color.FromArgb(38, 42, 61);
            buttonRoiDown.Cursor = Cursors.Hand;
            buttonRoiDown.FlatStyle = FlatStyle.Flat;
            buttonRoiDown.ForeColor = Color.FromArgb(230, 232, 240);
            buttonRoiDown.Location = new Point(740, 205);
            buttonRoiDown.Name = "buttonRoiDown";
            buttonRoiDown.Size = new Size(40, 30);
            buttonRoiDown.TabIndex = 25;
            buttonRoiDown.Text = "▼";
            buttonRoiDown.UseVisualStyleBackColor = false;
            buttonRoiDown.FlatAppearance.BorderSize = 0;
            buttonRoiDown.Click += buttonRoiDown_Click;
            //
            // labelRoiWidthCaption
            //
            labelRoiWidthCaption.AutoSize = true;
            labelRoiWidthCaption.ForeColor = Color.FromArgb(230, 232, 240);
            labelRoiWidthCaption.Location = new Point(680, 260);
            labelRoiWidthCaption.Name = "labelRoiWidthCaption";
            labelRoiWidthCaption.Size = new Size(58, 15);
            labelRoiWidthCaption.TabIndex = 26;
            labelRoiWidthCaption.Text = "Genişlik:";
            //
            // numericRoiWidth
            //
            numericRoiWidth.BackColor = Color.FromArgb(38, 42, 61);
            numericRoiWidth.ForeColor = Color.FromArgb(0, 229, 255);
            numericRoiWidth.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numericRoiWidth.Location = new Point(770, 257);
            numericRoiWidth.Maximum = new decimal(new int[] { 4000, 0, 0, 0 });
            numericRoiWidth.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericRoiWidth.Name = "numericRoiWidth";
            numericRoiWidth.Size = new Size(90, 23);
            numericRoiWidth.TabIndex = 27;
            numericRoiWidth.Value = new decimal(new int[] { 320, 0, 0, 0 });
            numericRoiWidth.ValueChanged += numericRoiWidth_ValueChanged;
            //
            // labelRoiHeightCaption
            //
            labelRoiHeightCaption.AutoSize = true;
            labelRoiHeightCaption.ForeColor = Color.FromArgb(230, 232, 240);
            labelRoiHeightCaption.Location = new Point(680, 290);
            labelRoiHeightCaption.Name = "labelRoiHeightCaption";
            labelRoiHeightCaption.Size = new Size(66, 15);
            labelRoiHeightCaption.TabIndex = 28;
            labelRoiHeightCaption.Text = "Yükseklik:";
            //
            // numericRoiHeight
            //
            numericRoiHeight.BackColor = Color.FromArgb(38, 42, 61);
            numericRoiHeight.ForeColor = Color.FromArgb(0, 229, 255);
            numericRoiHeight.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numericRoiHeight.Location = new Point(770, 287);
            numericRoiHeight.Maximum = new decimal(new int[] { 4000, 0, 0, 0 });
            numericRoiHeight.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericRoiHeight.Name = "numericRoiHeight";
            numericRoiHeight.Size = new Size(90, 23);
            numericRoiHeight.TabIndex = 29;
            numericRoiHeight.Value = new decimal(new int[] { 240, 0, 0, 0 });
            numericRoiHeight.ValueChanged += numericRoiHeight_ValueChanged;
            //
            // labelRoiPosition
            //
            labelRoiPosition.AutoSize = true;
            labelRoiPosition.ForeColor = Color.FromArgb(174, 180, 199);
            labelRoiPosition.Location = new Point(680, 325);
            labelRoiPosition.Name = "labelRoiPosition";
            labelRoiPosition.Size = new Size(110, 15);
            labelRoiPosition.TabIndex = 30;
            labelRoiPosition.Text = "Konum: X=0 Y=0";
            //
            // labelObjectTitle
            //
            labelObjectTitle.AutoSize = true;
            labelObjectTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelObjectTitle.ForeColor = Color.FromArgb(230, 232, 240);
            labelObjectTitle.Location = new Point(910, 104);
            labelObjectTitle.Name = "labelObjectTitle";
            labelObjectTitle.Size = new Size(140, 21);
            labelObjectTitle.TabIndex = 31;
            labelObjectTitle.Text = "Nesne Tanıma";
            //
            // labelClassNameCaption
            //
            labelClassNameCaption.AutoSize = true;
            labelClassNameCaption.ForeColor = Color.FromArgb(230, 232, 240);
            labelClassNameCaption.Location = new Point(910, 140);
            labelClassNameCaption.Name = "labelClassNameCaption";
            labelClassNameCaption.Size = new Size(72, 15);
            labelClassNameCaption.TabIndex = 32;
            labelClassNameCaption.Text = "Nesne adı:";
            //
            // textBoxClassName
            //
            textBoxClassName.BackColor = Color.FromArgb(38, 42, 61);
            textBoxClassName.BorderStyle = BorderStyle.FixedSingle;
            textBoxClassName.ForeColor = Color.FromArgb(230, 232, 240);
            textBoxClassName.Location = new Point(910, 158);
            textBoxClassName.Name = "textBoxClassName";
            textBoxClassName.Size = new Size(230, 23);
            textBoxClassName.TabIndex = 33;
            //
            // buttonAddSample
            //
            buttonAddSample.BackColor = Color.FromArgb(91, 110, 245);
            buttonAddSample.Cursor = Cursors.Hand;
            buttonAddSample.FlatStyle = FlatStyle.Flat;
            buttonAddSample.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonAddSample.ForeColor = Color.White;
            buttonAddSample.Location = new Point(910, 240);
            buttonAddSample.Name = "buttonAddSample";
            buttonAddSample.Size = new Size(230, 28);
            buttonAddSample.TabIndex = 34;
            buttonAddSample.Text = "Örnek Ekle";
            buttonAddSample.UseVisualStyleBackColor = false;
            buttonAddSample.FlatAppearance.BorderSize = 0;
            buttonAddSample.Click += buttonAddSample_Click;
            //
            // labelClassListCaption
            //
            labelClassListCaption.AutoSize = true;
            labelClassListCaption.ForeColor = Color.FromArgb(174, 180, 199);
            labelClassListCaption.Location = new Point(910, 280);
            labelClassListCaption.Name = "labelClassListCaption";
            labelClassListCaption.Size = new Size(122, 15);
            labelClassListCaption.TabIndex = 35;
            labelClassListCaption.Text = "Öğretilen nesneler:";
            //
            // listBoxClasses
            //
            listBoxClasses.BackColor = Color.FromArgb(38, 42, 61);
            listBoxClasses.BorderStyle = BorderStyle.FixedSingle;
            listBoxClasses.ForeColor = Color.FromArgb(230, 232, 240);
            listBoxClasses.ItemHeight = 15;
            listBoxClasses.Location = new Point(910, 298);
            listBoxClasses.Name = "listBoxClasses";
            listBoxClasses.Size = new Size(230, 90);
            listBoxClasses.TabIndex = 36;
            //
            // buttonDeleteClass
            //
            buttonDeleteClass.BackColor = Color.FromArgb(239, 83, 80);
            buttonDeleteClass.Cursor = Cursors.Hand;
            buttonDeleteClass.FlatStyle = FlatStyle.Flat;
            buttonDeleteClass.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonDeleteClass.ForeColor = Color.White;
            buttonDeleteClass.Location = new Point(910, 394);
            buttonDeleteClass.Name = "buttonDeleteClass";
            buttonDeleteClass.Size = new Size(230, 28);
            buttonDeleteClass.TabIndex = 37;
            buttonDeleteClass.Text = "Seçileni Sil";
            buttonDeleteClass.UseVisualStyleBackColor = false;
            buttonDeleteClass.FlatAppearance.BorderSize = 0;
            buttonDeleteClass.Click += buttonDeleteClass_Click;
            //
            // labelShapeCaption
            //
            labelShapeCaption.AutoSize = true;
            labelShapeCaption.ForeColor = Color.FromArgb(230, 232, 240);
            labelShapeCaption.Location = new Point(910, 190);
            labelShapeCaption.Name = "labelShapeCaption";
            labelShapeCaption.Size = new Size(100, 15);
            labelShapeCaption.TabIndex = 38;
            labelShapeCaption.Text = "Çerçeve şekli:";
            //
            // comboBoxShape
            //
            comboBoxShape.BackColor = Color.FromArgb(38, 42, 61);
            comboBoxShape.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxShape.FlatStyle = FlatStyle.Flat;
            comboBoxShape.ForeColor = Color.FromArgb(230, 232, 240);
            comboBoxShape.Items.AddRange(new object[] { "Otomatik (nesnenin şekline göre)", "Daire", "Dikdörtgen (nesnenin açısına göre)" });
            comboBoxShape.Location = new Point(910, 208);
            comboBoxShape.Name = "comboBoxShape";
            comboBoxShape.Size = new Size(230, 23);
            comboBoxShape.TabIndex = 39;
            comboBoxShape.SelectedIndex = 0;
            //
            // labelThumbCaption
            //
            labelThumbCaption.ForeColor = Color.FromArgb(174, 180, 199);
            labelThumbCaption.Location = new Point(910, 432);
            labelThumbCaption.Name = "labelThumbCaption";
            labelThumbCaption.Size = new Size(240, 18);
            labelThumbCaption.TabIndex = 40;
            labelThumbCaption.Text = "Eklenen örnekler (büyütmek için tıkla):";
            //
            // flowLayoutPanelSamples
            //
            flowLayoutPanelSamples.AutoScroll = true;
            flowLayoutPanelSamples.BackColor = Color.FromArgb(32, 34, 50);
            flowLayoutPanelSamples.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanelSamples.Location = new Point(910, 452);
            flowLayoutPanelSamples.Name = "flowLayoutPanelSamples";
            flowLayoutPanelSamples.Size = new Size(240, 246);
            flowLayoutPanelSamples.TabIndex = 41;
            //
            // buttonOpenDataset
            //
            buttonOpenDataset.BackColor = Color.FromArgb(38, 198, 218);
            buttonOpenDataset.Cursor = Cursors.Hand;
            buttonOpenDataset.FlatStyle = FlatStyle.Flat;
            buttonOpenDataset.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonOpenDataset.ForeColor = Color.White;
            buttonOpenDataset.Location = new Point(910, 708);
            buttonOpenDataset.Name = "buttonOpenDataset";
            buttonOpenDataset.Size = new Size(240, 28);
            buttonOpenDataset.TabIndex = 42;
            buttonOpenDataset.Text = "Veri Setini Aç (klasör)";
            buttonOpenDataset.UseVisualStyleBackColor = false;
            buttonOpenDataset.FlatAppearance.BorderSize = 0;
            buttonOpenDataset.Click += buttonOpenDataset_Click;
            //
            // CameraSettingsPanel
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 29, 42);
            Size = new Size(1180, 760);
            Controls.Add(labelTitle);
            Controls.Add(labelCameraCaption);
            Controls.Add(comboBoxCamera);
            Controls.Add(buttonConnect);
            Controls.Add(buttonDisconnect);
            Controls.Add(labelStatus);
            Controls.Add(pictureBoxPreview);
            Controls.Add(labelBrightnessCaption);
            Controls.Add(trackBarBrightness);
            Controls.Add(labelBrightnessValue);
            Controls.Add(labelContrastCaption);
            Controls.Add(trackBarContrast);
            Controls.Add(labelContrastValue);
            Controls.Add(labelSharpnessCaption);
            Controls.Add(trackBarSharpness);
            Controls.Add(labelSharpnessValue);
            Controls.Add(labelSaturationCaption);
            Controls.Add(trackBarSaturation);
            Controls.Add(labelSaturationValue);
            Controls.Add(buttonResetDefaults);
            Controls.Add(buttonSaveSettings);
            Controls.Add(labelRoiTitle);
            Controls.Add(buttonRoiUp);
            Controls.Add(buttonRoiLeft);
            Controls.Add(buttonRoiRight);
            Controls.Add(buttonRoiDown);
            Controls.Add(labelRoiWidthCaption);
            Controls.Add(numericRoiWidth);
            Controls.Add(labelRoiHeightCaption);
            Controls.Add(numericRoiHeight);
            Controls.Add(labelRoiPosition);
            Controls.Add(labelObjectTitle);
            Controls.Add(labelClassNameCaption);
            Controls.Add(textBoxClassName);
            Controls.Add(buttonAddSample);
            Controls.Add(labelClassListCaption);
            Controls.Add(listBoxClasses);
            Controls.Add(buttonDeleteClass);
            Controls.Add(labelShapeCaption);
            Controls.Add(comboBoxShape);
            Controls.Add(labelThumbCaption);
            Controls.Add(flowLayoutPanelSamples);
            Controls.Add(buttonOpenDataset);
            Font = new Font("Segoe UI", 9F);
            Name = "CameraSettingsPanel";
            Load += CameraSettingsPanel_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarSharpness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarSaturation).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericRoiWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericRoiHeight).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label labelTitle;
        private Label labelCameraCaption;
        private ComboBox comboBoxCamera;
        private Button buttonConnect;
        private Button buttonDisconnect;
        private Label labelStatus;
        private PictureBox pictureBoxPreview;
        private Label labelBrightnessCaption;
        private TrackBar trackBarBrightness;
        private Label labelBrightnessValue;
        private Label labelContrastCaption;
        private TrackBar trackBarContrast;
        private Label labelContrastValue;
        private Label labelSharpnessCaption;
        private TrackBar trackBarSharpness;
        private Label labelSharpnessValue;
        private Label labelSaturationCaption;
        private TrackBar trackBarSaturation;
        private Label labelSaturationValue;
        private Button buttonResetDefaults;
        private Button buttonSaveSettings;
        private Label labelRoiTitle;
        private Button buttonRoiUp;
        private Button buttonRoiLeft;
        private Button buttonRoiRight;
        private Button buttonRoiDown;
        private Label labelRoiWidthCaption;
        private NumericUpDown numericRoiWidth;
        private Label labelRoiHeightCaption;
        private NumericUpDown numericRoiHeight;
        private Label labelRoiPosition;
        private Label labelObjectTitle;
        private Label labelClassNameCaption;
        private TextBox textBoxClassName;
        private Button buttonAddSample;
        private Label labelClassListCaption;
        private ListBox listBoxClasses;
        private Button buttonDeleteClass;
        private Label labelShapeCaption;
        private ComboBox comboBoxShape;
        private Label labelThumbCaption;
        private FlowLayoutPanel flowLayoutPanelSamples;
        private Button buttonOpenDataset;
    }
}
