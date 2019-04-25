using System.Drawing;

namespace Asteroids
{
    /// <summary>
    /// Класс, описывающий поведение планеты Юпитер на игровом поле
    /// </summary>
    class Jupiter : BaseObject
    {
        /// <summary>
        /// Конструктор класса Jupiter
        /// </summary>
        public Jupiter(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        private Image _jupiter = Properties.Resources.jupiter_pic;

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_jupiter, Pos);
        }
        
        public override void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            if (Pos.X < 0) Pos.X = Game.Width;
            if (Pos.X > Game.Width) Pos.X = 0;
            if (Pos.Y < 0) Pos.Y = Game.Height;
            if (Pos.Y > Game.Height) Pos.Y = 0;
        }
    }
}
