using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sweets
{
    public partial class UpdateG : Form
    {
        string connectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=SweetsDB;Trusted_Connection=True;";

        public UpdateG()
        {
            InitializeComponent();
            LoadData();
        }

        private void UpdateG_Load(object sender, EventArgs e)
        {
        }

        private void LoadData()
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            using SqlDataAdapter da =
                new SqlDataAdapter("SELECT * FROM Groups", conn);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Избери група от таблицата.");
                return;
            }

            if (MessageBox.Show("Сигурни ли сте, че искате да изтриете групата?",
                    "Изтриване",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.No)
                return;

            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);

                using SqlCommand cmd =
                    new SqlCommand("DELETE FROM Groups WHERE GroupId=@id", conn);

                cmd.Parameters.AddWithValue("@id", textBox1.Text);

                conn.Open();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Групата е изтрита!");

                dataGridView1.DataSource = null;
                LoadData();
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при Delete:\n" + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            textBox1.Text = row.Cells["GroupId"].Value?.ToString();
            textBox2.Text = row.Cells["GroupName"].Value?.ToString();

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}