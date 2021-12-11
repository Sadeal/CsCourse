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

        private void UpdateState()
        {
            foreach (var particle in particles)
            {
                particle._life -= 1;
                if (particle._life <= 0)
                {
                    particle._life = 20 + Particle.rand.Next(100);
                    particle._x = canvas.Image.Width / 2;
                    particle._y = canvas.Image.Height / 2;
                    particle._direction = Particle.rand.Next(360);
                    particle._speed = 1 + Particle.rand.Next(10);
                    particle._radius = 2 + Particle.rand.Next(10);
                }
                else
                {
                    var directionInRadians = particle._direction / 180 * Math.PI;
                    particle._x += (float)(particle._speed * Math.Cos(directionInRadians));
                    particle._y -= (float)(particle._speed * Math.Sin(directionInRadians));
                    particle._life -= 1;
                }
            }
        }

        private void Render(Graphics g)
        {
            foreach (var particle in particles)
            {
                particle.Draw(g);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateState();

            using (var g = Graphics.FromImage(canvas.Image))
            {
                g.Clear(Color.White);
                Render(g);
            }
            canvas.Invalidate();
        }
    }
}
