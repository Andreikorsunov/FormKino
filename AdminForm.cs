using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kinoform
{
    class AdminForm : System.Windows.Forms.Form
    {
        static string conn_KinoDB = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\fermo\source\repos\FormKino\AppData\DTBkino.mdf;Integrated Security=True";
        SqlConnection connect_to_DB = new SqlConnection(conn_KinoDB);
        SqlCommand command;
        SqlDataAdapter adapter;
        Button restart, film_uuenda, film_kustuta, film_naita, pilet_naita1, pilet_naita2, pilet_naita3, pilet_kustuta;
        TextBox film_txt, aasta_txt, poster_txt;
        PictureBox poster;
        DataGridView dataGridView;
        DataTable tabel;
        public AdminForm()
        {
            this.Size = new System.Drawing.Size(800, 450);
            this.Text = "Administraatori paneel";

            film_naita = new Button
            {
                Location = new System.Drawing.Point(50, 25),
                Size = new System.Drawing.Size(80, 25),
                Text = "Näita filmid"
            };
            this.Controls.Add(film_naita);
            film_naita.Click += Film_naita_Click;

            pilet_naita1 = new Button
            {
                Location = new System.Drawing.Point(150, 25),
                Size = new System.Drawing.Size(100, 25),
                Text = "Väike pilet"
            };
            this.Controls.Add(pilet_naita1);
            pilet_naita1.Click += Pilet_naita_Click1;

            pilet_naita2 = new Button
            {
                Location = new System.Drawing.Point(280, 25),
                Size = new System.Drawing.Size(100, 25),
                Text = "Keskmine pilet"
            };
            this.Controls.Add(pilet_naita2);
            pilet_naita2.Click += Pilet_naita_Click2;

            pilet_naita3 = new Button
            {
                Location = new System.Drawing.Point(410, 25),
                Size = new System.Drawing.Size(100, 25),
                Text = "Suur pilet"
            };
            this.Controls.Add(pilet_naita3);
            pilet_naita3.Click += Pilet_naita_Click3;

            film_uuenda = new Button
            {
                Location = new System.Drawing.Point(650, 75),
                Size = new System.Drawing.Size(80, 25),
                Text = "Uuendamine",
            };
            this.Controls.Add(film_uuenda);
            film_uuenda.Click += Film_uuenda_Click;

            restart = new Button
            {
                Location = new System.Drawing.Point(650, 350),
                Size = new System.Drawing.Size(100, 50),
                Text = "Taaskäivitage",
            };
            this.Controls.Add(restart);
            restart.Click += Restart;

            film_kustuta = new Button
            {
                Location = new System.Drawing.Point(650, 100),
                Size = new System.Drawing.Size(120, 25),
                Text = "Kustutamine filmi",
            };
            this.Controls.Add(film_kustuta);
            film_kustuta.Click += Film_kustuta_Click;

            pilet_kustuta = new Button
            {
                Location = new System.Drawing.Point(650, 125),
                Size = new System.Drawing.Size(120, 25),
                Text = "Kustutamine pileti",
            };
            this.Controls.Add(pilet_kustuta);
            pilet_kustuta.Click += Pilet_kustuta_Click;
        }
        private void Restart(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private void Pilet_kustuta_Click(object sender, EventArgs e)
        {
            command = new SqlCommand("DELETE vsaal WHERE Id=@id", connect_to_DB);
            connect_to_DB.Open();
            command.Parameters.AddWithValue("@id", Id);
            command.ExecuteNonQuery();
            ClearData();
            connect_to_DB.Close();
            MessageBox.Show("Andmed on kustutatud");
        }
        private void Film_kustuta_Click(object sender, EventArgs e)
        {
            if (Id != 0)
            {
                command = new SqlCommand("DELETE AdmFilm WHERE Id=@id", connect_to_DB);
                connect_to_DB.Open();
                command.Parameters.AddWithValue("@id", Id);
                command.ExecuteNonQuery();
                ClearData();
                connect_to_DB.Close();
                MessageBox.Show("Andmed on kustutatud");
            }
            else
            {
                MessageBox.Show("Viga kustutamisega");
            }
        }

        private void Film_naita_Click(object sender, EventArgs e)
        {
            film_naita.Text = "Peida filmid";
            film_uuenda.Visible = true;
            film_kustuta.Visible = true;

            film_txt = new TextBox
            { Location = new System.Drawing.Point(450, 75) };
            aasta_txt = new TextBox
            { Location = new System.Drawing.Point(450, 100) };
            poster_txt = new TextBox
            { Location = new System.Drawing.Point(450, 125) };
            poster = new PictureBox
            {
                Size = new System.Drawing.Size(180, 250),
                Location = new System.Drawing.Point(450, 150),
                Image = Image.FromFile("../../Backgrounds/soon.jpg")
            };
            Data();
        }

        static int Id = 0;
        private void Film_uuenda_Click(object sender, EventArgs e)
        {

            if (film_txt.Text != "" && aasta_txt.Text != "" && poster_txt.Text != "" && poster.Image != null)
            {
                connect_to_DB.Open();
                command = new SqlCommand("UPDATE AdmFilm SET Film=@film,Aasta=@aasta,Poster=@poster WHERE Id=@id", connect_to_DB);

                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@film", film_txt.Text);
                command.Parameters.AddWithValue("@aasta", aasta_txt.Text);
                command.Parameters.AddWithValue("@poster", poster_txt.Text);
                command.ExecuteNonQuery();
                connect_to_DB.Close();
                ClearData();
                Data();
                MessageBox.Show("Andmed uuendatud");
            }
            else
            {
                MessageBox.Show("Viga");
            }
        }
        private void Pilet_naita_Click1(object sender, EventArgs e)
        {
            connect_to_DB.Open();
            DataTable tabel_p = new DataTable();
            DataGridView dataGridView_p = new DataGridView();
            DataSet dataset_p = new DataSet();
            SqlDataAdapter adapter_p = new SqlDataAdapter("SELECT rida,koht,film FROM [dbo].[vsaal]; SELECT Film FROM [dbo].[AdmFilm]", connect_to_DB);

            adapter_p.Fill(tabel_p);
            dataGridView_p.DataSource = tabel_p;
            dataGridView_p.Location = new System.Drawing.Point(10, 75);
            dataGridView_p.Size = new System.Drawing.Size(400, 200);

            SqlDataAdapter adapter_f = new SqlDataAdapter("SELECT Film FROM [dbo].[AdmFilm]", connect_to_DB);
            DataTable tabel_f = new DataTable();
            DataSet dataset_f = new DataSet();
            adapter_f.Fill(tabel_f);

            DataGridViewComboBoxCell cbc = new DataGridViewComboBoxCell();
            ComboBox com_f = new ComboBox();
            foreach (DataRow row in tabel_f.Rows)
            {
                com_f.Items.Add(row["Film"]);
                cbc.Items.Add(row["Film"]);
            }
            cbc.Value = com_f;
            connect_to_DB.Close();
            this.Controls.Add(dataGridView_p);
            this.Controls.Add(com_f);
        }
        private void Pilet_naita_Click2(object sender, EventArgs e)
        {
            connect_to_DB.Open();
            DataTable tabel_p = new DataTable();
            DataGridView dataGridView_p = new DataGridView();
            DataSet dataset_p = new DataSet();
            SqlDataAdapter adapter_p = new SqlDataAdapter("SELECT rida,koht,film FROM [dbo].[ksaal]; SELECT Film FROM [dbo].[AdmFilm]", connect_to_DB);

            adapter_p.Fill(tabel_p);
            dataGridView_p.DataSource = tabel_p;
            dataGridView_p.Location = new System.Drawing.Point(10, 75);
            dataGridView_p.Size = new System.Drawing.Size(400, 200);

            SqlDataAdapter adapter_f = new SqlDataAdapter("SELECT Film FROM [dbo].[AdmFilm]", connect_to_DB);
            DataTable tabel_f = new DataTable();
            DataSet dataset_f = new DataSet();
            adapter_f.Fill(tabel_f);

            DataGridViewComboBoxCell cbc = new DataGridViewComboBoxCell();
            ComboBox com_f = new ComboBox();
            foreach (DataRow row in tabel_f.Rows)
            {
                com_f.Items.Add(row["Film"]);
                cbc.Items.Add(row["Film"]);
            }
            cbc.Value = com_f;
            connect_to_DB.Close();
            this.Controls.Add(dataGridView_p);
            this.Controls.Add(com_f);
        }
        private void Pilet_naita_Click3(object sender, EventArgs e)
        {
            connect_to_DB.Open();
            DataTable tabel_p = new DataTable();
            DataGridView dataGridView_p = new DataGridView();
            DataSet dataset_p = new DataSet();
            SqlDataAdapter adapter_p = new SqlDataAdapter("SELECT rida,koht,film FROM [dbo].[ssaal]; SELECT Film FROM [dbo].[AdmFilm]", connect_to_DB);

            adapter_p.Fill(tabel_p);
            dataGridView_p.DataSource = tabel_p;
            dataGridView_p.Location = new System.Drawing.Point(10, 75);
            dataGridView_p.Size = new System.Drawing.Size(400, 200);

            SqlDataAdapter adapter_f = new SqlDataAdapter("SELECT Film FROM [dbo].[AdmFilm]", connect_to_DB);
            DataTable tabel_f = new DataTable();
            DataSet dataset_f = new DataSet();
            adapter_f.Fill(tabel_f);

            DataGridViewComboBoxCell cbc = new DataGridViewComboBoxCell();
            ComboBox com_f = new ComboBox();
            foreach (DataRow row in tabel_f.Rows)
            {
                com_f.Items.Add(row["Film"]);
                cbc.Items.Add(row["Film"]);
            }
            cbc.Value = com_f;
            connect_to_DB.Close();
            this.Controls.Add(dataGridView_p);
            this.Controls.Add(com_f);
        }
        public void Data()
        {
            connect_to_DB.Open();
            tabel = new DataTable();
            dataGridView = new DataGridView();
            dataGridView.RowHeaderMouseClick += DataGridView_RowHeaderMouseClick;
            adapter = new SqlDataAdapter("SELECT * FROM [dbo].[AdmFilm]", connect_to_DB);
            adapter.Fill(tabel);
            dataGridView.DataSource = tabel;
            dataGridView.Location = new System.Drawing.Point(10, 75);
            dataGridView.Size = new System.Drawing.Size(400, 200);
            connect_to_DB.Close();
            this.Controls.Add(dataGridView);
            this.Controls.Add(film_txt);
            this.Controls.Add(aasta_txt);
            this.Controls.Add(poster_txt);
            this.Controls.Add(poster);
        }
        private void DataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            film_txt.Text = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            aasta_txt.Text = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            poster_txt.Text = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
            poster.Image = Image.FromFile(@"..\..\Filmid\" + dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString());
            this.Text = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
        private void ClearData()
        {
            film_txt.Text = "";
            aasta_txt.Text = "";
            poster_txt.Text = "";
            poster.Image = Image.FromFile("../../Filmid/soon.jpg");
        }
    }
}