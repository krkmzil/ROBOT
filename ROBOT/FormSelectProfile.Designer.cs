namespace ROBOT
{
    partial class FormSelectProfile
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
            labelPrompt = new Label();
            listBoxProfiles = new ListBox();
            buttonOk = new Button();
            buttonCancel = new Button();
            SuspendLayout();
            //
            // labelPrompt
            //
            labelPrompt.AutoSize = true;
            labelPrompt.ForeColor = Color.FromArgb(230, 232, 240);
            labelPrompt.Location = new Point(20, 20);
            labelPrompt.Name = "labelPrompt";
            labelPrompt.Size = new Size(120, 15);
            labelPrompt.TabIndex = 0;
            labelPrompt.Text = "Kayıtlı profiller:";
            //
            // listBoxProfiles
            //
            listBoxProfiles.BackColor = Color.FromArgb(38, 42, 61);
            listBoxProfiles.BorderStyle = BorderStyle.FixedSingle;
            listBoxProfiles.ForeColor = Color.FromArgb(230, 232, 240);
            listBoxProfiles.ItemHeight = 15;
            listBoxProfiles.Location = new Point(20, 45);
            listBoxProfiles.Name = "listBoxProfiles";
            listBoxProfiles.Size = new Size(340, 154);
            listBoxProfiles.TabIndex = 1;
            listBoxProfiles.DoubleClick += listBoxProfiles_DoubleClick;
            //
            // buttonOk
            //
            buttonOk.BackColor = Color.FromArgb(76, 175, 80);
            buttonOk.Cursor = Cursors.Hand;
            buttonOk.FlatStyle = FlatStyle.Flat;
            buttonOk.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonOk.ForeColor = Color.White;
            buttonOk.Location = new Point(180, 210);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(85, 28);
            buttonOk.TabIndex = 2;
            buttonOk.Text = "Seç";
            buttonOk.UseVisualStyleBackColor = false;
            buttonOk.FlatAppearance.BorderSize = 0;
            buttonOk.Click += buttonOk_Click;
            //
            // buttonCancel
            //
            buttonCancel.BackColor = Color.FromArgb(38, 42, 61);
            buttonCancel.Cursor = Cursors.Hand;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.ForeColor = Color.FromArgb(230, 232, 240);
            buttonCancel.Location = new Point(275, 210);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(85, 28);
            buttonCancel.TabIndex = 3;
            buttonCancel.Text = "İptal";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.FlatAppearance.BorderSize = 0;
            //
            // FormSelectProfile
            //
            CancelButton = buttonCancel;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 29, 42);
            ClientSize = new Size(380, 255);
            Controls.Add(labelPrompt);
            Controls.Add(listBoxProfiles);
            Controls.Add(buttonOk);
            Controls.Add(buttonCancel);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormSelectProfile";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Profil Seç";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label labelPrompt;
        private ListBox listBoxProfiles;
        private Button buttonOk;
        private Button buttonCancel;
    }
}
