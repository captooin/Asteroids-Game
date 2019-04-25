using System;
using System.Windows.Forms;

namespace Asteroids
{
    static class Program
    {
        static void Main()
        {
            //Инициализация формы под игровое поле
            Form form = new Form
            {
                Width = Screen.PrimaryScreen.Bounds.Width,
                Height = Screen.PrimaryScreen.Bounds.Height
            };
            Game.Init(form);
            form.Show();
            Application.Run(form);
        
        }
    }
}
