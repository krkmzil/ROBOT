namespace ROBOT
{
    public partial class FormInputName : Form
    {
        public string EnteredName => textBoxName.Text.Trim();

        public FormInputName(string promptText = "Profil adı:", string defaultText = "")
        {
            InitializeComponent();
            labelPrompt.Text = promptText;
            textBoxName.Text = defaultText;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Bir isim gir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
            }
        }
    }
}
