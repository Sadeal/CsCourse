using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsCourse
{

    public class KillCircle
    {

        public int MousePositionX = 0;
        public int MousePositionY = 0;
        public float X;
        public float Y;
        public int Radius;
        public int Count;
        public Color _color = Color.Purple;
        public Color _color2 = Color.Black;

        public Action<Particle> OnParticleOverlap;
        public static Particle particle;

        public KillCircle(float x, float y, int radius)
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
            float X2 = particle.GetX();
            float Y2 = particle.GetY();

            double r = Math.Sqrt((X2 - X) * (X2 - X) + (Y2 - Y) * (Y2 - Y));
            return (r < Radius);
        }

        public static Random rand = new Random();

        public virtual void Draw(Graphics g)
        {
            var color = _color;
            var color2 = _color2;
            SolidBrush SB1 = new SolidBrush(color);
            SolidBrush SB2 = new SolidBrush(color2);
            g.FillEllipse(SB1, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            g.DrawString(Convert.ToString(Count), new Font("Verdana", 8), SB2, X, Y);
            SB1.Dispose();
            SB2.Dispose();
        }

        public void SetColor(Color first, Color second)
        {
            _color = first;
            _color2 = second;
        }

        public void SetRadius(int radius)
        {
            Radius = radius;
        }

        public void SetCount(int count)
        {
            Count += count;
        }

        public void SetX(int x)
        {
            X = x;
        }

        public void SetY(float y)
        {
            Y = y;
        }

        public int GetRadius()
        {
            return Radius;
        }

        public float GetX()
        {
            return X;
        }

        public float GetY()
        {
            return Y;
        }
    }
}
