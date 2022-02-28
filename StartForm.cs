using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kinoform
{
    class StartForm : System.Windows.Forms.Form
    {
        public StartForm()
        {
            this.Text = "Saali valik";
            this.BackgroundImage = Image.FromFile(@"..\..\Backgrounds\bg.jpg");
            this.Width = 800;
            this.Height = 300;

            Button Start_btn = new Button
            {
                Text = "Väike saal",
                Size = new System.Drawing.Size(90, 80),
                Location = new System.Drawing.Point(370, 90)
            };
            this.Controls.Add(Start_btn);
            Start_btn.Click += Start_btn_Click;

            Button Start_btn_2 = new Button
            {
                Text = "Keskmine saal",
                Size = new System.Drawing.Size(90, 100),
                Location = new System.Drawing.Point(490, 75)
            };
            Start_btn_2.Click += Start_btn_2_Click;
            this.Controls.Add(Start_btn_2);

            Button Start_btn_3 = new Button
            {
                Text = "Suur saal",
                Size = new System.Drawing.Size(90, 120),
                Location = new System.Drawing.Point(610, 65)
            };
            Start_btn_3.Click += Start_btn_3_Click;
            this.Controls.Add(Start_btn_3);

            PictureBox Start_btn_4 = new PictureBox
            {
                Image = Image.FromFile(@"..\..\Backgrounds\cr7.png"),
                Location = new System.Drawing.Point(3, 11),
                Size = new System.Drawing.Size(57, 41),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(Start_btn_4);
            Start_btn_4.Click += Start_btn_4_Click;
        }
        private void Start_btn_Click(object sender, EventArgs e)
        {
            ValiFilm vf = new ValiFilm();
            vf.Show();
        }
        private void Start_btn_2_Click(object sender, EventArgs e)
        {
            ValiFilm2 vf = new ValiFilm2();
            vf.Show();
        }
        private void Start_btn_3_Click(object sender, EventArgs e)
        {
            ValiFilm3 vf = new ValiFilm3();
            vf.Show();
        }
        private void Start_btn_4_Click(object sender, EventArgs e)
        {
            string password = Interaction.InputBox("Password", "Password");
            if (password == "admin")
            {
                AdminForm af = new AdminForm();
                af.Show();
            }
            else
            {
                MessageBox.Show("Sa ei ole admin");
            }
        }
    }
}