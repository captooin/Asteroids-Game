using System.Drawing;
using System;

namespace Asteroids
{
    /// <summary>
    /// Класс, описывающий поведение корабля (игрока)
    /// </summary>
    class Ship : BaseObject
    {
        public Ship (Point pos, Point dir, Size size) : base(pos, dir,size) { }

        private Image _ship = Properties.Resources.ship_pic;
        private int _energy = 100;
        public int Energy => _energy;

        /// <summary>
        /// Метод изменения уровня энергии
        /// </summary>
        /// <param name="n"></param>
        public void EnergyChange(int n)
        {
            _energy -= n;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_ship, Pos);
        }

        public override void Update() { }

        /// <summary>
        /// Метод, позволяющий игроку двигаться вверх
        /// </summary>
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y -= Dir.Y;
        }
        /// <summary>
        /// Метод, позволяющий игроку двигаться вниз
        /// </summary>
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y += Dir.Y;
        }
        /// <summary>
        /// Событие, запускающееся во время смерти
        /// </summary>
        public static event Message MessageDie;

        /// <summary>
        /// Делегат для ведения бортового журнала
        /// </summary>
        public static Action<string> Journal;

        /// <summary>
        /// Метод, запускающий событие смерти
        /// </summary>
        public void Die() { MessageDie?.Invoke(); }
    }
}
