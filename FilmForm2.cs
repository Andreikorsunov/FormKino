﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kinoform
{
    public partial class FilmForm2 : Form
    {
        Label message = new Label();
        Button[] btn = new Button[3];
        string[] texts = new string[3];
        TableLayoutPanel tlp = new TableLayoutPanel();
        Button btn_tabel;
        static List<Pilet> piletid;
        int k, r;
        static string[] read_kohad;
        static string conn_DTBkino = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\fermo\source\repos\FormKino\AppData\DTBkino.mdf;Integrated Security=True";
        SqlConnection connect_to_DB = new SqlConnection(conn_DTBkino);
        SqlCommand command;
        SqlDataAdapter adapter;
        public FilmForm2(int read, int kohad, string film)
        {
            this.tlp.ColumnCount = kohad;
            this.tlp.RowCount = read;
            this.tlp.ColumnStyles.Clear();
            this.tlp.RowStyles.Clear();
            this.Text = film;
            int i, j;
            read_kohad = Ostetud_piletid();
            piletid = new List<Pilet> { };
            for (i = 0; i < read; i++)
            {
                this.tlp.RowStyles.Add(new RowStyle(SizeType.Percent));
                this.tlp.RowStyles[i].Height = 100 / read;
            }
            for (j = 0; j < kohad; j++)
            {
                this.tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
                this.tlp.ColumnStyles[j].Width = 100 / kohad;
            }
            this.Size = new System.Drawing.Size(read * 65, kohad * 100);
            for (r = 0; r < read; r++)
            {
                for (k = 0; k < kohad; k++)
                {
                    Button btn_tabel = Uusnupp((sender, e) => Pileti_valik(sender, e));
                    foreach (var item in read_kohad)
                    {
                        if (item.ToString() == btn_tabel.Name)
                        {
                            btn_tabel.BackColor = Color.Red;
                            btn_tabel.Enabled = false;
                        }
                    }
                    this.tlp.Controls.Add(btn_tabel, k, r);
                }
            }
            this.tlp.Dock = DockStyle.Fill;
            this.Controls.Add(tlp);
        }
        public Button Uusnupp(Action<object, EventArgs> click)
        {
            btn_tabel = new Button
            {
                Text = string.Format("rida  {0}, koht {1}", r + 1, k + 1),
                Name = string.Format("{1}{0}", k + 1, r + 1),
                Dock = DockStyle.Fill,
                BackColor = Color.Green
            };
            btn_tabel.Click += new EventHandler(Pileti_valik);
            return btn_tabel;
        }
        public void Saada_piletid(List<Pilet> piletid)
        {
            string film=ValiFilm2.filmnimi;
            connect_to_DB.Open();
            string text = "Filmi näidatakse keskmises saalis,\nTe valite film " + film +",\nMe ootame teid Apollonos! ";
            foreach (var item in piletid)
            {
                text += "\nRida: " + item.Rida + " Koht: " + item.Koht + "\n";
                command = new SqlCommand("INSERT INTO ksaal(Rida,Koht,Film) VALUES (@rida,@koht,@film)", connect_to_DB);
                command.Parameters.AddWithValue("@rida", item.Rida);
                command.Parameters.AddWithValue("@koht", item.Koht);
                command.Parameters.AddWithValue("@film", film);
                command.ExecuteNonQuery();
            }
            connect_to_DB.Close();
            MailAddress fromAdress = new MailAddress("andrei.korshunov2004@gmail.com", "Apollon kino");

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(fromAdress.Address, "sinu parool");
            try
            {
                string email = Interaction.InputBox("Sisesta oma emaili, me saadame sulle info ostetud pileti kohta", "Email");
                MailAddress toAdress = new MailAddress(email);
                MailMessage message = new MailMessage(fromAdress, toAdress);
                message.Body = text;
                message.Subject = "Ostetud pilet";
                smtpClient.Send(message);
                piletid.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public string[] Ostetud_piletid()
        {
            try
            {
                connect_to_DB.Open();
                if (ValiFilm2.filmnimi == "Spiderman")
                {
                    adapter = new SqlDataAdapter("SELECT * FROM [dbo].[ksaal] WHERE Film = 'Spiderman' ", connect_to_DB);
                    DataTable tabel = new DataTable();
                    adapter.Fill(tabel);
                    read_kohad = new string[tabel.Rows.Count];
                    var index = 0;
                    foreach (DataRow row in tabel.Rows)
                    {
                        var rida = row["Rida"];
                        var koht = row["Koht"];
                        read_kohad[index++] = $"{rida}{koht}";
                    }
                    connect_to_DB.Close();
                }
                else if(ValiFilm2.filmnimi == "Batman")
                {
                    adapter = new SqlDataAdapter("SELECT * FROM [dbo].[ksaal] WHERE Film = 'Batman' ", connect_to_DB);
                    DataTable tabel = new DataTable();
                    adapter.Fill(tabel);
                    read_kohad = new string[tabel.Rows.Count];
                    var index = 0;
                    foreach (DataRow row in tabel.Rows)
                    {
                        var rida = row["Rida"];
                        var koht = row["Koht"];
                        read_kohad[index++] = $"{rida}{koht}";
                    }
                    connect_to_DB.Close();
                }
                else if (ValiFilm2.filmnimi == "Superman")
                {
                    adapter = new SqlDataAdapter("SELECT * FROM [dbo].[ksaal] WHERE Film = 'Superman' ", connect_to_DB);
                    DataTable tabel = new DataTable();
                    adapter.Fill(tabel);
                    read_kohad = new string[tabel.Rows.Count];
                    var index = 0;
                    foreach (DataRow row in tabel.Rows)
                    {
                        var rida = row["Rida"];
                        var koht = row["Koht"];
                        read_kohad[index++] = $"{rida}{koht}";
                    }
                    connect_to_DB.Close();
                }
                else if (ValiFilm2.filmnimi == "Megamind")
                {
                    adapter = new SqlDataAdapter("SELECT * FROM [dbo].[ksaal] WHERE Film = 'Megamind' ", connect_to_DB);
                    DataTable tabel = new DataTable();
                    adapter.Fill(tabel);
                    read_kohad = new string[tabel.Rows.Count];
                    var index = 0;
                    foreach (DataRow row in tabel.Rows)
                    {
                        var rida = row["Rida"];
                        var koht = row["Koht"];
                        read_kohad[index++] = $"{rida}{koht}";
                    }
                    connect_to_DB.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return read_kohad;
        }
        private void Pileti_valik(object sender, EventArgs e)
        {
            Button btn_click = (Button)sender;
            btn_click.BackColor = Color.Yellow;
            var rida = int.Parse(btn_click.Name[0].ToString());
            var koht = int.Parse(btn_click.Name[1].ToString());
            var vas = MessageBox.Show("Sinu pilet on: Rida: " + rida + " Koht: " + koht, "Kas ostad?", MessageBoxButtons.YesNo);
            if (vas == DialogResult.Yes)
            {
                btn_click.BackColor = Color.Red;
                btn_click.Enabled = false;
                try
                {
                    Pilet pilet = new Pilet(rida, koht);
                    piletid.Add(pilet);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (vas == DialogResult.No)
            {
                btn_click.BackColor = Color.Green;
            };
            if (MessageBox.Show("Sul on ostetud: " + piletid.Count() + " piletid", "Kas tahad saada neid e-mailile?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Saada_piletid(piletid);
            }
        }
    }
}
