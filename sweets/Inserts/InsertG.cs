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
    public partial class InsertG : Form
    {
        public InsertG()
        {
            InitializeComponent();
        }
        private void InsertG_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string groupId = textBox1.Text;
            string groupName = textBox2.Text;

            if (string.IsNullOrWhiteSpace(groupId))
            {
                MessageBox.Show("Моля, въведете ID на групата.");
                return;
            }

            if (string.IsNullOrWhiteSpace(groupName))
            {
                MessageBox.Show("Моля, въведете име на групата.");
                return;
            }

            try
            {
                string connectionString =
                    "Server=(localdb)\\MSSQLLocalDB;Database=SweetsDB;Trusted_Connection=True;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = @"
                INSERT INTO Groups
                (GroupId, GroupName)
                VALUES
                (@groupId, @groupName)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@groupId", SqlDbType.NVarChar, 20).Value = groupId;
                        cmd.Parameters.Add("@groupName", SqlDbType.NVarChar, 100).Value = groupName;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Групата е добавена успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при запис:\n" + ex.Message);
            }
        
        }
    }
}
