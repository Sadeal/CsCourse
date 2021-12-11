using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsCourse
{
    public class Particle
    {
        public int _radius;
        public float _x;
        public float _y;

        public float _speedX;
        public float _speedY;
        public float _amount; // добавить
        public float _life;

        public static Random rand = new Random();

        public Particle()
        {
            _amount = rand.Next(500);
            var _direction = (double)rand.Next(360);
            var _speed = 1 + rand.Next(10);
            _speedX = (float)(Math.Cos(_direction / 180 * Math.PI) * _speed);
            _speedY = -(float)(Math.Sin(_direction / 180 * Math.PI) * _speed);
            _radius = 2 + rand.Next(10);
            _life = 20 + rand.Next(100);
        }
        public void Draw(Graphics g)
        {
            // рассчитываем коэффициент прозрачности по шкале от 0 до 1.0
            float k = Math.Min(1f, _life / 100);
            int alpha = (int)(k * 255);

            // создаем цвет из уже существующего, но привязываем к нему еще и значение альфа канала
            var _color = Color.FromArgb(alpha, Color.Black);

            var SB = new SolidBrush(_color);
            g.FillEllipse(SB, _x - _radius, _y - _radius, _radius * 2, _radius * 2); // залитый круг с фикс. радиусом

            SB.Dispose(); // удаление кисти (free spacing)
        }
    }
}
