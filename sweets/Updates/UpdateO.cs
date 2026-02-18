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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sweets
{
    public partial class UpdateO : Form
    {
        string connectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=SweetsDB;Trusted_Connection=True;";
        public UpdateO()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);

                string sql = "SELECT OrderId, ClientName, AssortmentId, Quantity, DeliveryDate FROM dbo.Orders";

                using SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadData грешка:\n" + ex.Message);
            }
        }

        private void UpdateO_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string orderId = textBox1.Text;
                string clientName = textBox2.Text;
                string assortmentId = textBox3.Text;
                int quantity = (int)numericUpDown1.Value;
                DateTime deliveryDate = dateTimePicker1.Value;

                if (string.IsNullOrWhiteSpace(orderId))
                {
                    MessageBox.Show("Избери запис от таблицата.");
                    return;
                }

                if (quantity <= 0)
                {
                    MessageBox.Show("Количеството трябва да е > 0.");
                    return;
                }

                using SqlConnection conn = new SqlConnection(connectionString);

                string sql = @"
                    UPDATE Orders
                    SET
                        ClientName   = @client,
                        AssortmentId = @assortment,
                        Quantity     = @quantity,
                        DeliveryDate = @date
                    WHERE OrderId = @id";

                using SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", orderId);
                cmd.Parameters.AddWithValue("@client", clientName);
                cmd.Parameters.AddWithValue("@assortment", assortmentId);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@date", deliveryDate);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    MessageBox.Show("Няма намерена поръчка с това ID.");
                    return;
                }

                MessageBox.Show("Поръчката е обновена!");

                dataGridView1.DataSource = null;
                LoadData();
                dataGridView1.Refresh();
            }
            catch (SqlException ex)
            {
                // FK грешка (AssortmentId)
                if (ex.Number == 547)
                {
                    MessageBox.Show("Няма такъв артикул (AssortmentId).");
                }
                else
                {
                    MessageBox.Show("SQL грешка:\n" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка:\n" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Изберете поръчка.");
                return;
            }

            using SqlConnection conn = new SqlConnection(connectionString);
            using SqlCommand cmd =
                new SqlCommand(
                    "DELETE FROM Orders WHERE OrderId = @id",
                    conn);

            cmd.Parameters.AddWithValue("@id", textBox1.Text);

            conn.Open();
            cmd.ExecuteNonQuery();

            MessageBox.Show("Поръчката е изтрита.");

            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            textBox1.Text = row.Cells["OrderId"].Value.ToString();
            textBox2.Text = row.Cells["ClientName"].Value.ToString();
            textBox3.Text = row.Cells["AssortmentId"].Value.ToString();
            numericUpDown1.Value = Convert.ToInt32(row.Cells["Quantity"].Value);
            dateTimePicker1.Value = Convert.ToDateTime(row.Cells["DeliveryDate"].Value);
        }
    }
}
