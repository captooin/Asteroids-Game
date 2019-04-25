using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Asteroids
{
    class Game
    {
        //Объявление инструментов отрисовки графики
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        //Ширина и высота игрового поля        
        public static int Width { get; set; }
        public static int Height { get; set; }
        static Game() { }
        //Инициализация генератора случайных чисел для разнообразия поведения игровых объектов
        static Random r = new Random();
        //Инициализация таймера, задающего частоту обновления кадра
        static Timer timer = new Timer { Interval = 100 };

        //Объявление всех игровых объектов
        private static BaseObject[] _objs;
        private static List<Bullet> _bullets;
        private static List<Asteroid> _asteroids;
        private static Ship _ship;
        private static Heal _heal;

        //константы для быстрого регулирования скорости основных объектов и счетчики
        private static int asteroids_destroyed = 0;
        private static int asteroids_count = 3;
        private const int BULLET_VELOCITY = 6;
        private const int ASTEROID_VELOCITY = 6;


        /// <summary>
        /// Метод, инициализирующий все игровые объекты
        /// </summary>
        public static void Load()
        {
            _ship = new Ship(new Point(0, 300), new Point(5, 5), new Size(50, 50));
            _asteroids = new List<Asteroid>();
            _bullets = new List<Bullet>();
            _objs = new BaseObject[35];

            for (int i = 0; i < _objs.Length - 1; i++)
                _objs[i] = new Star(new Point(Width, i * 20), new Point(r.Next(1, _objs.Length), r.Next(-3, 3)), new Size(5, 5));
            _objs[_objs.Length - 1] = new Jupiter(new Point(600, 20), new Point(2, 4), new Size(0, 0));

            AsteroidsLoad();
        }

        /// <summary>
        /// Метод, инициализирующий группу астероидов
        /// </summary>
        private static void AsteroidsLoad()
        {
            //Шанс на спавн аптечки ~ 20% при условии, что энергия корабля меньше 80, спавн только между волнами астероидов
            if (_ship.Energy < 80 && r.Next(0, 100) < 20)
                _heal = new Heal(new Point(1000, r.Next(50, 750)), new Point(ASTEROID_VELOCITY * 2, 0), new Size(20, 20));

            for (int i = 0; i < asteroids_count; i++)
            {
                _asteroids.Add(new Asteroid(new Point(1000+i*50, r.Next(50, 750)), new Point(ASTEROID_VELOCITY, 0), new Size(20, 20)));
            }
            asteroids_count++;
        }

        /// <summary>
        /// Метод, инициализирующий игровое поле и графические инструменты
        /// </summary>
        /// <param name="form"></param>
        public static void Init(Form form)
        {
            Load();
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            //Далее контроль ширины и высоты игрового поля
            try
            {
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            if (Width > 1000 || Height > 1000)
                throw new ArgumentOutOfRangeException();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Размеры экрана превышают допустимые.");
                if (Width > 1000) Width = 1000; form.Width = 1000;
                if (Height > 800) Height = 800; form.Height = 800;
                Console.WriteLine("Выставлен максимальный размер окна.");
            }

            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            timer.Start();
            timer.Tick += Timer_Tick;
            form.KeyDown += Form_KeyDown;
            Ship.MessageDie += Finish;
            Ship.Journal += Console.WriteLine;
            Ship.Journal("LET'S BEGIN!");
        }


        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) _bullets.Add(new Bullet(new Point(_ship.Rect.X + 48, _ship.Rect.Y + 20), new Point(BULLET_VELOCITY, 0), new Size(4, 1)));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Метод отрисовки всех игровых объектов и игрового поля
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            _ship?.Draw();
            foreach (BaseObject obj in _objs) obj.Draw();
            foreach (Bullet b in _bullets) b.Draw();
            foreach (Asteroid a in _asteroids) a.Draw();
            _heal?.Draw();

            Buffer.Graphics.DrawString("Energy:" + _ship?.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            Buffer.Graphics.DrawString("Asteroids destroyed:" + asteroids_destroyed, SystemFonts.DefaultFont, Brushes.White, 80, 0);
            Buffer.Render();            
        }

        /// <summary>
        /// Совокупный метод, определяющий движения объектов по игровому полю и их взаимодействие
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();

            //foreach (Asteroid asteroid in _asteroids)
            for (int i = 0; i < _asteroids.Count; i++)
            {
                _asteroids[i].Update();
                foreach (Bullet bullet in _bullets)
                {
                    bullet.Update();
                    if (bullet.Collision(_asteroids[i]))
                    {
                        System.Media.SystemSounds.Asterisk.Play();
                        _bullets.Remove(bullet);
                        _asteroids.RemoveAt(i);
                        Ship.Journal($"Asteroid Destroyed! Total: {++asteroids_destroyed}");
                        break;
                    }
                }
                if (i >= _asteroids.Count) break;
                if (!_ship.Collision(_asteroids[i])) continue;
                _ship?.EnergyChange(r.Next(1, 10));
                Ship.Journal($"BOOM! Energy: {_ship.Energy}");
                _asteroids.RemoveAt(i);
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0) _ship?.Die();
            }
            if (_asteroids.Count == 0) AsteroidsLoad();
            
            _heal?.Update();
            if (_heal != null && _ship.Collision(_heal))
            {
                _ship?.EnergyChange(-20);
                _heal = null;
            }
            Buffer.Render();
        }

        /// <summary>
        /// Метод завершения игры
        /// </summary>
        public static void Finish()
        {
            timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Ship.Journal("THE END");
            Buffer.Render();
        }
    }
}
