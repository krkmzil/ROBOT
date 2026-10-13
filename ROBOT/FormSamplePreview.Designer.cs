namespace ROBOT
{
    partial class FormSamplePreview
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
            labelDetails = new Label();
            pictureBoxSample = new PictureBox();
            buttonDelete = new Button();
            buttonClose = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSample).BeginInit();
            SuspendLayout();
            //
            // labelTitle
            //
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelTitle.ForeColor = Color.FromArgb(230, 232, 240);
            labelTitle.Location = new Point(20, 16);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(100, 25);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Örnek";
            //
            // labelDetails
            //
            labelDetails.AutoSize = true;
            labelDetails.ForeColor = Color.FromArgb(174, 180, 199);
            labelDetails.Location = new Point(22, 48);
            labelDetails.Name = "labelDetails";
            labelDetails.Size = new Size(100, 15);
            labelDetails.TabIndex = 1;
            //
            // pictureBoxSample
            //
            pictureBoxSample.BackColor = Color.FromArgb(18, 19, 28);
            pictureBoxSample.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxSample.Location = new Point(20, 76);
            pictureBoxSample.Name = "pictureBoxSample";
            pictureBoxSample.Size = new Size(520, 440);
            pictureBoxSample.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxSample.TabIndex = 2;
            pictureBoxSample.TabStop = false;
            //
            // buttonDelete
            //
            buttonDelete.BackColor = Color.FromArgb(239, 83, 80);
            buttonDelete.Cursor = Cursors.Hand;
            buttonDelete.FlatStyle = FlatStyle.Flat;
            buttonDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonDelete.ForeColor = Color.White;
            buttonDelete.Location = new Point(300, 530);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(140, 32);
            buttonDelete.TabIndex = 3;
            buttonDelete.Text = "Bu Örneği Sil";
            buttonDelete.UseVisualStyleBackColor = false;
            buttonDelete.FlatAppearance.BorderSize = 0;
            buttonDelete.Click += buttonDelete_Click;
            //
            // buttonClose
            //
            buttonClose.BackColor = Color.FromArgb(38, 42, 61);
            buttonClose.Cursor = Cursors.Hand;
            buttonClose.DialogResult = DialogResult.Cancel;
            buttonClose.FlatStyle = FlatStyle.Flat;
            buttonClose.ForeColor = Color.FromArgb(230, 232, 240);
            buttonClose.Location = new Point(450, 530);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(90, 32);
            buttonClose.TabIndex = 4;
            buttonClose.Text = "Kapat";
            buttonClose.UseVisualStyleBackColor = false;
            buttonClose.FlatAppearance.BorderSize = 0;
            //
            // FormSamplePreview
            //
            CancelButton = buttonClose;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 29, 42);
            ClientSize = new Size(560, 580);
            Controls.Add(labelTitle);
            Controls.Add(labelDetails);
            Controls.Add(pictureBoxSample);
            Controls.Add(buttonDelete);
            Controls.Add(buttonClose);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormSamplePreview";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Örnek";
            ((System.ComponentModel.ISupportInitialize)pictureBoxSample).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label labelTitle;
        private Label labelDetails;
        private PictureBox pictureBoxSample;
        private Button buttonDelete;
        private Button buttonClose;
    }
}
