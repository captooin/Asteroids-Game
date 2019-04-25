using System;
using System.Drawing;

namespace Asteroids
{
    /// <summary>
    /// Класс объекта, представляющего собой движущуюся на заднем плане звезду
    /// </summary>
    class Star : BaseObject
    {
        /// <summary>
        /// Конструктор rkfccf Star
        /// </summary>
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }

        public override void Update()
        {
            Pos.X -= Dir.X;
            Pos.Y += Dir.Y;
            if (Pos.X < 0) Pos.X = Game.Width;
            if (Pos.Y < 0) Pos.Y = Game.Height;
            if (Pos.Y > Game.Height) Pos.Y = 0;
        }

    }
}
