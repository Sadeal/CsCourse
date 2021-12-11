using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsCourse
{
    class Emitter
    {
        List<Particle> particles = new List<Particle>();

        public int MousePositionX = 0;
        public int MousePositionY = 0;

        public void UpdateState()
        {
            foreach (var particle in particles)
            {
                particle._life -= 1;
                if (particle._life < 0)
                {
                    particle._life = 20 + Particle.rand.Next(100);
                    particle._x = MousePositionX;
                    particle._y = MousePositionY;
                    var _direction = (double)Particle.rand.Next(360);
                    var _speed = 1 + Particle.rand.Next(10);
                    particle._speedX = (float)(Math.Cos(_direction / 180 * Math.PI) * _speed);
                    particle._speedY = -(float)(Math.Sin(_direction / 180 * Math.PI) * _speed);
                    particle._radius = 2 + Particle.rand.Next(10);
                }
                else
                {
                    particle._x += particle._speedX;
                    particle._y += particle._speedY;
                }
            }

            for (var i = 0; i < 10; ++i) // _amount
            {
                if (particles.Count < 500)
                {
                    var particle = new Particle();
                    particle._x = MousePositionX;
                    particle._y = MousePositionY;
                    particles.Add(particle);
                }
                else
                {
                    break;
                }
            }
        }

        public void Render(Graphics g)
        {
            foreach (var particle in particles)
            {
                particle.Draw(g);
            }
        }
    }
}
