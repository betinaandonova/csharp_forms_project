using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace sweets

{

    public partial class InsertA : Form

    {
        public InsertA()

        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)

        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string assortmentId = textBox1.Text;
            string assortmentName = textBox2.Text;
            string groupId = textBox3.Text;
            decimal weight;
            decimal unitPrice;
            string recipe = richTextBox1.Text;

            if (string.IsNullOrWhiteSpace(assortmentId))
            {
                MessageBox.Show("Моля, въведете ID на артикула.");
                return;
            }

            if (string.IsNullOrWhiteSpace(assortmentName))
            {
                MessageBox.Show("Моля, въведете име на артикула.");
                return;
            }

            if (string.IsNullOrWhiteSpace(groupId))
            {
                MessageBox.Show("Моля, въведете група.");
                return;
            }

            if (string.IsNullOrWhiteSpace(recipe))

            {
                MessageBox.Show("Моля, въведете рецепта.");
                return;
            }

            if (!decimal.TryParse(textBox4.Text, out weight))
            {
                MessageBox.Show("Моля, въведете валидно тегло.");
                return;
            }

            if (!decimal.TryParse(textBox5.Text, out unitPrice))
            {
                MessageBox.Show("Моля, въведете валидна единична цена.");
                return;
            }

            try
            {
              string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=SweetsDB;Trusted_Connection=True;";

                using SqlConnection conn = new SqlConnection(connectionString);

                string sql = @"

                    INSERT INTO Assortment 
                    (AssortmentId, AssortmentName, GroupId, Weight, UnitPrice, Recipe)
                    VALUES
                    (@id, @name, @group, @weight, @price, @recipe)";

                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", assortmentId);
                cmd.Parameters.AddWithValue("@name", assortmentName);
                cmd.Parameters.AddWithValue("@group", groupId);
                cmd.Parameters.AddWithValue("@weight", weight);
                cmd.Parameters.AddWithValue("@price", unitPrice);
                cmd.Parameters.AddWithValue("@recipe", recipe);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Записът е добавен успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при запис:\n" + ex.Message);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)

        {

        }

    }

}

