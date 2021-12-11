using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsCourse
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
            canvas.Image = new Bitmap(canvas.Width, canvas.Height); // привязка изображения
        }

        Emitter emitter = new Emitter();

        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState();

            using (var g = Graphics.FromImage(canvas.Image))
            {
                g.Clear(Color.White);
                emitter.Render(g);
            }
            canvas.Invalidate();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            emitter.MousePositionX = e.X;
            emitter.MousePositionY = e.Y;
        }
    }
}
