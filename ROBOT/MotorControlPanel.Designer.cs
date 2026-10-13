namespace ROBOT
{
    partial class MotorControlPanel
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelPort = new Label();
            comboBoxPort = new ComboBox();
            buttonConnect = new Button();
            buttonDisconnect = new Button();
            labelStatus = new Label();
            labelAlarm = new Label();
            labelDroXCaption = new Label();
            labelDroXValue = new Label();
            buttonZeroX = new Button();
            labelDroYCaption = new Label();
            labelDroYValue = new Label();
            buttonZeroY = new Button();
            labelDroZCaption = new Label();
            labelDroZValue = new Label();
            buttonZeroZ = new Button();
            labelJogTitle = new Label();
            labelStepTitle = new Label();
            buttonStep01 = new Button();
            buttonStep1 = new Button();
            buttonStep10 = new Button();
            buttonJogYPlus = new Button();
            buttonJogXMinus = new Button();
            labelJogStepCenter = new Label();
            buttonJogXPlus = new Button();
            buttonJogYMinus = new Button();
            buttonJogZPlus = new Button();
            buttonJogZMinus = new Button();
            labelCustomStep = new Label();
            textBoxCustomStep = new TextBox();
            textBoxGcode = new TextBox();
            buttonSend = new Button();
            buttonStatus = new Button();
            buttonHome = new Button();
            labelBatch = new Label();
            textBoxBatch = new TextBox();
            buttonSendBatch = new Button();
            textBoxLog = new TextBox();
            labelDroBCaption = new Label();
            labelDroBValue = new Label();
            buttonZeroB = new Button();
            buttonJogBMinus = new Button();
            buttonJogBPlus = new Button();
            buttonBFullTurn = new Button();
            SuspendLayout();
            //
            // labelPort
            //
            labelPort.AutoSize = true;
            labelPort.ForeColor = Color.FromArgb(230, 232, 240);
            labelPort.Location = new Point(12, 15);
            labelPort.Name = "labelPort";
            labelPort.Size = new Size(32, 15);
            labelPort.TabIndex = 0;
            labelPort.Text = "Port:";
            //
            // comboBoxPort
            //
            comboBoxPort.BackColor = Color.FromArgb(38, 42, 61);
            comboBoxPort.ForeColor = Color.FromArgb(230, 232, 240);
            comboBoxPort.FlatStyle = FlatStyle.Flat;
            comboBoxPort.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPort.Location = new Point(60, 12);
            comboBoxPort.Name = "comboBoxPort";
            comboBoxPort.Size = new Size(150, 23);
            comboBoxPort.TabIndex = 1;
            //
            // buttonConnect
            //
            buttonConnect.BackColor = Color.FromArgb(76, 175, 80);
            buttonConnect.Cursor = Cursors.Hand;
            buttonConnect.FlatStyle = FlatStyle.Flat;
            buttonConnect.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonConnect.ForeColor = Color.White;
            buttonConnect.Location = new Point(220, 11);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(90, 25);
            buttonConnect.TabIndex = 2;
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
            buttonDisconnect.Location = new Point(320, 11);
            buttonDisconnect.Name = "buttonDisconnect";
            buttonDisconnect.Size = new Size(120, 25);
            buttonDisconnect.TabIndex = 3;
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
            labelStatus.Location = new Point(460, 16);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(63, 15);
            labelStatus.TabIndex = 4;
            labelStatus.Text = "Bağlı Değil";
            //
            // labelAlarm
            //
            labelAlarm.AutoSize = true;
            labelAlarm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelAlarm.ForeColor = Color.FromArgb(255, 87, 34);
            labelAlarm.Location = new Point(560, 16);
            labelAlarm.Name = "labelAlarm";
            labelAlarm.Size = new Size(0, 15);
            labelAlarm.TabIndex = 14;
            //
            // textBoxGcode
            //
            textBoxGcode.BackColor = Color.FromArgb(38, 42, 61);
            textBoxGcode.BorderStyle = BorderStyle.FixedSingle;
            textBoxGcode.Font = new Font("Consolas", 9.5F);
            textBoxGcode.ForeColor = Color.FromArgb(230, 232, 240);
            textBoxGcode.Location = new Point(12, 55);
            textBoxGcode.Name = "textBoxGcode";
            textBoxGcode.Size = new Size(400, 23);
            textBoxGcode.TabIndex = 5;
            textBoxGcode.KeyDown += textBoxGcode_KeyDown;
            //
            // buttonSend
            //
            buttonSend.BackColor = Color.FromArgb(91, 110, 245);
            buttonSend.Cursor = Cursors.Hand;
            buttonSend.FlatStyle = FlatStyle.Flat;
            buttonSend.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonSend.ForeColor = Color.White;
            buttonSend.Location = new Point(420, 54);
            buttonSend.Name = "buttonSend";
            buttonSend.Size = new Size(90, 25);
            buttonSend.TabIndex = 6;
            buttonSend.Text = "Gönder";
            buttonSend.UseVisualStyleBackColor = false;
            buttonSend.FlatAppearance.BorderSize = 0;
            buttonSend.Click += buttonSend_Click;
            //
            // buttonStatus
            //
            buttonStatus.BackColor = Color.FromArgb(38, 198, 218);
            buttonStatus.Cursor = Cursors.Hand;
            buttonStatus.FlatStyle = FlatStyle.Flat;
            buttonStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonStatus.ForeColor = Color.White;
            buttonStatus.Location = new Point(520, 54);
            buttonStatus.Name = "buttonStatus";
            buttonStatus.Size = new Size(140, 25);
            buttonStatus.TabIndex = 8;
            buttonStatus.Text = "Pozisyon Sorgula";
            buttonStatus.UseVisualStyleBackColor = false;
            buttonStatus.FlatAppearance.BorderSize = 0;
            buttonStatus.Click += buttonStatus_Click;
            //
            // buttonHome
            //
            buttonHome.BackColor = Color.FromArgb(255, 167, 38);
            buttonHome.Cursor = Cursors.Hand;
            buttonHome.FlatStyle = FlatStyle.Flat;
            buttonHome.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonHome.ForeColor = Color.White;
            buttonHome.Location = new Point(670, 54);
            buttonHome.Name = "buttonHome";
            buttonHome.Size = new Size(100, 25);
            buttonHome.TabIndex = 9;
            buttonHome.Text = "HOME";
            buttonHome.UseVisualStyleBackColor = false;
            buttonHome.FlatAppearance.BorderSize = 0;
            buttonHome.Click += buttonHome_Click;
            //
            // labelBatch
            //
            labelBatch.AutoSize = true;
            labelBatch.ForeColor = Color.FromArgb(174, 180, 199);
            labelBatch.Location = new Point(12, 92);
            labelBatch.Name = "labelBatch";
            labelBatch.Size = new Size(280, 15);
            labelBatch.TabIndex = 10;
            labelBatch.Text = "Toplu G-code (her satır sırayla gönderilir):";
            //
            // textBoxBatch
            //
            textBoxBatch.BackColor = Color.FromArgb(38, 42, 61);
            textBoxBatch.BorderStyle = BorderStyle.FixedSingle;
            textBoxBatch.Font = new Font("Consolas", 9.5F);
            textBoxBatch.ForeColor = Color.FromArgb(230, 232, 240);
            textBoxBatch.Location = new Point(12, 110);
            textBoxBatch.Multiline = true;
            textBoxBatch.Name = "textBoxBatch";
            textBoxBatch.ScrollBars = ScrollBars.Vertical;
            textBoxBatch.Size = new Size(600, 90);
            textBoxBatch.TabIndex = 11;
            //
            // buttonSendBatch
            //
            buttonSendBatch.BackColor = Color.FromArgb(91, 110, 245);
            buttonSendBatch.Cursor = Cursors.Hand;
            buttonSendBatch.FlatStyle = FlatStyle.Flat;
            buttonSendBatch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonSendBatch.ForeColor = Color.White;
            buttonSendBatch.Location = new Point(620, 110);
            buttonSendBatch.Name = "buttonSendBatch";
            buttonSendBatch.Size = new Size(150, 90);
            buttonSendBatch.TabIndex = 12;
            buttonSendBatch.Text = "Toplu Gönder";
            buttonSendBatch.UseVisualStyleBackColor = false;
            buttonSendBatch.FlatAppearance.BorderSize = 0;
            buttonSendBatch.Click += buttonSendBatch_Click;
            //
            // textBoxLog
            //
            textBoxLog.BackColor = Color.FromArgb(18, 19, 28);
            textBoxLog.BorderStyle = BorderStyle.FixedSingle;
            textBoxLog.Font = new Font("Consolas", 9.5F);
            textBoxLog.ForeColor = Color.FromArgb(140, 230, 160);
            textBoxLog.Location = new Point(12, 215);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ReadOnly = true;
            textBoxLog.ScrollBars = ScrollBars.Vertical;
            textBoxLog.Size = new Size(760, 530);
            textBoxLog.TabIndex = 7;
            textBoxLog.TabStop = false;
            //
            // labelDroXCaption
            //
            labelDroXCaption.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelDroXCaption.ForeColor = Color.FromArgb(230, 232, 240);
            labelDroXCaption.Location = new Point(790, 55);
            labelDroXCaption.Name = "labelDroXCaption";
            labelDroXCaption.Size = new Size(24, 30);
            labelDroXCaption.TabIndex = 15;
            labelDroXCaption.Text = "X";
            labelDroXCaption.TextAlign = ContentAlignment.MiddleCenter;
            //
            // labelDroXValue
            //
            labelDroXValue.BackColor = Color.FromArgb(18, 19, 28);
            labelDroXValue.BorderStyle = BorderStyle.FixedSingle;
            labelDroXValue.Font = new Font("Consolas", 16F, FontStyle.Bold);
            labelDroXValue.ForeColor = Color.FromArgb(0, 229, 255);
            labelDroXValue.Location = new Point(790, 88);
            labelDroXValue.Name = "labelDroXValue";
            labelDroXValue.Size = new Size(190, 40);
            labelDroXValue.TabIndex = 16;
            labelDroXValue.Text = "0.000";
            labelDroXValue.TextAlign = ContentAlignment.MiddleRight;
            //
            // buttonZeroX
            //
            buttonZeroX.BackColor = Color.FromArgb(91, 110, 245);
            buttonZeroX.Cursor = Cursors.Hand;
            buttonZeroX.FlatStyle = FlatStyle.Flat;
            buttonZeroX.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            buttonZeroX.ForeColor = Color.White;
            buttonZeroX.Location = new Point(790, 130);
            buttonZeroX.Name = "buttonZeroX";
            buttonZeroX.Size = new Size(190, 28);
            buttonZeroX.TabIndex = 17;
            buttonZeroX.Text = "Zero X";
            buttonZeroX.UseVisualStyleBackColor = false;
            buttonZeroX.FlatAppearance.BorderSize = 0;
            buttonZeroX.Click += buttonZeroX_Click;
            //
            // labelDroYCaption
            //
            labelDroYCaption.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelDroYCaption.ForeColor = Color.FromArgb(230, 232, 240);
            labelDroYCaption.Location = new Point(790, 175);
            labelDroYCaption.Name = "labelDroYCaption";
            labelDroYCaption.Size = new Size(24, 30);
            labelDroYCaption.TabIndex = 18;
            labelDroYCaption.Text = "Y";
            labelDroYCaption.TextAlign = ContentAlignment.MiddleCenter;
            //
            // labelDroYValue
            //
            labelDroYValue.BackColor = Color.FromArgb(18, 19, 28);
            labelDroYValue.BorderStyle = BorderStyle.FixedSingle;
            labelDroYValue.Font = new Font("Consolas", 16F, FontStyle.Bold);
            labelDroYValue.ForeColor = Color.FromArgb(0, 229, 255);
            labelDroYValue.Location = new Point(790, 208);
            labelDroYValue.Name = "labelDroYValue";
            labelDroYValue.Size = new Size(190, 40);
            labelDroYValue.TabIndex = 19;
            labelDroYValue.Text = "0.000";
            labelDroYValue.TextAlign = ContentAlignment.MiddleRight;
            //
            // buttonZeroY
            //
            buttonZeroY.BackColor = Color.FromArgb(91, 110, 245);
            buttonZeroY.Cursor = Cursors.Hand;
            buttonZeroY.FlatStyle = FlatStyle.Flat;
            buttonZeroY.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            buttonZeroY.ForeColor = Color.White;
            buttonZeroY.Location = new Point(790, 250);
            buttonZeroY.Name = "buttonZeroY";
            buttonZeroY.Size = new Size(190, 28);
            buttonZeroY.TabIndex = 20;
            buttonZeroY.Text = "Zero Y";
            buttonZeroY.UseVisualStyleBackColor = false;
            buttonZeroY.FlatAppearance.BorderSize = 0;
            buttonZeroY.Click += buttonZeroY_Click;
            //
            // labelDroZCaption
            //
            labelDroZCaption.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelDroZCaption.ForeColor = Color.FromArgb(230, 232, 240);
            labelDroZCaption.Location = new Point(790, 295);
            labelDroZCaption.Name = "labelDroZCaption";
            labelDroZCaption.Size = new Size(24, 30);
            labelDroZCaption.TabIndex = 21;
            labelDroZCaption.Text = "Z";
            labelDroZCaption.TextAlign = ContentAlignment.MiddleCenter;
            //
            // labelDroZValue
            //
            labelDroZValue.BackColor = Color.FromArgb(18, 19, 28);
            labelDroZValue.BorderStyle = BorderStyle.FixedSingle;
            labelDroZValue.Font = new Font("Consolas", 16F, FontStyle.Bold);
            labelDroZValue.ForeColor = Color.FromArgb(0, 229, 255);
            labelDroZValue.Location = new Point(790, 328);
            labelDroZValue.Name = "labelDroZValue";
            labelDroZValue.Size = new Size(190, 40);
            labelDroZValue.TabIndex = 22;
            labelDroZValue.Text = "0.000";
            labelDroZValue.TextAlign = ContentAlignment.MiddleRight;
            //
            // buttonZeroZ
            //
            buttonZeroZ.BackColor = Color.FromArgb(91, 110, 245);
            buttonZeroZ.Cursor = Cursors.Hand;
            buttonZeroZ.FlatStyle = FlatStyle.Flat;
            buttonZeroZ.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            buttonZeroZ.ForeColor = Color.White;
            buttonZeroZ.Location = new Point(790, 370);
            buttonZeroZ.Name = "buttonZeroZ";
            buttonZeroZ.Size = new Size(190, 28);
            buttonZeroZ.TabIndex = 23;
            buttonZeroZ.Text = "Zero Z";
            buttonZeroZ.UseVisualStyleBackColor = false;
            buttonZeroZ.FlatAppearance.BorderSize = 0;
            buttonZeroZ.Click += buttonZeroZ_Click;
            //
            // labelJogTitle
            //
            labelJogTitle.AutoSize = true;
            labelJogTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelJogTitle.ForeColor = Color.FromArgb(230, 232, 240);
            labelJogTitle.Location = new Point(790, 405);
            labelJogTitle.Name = "labelJogTitle";
            labelJogTitle.Size = new Size(37, 20);
            labelJogTitle.TabIndex = 24;
            labelJogTitle.Text = "JOG";
            //
            // labelStepTitle
            //
            labelStepTitle.AutoSize = true;
            labelStepTitle.ForeColor = Color.FromArgb(174, 180, 199);
            labelStepTitle.Location = new Point(790, 432);
            labelStepTitle.Name = "labelStepTitle";
            labelStepTitle.Size = new Size(80, 15);
            labelStepTitle.TabIndex = 25;
            labelStepTitle.Text = "ADIM BOYUTU";
            //
            // buttonStep01
            //
            buttonStep01.BackColor = Color.FromArgb(38, 42, 61);
            buttonStep01.Cursor = Cursors.Hand;
            buttonStep01.FlatStyle = FlatStyle.Flat;
            buttonStep01.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            buttonStep01.ForeColor = Color.FromArgb(230, 232, 240);
            buttonStep01.Location = new Point(790, 450);
            buttonStep01.Name = "buttonStep01";
            buttonStep01.Size = new Size(62, 30);
            buttonStep01.TabIndex = 26;
            buttonStep01.Text = "0.1 mm";
            buttonStep01.UseVisualStyleBackColor = false;
            buttonStep01.FlatAppearance.BorderSize = 0;
            buttonStep01.Click += buttonStep_Click;
            //
            // buttonStep1
            //
            buttonStep1.BackColor = Color.FromArgb(91, 110, 245);
            buttonStep1.Cursor = Cursors.Hand;
            buttonStep1.FlatStyle = FlatStyle.Flat;
            buttonStep1.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            buttonStep1.ForeColor = Color.White;
            buttonStep1.Location = new Point(858, 450);
            buttonStep1.Name = "buttonStep1";
            buttonStep1.Size = new Size(62, 30);
            buttonStep1.TabIndex = 27;
            buttonStep1.Text = "1 mm";
            buttonStep1.UseVisualStyleBackColor = false;
            buttonStep1.FlatAppearance.BorderSize = 0;
            buttonStep1.Click += buttonStep_Click;
            //
            // buttonStep10
            //
            buttonStep10.BackColor = Color.FromArgb(38, 42, 61);
            buttonStep10.Cursor = Cursors.Hand;
            buttonStep10.FlatStyle = FlatStyle.Flat;
            buttonStep10.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            buttonStep10.ForeColor = Color.FromArgb(230, 232, 240);
            buttonStep10.Location = new Point(926, 450);
            buttonStep10.Name = "buttonStep10";
            buttonStep10.Size = new Size(62, 30);
            buttonStep10.TabIndex = 28;
            buttonStep10.Text = "10 mm";
            buttonStep10.UseVisualStyleBackColor = false;
            buttonStep10.FlatAppearance.BorderSize = 0;
            buttonStep10.Click += buttonStep_Click;
            //
            // buttonJogYPlus
            //
            buttonJogYPlus.BackColor = Color.FromArgb(38, 42, 61);
            buttonJogYPlus.Cursor = Cursors.Hand;
            buttonJogYPlus.FlatStyle = FlatStyle.Flat;
            buttonJogYPlus.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonJogYPlus.ForeColor = Color.FromArgb(230, 232, 240);
            buttonJogYPlus.Location = new Point(858, 490);
            buttonJogYPlus.Name = "buttonJogYPlus";
            buttonJogYPlus.Size = new Size(62, 46);
            buttonJogYPlus.TabIndex = 29;
            buttonJogYPlus.Text = "Y+";
            buttonJogYPlus.UseVisualStyleBackColor = false;
            buttonJogYPlus.FlatAppearance.BorderSize = 0;
            buttonJogYPlus.Click += buttonJog_Click;
            //
            // buttonJogXMinus
            //
            buttonJogXMinus.BackColor = Color.FromArgb(38, 42, 61);
            buttonJogXMinus.Cursor = Cursors.Hand;
            buttonJogXMinus.FlatStyle = FlatStyle.Flat;
            buttonJogXMinus.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonJogXMinus.ForeColor = Color.FromArgb(230, 232, 240);
            buttonJogXMinus.Location = new Point(790, 542);
            buttonJogXMinus.Name = "buttonJogXMinus";
            buttonJogXMinus.Size = new Size(62, 46);
            buttonJogXMinus.TabIndex = 30;
            buttonJogXMinus.Text = "X-";
            buttonJogXMinus.UseVisualStyleBackColor = false;
            buttonJogXMinus.FlatAppearance.BorderSize = 0;
            buttonJogXMinus.Click += buttonJog_Click;
            //
            // labelJogStepCenter
            //
            labelJogStepCenter.BackColor = Color.FromArgb(18, 19, 28);
            labelJogStepCenter.BorderStyle = BorderStyle.FixedSingle;
            labelJogStepCenter.Font = new Font("Consolas", 10F, FontStyle.Bold);
            labelJogStepCenter.ForeColor = Color.FromArgb(0, 229, 255);
            labelJogStepCenter.Location = new Point(858, 542);
            labelJogStepCenter.Name = "labelJogStepCenter";
            labelJogStepCenter.Size = new Size(62, 46);
            labelJogStepCenter.TabIndex = 31;
            labelJogStepCenter.Text = "1mm";
            labelJogStepCenter.TextAlign = ContentAlignment.MiddleCenter;
            //
            // buttonJogXPlus
            //
            buttonJogXPlus.BackColor = Color.FromArgb(38, 42, 61);
            buttonJogXPlus.Cursor = Cursors.Hand;
            buttonJogXPlus.FlatStyle = FlatStyle.Flat;
            buttonJogXPlus.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonJogXPlus.ForeColor = Color.FromArgb(230, 232, 240);
            buttonJogXPlus.Location = new Point(926, 542);
            buttonJogXPlus.Name = "buttonJogXPlus";
            buttonJogXPlus.Size = new Size(62, 46);
            buttonJogXPlus.TabIndex = 32;
            buttonJogXPlus.Text = "X+";
            buttonJogXPlus.UseVisualStyleBackColor = false;
            buttonJogXPlus.FlatAppearance.BorderSize = 0;
            buttonJogXPlus.Click += buttonJog_Click;
            //
            // buttonJogYMinus
            //
            buttonJogYMinus.BackColor = Color.FromArgb(38, 42, 61);
            buttonJogYMinus.Cursor = Cursors.Hand;
            buttonJogYMinus.FlatStyle = FlatStyle.Flat;
            buttonJogYMinus.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonJogYMinus.ForeColor = Color.FromArgb(230, 232, 240);
            buttonJogYMinus.Location = new Point(858, 594);
            buttonJogYMinus.Name = "buttonJogYMinus";
            buttonJogYMinus.Size = new Size(62, 46);
            buttonJogYMinus.TabIndex = 33;
            buttonJogYMinus.Text = "Y-";
            buttonJogYMinus.UseVisualStyleBackColor = false;
            buttonJogYMinus.FlatAppearance.BorderSize = 0;
            buttonJogYMinus.Click += buttonJog_Click;
            //
            // buttonJogZPlus
            //
            buttonJogZPlus.BackColor = Color.FromArgb(255, 167, 38);
            buttonJogZPlus.Cursor = Cursors.Hand;
            buttonJogZPlus.FlatStyle = FlatStyle.Flat;
            buttonJogZPlus.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonJogZPlus.ForeColor = Color.White;
            buttonJogZPlus.Location = new Point(790, 650);
            buttonJogZPlus.Name = "buttonJogZPlus";
            buttonJogZPlus.Size = new Size(96, 38);
            buttonJogZPlus.TabIndex = 34;
            buttonJogZPlus.Text = "Z+";
            buttonJogZPlus.UseVisualStyleBackColor = false;
            buttonJogZPlus.FlatAppearance.BorderSize = 0;
            buttonJogZPlus.Click += buttonJog_Click;
            //
            // buttonJogZMinus
            //
            buttonJogZMinus.BackColor = Color.FromArgb(255, 167, 38);
            buttonJogZMinus.Cursor = Cursors.Hand;
            buttonJogZMinus.FlatStyle = FlatStyle.Flat;
            buttonJogZMinus.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonJogZMinus.ForeColor = Color.White;
            buttonJogZMinus.Location = new Point(892, 650);
            buttonJogZMinus.Name = "buttonJogZMinus";
            buttonJogZMinus.Size = new Size(96, 38);
            buttonJogZMinus.TabIndex = 35;
            buttonJogZMinus.Text = "Z-";
            buttonJogZMinus.UseVisualStyleBackColor = false;
            buttonJogZMinus.FlatAppearance.BorderSize = 0;
            buttonJogZMinus.Click += buttonJog_Click;
            //
            // labelCustomStep
            //
            labelCustomStep.AutoSize = true;
            labelCustomStep.ForeColor = Color.FromArgb(174, 180, 199);
            labelCustomStep.Location = new Point(790, 742);
            labelCustomStep.Name = "labelCustomStep";
            labelCustomStep.Size = new Size(130, 15);
            labelCustomStep.TabIndex = 36;
            labelCustomStep.Text = "ÖZEL MESAFE (mm):";
            //
            // textBoxCustomStep
            //
            textBoxCustomStep.BackColor = Color.FromArgb(38, 42, 61);
            textBoxCustomStep.BorderStyle = BorderStyle.FixedSingle;
            textBoxCustomStep.Font = new Font("Consolas", 11F, FontStyle.Bold);
            textBoxCustomStep.ForeColor = Color.FromArgb(0, 229, 255);
            textBoxCustomStep.Location = new Point(790, 760);
            textBoxCustomStep.Name = "textBoxCustomStep";
            textBoxCustomStep.Size = new Size(190, 27);
            textBoxCustomStep.TabIndex = 37;
            textBoxCustomStep.TextAlign = HorizontalAlignment.Center;
            //
            // labelDroBCaption
            //
            labelDroBCaption.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelDroBCaption.ForeColor = Color.FromArgb(230, 232, 240);
            labelDroBCaption.Location = new Point(1000, 55);
            labelDroBCaption.Name = "labelDroBCaption";
            labelDroBCaption.Size = new Size(24, 30);
            labelDroBCaption.TabIndex = 38;
            labelDroBCaption.Text = "B";
            labelDroBCaption.TextAlign = ContentAlignment.MiddleCenter;
            //
            // labelDroBValue
            //
            labelDroBValue.BackColor = Color.FromArgb(18, 19, 28);
            labelDroBValue.BorderStyle = BorderStyle.FixedSingle;
            labelDroBValue.Font = new Font("Consolas", 16F, FontStyle.Bold);
            labelDroBValue.ForeColor = Color.FromArgb(0, 229, 255);
            labelDroBValue.Location = new Point(1000, 88);
            labelDroBValue.Name = "labelDroBValue";
            labelDroBValue.Size = new Size(160, 40);
            labelDroBValue.TabIndex = 39;
            labelDroBValue.Text = "0.000";
            labelDroBValue.TextAlign = ContentAlignment.MiddleRight;
            //
            // buttonZeroB
            //
            buttonZeroB.BackColor = Color.FromArgb(91, 110, 245);
            buttonZeroB.Cursor = Cursors.Hand;
            buttonZeroB.FlatStyle = FlatStyle.Flat;
            buttonZeroB.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            buttonZeroB.ForeColor = Color.White;
            buttonZeroB.Location = new Point(1000, 130);
            buttonZeroB.Name = "buttonZeroB";
            buttonZeroB.Size = new Size(160, 28);
            buttonZeroB.TabIndex = 40;
            buttonZeroB.Text = "Zero B";
            buttonZeroB.UseVisualStyleBackColor = false;
            buttonZeroB.FlatAppearance.BorderSize = 0;
            buttonZeroB.Click += buttonZeroB_Click;
            //
            // buttonJogBPlus
            //
            buttonJogBPlus.BackColor = Color.FromArgb(255, 167, 38);
            buttonJogBPlus.Cursor = Cursors.Hand;
            buttonJogBPlus.FlatStyle = FlatStyle.Flat;
            buttonJogBPlus.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonJogBPlus.ForeColor = Color.White;
            buttonJogBPlus.Location = new Point(790, 694);
            buttonJogBPlus.Name = "buttonJogBPlus";
            buttonJogBPlus.Size = new Size(96, 38);
            buttonJogBPlus.TabIndex = 41;
            buttonJogBPlus.Text = "B+";
            buttonJogBPlus.UseVisualStyleBackColor = false;
            buttonJogBPlus.FlatAppearance.BorderSize = 0;
            buttonJogBPlus.Click += buttonJog_Click;
            //
            // buttonJogBMinus
            //
            buttonJogBMinus.BackColor = Color.FromArgb(255, 167, 38);
            buttonJogBMinus.Cursor = Cursors.Hand;
            buttonJogBMinus.FlatStyle = FlatStyle.Flat;
            buttonJogBMinus.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonJogBMinus.ForeColor = Color.White;
            buttonJogBMinus.Location = new Point(892, 694);
            buttonJogBMinus.Name = "buttonJogBMinus";
            buttonJogBMinus.Size = new Size(96, 38);
            buttonJogBMinus.TabIndex = 42;
            buttonJogBMinus.Text = "B-";
            buttonJogBMinus.UseVisualStyleBackColor = false;
            buttonJogBMinus.FlatAppearance.BorderSize = 0;
            buttonJogBMinus.Click += buttonJog_Click;
            //
            // buttonBFullTurn
            //
            buttonBFullTurn.BackColor = Color.FromArgb(91, 110, 245);
            buttonBFullTurn.Cursor = Cursors.Hand;
            buttonBFullTurn.FlatStyle = FlatStyle.Flat;
            buttonBFullTurn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonBFullTurn.ForeColor = Color.White;
            buttonBFullTurn.Location = new Point(996, 694);
            buttonBFullTurn.Name = "buttonBFullTurn";
            buttonBFullTurn.Size = new Size(70, 38);
            buttonBFullTurn.TabIndex = 43;
            buttonBFullTurn.Text = "1 tur";
            buttonBFullTurn.UseVisualStyleBackColor = false;
            buttonBFullTurn.FlatAppearance.BorderSize = 0;
            buttonBFullTurn.Click += buttonBFullTurn_Click;
            //
            // MotorControlPanel
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 29, 42);
            Size = new Size(1180, 800);
            Controls.Add(labelPort);
            Controls.Add(comboBoxPort);
            Controls.Add(buttonConnect);
            Controls.Add(buttonDisconnect);
            Controls.Add(labelStatus);
            Controls.Add(labelAlarm);
            Controls.Add(labelDroXCaption);
            Controls.Add(labelDroXValue);
            Controls.Add(buttonZeroX);
            Controls.Add(labelDroYCaption);
            Controls.Add(labelDroYValue);
            Controls.Add(buttonZeroY);
            Controls.Add(labelDroZCaption);
            Controls.Add(labelDroZValue);
            Controls.Add(buttonZeroZ);
            Controls.Add(labelJogTitle);
            Controls.Add(labelStepTitle);
            Controls.Add(buttonStep01);
            Controls.Add(buttonStep1);
            Controls.Add(buttonStep10);
            Controls.Add(buttonJogYPlus);
            Controls.Add(buttonJogXMinus);
            Controls.Add(labelJogStepCenter);
            Controls.Add(buttonJogXPlus);
            Controls.Add(buttonJogYMinus);
            Controls.Add(buttonJogZPlus);
            Controls.Add(buttonJogZMinus);
            Controls.Add(labelCustomStep);
            Controls.Add(textBoxCustomStep);
            Controls.Add(textBoxGcode);
            Controls.Add(buttonSend);
            Controls.Add(buttonStatus);
            Controls.Add(buttonHome);
            Controls.Add(labelBatch);
            Controls.Add(textBoxBatch);
            Controls.Add(buttonSendBatch);
            Controls.Add(textBoxLog);
            Controls.Add(labelDroBCaption);
            Controls.Add(labelDroBValue);
            Controls.Add(buttonZeroB);
            Controls.Add(buttonJogBPlus);
            Controls.Add(buttonJogBMinus);
            Controls.Add(buttonBFullTurn);
            Font = new Font("Segoe UI", 9F);
            Name = "MotorControlPanel";
            Load += MotorControlPanel_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelPort;
        private ComboBox comboBoxPort;
        private Button buttonConnect;
        private Button buttonDisconnect;
        private Label labelStatus;
        private Label labelAlarm;
        private Label labelDroXCaption;
        private Label labelDroXValue;
        private Button buttonZeroX;
        private Label labelDroYCaption;
        private Label labelDroYValue;
        private Button buttonZeroY;
        private Label labelDroZCaption;
        private Label labelDroZValue;
        private Button buttonZeroZ;
        private Label labelJogTitle;
        private Label labelStepTitle;
        private Button buttonStep01;
        private Button buttonStep1;
        private Button buttonStep10;
        private Button buttonJogYPlus;
        private Button buttonJogXMinus;
        private Label labelJogStepCenter;
        private Button buttonJogXPlus;
        private Button buttonJogYMinus;
        private Button buttonJogZPlus;
        private Button buttonJogZMinus;
        private Label labelCustomStep;
        private TextBox textBoxCustomStep;
        private TextBox textBoxGcode;
        private Button buttonSend;
        private Button buttonStatus;
        private Button buttonHome;
        private Label labelBatch;
        private TextBox textBoxBatch;
        private Button buttonSendBatch;
        private TextBox textBoxLog;
        private Label labelDroBCaption;
        private Label labelDroBValue;
        private Button buttonZeroB;
        private Button buttonJogBMinus;
        private Button buttonJogBPlus;
        private Button buttonBFullTurn;
    }
}
