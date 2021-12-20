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

        public static ColorCircle colorCircle;
        public static Object obj;
        public static KillCircle killCircle;

        public Main()
        {
            InitializeComponent();
            canvas.Image = new Bitmap(canvas.Width, canvas.Height); // привязка изображения
            InitColorCircle();
            InitObject();
            InitKillCircle();
        }

        Emitter emitter = new Emitter();

        private void InitColorCircle()
        {
            colorCircle = new ColorCircle(
                canvas.Image.Width / 2,
                canvas.Image.Height / 2,
                50
                );

            colorCircle.OnParticleOverlap += (prt) =>
            {
                (prt as Particle).SetColor(colorCircle._color);
                (prt as Particle).SetRadius(colorCircle.GetPrtRadius());
            };
        }

        private void InitObject()
        {
            obj = new Object(
                canvas.Image.Width / 2 - canvas.Image.Width / 4,
                canvas.Image.Height / 2 + canvas.Image.Height / 4,
                50
                );

            obj.OnParticleOverlap += (prt) =>
            {
                var difX = prt.GetX() - obj.GetX();
                var difY = prt.GetY() - obj.GetY();
                if (obj.GetX() < prt.GetX())
                {
                    if (difX > 5) difX = 5;
                    prt.SetSpeedX(difX);
                }
                else
                {
                    if (difX < -5) difX = -5;
                    prt.SetSpeedX(difX);
                }

                if (obj.GetY() < prt.GetY())
                {
                    if (difY > 3) difY = 3;
                    (prt as Particle).SetSpeedY(difY);
                }
                else
                {
                    if (difY < -3) difY = -3;
                    (prt as Particle).SetSpeedY(difY);
                }
                    prt.SetColor(obj.GetColor());
            };
        }

        public void InitKillCircle()
        {

            killCircle = new KillCircle(
                canvas.Image.Width / 2 + canvas.Image.Width / 4,
                canvas.Image.Height / 2 + canvas.Image.Height / 4,
                50
                );

            killCircle.OnParticleOverlap += (prt) =>
            {
                prt.SetLife(0);
                killCircle.SetCount(1);
            };
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState();
            obj.UpdateState();

            using (var g = Graphics.FromImage(canvas.Image))
            {
                g.Clear(Color.White);

                /*
                foreach(var particle in Particle)
                {
                    // проверяю было ли пересечение с игроком
                    if (ColorCircle.OverlapsWith(Particle)
                    {
                        // и если было вывожу информацию на форму
                        txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
                    }
                }
                */

                foreach (var particle in emitter.particles.ToList())
                {
                    if (colorCircle.OverlapsWith(particle))
                    {
                        colorCircle.OverlapParticle(particle);
                    }

                    if (obj.OverlapsWith(particle))
                    {
                        obj.OverlapParticle(particle);
                    }

                    if (killCircle.OverlapsWith(particle))
                    {
                        killCircle.OverlapParticle(particle);
                    }
                }

                emitter.Render(g);
                colorCircle.Draw(g);
                obj.Draw(g);
                killCircle.Draw(g);
            }

            canvas.Invalidate();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            obj.MousePositionX = e.X;
            obj.MousePositionY = e.Y;
        }

        private void canvas_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) obj.Radius += 5;
            else obj.Radius -= 5;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            colorCircle.SetRadius(trackBar1.Value);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            colorCircle.SetX(trackBar2.Value);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            colorCircle.SetY(trackBar3.Value);
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            colorCircle.SetPrtRadius(trackBar4.Value);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                colorCircle.SetColor(colorDialog1.Color);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Start")
            {
                timer1.Start();
                button2.Text = "Stop";
            }
            else
            {
                timer1.Stop();
                button2.Text = "Start";
            }
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            double r = Math.Sqrt((e.X - killCircle.GetX()) * (e.X - killCircle.GetX()) + (e.Y - killCircle.GetY()) * (e.Y - killCircle.GetY()));

            if (e.Button == MouseButtons.Left)
            {
                killCircle.SetX(e.X);
                killCircle.SetY(e.Y);
                killCircle.SetColor(Color.Purple, Color.Black);
                killCircle.SetRadius(50);
            }
            else
            {
                if ((e.Button == MouseButtons.Right)&&(r <= killCircle.GetRadius()))
                {
                    killCircle.SetColor(Color.White, Color.White);
                    killCircle.SetRadius(0);
                    killCircle.Count = 0;
                    killCircle.SetX(-10);
                    killCircle.SetY(-10);
                }
            }
        }

        /*
        public virtual GraphicsPath GetGraphicsPath()
        {
            // пока возвращаем пустую форму
            return new GraphicsPath();
        }

        public virtual bool Overlaps(Particle particle, Graphics g)
        {
            // берем информацию о форме
            var path1 = this.GetGraphicsPath();
            var path2 = particle.GetGraphicsPath();

            // применяем к объектам матрицы трансформации
            path1.Transform(this.GetTransform());
            path2.Transform(obj.GetTransform());

            // используем класс Region, который позволяет определить 
            // пересечение объектов в данном графическом контексте
            var region = new Region(path1);
            region.Intersect(path2); // пересекаем формы
            return !region.IsEmpty(g); // если полученная форма не пуста то значит было пересечение
        }
        */

    }
}
