using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsCourse
{
    public partial class Main : Form
    {
        List<Particle> particles = new List<Particle>();
        public Main()
        {
            InitializeComponent();

            canvas.Image = new Bitmap(canvas.Width, canvas.Height); // привязка изображения

            for (var i = 0; i < 500; ++i) // _amount
            {
                var particle = new Particle();

                particle._x = canvas.Image.Width / 2;
                particle._y = canvas.Image.Height / 2;

                particles.Add(particle);
            }
        }

        int counter = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            using (var g = Graphics.FromImage(canvas.Image))
            {
                g.Clear(Color.White);
                g.DrawString(
                    counter.ToString(),
                    new Font("Verdana", 9),
                    new SolidBrush(Color.Black),
                    new PointF
                    { // по центру экрана
                        X = canvas.Image.Width / 2,
                        Y = canvas.Image.Height / 2
                    }
                );
            }
            canvas.Invalidate();
        }
    }
}
