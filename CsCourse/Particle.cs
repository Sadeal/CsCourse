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
        public Color _color = Color.Blue;

        public static Random rand = new Random();

        public void Draw(Graphics g)
        {
            // рассчитываем коэффициент прозрачности по шкале от 0 до 1.0
            float k = Math.Min(1f, _life / 100);
            int alpha = (int)(k * 255);

            // создаем цвет из уже существующего, но привязываем к нему еще и значение альфа канала
            var color = Color.FromArgb(alpha, _color);

            var SB = new SolidBrush(color);
            g.FillEllipse(SB, _x - _radius, _y - _radius, _radius * 2, _radius * 2); // залитый круг с фикс. радиусом

            SB.Dispose(); // удаление кисти (free spacing)
        }

        public float GetX()
        {
            return _x;
        }

        public float GetY()
        {
            return _y;
        }

        public Color GetColor()
        {
            return _color;
        }

        public float GetSpeedX()
        {
            return _speedX;
        }

        public float GetSpeedY()
        {
            return _speedY;
        }

        public void SetSpeedX(float speedX)
        {
            _speedX = speedX;
        }

        public void SetSpeedY(float speedY)
        {
            _speedY = speedY;
        }

        public void SetColor(Color color)
        {
            _color = color;
        }

        public void SetRadius(decimal radius)
        {
            _radius = (int)radius;
        }
    }
}
