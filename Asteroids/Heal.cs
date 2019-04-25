using System;
using System.Drawing;


namespace Asteroids
{
    /// <summary>
    /// Класс, описывающй поведение аптечек
    /// </summary>
    class Heal : BaseObject
    {
        public Heal (Point pos, Point dir, Size size) :base(pos, dir, size) { }

        private Image _heal = Properties.Resources.heart;

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_heal, Pos);
        }

        public override void Update()
        {
            Pos.X -= Dir.X;
        }
    }
}
