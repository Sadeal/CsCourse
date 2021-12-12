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
        public float GravitationX = 0;
        public float GravitationY = 1;
        public int _x; // координата X центра эмиттера, будем ее использовать вместо MousePositionX
        public int _y; // соответствующая координата Y 
        public int _direction = 0; // вектор направления в градусах куда сыпет эмиттер
        public int _spreading = 360; // разброс частиц относительно Direction
        public int _speedMin = 1; // начальная минимальная скорость движения частицы
        public int _speedMax = 10; // начальная максимальная скорость движения частицы
        public int _radiusMin = 2; // минимальный радиус частицы
        public int _radiusMax = 10; // максимальный радиус частицы
        public int _lifeMin = 20; // минимальное время жизни частицы
        public int _lifeMax = 100; // максимальное время жизни частицы
        public int _particlesPerTick = 1;

        public virtual void ResetParticle(Particle particle)
        {
            particle._life = Particle.rand.Next(_lifeMin, _lifeMax);

            particle._x = _x;
            particle._y = _y;

            var direction = _direction
                + (double)Particle.rand.Next(_spreading)
                - _spreading / 2;

            var speed = Particle.rand.Next(_speedMin, _speedMax);

            particle._speedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            particle._speedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            particle._radius = Particle.rand.Next(_radiusMin, _radiusMax);
        }

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
                    var particle = new ParticleColorful();
                    particles.Add(particle);
                    particle.FromColor = Color.Yellow;
                    particle.ToColor = Color.FromArgb(0, Color.Magenta);
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
