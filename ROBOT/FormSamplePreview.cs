namespace ROBOT
{
    // Öğretilen bir örnek resmini büyük gösterir; istenirse o örneği silmeyi önerir.
    public partial class FormSamplePreview : Form
    {
        public bool DeleteRequested { get; private set; }

        public FormSamplePreview(Bitmap image, string title, string details)
        {
            InitializeComponent();
            Text = title;
            labelTitle.Text = title;
            labelDetails.Text = details;
            pictureBoxSample.Image = image; // Resmin sahibi çağıran taraftır, burada dispose edilmez.
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteRequested = true;
            DialogResult = DialogResult.OK;
        }
    }
}
