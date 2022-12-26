using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Catch_the_Formula
{
    public partial class Form1 : Form
    {

        bool goLeft, goRight;

        int speed = 8;
        int score = 0;
        int missed = 0;

        Random randX = new Random();
        Random randY = new Random();

        PictureBox splash = new PictureBox();


        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void MainGameTimerEvents(object sender, EventArgs e)
        {
            txtScore.Text = "Saved: " + score;
            txtMiss.Text = "Missed: " + missed;

            if (goLeft == true && player.Left > 0)
            {
                player.Left -= 12;
                player.Image = Properties.Resources.Plankton_normal2;
            }
            if (goRight == true && player.Left + player.Width < this.ClientSize.Width)
            {
                player.Left += 12;
                player.Image = Properties.Resources.Plankton1;
            }

            foreach(Control x in this.Controls)
            {

                if (x is PictureBox && (string)x.Tag == "Formula")
                {

                    x.Top += speed;


                    if(x.Top + x.Height > this.ClientSize.Height)
                    {

                        splash.Image = Properties.Resources.splash;
                        splash.Location = x.Location;
                        splash.Height = 60;
                        splash.Width = 60;
                        splash.BackColor = Color.Transparent;

                        this.Controls.Add(splash);


                        x.Top = randY.Next(80, 300) * -1;
                        x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                        missed += 1;
                        player.Image = Properties.Resources.Plankton_hurt;

                    }


                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        x.Top = randY.Next(80, 300) * -1;
                        x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                        score += 1;
                    }

                }
            }

            if(score > 10)
            {
                speed = 12; 
            }

            if(missed > 5)
            {
                GameTimer.Stop();
                MessageBox.Show("Game Over!", Environment.NewLine + "We've Lost The secret Formula!" + Environment.NewLine + "Click OK to Retry!");
                RestartGame();
            }

        }

        private void KeyisDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }

        }

        private void KeyisUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }


        }

        private void RestartGame()
        {

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "Formula")
                {
                    x.Top = randY.Next(80, 300) * -1;
                    x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                }
            }

            player.Left = this.ClientSize.Width / 2;
            player.Image = Properties.Resources.Plankton1;

            score = 0;
            missed = 0;
            speed = 8;

            goLeft = false;
            goRight = false;

            GameTimer.Start();

        }

    }
}
