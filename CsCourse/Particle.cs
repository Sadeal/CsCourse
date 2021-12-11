using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsCourse
{
    class Particle
    {
        public int _radius;
        public float _x;
        public float _y;

        public float _direction;
        public float _speed;
        public float _amount;

        public static Random rand = new Random();

        public Particle()
        {
            _amount = rand.Next(500);
            _direction = rand.Next(360);
            _speed = 1 + rand.Next(10);
            _radius = 2 + rand.Next(10);
        }
    }
}
