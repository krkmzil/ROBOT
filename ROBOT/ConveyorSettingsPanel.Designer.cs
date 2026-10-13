namespace ROBOT
{
    partial class ConveyorSettingsPanel
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
            labelStatus = new Label();
            labelCalibTitle = new Label();
            labelMmPerRevCaption = new Label();
            numericMmPerRev = new NumericUpDown();
            labelMmUnit = new Label();
            labelCalibHelp = new Label();
            buttonOneTurn = new Button();
            labelSpeedTitle = new Label();
            labelSpeedCaption = new Label();
            numericSpeed = new NumericUpDown();
            labelSpeedMax = new Label();
            checkBoxInvert = new CheckBox();
            labelRunTitle = new Label();
            buttonStartForward = new Button();
            buttonStartBackward = new Button();
            buttonStop = new Button();
            labelRunState = new Label();
            labelDistTitle = new Label();
            labelDistCaption = new Label();
            numericDistance = new NumericUpDown();
            buttonMoveForward = new Button();
            buttonMoveBackward = new Button();
            ((System.ComponentModel.ISupportInitialize)numericMmPerRev).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericDistance).BeginInit();
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
            labelTitle.Text = "Konveyör Ayarları";
            //
            // labelStatus
            //
            labelStatus.AutoSize = true;
            labelStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelStatus.ForeColor = Color.FromArgb(239, 83, 80);
            labelStatus.Location = new Point(20, 68);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(200, 15);
            labelStatus.TabIndex = 1;
            labelStatus.Text = "Robot bağlı değil";
            //
            // labelCalibTitle
            //
            labelCalibTitle.AutoSize = true;
            labelCalibTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelCalibTitle.ForeColor = Color.FromArgb(230, 232, 240);
            labelCalibTitle.Location = new Point(20, 110);
            labelCalibTitle.Name = "labelCalibTitle";
            labelCalibTitle.Size = new Size(90, 20);
            labelCalibTitle.TabIndex = 2;
            labelCalibTitle.Text = "Kalibrasyon";
            //
            // labelMmPerRevCaption
            //
            labelMmPerRevCaption.AutoSize = true;
            labelMmPerRevCaption.ForeColor = Color.FromArgb(230, 232, 240);
            labelMmPerRevCaption.Location = new Point(20, 142);
            labelMmPerRevCaption.Name = "labelMmPerRevCaption";
            labelMmPerRevCaption.Size = new Size(300, 15);
            labelMmPerRevCaption.TabIndex = 3;
            labelMmPerRevCaption.Text = "Bir tam motor turunda bant kaç mm ilerliyor?";
            //
            // numericMmPerRev
            //
            numericMmPerRev.BackColor = Color.FromArgb(38, 42, 61);
            numericMmPerRev.DecimalPlaces = 1;
            numericMmPerRev.ForeColor = Color.FromArgb(0, 229, 255);
            numericMmPerRev.Location = new Point(20, 164);
            numericMmPerRev.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            numericMmPerRev.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericMmPerRev.Name = "numericMmPerRev";
            numericMmPerRev.Size = new Size(110, 23);
            numericMmPerRev.TabIndex = 4;
            numericMmPerRev.Value = new decimal(new int[] { 100, 0, 0, 0 });
            numericMmPerRev.ValueChanged += numericMmPerRev_ValueChanged;
            //
            // labelMmUnit
            //
            labelMmUnit.AutoSize = true;
            labelMmUnit.ForeColor = Color.FromArgb(174, 180, 199);
            labelMmUnit.Location = new Point(140, 167);
            labelMmUnit.Name = "labelMmUnit";
            labelMmUnit.Size = new Size(50, 15);
            labelMmUnit.TabIndex = 5;
            labelMmUnit.Text = "mm / tur";
            //
            // labelCalibHelp
            //
            labelCalibHelp.ForeColor = Color.FromArgb(174, 180, 199);
            labelCalibHelp.Location = new Point(20, 194);
            labelCalibHelp.Name = "labelCalibHelp";
            labelCalibHelp.Size = new Size(520, 34);
            labelCalibHelp.TabIndex = 6;
            labelCalibHelp.Text = "Ölçmek için: \"Bir Tur\"a bas, bandın kaç mm gittiğini cetvelle ölç ve yukarıya yaz.";
            //
            // buttonOneTurn
            //
            buttonOneTurn.BackColor = Color.FromArgb(91, 110, 245);
            buttonOneTurn.Cursor = Cursors.Hand;
            buttonOneTurn.FlatStyle = FlatStyle.Flat;
            buttonOneTurn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonOneTurn.ForeColor = Color.White;
            buttonOneTurn.Location = new Point(20, 232);
            buttonOneTurn.Name = "buttonOneTurn";
            buttonOneTurn.Size = new Size(150, 32);
            buttonOneTurn.TabIndex = 7;
            buttonOneTurn.Text = "Bir Tur";
            buttonOneTurn.UseVisualStyleBackColor = false;
            buttonOneTurn.FlatAppearance.BorderSize = 0;
            buttonOneTurn.Click += buttonOneTurn_Click;
            //
            // labelSpeedTitle
            //
            labelSpeedTitle.AutoSize = true;
            labelSpeedTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelSpeedTitle.ForeColor = Color.FromArgb(230, 232, 240);
            labelSpeedTitle.Location = new Point(20, 285);
            labelSpeedTitle.Name = "labelSpeedTitle";
            labelSpeedTitle.Size = new Size(90, 20);
            labelSpeedTitle.TabIndex = 8;
            labelSpeedTitle.Text = "Hız ve Yön";
            //
            // labelSpeedCaption
            //
            labelSpeedCaption.AutoSize = true;
            labelSpeedCaption.ForeColor = Color.FromArgb(230, 232, 240);
            labelSpeedCaption.Location = new Point(20, 317);
            labelSpeedCaption.Name = "labelSpeedCaption";
            labelSpeedCaption.Size = new Size(70, 15);
            labelSpeedCaption.TabIndex = 9;
            labelSpeedCaption.Text = "Hız (mm/sn):";
            //
            // numericSpeed
            //
            numericSpeed.BackColor = Color.FromArgb(38, 42, 61);
            numericSpeed.DecimalPlaces = 1;
            numericSpeed.ForeColor = Color.FromArgb(0, 229, 255);
            numericSpeed.Location = new Point(110, 313);
            numericSpeed.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericSpeed.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numericSpeed.Name = "numericSpeed";
            numericSpeed.Size = new Size(90, 23);
            numericSpeed.TabIndex = 10;
            numericSpeed.Value = new decimal(new int[] { 20, 0, 0, 0 });
            numericSpeed.ValueChanged += settingChanged;
            //
            // labelSpeedMax
            //
            labelSpeedMax.AutoSize = true;
            labelSpeedMax.ForeColor = Color.FromArgb(174, 180, 199);
            labelSpeedMax.Location = new Point(210, 317);
            labelSpeedMax.Name = "labelSpeedMax";
            labelSpeedMax.Size = new Size(120, 15);
            labelSpeedMax.TabIndex = 11;
            labelSpeedMax.Text = "En fazla: -";
            //
            // checkBoxInvert
            //
            checkBoxInvert.AutoSize = true;
            checkBoxInvert.Cursor = Cursors.Hand;
            checkBoxInvert.ForeColor = Color.FromArgb(230, 232, 240);
            checkBoxInvert.Location = new Point(20, 350);
            checkBoxInvert.Name = "checkBoxInvert";
            checkBoxInvert.Size = new Size(300, 19);
            checkBoxInvert.TabIndex = 12;
            checkBoxInvert.Text = "Yönü ters çevir (ileri ile geri yer değiştirir)";
            checkBoxInvert.UseVisualStyleBackColor = true;
            checkBoxInvert.CheckedChanged += settingChanged;
            //
            // labelRunTitle
            //
            labelRunTitle.AutoSize = true;
            labelRunTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelRunTitle.ForeColor = Color.FromArgb(230, 232, 240);
            labelRunTitle.Location = new Point(20, 400);
            labelRunTitle.Name = "labelRunTitle";
            labelRunTitle.Size = new Size(130, 20);
            labelRunTitle.TabIndex = 13;
            labelRunTitle.Text = "Sürekli Çalıştır";
            //
            // buttonStartForward
            //
            buttonStartForward.BackColor = Color.FromArgb(76, 175, 80);
            buttonStartForward.Cursor = Cursors.Hand;
            buttonStartForward.FlatStyle = FlatStyle.Flat;
            buttonStartForward.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonStartForward.ForeColor = Color.White;
            buttonStartForward.Location = new Point(20, 432);
            buttonStartForward.Name = "buttonStartForward";
            buttonStartForward.Size = new Size(150, 44);
            buttonStartForward.TabIndex = 14;
            buttonStartForward.Text = "İleri Başlat";
            buttonStartForward.UseVisualStyleBackColor = false;
            buttonStartForward.FlatAppearance.BorderSize = 0;
            buttonStartForward.Click += buttonStartForward_Click;
            //
            // buttonStartBackward
            //
            buttonStartBackward.BackColor = Color.FromArgb(38, 198, 218);
            buttonStartBackward.Cursor = Cursors.Hand;
            buttonStartBackward.FlatStyle = FlatStyle.Flat;
            buttonStartBackward.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonStartBackward.ForeColor = Color.White;
            buttonStartBackward.Location = new Point(180, 432);
            buttonStartBackward.Name = "buttonStartBackward";
            buttonStartBackward.Size = new Size(150, 44);
            buttonStartBackward.TabIndex = 15;
            buttonStartBackward.Text = "Geri Başlat";
            buttonStartBackward.UseVisualStyleBackColor = false;
            buttonStartBackward.FlatAppearance.BorderSize = 0;
            buttonStartBackward.Click += buttonStartBackward_Click;
            //
            // buttonStop
            //
            buttonStop.BackColor = Color.FromArgb(239, 83, 80);
            buttonStop.Cursor = Cursors.Hand;
            buttonStop.FlatStyle = FlatStyle.Flat;
            buttonStop.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonStop.ForeColor = Color.White;
            buttonStop.Location = new Point(340, 432);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(150, 44);
            buttonStop.TabIndex = 16;
            buttonStop.Text = "DURDUR";
            buttonStop.UseVisualStyleBackColor = false;
            buttonStop.FlatAppearance.BorderSize = 0;
            buttonStop.Click += buttonStop_Click;
            //
            // labelRunState
            //
            labelRunState.AutoSize = true;
            labelRunState.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelRunState.ForeColor = Color.FromArgb(174, 180, 199);
            labelRunState.Location = new Point(20, 486);
            labelRunState.Name = "labelRunState";
            labelRunState.Size = new Size(120, 15);
            labelRunState.TabIndex = 17;
            labelRunState.Text = "Konveyör durdu";
            //
            // labelDistTitle
            //
            labelDistTitle.AutoSize = true;
            labelDistTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelDistTitle.ForeColor = Color.FromArgb(230, 232, 240);
            labelDistTitle.Location = new Point(20, 530);
            labelDistTitle.Name = "labelDistTitle";
            labelDistTitle.Size = new Size(170, 20);
            labelDistTitle.TabIndex = 18;
            labelDistTitle.Text = "Mesafe Kadar İlerle";
            //
            // labelDistCaption
            //
            labelDistCaption.AutoSize = true;
            labelDistCaption.ForeColor = Color.FromArgb(230, 232, 240);
            labelDistCaption.Location = new Point(20, 565);
            labelDistCaption.Name = "labelDistCaption";
            labelDistCaption.Size = new Size(80, 15);
            labelDistCaption.TabIndex = 19;
            labelDistCaption.Text = "Mesafe (mm):";
            //
            // numericDistance
            //
            numericDistance.BackColor = Color.FromArgb(38, 42, 61);
            numericDistance.DecimalPlaces = 1;
            numericDistance.ForeColor = Color.FromArgb(0, 229, 255);
            numericDistance.Location = new Point(110, 561);
            numericDistance.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericDistance.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numericDistance.Name = "numericDistance";
            numericDistance.Size = new Size(90, 23);
            numericDistance.TabIndex = 20;
            numericDistance.Value = new decimal(new int[] { 50, 0, 0, 0 });
            numericDistance.ValueChanged += settingChanged;
            //
            // buttonMoveForward
            //
            buttonMoveForward.BackColor = Color.FromArgb(91, 110, 245);
            buttonMoveForward.Cursor = Cursors.Hand;
            buttonMoveForward.FlatStyle = FlatStyle.Flat;
            buttonMoveForward.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonMoveForward.ForeColor = Color.White;
            buttonMoveForward.Location = new Point(215, 558);
            buttonMoveForward.Name = "buttonMoveForward";
            buttonMoveForward.Size = new Size(110, 30);
            buttonMoveForward.TabIndex = 21;
            buttonMoveForward.Text = "İleri Git";
            buttonMoveForward.UseVisualStyleBackColor = false;
            buttonMoveForward.FlatAppearance.BorderSize = 0;
            buttonMoveForward.Click += buttonMoveForward_Click;
            //
            // buttonMoveBackward
            //
            buttonMoveBackward.BackColor = Color.FromArgb(91, 110, 245);
            buttonMoveBackward.Cursor = Cursors.Hand;
            buttonMoveBackward.FlatStyle = FlatStyle.Flat;
            buttonMoveBackward.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonMoveBackward.ForeColor = Color.White;
            buttonMoveBackward.Location = new Point(335, 558);
            buttonMoveBackward.Name = "buttonMoveBackward";
            buttonMoveBackward.Size = new Size(110, 30);
            buttonMoveBackward.TabIndex = 22;
            buttonMoveBackward.Text = "Geri Git";
            buttonMoveBackward.UseVisualStyleBackColor = false;
            buttonMoveBackward.FlatAppearance.BorderSize = 0;
            buttonMoveBackward.Click += buttonMoveBackward_Click;
            //
            // ConveyorSettingsPanel
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 29, 42);
            Size = new Size(1180, 800);
            Controls.Add(labelTitle);
            Controls.Add(labelStatus);
            Controls.Add(labelCalibTitle);
            Controls.Add(labelMmPerRevCaption);
            Controls.Add(numericMmPerRev);
            Controls.Add(labelMmUnit);
            Controls.Add(labelCalibHelp);
            Controls.Add(buttonOneTurn);
            Controls.Add(labelSpeedTitle);
            Controls.Add(labelSpeedCaption);
            Controls.Add(numericSpeed);
            Controls.Add(labelSpeedMax);
            Controls.Add(checkBoxInvert);
            Controls.Add(labelRunTitle);
            Controls.Add(buttonStartForward);
            Controls.Add(buttonStartBackward);
            Controls.Add(buttonStop);
            Controls.Add(labelRunState);
            Controls.Add(labelDistTitle);
            Controls.Add(labelDistCaption);
            Controls.Add(numericDistance);
            Controls.Add(buttonMoveForward);
            Controls.Add(buttonMoveBackward);
            Font = new Font("Segoe UI", 9F);
            Name = "ConveyorSettingsPanel";
            ((System.ComponentModel.ISupportInitialize)numericMmPerRev).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericDistance).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label labelTitle;
        private Label labelStatus;
        private Label labelCalibTitle;
        private Label labelMmPerRevCaption;
        private NumericUpDown numericMmPerRev;
        private Label labelMmUnit;
        private Label labelCalibHelp;
        private Button buttonOneTurn;
        private Label labelSpeedTitle;
        private Label labelSpeedCaption;
        private NumericUpDown numericSpeed;
        private Label labelSpeedMax;
        private CheckBox checkBoxInvert;
        private Label labelRunTitle;
        private Button buttonStartForward;
        private Button buttonStartBackward;
        private Button buttonStop;
        private Label labelRunState;
        private Label labelDistTitle;
        private Label labelDistCaption;
        private NumericUpDown numericDistance;
        private Button buttonMoveForward;
        private Button buttonMoveBackward;
    }
}
