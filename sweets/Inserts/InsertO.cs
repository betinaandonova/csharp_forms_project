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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace sweets
{
    public partial class InsertO : Form
    {
        public InsertO()
        {
            InitializeComponent();
        }
        private void InsertO_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                MessageBox.Show(
                    "Няма как да се направи поръчката!",
                    "Отказ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (!radioButton1.Checked)
            {
                MessageBox.Show(
                    "Моля, изберете дали поръчката може да бъде направена.",
                    "Информация",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            string orderId = textBox1.Text;
            string clientName = textBox4.Text;
            string assortmentId = textBox2.Text;
            int quantity = (int)numericUpDown1.Value;
            DateTime deliveryDate = dateTimePicker1.Value;

            if (string.IsNullOrWhiteSpace(orderId))
            {
                MessageBox.Show("Моля, въведете Order ID.");
                return;
            }

            if (string.IsNullOrWhiteSpace(clientName))
            {
                MessageBox.Show("Моля, въведете име на клиент.");
                return;
            }

            if (string.IsNullOrWhiteSpace(assortmentId))
            {
                MessageBox.Show("Моля, въведете Assortment ID.");
                return;
            }

            if (quantity <= 0)
            {
                MessageBox.Show("Количеството трябва да е по-голямо от 0.");
                return;
            }

            try
            {
                string connectionString =
                    "Server=(localdb)\\MSSQLLocalDB;Database=SweetsDB;Trusted_Connection=True;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = @"
                INSERT INTO Orders
                (OrderId, ClientName, AssortmentId, Quantity, DeliveryDate)
                VALUES
                (@orderId, @clientName, @assortmentId, @quantity, @deliveryDate)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@orderId", SqlDbType.NVarChar, 20).Value = orderId;
                        cmd.Parameters.Add("@clientName", SqlDbType.NVarChar, 150).Value = clientName;
                        cmd.Parameters.Add("@assortmentId", SqlDbType.NVarChar, 20).Value = assortmentId;
                        cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
                        cmd.Parameters.Add("@deliveryDate", SqlDbType.Date).Value = deliveryDate;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Поръчката е записана успешно!",
                    "Успех",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при запис:\n" + ex.Message);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
