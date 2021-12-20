using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsCourse
{

    public class Object
    {
        public List<Object> objects = new List<Object>();

        public int MousePositionX = 0;
        public int MousePositionY = 0;
        public float X;
        public float Y;
        public int Radius;
        public float SpeedX;
        public float SpeedY;
        public int pRadius = 10;
        public Color _color = Color.Green;

        public Action<Particle> OnParticleOverlap;
        public static Particle particle;

        public Object(float x, float y, int radius)
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
            /*
            float gX = X - particle.GetX();
            float gY = Y - particle.GetY();
            */
            float X2 = particle.GetX();
            float Y2 = particle.GetY();

            double r = Math.Sqrt((X2 - X) * (X2 - X) + (Y2 - Y) * (Y2 - Y));
            return (r - 10 < Radius);
        }

        public static Random rand = new Random();

        public void UpdateState()
        {
            X = MousePositionX;
            Y = MousePositionY;
        }

        public virtual void Draw(Graphics g)
        {
                var color = _color;
                SolidBrush SB = new SolidBrush(color);
                g.FillEllipse(SB, X - Radius, Y - Radius, Radius * 2, Radius * 2);
                SB.Dispose();
        }

        public float GetX()
        {
            return X;
        }

        public float GetY()
        {
            return Y;
        }

        public Color GetColor()
        {
            return _color;
        }
    }
}
