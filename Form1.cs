using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarsField
{
    public partial class Form1 : Form
    {
        public class Star
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }
        }

        public Star[] star = new Star[15000];

        private Random random;
        private Graphics Graphics;


        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics.Clear(Color.Black);

            foreach (var stars in star)
            {
                DrawStar(stars);
                MoveStar(stars);
            }
            pictureBox1.Refresh();
        }

        private void MoveStar(Star stars)
        {
            stars.Z -= 30;
            if (stars.Z<1)
            {
                stars.Z = random.Next(1, pictureBox1.Width);
                stars.X = random.Next(-pictureBox1.Width, pictureBox1.Width);
                stars.Y = random.Next(-pictureBox1.Height, pictureBox1.Height);
            }
        }

        private void DrawStar(Star stars)
        {
            float starSize = Map(stars.Z,0,pictureBox1.Width,9,0);

            float x = Map(stars.X / stars.Z, 0, 1, 0, pictureBox1.Width) + pictureBox1.Width;

            float y = Map(stars.Y / stars.Z, 0, 1, 0, pictureBox1.Height) + pictureBox1.Height;

            Graphics.FillEllipse(Brushes.AliceBlue, x, y, starSize, starSize);
        }

        private float Map(float n,float start1,float stop1,float start2,float stop2)
        {
            return (n - start1) / (stop1 - start1) * (stop2 - start2) + start2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics = Graphics.FromImage(pictureBox1.Image);

            random = new Random();

            for (int i = 0; i < star.Length; i++)
            {
                star[i] = new Star()
                {
                    X = random.Next(-pictureBox1.Width, pictureBox1.Width),
                    Y = random.Next(-pictureBox1.Height, pictureBox1.Height),
                    Z = random.Next(1, pictureBox1.Width)
                };
            }

            timer1.Start();
        }
    }
}
