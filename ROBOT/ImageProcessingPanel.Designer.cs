namespace ROBOT
{
    partial class ImageProcessingPanel
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
            buttonSelectProfile = new Button();
            labelActiveProfile = new Label();
            pictureBoxPreview = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).BeginInit();
            SuspendLayout();
            //
            // labelTitle
            //
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelTitle.ForeColor = Color.FromArgb(230, 232, 240);
            labelTitle.Location = new Point(20, 20);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(260, 30);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Görüntü İşleme Programı";
            //
            // buttonSelectProfile
            //
            buttonSelectProfile.BackColor = Color.FromArgb(142, 68, 173);
            buttonSelectProfile.Cursor = Cursors.Hand;
            buttonSelectProfile.FlatStyle = FlatStyle.Flat;
            buttonSelectProfile.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonSelectProfile.ForeColor = Color.White;
            buttonSelectProfile.Location = new Point(20, 68);
            buttonSelectProfile.Name = "buttonSelectProfile";
            buttonSelectProfile.Size = new Size(160, 30);
            buttonSelectProfile.TabIndex = 1;
            buttonSelectProfile.Text = "Profil Seç";
            buttonSelectProfile.UseVisualStyleBackColor = false;
            buttonSelectProfile.FlatAppearance.BorderSize = 0;
            buttonSelectProfile.Click += buttonSelectProfile_Click;
            //
            // labelActiveProfile
            //
            labelActiveProfile.AutoSize = true;
            labelActiveProfile.ForeColor = Color.FromArgb(174, 180, 199);
            labelActiveProfile.Location = new Point(195, 76);
            labelActiveProfile.Name = "labelActiveProfile";
            labelActiveProfile.Size = new Size(140, 15);
            labelActiveProfile.TabIndex = 2;
            labelActiveProfile.Text = "Aktif profil: (seçilmedi)";
            //
            // pictureBoxPreview
            //
            pictureBoxPreview.BackColor = Color.FromArgb(18, 19, 28);
            pictureBoxPreview.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxPreview.Location = new Point(20, 110);
            pictureBoxPreview.Name = "pictureBoxPreview";
            pictureBoxPreview.Size = new Size(640, 360);
            pictureBoxPreview.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPreview.TabIndex = 3;
            pictureBoxPreview.TabStop = false;
            //
            // ImageProcessingPanel
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 29, 42);
            Size = new Size(900, 600);
            Controls.Add(labelTitle);
            Controls.Add(buttonSelectProfile);
            Controls.Add(labelActiveProfile);
            Controls.Add(pictureBoxPreview);
            Font = new Font("Segoe UI", 9F);
            Name = "ImageProcessingPanel";
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label labelTitle;
        private Button buttonSelectProfile;
        private Label labelActiveProfile;
        private PictureBox pictureBoxPreview;
    }
}
