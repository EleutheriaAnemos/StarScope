namespace StarScope
{
    public partial class Form1 : Form
    {

        private readonly Service horoscopeService = new Service();

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeControls();
        }

        private void InitializeControls()
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var cmbConstellation = this.Controls["comboBox1"] as ComboBox;
            var cmbTimeRange = this.Controls["comboBox2"] as ComboBox;
            var txtResult = this.Controls["richTextBox1"] as RichTextBox;

            try
            {
                string selectedConstellation = cmbConstellation.Text;
                string selectedTime = cmbTimeRange.Text;

                var result = await horoscopeService.GetHoroscopeAsync(selectedConstellation, selectedTime);
                txtResult.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败：" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var txtResult = this.Controls["richTextBox1"] as RichTextBox;
            if (txtResult != null && !string.IsNullOrEmpty(txtResult.Text))
            {
                Clipboard.SetText(txtResult.Text);
                MessageBox.Show("内容已复制到剪切板！");
            }
            else
            {
                MessageBox.Show("没有内容可以复制！");
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
     
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }
    }
}
