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

namespace sweets.Query
{
    public partial class secondQuery : Form
    {
        string connectionString =
           "Server=(localdb)\\MSSQLLocalDB;Database=SweetsDB;Trusted_Connection=True;";
        public secondQuery()
        {
            InitializeComponent();
        }

        private void secondQuery_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (!DateTime.TryParse(textBox1.Text, out DateTime dateFrom) ||
                !DateTime.TryParse(textBox2.Text, out DateTime dateTo))
            {
                MessageBox.Show("Моля, въведете валидни дати (пример: 2025-01-01).");
                return;
            }

            if (dateFrom > dateTo)
            {
                MessageBox.Show("Началната дата не може да е след крайната.");
                return;
            }

            using SqlConnection conn = new SqlConnection(connectionString);
            //Da se opredeli oborotut za period ot vreme,
            //sumarno za vsyaka grupa sladki po asortiment na sladkite, po grupa sladki, po porachka.
            string sql = @"
        SELECT
            g.GroupName AS 'Група',
            SUM(o.Quantity * a.UnitPrice) AS 'Оборот' 
        FROM Orders o
        JOIN Assortment a ON a.AssortmentId = o.AssortmentId
        JOIN Groups g ON g.GroupId = a.GroupId
        WHERE o.DeliveryDate BETWEEN @from AND @to
        GROUP BY g.GroupName
        ORDER BY Оборот DESC";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@from", SqlDbType.Date).Value = dateFrom.Date;
            cmd.Parameters.Add("@to", SqlDbType.Date).Value = dateTo.Date;

            using SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Няма данни за избрания период.");
            }

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dt;
        

        }
    }
}
