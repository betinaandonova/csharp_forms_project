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

namespace sweets.Query
{
    public partial class thirdQuery : Form
    {
        string connectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=SweetsDB;Trusted_Connection=True;";
        public thirdQuery()
        {
            InitializeComponent();
        }

        private void thirdQuery_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Моля, въведете вид сладки (група).");
                return;
            }

            using SqlConnection conn = new SqlConnection(connectionString);
            //Da se napravi otchet za vsichki klienti, napravili oborot nad 1000 leva za opredelen vid sladki, zadadeni chrez parametur.
            string sql = @"
        SELECT
            o.ClientName AS Клиент,
            g.GroupName  AS ВидСладки,
            SUM(o.Quantity * a.UnitPrice) AS Оборот
        FROM Orders o
        JOIN Assortment a ON a.AssortmentId = o.AssortmentId
        JOIN Groups g ON g.GroupId = a.GroupId
        WHERE g.GroupName = @groupName
        GROUP BY o.ClientName, g.GroupName
        HAVING SUM(o.Quantity * a.UnitPrice) > 1000
        ORDER BY Оборот DESC";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@groupName", SqlDbType.NVarChar, 100)
                          .Value = textBox1.Text.Trim();

            using SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Няма клиенти с оборот над 1000 лв за този вид сладки.");
            }

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dt;
        

        }
    }
}
