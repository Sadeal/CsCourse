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
        Random rand = new Random();

        public int MousePositionX = 0;
        public int MousePositionY = 0;

            particle._radius = Particle.rand.Next(_radiusMin, _radiusMax);
        }

        public void UpdateState()
        {
            int particlesToCreate = _particlesPerTick;

            foreach (var particle in particles)
            {
                particle._life -= 1;
                if (particle._life < 0)
                {
                    particle._life = 20 + Particle.rand.Next(100);
                    particle._x = rand.Next(0, 775);
                    particle._y = 0;
                    var _direction = (double)Particle.rand.Next(360);
                    var _speed = 1 + Particle.rand.Next(10);
                    particle._speedX = (float)(Math.Cos(_direction / 180 * Math.PI) * _speed);
                    particle._speedY = -(float)(Math.Sin(_direction / 180 * Math.PI) * _speed);
                    particle._radius = 2 + Particle.rand.Next(10);
                    if(particlesToCreate > 0)
                    {
                        particlesToCreate -= 1;
                        ResetParticle(particle);
                    }
                }
                else
                {
                    particle._speedX += GravitationX;
                    particle._speedY += GravitationY;
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
            /*while(particlesToCreate >= 1)
            {
                particlesToCreate -= 1;
                var particle = CreateParticle();
                ResetParticle(particle);
                particles.Add(particle);
            }*/
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
