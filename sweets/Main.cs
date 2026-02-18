using sweets.Query;

namespace sweets
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            InsertA newForm = new InsertA();
            newForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertG newForm = new InsertG();
            newForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertO newForm = new InsertO();
            newForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UpdateA newForm = new UpdateA();
            newForm.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            UpdateG newForm = new UpdateG();
            newForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UpdateO newForm = new UpdateO();
            newForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            firstQuery newForm = new firstQuery();
            newForm.Show();
        }


        private void button8_Click_1(object sender, EventArgs e)
        {
            secondQuery newForm = new secondQuery();
            newForm.Show();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            thirdQuery newForm = new thirdQuery();
            newForm.Show();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            fourthQuery newForm = new fourthQuery();
            newForm.Show();
        }
    }

}
