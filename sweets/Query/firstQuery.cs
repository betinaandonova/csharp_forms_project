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
    public partial class firstQuery : Form
    {
        string connectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=SweetsDB;Trusted_Connection=True;";
        public firstQuery()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DateTime selectedDate = dateTimePicker1.Value.Date;

            using SqlConnection conn = new SqlConnection(connectionString);

            //Da se izvedat vsichki poruchki za konkretna data, zadadena chrez parametur.
            string sql = @"
        SELECT
            o.OrderId      AS 'Номер',
            o.ClientName   AS 'Клиент',
            a.AssortmentName AS 'Артикул',
            g.GroupName    AS 'Група',
            o.Quantity     AS 'Количество',
            o.DeliveryDate AS 'Дата'
        FROM dbo.Orders o
        JOIN dbo.Assortment a ON a.AssortmentId = o.AssortmentId
        JOIN dbo.Groups g ON g.GroupId = a.GroupId
        WHERE o.DeliveryDate = @date";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@date", SqlDbType.Date).Value = selectedDate;

            using SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dt;

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Няма поръчки за тази дата.");
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void firstQuery_Load(object sender, EventArgs e)
        {

        }
    }
}
