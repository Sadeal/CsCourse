using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsCourse
{

    public class ColorCircle
    {
        public float X = 50;
        public float Y = 50;
        public int Radius;
        public Color _color = Color.Red;

        public Action<Particle> OnParticleOverlap;
        public static Particle particle;

        public ColorCircle(float x, float y, int radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public void OverlapParticle(Particle particle)
        {
            OnParticleOverlap?.Invoke(particle);
        }

        public bool OverlapsWith(Particle particle)
        {
            float gX = X - particle.GetX();
            float gY = Y - particle.GetY();

            double r = Math.Sqrt(gX * gX + gY * gY);
            return (r + particle._radius < Radius);
        }

        public static Random rand = new Random();

        /*
        public void UpdateState()
        {
            Form2 f2 = new Form2();
            Radius = (int)f2.nud1.Value;
        }
        */

        public virtual void Draw(Graphics g)
        {
            var color = _color;
            Pen Pen = new Pen(color, 3);
            g.DrawEllipse(Pen, X, Y, GetRadius() * 2, GetRadius() * 2);
            Pen.Dispose();
        }

        public void SetRadius(int _radius)
        {
            Radius = _radius;
        }
        
        public int GetRadius()
        {
            return Radius;
        }
    }
}
