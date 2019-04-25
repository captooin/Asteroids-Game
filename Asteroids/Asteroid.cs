using System.Drawing;
using System;

namespace Asteroids
{
    /// <summary>
    /// Класс, описывающий поведение объекта Астероид на игровом поле
    /// </summary>
    class Asteroid : BaseObject, IComparable<Asteroid>
    {
        /// <summary>
        /// Убойная сила астероида
        /// </summary>
        public int Power { get; set; }

        //Инициализация изображения для астероида
        private Image _asteroid = Properties.Resources.asteroid_pic;
        /// <summary>
        /// Конструктор класса Asteroid
        /// </summary>
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_asteroid, Pos);
        }

        public override void Update()
        {
            Pos.X -= Dir.X;
            if (Pos.X < 0)
            {
                Pos.X = Game.Width + 10;
                Pos.Y = Game.Height - Pos.Y;
            }
        }
        
        /// <summary>
        /// Реализация интерфейса сравнения
        /// </summary>
        public int CompareTo(Asteroid other)
        {
            if (Power > other.Power) return 1;
            if (Power < other.Power) return -1;
            return 0;
        }
    }
}
