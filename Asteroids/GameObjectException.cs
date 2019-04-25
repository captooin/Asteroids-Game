using System;

namespace Asteroids
{
    /// <summary>
    /// Исключение, которое выдается при несоответствии параметров игрового объекта их допустимым значениям.
    /// </summary>
    class GameObjectException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса GameObjectException
        /// </summary>
        public GameObjectException()
        {
            Console.WriteLine(base.Message);
        }

    }
}
