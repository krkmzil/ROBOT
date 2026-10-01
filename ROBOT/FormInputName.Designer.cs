namespace ROBOT
{
    partial class FormInputName
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
            textBoxName = new TextBox();
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
            labelPrompt.Size = new Size(100, 15);
            labelPrompt.TabIndex = 0;
            labelPrompt.Text = "Profil adı:";
            //
            // textBoxName
            //
            textBoxName.BackColor = Color.FromArgb(38, 42, 61);
            textBoxName.BorderStyle = BorderStyle.FixedSingle;
            textBoxName.ForeColor = Color.FromArgb(230, 232, 240);
            textBoxName.Location = new Point(20, 45);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(340, 23);
            textBoxName.TabIndex = 1;
            //
            // buttonOk
            //
            buttonOk.BackColor = Color.FromArgb(76, 175, 80);
            buttonOk.Cursor = Cursors.Hand;
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.FlatStyle = FlatStyle.Flat;
            buttonOk.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonOk.ForeColor = Color.White;
            buttonOk.Location = new Point(180, 85);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(85, 28);
            buttonOk.TabIndex = 2;
            buttonOk.Text = "Kaydet";
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
            buttonCancel.Location = new Point(275, 85);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(85, 28);
            buttonCancel.TabIndex = 3;
            buttonCancel.Text = "İptal";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.FlatAppearance.BorderSize = 0;
            //
            // FormInputName
            //
            AcceptButton = buttonOk;
            CancelButton = buttonCancel;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 29, 42);
            ClientSize = new Size(380, 130);
            Controls.Add(labelPrompt);
            Controls.Add(textBoxName);
            Controls.Add(buttonOk);
            Controls.Add(buttonCancel);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormInputName";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Profil Adı";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label labelPrompt;
        private TextBox textBoxName;
        private Button buttonOk;
        private Button buttonCancel;
    }
}
