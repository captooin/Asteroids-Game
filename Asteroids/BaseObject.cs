using System;
using System.Drawing;

namespace Asteroids
{
    /// <summary>
    /// Абстрактный класс объектов игрового поля
    /// </summary>
    abstract class BaseObject : ICollision
    {
        /// <summary>
        /// Текущая позиция на игровом поле
        /// </summary>
        protected Point Pos;
        /// <summary>
        /// Направление движения, заданное приращениями по осям игрового поля
        /// </summary>
        protected Point Dir;
        /// <summary>
        /// Размер
        /// </summary>
        protected Size Size;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pos">Текущая позиция на игровом поле</param>
        /// <param name="dir">Направление движения, заданное приращениями по осям игрового поля</param>
        /// <param name="size">Размер</param>
        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
            //реализация исключения, которое выдается при некорректно заданных параметрах объекта
            if (Dir.X < -5 || Dir.Y < -5 || Size.Height > 50 || Size.Width > 50)
                throw new GameObjectException();

        }
        /// <summary>
        /// Метод отрисовки
        /// </summary>
        public abstract void Draw();
        /// <summary>
        /// Метод, описывающий движение объекта на игровом поле
        /// </summary>
        public abstract void Update();
        
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        /// <summary>
        /// Коллайдер
        /// </summary>
        public Rectangle Rect => new Rectangle(Pos, Size);

        public delegate void Message();
    }
}
