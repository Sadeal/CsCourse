﻿using System;
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

        public Main()
        {
            InitializeComponent();
            canvas.Image = new Bitmap(canvas.Width, canvas.Height); // привязка изображения
            InitColorCircle();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState();

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
                }

                colorCircle.OnParticleOverlap += (prt) =>
                {
                    (prt as Particle).SetColor(colorCircle._color);
                };

                emitter.Render(g);
                colorCircle.Draw(g);
            }

            canvas.Invalidate();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            emitter.MousePositionX = e.X;
            emitter.MousePositionY = e.Y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
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
