using System.Drawing;
using System;
namespace Asteroids
{
    /// <summary>
    /// Класс, описывающий поведение пули
    /// </summary>
    class Bullet : BaseObject
    {
        /// <summary>
        /// Констуктор
        /// </summary>
        /// <param name="pos">Позиция пули</param>
        /// <param name="dir">Скорость движения</param>
        /// <param name="size">Длина и толщина пули</param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size) { }
        
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X += Dir.X;
        }
    }
}
