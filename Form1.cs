using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudJump
{
    public partial class Form1 : Form
    {
        bool goleft, goright; // determine the direction of player
        bool jumping;

        int jumpSpeed = 5;
        int force = 2;
        int score = 0;

        public Form1()
        {
            InitializeComponent();
        }

        // This function will trigger each time the buttons are pressed.
        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }
            if (e.KeyCode == Keys.Space && !jumping)
            {
                jumping = true;
            }
        }
        
        // This function will trigger each time the buttons are released.
        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if (jumping)
            {
                jumping = false;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            txtScore.Text = "Stars: " + score;

            // Gives the game a gravity effect
            player.Top += jumpSpeed;

            // Checking if player is jumping and force of jump < 0 if so, change jump back to false
            if (jumping && force < 0)
            {
                jumping = false;
            }

            // If goleft = true then push player towards left of screen
            if (goleft)
            {
                player.Left -= 5;
            }

            // If goright = true then push player towards right of screen
            if (goright)
            {
                player.Left += 5;
            }

            // This is to give the jump a limit.
            if (jumping)
            {
                jumpSpeed = -12;
                force -= 1;
            }

            // ELSE character is not jumping and still has gravity effect
            else
            {
                jumpSpeed = 4;
            }

            foreach (Control x in this.Controls)
            {
                //When player intersects with platform then allow player onto platform
                if (x is PictureBox && (string)x.Tag == "platform")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && !jumping)
                    {
                        force = 8;
                        player.Top = x.Top - player.Height;
                        x.BringToFront();
                    }
                }

                //Eachtime player intersects with star, remove it and increase score
                if (x is PictureBox && (string)x.Tag == "star")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && !jumping)
                    {
                        this.Controls.Remove(x);
                        score++;
                    }
                }
                
                //Loosing condition
                if (x is PictureBox && (string)x.Tag == "edge")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && !jumping)
                    {
                        Timer1.Stop();
                        MessageBox.Show("YOU LOST!  Stars: " + score); ;
                    }
                }
            }

                // Winning condition
                if (player.Bounds.IntersectsWith(door.Bounds))
                {
                    Timer1.Stop();
                    MessageBox.Show("YOU WON!   Stars: " + score); ;
            }
        }

    }
}
