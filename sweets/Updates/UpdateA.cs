using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace sweets
{
    public partial class UpdateA : Form
    {
        string connectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=SweetsDB;Trusted_Connection=True;";

        public UpdateA()
        {
            InitializeComponent();
            LoadData();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void UpdateA_Load(object sender, EventArgs e)
        {
        }

        private void LoadData()
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            using SqlDataAdapter da =
                new SqlDataAdapter("SELECT * FROM assortment", conn);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            textBox1.Text = row.Cells["AssortmentId"].Value.ToString();
            textBox2.Text = row.Cells["AssortmentName"].Value.ToString();
            textBox3.Text = row.Cells["GroupId"].Value.ToString();
            textBox4.Text = row.Cells["Weight"].Value.ToString();
            textBox5.Text = row.Cells["UnitPrice"].Value.ToString();
            richTextBox1.Text = row.Cells["Recipe"].Value.ToString();
        }

        private void buttonDelete_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Изберете ред");
                return;
            }

            using SqlConnection conn = new SqlConnection(connectionString);

            using SqlCommand cmd =
                new SqlCommand(
                    "DELETE FROM assortment WHERE AssortmentId=@id",
                    conn);

            cmd.Parameters.AddWithValue("@id", textBox1.Text);

            conn.Open();
            cmd.ExecuteNonQuery();

            MessageBox.Show("Изтрито");

            LoadData();
        }

        private void buttonUpdate_Click_1(object sender, EventArgs e)
        {
            try
            {
                string id = textBox1.Text;
                string name = textBox2.Text;
                string group = textBox3.Text;
                string recipe = richTextBox1.Text;

                if (string.IsNullOrWhiteSpace(id))
                {
                    MessageBox.Show("Избери запис от таблицата.");
                    return;
                }

                if (!decimal.TryParse(textBox4.Text, out decimal weight))
                {
                    MessageBox.Show("Грешно тегло");
                    return;
                }

                if (!decimal.TryParse(textBox5.Text, out decimal price))
                {
                    MessageBox.Show("Грешна цена");
                    return;
                }

                using SqlConnection conn = new SqlConnection(connectionString);

                string sql = @"
            UPDATE assortment
            SET
                AssortmentName = @name,
                GroupId        = @group,
                Weight         = @weight,
                UnitPrice      = @price,
                Recipe         = @recipe
            WHERE AssortmentId = @id";

                using SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@group", group);
                cmd.Parameters.AddWithValue("@weight", weight);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@recipe", recipe);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    MessageBox.Show("Няма намерен запис с това ID.");
                    return;
                }

                MessageBox.Show("Записът е обновен!");


                dataGridView1.DataSource = null;
                LoadData();
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка:\n" + ex.Message);
            }
        }

    }
}
