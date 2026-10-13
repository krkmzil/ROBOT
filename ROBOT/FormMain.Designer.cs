namespace ROBOT
{
    partial class FormMain
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
            panelSidebar = new Panel();
            buttonNavImage = new Button();
            buttonNavConveyor = new Button();
            buttonNavCamera = new Button();
            buttonNavMotor = new Button();
            labelBrand = new Label();
            panelContent = new Panel();
            panelSidebar.SuspendLayout();
            SuspendLayout();
            //
            // panelSidebar
            //
            panelSidebar.BackColor = Color.FromArgb(20, 21, 32);
            panelSidebar.Controls.Add(buttonNavImage);
            panelSidebar.Controls.Add(buttonNavConveyor);
            panelSidebar.Controls.Add(buttonNavCamera);
            panelSidebar.Controls.Add(buttonNavMotor);
            panelSidebar.Controls.Add(labelBrand);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(220, 800);
            panelSidebar.TabIndex = 0;
            //
            // labelBrand
            //
            labelBrand.AutoSize = true;
            labelBrand.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            labelBrand.ForeColor = Color.White;
            labelBrand.Location = new Point(20, 24);
            labelBrand.Name = "labelBrand";
            labelBrand.Size = new Size(150, 25);
            labelBrand.TabIndex = 0;
            labelBrand.Text = "ROBOT KONTROL";
            //
            // buttonNavMotor
            //
            buttonNavMotor.Cursor = Cursors.Hand;
            buttonNavMotor.FlatStyle = FlatStyle.Flat;
            buttonNavMotor.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonNavMotor.ForeColor = Color.White;
            buttonNavMotor.Location = new Point(15, 90);
            buttonNavMotor.Name = "buttonNavMotor";
            buttonNavMotor.Size = new Size(190, 50);
            buttonNavMotor.TabIndex = 1;
            buttonNavMotor.Text = "Motor Kontrol";
            buttonNavMotor.TextAlign = ContentAlignment.MiddleLeft;
            buttonNavMotor.Padding = new Padding(15, 0, 0, 0);
            buttonNavMotor.UseVisualStyleBackColor = false;
            buttonNavMotor.FlatAppearance.BorderSize = 0;
            buttonNavMotor.Click += buttonNavMotor_Click;
            //
            // buttonNavCamera
            //
            buttonNavCamera.Cursor = Cursors.Hand;
            buttonNavCamera.FlatStyle = FlatStyle.Flat;
            buttonNavCamera.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonNavCamera.ForeColor = Color.White;
            buttonNavCamera.Location = new Point(15, 150);
            buttonNavCamera.Name = "buttonNavCamera";
            buttonNavCamera.Size = new Size(190, 50);
            buttonNavCamera.TabIndex = 2;
            buttonNavCamera.Text = "Kamera Ayarları";
            buttonNavCamera.TextAlign = ContentAlignment.MiddleLeft;
            buttonNavCamera.Padding = new Padding(15, 0, 0, 0);
            buttonNavCamera.UseVisualStyleBackColor = false;
            buttonNavCamera.FlatAppearance.BorderSize = 0;
            buttonNavCamera.Click += buttonNavCamera_Click;
            //
            // buttonNavConveyor
            //
            buttonNavConveyor.Cursor = Cursors.Hand;
            buttonNavConveyor.FlatStyle = FlatStyle.Flat;
            buttonNavConveyor.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonNavConveyor.ForeColor = Color.White;
            buttonNavConveyor.Location = new Point(15, 210);
            buttonNavConveyor.Name = "buttonNavConveyor";
            buttonNavConveyor.Size = new Size(190, 50);
            buttonNavConveyor.TabIndex = 3;
            buttonNavConveyor.Text = "Konveyör Ayarları";
            buttonNavConveyor.TextAlign = ContentAlignment.MiddleLeft;
            buttonNavConveyor.Padding = new Padding(15, 0, 0, 0);
            buttonNavConveyor.UseVisualStyleBackColor = false;
            buttonNavConveyor.FlatAppearance.BorderSize = 0;
            buttonNavConveyor.Click += buttonNavConveyor_Click;
            //
            // buttonNavImage
            //
            buttonNavImage.Cursor = Cursors.Hand;
            buttonNavImage.FlatStyle = FlatStyle.Flat;
            buttonNavImage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonNavImage.ForeColor = Color.White;
            buttonNavImage.Location = new Point(15, 270);
            buttonNavImage.Name = "buttonNavImage";
            buttonNavImage.Size = new Size(190, 50);
            buttonNavImage.TabIndex = 4;
            buttonNavImage.Text = "Görüntü İşleme";
            buttonNavImage.TextAlign = ContentAlignment.MiddleLeft;
            buttonNavImage.Padding = new Padding(15, 0, 0, 0);
            buttonNavImage.UseVisualStyleBackColor = false;
            buttonNavImage.FlatAppearance.BorderSize = 0;
            buttonNavImage.Click += buttonNavImage_Click;
            //
            // panelContent
            //
            panelContent.BackColor = Color.FromArgb(27, 29, 42);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(220, 0);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(1060, 800);
            panelContent.TabIndex = 1;
            //
            // FormMain
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 29, 42);
            ClientSize = new Size(1400, 800);
            MinimumSize = new Size(1416, 839);
            Controls.Add(panelContent);
            Controls.Add(panelSidebar);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Robot Kontrol";
            panelSidebar.ResumeLayout(false);
            panelSidebar.PerformLayout();
            ResumeLayout(false);
        }

        private Panel panelSidebar;
        private Label labelBrand;
        private Button buttonNavMotor;
        private Button buttonNavCamera;
        private Button buttonNavConveyor;
        private Button buttonNavImage;
        private Panel panelContent;
    }
}
