using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Net.NetworkInformation;

namespace BandrolRefUpdate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string filePath = string.Empty;

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtIp.Text))
                {
                    MessageBox.Show("Ip Adres Giriniz");
                    return;
                }

                Ping p1 = new Ping();
                PingReply PR = p1.Send(txtYeniIPAdres.Text);

                if (PR.Status != IPStatus.Success)
                {
                    MessageBox.Show("PC'ye ulaþýlamadý.");
                    return;
                }

                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("Dosyanýn olduðu klasörü seçiniz");
                    return;
                }

                string sqlConnectionString = $"Data Source={txtIp.Text};initial catalog=Terminal;TrustServerCertificate=True;persist security info=True;user id=sa;password=ds*T20X1X00X?FLg-!gts";

                using (SqlConnection connection = new SqlConnection(
            sqlConnectionString))
                {
                    string script = File.ReadAllText(filePath);
                    Server server = new Server(new ServerConnection(connection));
                    server.ConnectionContext.ExecuteNonQuery(script);
                }

                MessageBox.Show("Ýþlem baþarýlý");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            dialog.InitialDirectory = "C:\\Users";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
            }
            //MessageBox.Show(filePath);
            lblDosyaYolu.Text = filePath;
        }

        private void btnYeniGonder_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtYeniIPAdres.Text))
                {
                    MessageBox.Show("IP Adres Griniz");
                    return;
                }

                Ping p1 = new Ping();
                PingReply PR = p1.Send(txtYeniIPAdres.Text);
                
                if (PR.Status != IPStatus.Success)
                {
                    MessageBox.Show("PC'ye ulaþýlamadý.");
                    return;
                }


                string sqlConnectionString = $"Data Source={txtYeniIPAdres.Text};initial catalog=Terminal;persist security info=True;TrustServerCertificate=True;user id=sa;password=ds*T20X1X00X?FLg-!gts";
                using (SqlConnection connection = new SqlConnection(
               sqlConnectionString))
                {
                    SqlCommand command = new SqlCommand("UPDATE dbo.BandrolRef SET dbo.BandrolRef.pType = 'A'", connection);
                    connection.Open();

                    command.ExecuteNonQuery();
                    connection.Close();
                }
                MessageBox.Show("Ýþlem baþarýlý");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
