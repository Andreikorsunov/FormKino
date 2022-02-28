using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kinoform
{
    class ValiFilm3 : System.Windows.Forms.Form
    {
        public static string filmnimi;
        public ValiFilm3()
        {
            this.Text = "Suur saal";
            this.BackgroundImage = Image.FromFile(@"..\..\Backgrounds\bg2.jpg");
            this.Size = new System.Drawing.Size(800, 450);

            PictureBox film = new PictureBox
            {
                Image = Image.FromFile(@"..\..\Filmid\smees.jpg"),
                Location = new System.Drawing.Point(20, 75),
                Size = new System.Drawing.Size(150, 250),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(film);
            film.Click += Osta_Pilet;

            PictureBox film2 = new PictureBox
            {
                Image = Image.FromFile(@"..\..\Filmid\megamozg.jpg"),
                Location = new System.Drawing.Point(220, 75),
                Size = new System.Drawing.Size(150, 250),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(film2);
            film2.Click += Osta_Pilet2;

            PictureBox film3 = new PictureBox
            {
                Image = Image.FromFile(@"..\..\Filmid\bat.jpg"),
                Location = new System.Drawing.Point(420, 75),
                Size = new System.Drawing.Size(150, 250),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(film3);
            film3.Click += Osta_Pilet3;

            PictureBox film4 = new PictureBox
            {
                Image = Image.FromFile(@"..\..\Backgrounds\soon.jpg"),
                Location = new System.Drawing.Point(620, 75),
                Size = new System.Drawing.Size(150, 250),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(film4);
            //film4.Click += Osta_Pilet4;
        }
        private void Osta_Pilet(object sender, EventArgs e)
        {
            filmnimi = "Superman";
            FilmForm3 uus_aken = new FilmForm3(9, 9, filmnimi);
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.ShowDialog();
        }
        private void Osta_Pilet2(object sender, EventArgs e)
        {
            filmnimi = "Megamind";
            FilmForm3 uus_aken = new FilmForm3(9, 9, filmnimi);
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.ShowDialog();
        }
        private void Osta_Pilet3(object sender, EventArgs e)
        {
            filmnimi = "Batman";
            FilmForm3 uus_aken = new FilmForm3(9, 9, filmnimi);
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.ShowDialog();
        }
    }
}
