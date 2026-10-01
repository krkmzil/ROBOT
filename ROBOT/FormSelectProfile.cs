namespace ROBOT
{
    public partial class FormSelectProfile : Form
    {
        public string? SelectedProfileName { get; private set; }

        public FormSelectProfile(IEnumerable<string> profileNames)
        {
            InitializeComponent();
            listBoxProfiles.Items.AddRange(profileNames.ToArray());
            if (listBoxProfiles.Items.Count > 0)
                listBoxProfiles.SelectedIndex = 0;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (listBoxProfiles.SelectedItem == null)
            {
                MessageBox.Show("Bir profil seç.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelectedProfileName = listBoxProfiles.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
        }

        private void listBoxProfiles_DoubleClick(object sender, EventArgs e)
        {
            buttonOk_Click(sender, e);
        }
    }
}
