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
using System.Windows.Forms.DataVisualization.Charting;


namespace sweets.Query
{
    public partial class fourthQuery : Form
    {
        string connectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=SweetsDB;Trusted_Connection=True;";

        public fourthQuery()
        {
            InitializeComponent();
        }

        private void fourthQuery_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadMonthlyChart();
            LoadYearlyChart();
        }
        private void LoadMonthlyChart()
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            ChartArea area = new ChartArea();
            area.AxisX.Title = "Месец / Година";
            area.AxisY.Title = "Печалба (лв)";
            chart1.ChartAreas.Add(area);

            using SqlConnection conn = new SqlConnection(connectionString);
            //Da se nachertayat grafiki za vsyaka grupa sladki po mesec, po godini i pechalbata, koyato e otchetena.
            string sql = @"
        SELECT
            g.GroupName,
            YEAR(o.DeliveryDate) AS Year,
            MONTH(o.DeliveryDate) AS Month,
            SUM(o.Quantity * a.UnitPrice) AS Profit
        FROM Orders o
        JOIN Assortment a ON a.AssortmentId = o.AssortmentId
        JOIN Groups g ON g.GroupId = a.GroupId
        GROUP BY g.GroupName, YEAR(o.DeliveryDate), MONTH(o.DeliveryDate)
        ORDER BY g.GroupName, Year, Month";

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            var groups = dt.AsEnumerable()
                           .Select(r => r["GroupName"].ToString())
                           .Distinct();

            foreach (string group in groups)
            {
                Series series = new Series(group);
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 3;

                foreach (DataRow row in dt.Select($"GroupName = '{group}'"))
                {
                    string label = $"{row["Month"]}/{row["Year"]}";
                    decimal profit = Convert.ToDecimal(row["Profit"]);
                    series.Points.AddXY(label, profit);
                }

                chart1.Series.Add(series);
            }
        }
        private void LoadYearlyChart()
        {
            chart2.Series.Clear();
            chart2.ChartAreas.Clear();

            ChartArea area = new ChartArea();
            area.AxisX.Title = "Година";
            area.AxisY.Title = "Печалба (лв)";
            chart2.ChartAreas.Add(area);

            using SqlConnection conn = new SqlConnection(connectionString);

            string sql = @"
        SELECT
            g.GroupName,
            YEAR(o.DeliveryDate) AS Year,
            SUM(o.Quantity * a.UnitPrice) AS Profit
        FROM Orders o
        JOIN Assortment a ON a.AssortmentId = o.AssortmentId
        JOIN Groups g ON g.GroupId = a.GroupId
        GROUP BY g.GroupName, YEAR(o.DeliveryDate)
        ORDER BY g.GroupName, Year";

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            var groups = dt.AsEnumerable()
                           .Select(r => r["GroupName"].ToString())
                           .Distinct();

            foreach (string group in groups)
            {
                Series series = new Series(group);
                series.ChartType = SeriesChartType.Column; // годишно – колони
                series.BorderWidth = 2;

                foreach (DataRow row in dt.Select($"GroupName = '{group}'"))
                {
                    string year = row["Year"].ToString();
                    decimal profit = Convert.ToDecimal(row["Profit"]);
                    series.Points.AddXY(year, profit);
                }

                chart2.Series.Add(series);
            }
        }

    }
}
