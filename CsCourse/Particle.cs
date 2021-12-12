using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

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
        public Color _color = Color.Red;

        public static Random rand = new Random();

        public virtual GraphicsPath GetGraphicsPath()
        {
            // пока возвращаем пустую форму
            return new GraphicsPath();
        }

        public virtual bool Overlaps(Particle obj, Graphics g)
        {
            // берем информацию о форме
            var path1 = this.GetGraphicsPath();
            var path2 = obj.GetGraphicsPath();

            // используем класс Region, который позволяет определить 
            // пересечение объектов в данном графическом контексте
            var region = new Region(path1);
            region.Intersect(path2); // пересекаем формы
            return !region.IsEmpty(g); // если полученная форма не пуста то значит было пересечение
        }

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
        public virtual void Draw(Graphics g)
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
        public void SetColor(Color color)
        {
            _color = color;
        }
    }
    public class ParticleColorful : Particle
    {
        // два новых поля под цвет начальный и конечный
        public Color FromColor;
        public Color ToColor;

        // для смеси цветов
        public static Color MixColor(Color color1, Color color2, float k)
        {
            return Color.FromArgb(
                (int)(color2.A * k + color1.A * (1 - k)),
                (int)(color2.R * k + color1.R * (1 - k)),
                (int)(color2.G * k + color1.G * (1 - k)),
                (int)(color2.B * k + color1.B * (1 - k))
            );
        }

        public override void Draw(Graphics g)
        {
            float k = Math.Min(1f, _life / 100);

            // так как k уменьшается от 1 до 0, то порядок цветов обратный
            var color = Color.Blue;
            var SB = new SolidBrush(color);
            g.FillEllipse(SB, _x - _radius, _y - _radius, _radius * 2, _radius * 2);
            SB.Dispose();
        }
    }
}
