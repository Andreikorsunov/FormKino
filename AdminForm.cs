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
        Button film_uuenda, film_kustuta, film_naita, film_lisa;
        TextBox film_txt, aasta_txt, poster_txt;
        PictureBox poster;
        DataGridView dataGridView;
        DataTable tabel;
        Label fname, faasta, fposter;
        int Id;
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

            film_lisa = new Button
            {
                Location = new System.Drawing.Point(650, 75),
                Size = new System.Drawing.Size(120, 25),
                Text = "Lisamine",
            };
            this.Controls.Add(film_lisa);
            film_lisa.Click += Film_lisa_Click;

            film_uuenda = new Button
            {
                Location = new System.Drawing.Point(650, 100),
                Size = new System.Drawing.Size(120, 25),
                Text = "Uuendamine",
            };
            this.Controls.Add(film_uuenda);
            film_uuenda.Click += Film_uuenda_Click;

            film_kustuta = new Button
            {
                Location = new System.Drawing.Point(650, 125),
                Size = new System.Drawing.Size(120, 25),
                Text = "Kustutamine filmi",
            };
            this.Controls.Add(film_kustuta);
            film_kustuta.Click += Film_kustuta_Click;

            fname = new Label();
            fname.Text = "Filmi nimi";
            fname.Location = new Point(550, 75);
            fname.Font = new System.Drawing.Font("Times New Roman", 12);
            this.Controls.Add(fname);

            faasta = new Label();
            faasta.Text = "Filmi aasta";
            faasta.Location = new Point(550, 100);
            faasta.Font = new System.Drawing.Font("Times New Roman", 12);
            this.Controls.Add(faasta);

            fposter = new Label();
            fposter.Text = "Filmi poster";
            fposter.Location = new Point(550, 125);
            fposter.Font = new System.Drawing.Font("Times New Roman", 12);
            this.Controls.Add(fposter);

            fname.Visible = false;
            faasta.Visible = false;
            fposter.Visible = false;

            film_lisa.Visible = false;
            film_uuenda.Visible = false;
            film_kustuta.Visible = false;
        }
        private void Film_lisa_Click(object sender, EventArgs e)
        {
            if (film_txt.Text != "" && aasta_txt.Text != "" && poster_txt.Text != "")
            {
                connect_to_DB.Open();
                command = new SqlCommand("INSERT INTO AdmFilm(Film,Aasta,Poster) VALUES(@film,@aasta,@poster)", connect_to_DB);
                command.Parameters.AddWithValue("@film", film_txt.Text);
                command.Parameters.AddWithValue("@aasta", aasta_txt.Text);
                command.Parameters.AddWithValue("@poster", poster_txt.Text);
                command.ExecuteNonQuery();
                ClearData();
                connect_to_DB.Close();
                MessageBox.Show("Andmed lisatud");
            }
            else
            {
                MessageBox.Show("Viga");
            }
        }
        private void Film_kustuta_Click(object sender, EventArgs e)
        {
            if (Id != 0)
            {
                connect_to_DB.Open();
                command = new SqlCommand("DELETE FROM AdmFilm WHERE Id=@id", connect_to_DB);
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
            if(film_naita.Text=="Näita filmid")
            {
                film_naita.Text = "Peida filmid";
                film_lisa.Visible = true;
                film_uuenda.Visible = true;
                film_kustuta.Visible = true;
                fname.Visible = true;
                faasta.Visible = true;
                fposter.Visible = true;

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
                    Image = Image.FromFile("../../Backgrounds/soon.jpg"),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                Data();
            }
            else if (film_naita.Text == "Peida filmid")
            {
                film_naita.Text = "Näita filmid";
                film_lisa.Visible = false;
                film_uuenda.Visible = false;
                film_kustuta.Visible = false;
                fname.Visible = false;
                faasta.Visible = false;
                fposter.Visible = false;
                film_txt.Visible = false;
                aasta_txt.Visible = false;
                poster_txt.Visible = false;
                poster.Visible = false;
            }
        }
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
                ClearData();
                connect_to_DB.Close();
                MessageBox.Show("Andmed uuendatud");
            }
            else
            {
                MessageBox.Show("Viga");
            }
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
        }
        private void ClearData()
        {
            film_txt.Text = "";
            aasta_txt.Text = "";
            poster_txt.Text = "";
        }
    }
}