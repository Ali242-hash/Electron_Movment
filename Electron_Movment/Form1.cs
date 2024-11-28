using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Electron_Movment
{
    public partial class Form1 : Form
    {
        Timer ElectronTimer = new Timer();
        int voltspeed = 12;
        int dirextionX = 0;//-1 = left, 1=right, 0 = vertical
        int directionY = -1; // -1 = up, 1= down, 0 = horizantal
        int electronResult = 0;
        Timer GenerateTimer = new Timer();
        int RandomSpeed = 300000;
        int Distance = 0;





        public Form1()
        {
            InitializeComponent();
            Start();
        }

        void Start()
        {
            Setuptimer();
            Addevent();
            label1.Font = new Font(label1.Font.FontFamily, 10, FontStyle.Bold);
            CalculateDistance();


        }

        void CalculateDistance()
        {

            Distance = (line1.Top - line3.Bottom) + (line2.Left - line4.Right) + (line3.Bottom - line1.Top) + (line4.Right - line2.Left);
        }

        void Setuptimer()
        {
            ElectronTimer.Interval = 16;
            ElectronTimer.Start();
            ElectronTimer.Tick += ElectronEvent;

            GenerateTimer.Interval = RandomSpeed;
            GenerateTimer.Start();
            GenerateTimer.Tick += GenerateEvent;
        }

        void GenerateEvent(object s, EventArgs e)
        {
            PictureBox Electron = new PictureBox();
            Electron.Width = 30;
            Electron.Height = 30;

            Electron.BackColor = Color.Blue;


            Electron.Location = new Point(line1.Left - line1.Width / 2);
            Electron.Top = 150;


            this.Controls.Add(Electron);

            voltspeed = Distance / RandomSpeed / ElectronTimer.Interval;
            electron.Left = (line1.Left + line1.Right) / 2;
            electron.Top = line1.Top;
            dirextionX = -1;
            directionY = 0;

            electronResult = 0;
            label1.Text = $" Electron Passes {electronResult}";


        }



        void Addevent()
        {
            ButtonIncrease.Click += Uppervoltage;
            ButtonDecrease.Click += Lowervoltage;
            Buttonchangelamp.Click += LampChange;
        }

        void LampChange(object s, EventArgs e)
        {
            lamp.BackColor = Color.ForestGreen;
        }

        void Uppervoltage(object s, EventArgs e)
        {
            if (voltspeed < 24)
            {
                voltspeed += 1;
                label1.Text = voltspeed.ToString();
                label1.Text = $"Volt speed {voltspeed}";

            }
        }

        void Lowervoltage(object s, EventArgs e)
        {
            if (voltspeed > 1)
            {
                voltspeed -= 1;
                label1.Text = voltspeed.ToString();
                label1.Text = $"Volt speed {voltspeed}";
            }
        }

        void ElectronEvent(object s, EventArgs e)
        {
            if (dirextionX == 0 && directionY == -1)
            {
                if (electron.Top > line1.Top)
                {
                    electron.Top -= voltspeed;
                }
                else
                {
                    electron.Top = line1.Top;
                    dirextionX = -1;
                    directionY = 0;
                }
            }

            else if (dirextionX == -1 && directionY == 0)
            {
                if (electron.Left > line2.Left)
                {
                    electron.Left -= voltspeed;
                }
                else
                {
                    electron.Left = line2.Left;
                    dirextionX = 0;
                    directionY = 1;
                }
            }
            else if (dirextionX == 0 && directionY == 1)
            {
                if (electron.Top < line3.Bottom - electron.Height + 10)
                {
                    electron.Top += voltspeed;
                }
                else
                {
                    electron.Top = line3.Bottom - electron.Height + 10;
                    dirextionX = 1;
                    directionY = 0;
                }
            }
            else if (dirextionX == 1 && directionY == 0)
            {
                if (electron.Left < line4.Right - electron.Width + 10)
                {
                    electron.Left += voltspeed;

                }

                else
                {
                    electron.Left = line4.Right - electron.Width + 10;
                    dirextionX = 0;
                    directionY = -1;
                    electronResult += 1;
                    label1.Text = $"Electron Passes {electronResult}";
                    electron.Visible = true;
                }
            }

            if (electron.Left > lamp.Left && electron.Left < lamp.Right && electron.Top > lamp.Top && electron.Top < lamp.Bottom)
            {
                lamp.BackColor = Color.Gold;
            }
        }
    }
}
