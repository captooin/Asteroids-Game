using System.Drawing;

namespace Asteroids
{
    /// <summary>
    /// Определяет обобщенный метод выявления столкновений текущего объекта с другими
    /// </summary>
    interface ICollision
    {
        /// <summary>
        /// Метод выявления столкновений текущего объекта с другими
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Collision(ICollision obj);
        /// <summary>
        /// Коллайдер объекта
        /// </summary>
        Rectangle Rect { get; }
    }
}
