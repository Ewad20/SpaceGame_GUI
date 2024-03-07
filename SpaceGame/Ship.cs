using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceGame
{
    internal class Ship
    {
        public Point Position { get; set; } // Aktualna pozycja statku gracza
        public static float SuperShot { get; set; } // Wartość określająca dostępność super-strzału
        public int Health { get; set; }
        public List<Bullet> BulletsList { get; set; } // Aktualna lista pocisków na planszy

        Panel GamePanel; // Pole reprezentujące panel gry, na którym znajduje się statek
        public Ship(Point p, Panel w) // Konstruktor statku gracza
        {
            this.Position = p; // Przypisanie pozycji początkowej statku
            this.GamePanel = w;  // Przypisanie panelu gry, na którym będzie znajdować się statek
            BulletsList = new List<Bullet>(); // Inicjalizacja listy pocisków statku
            Health = 100;  // Ustawienie początkowego zdrowia statku
        }
        public void Move(int speed, Keys key) // Metoda do poruszania statkiem gracza
        {
            Point distance = new Point();  // Inicjalizacja obiektu Point do przechowywania zmiany pozycji statku
            moveKeyboard(ref distance, speed, key); // Pobiera zmianę pozycji na podstawie klawiszy naciśniętych przez gracza
            collisions(distance); // Wywołanie metody collisions, która sprawdza kolizje i aktualizuje pozycję statku w określonych granicach okna gry
        }
        public void Shoot (Keys key) // Metoda do obsługi strzałów statku gracza
        {
            Point distance = new Point();  // Inicjalizacja obiektu Point do przechowywania informacji o strzale
            shootKeyboard(key);  // Wywołanie metody shootKeyboard, która obsługuje strzał na podstawie klawiszy naciśniętych przez gracza
        }
        private void moveKeyboard(ref Point distance, int speed, Keys key)
        {
            // Obsługa klawiszy W, S, A, D do poruszania statkiem
            if (key == Keys.W)
                distance = new Point(0, -1);
            if (key == Keys.S)
                distance = new Point(0, 1);
            if (key == Keys.D)
                distance = new Point(1, 0);
            if (key == Keys.A)
                distance = new Point(-1, 0);

            distance.X *= speed; // Skalowanie odległości przez prędkość statku
            distance.Y *= speed;
        }
        private void shootKeyboard( Keys key) // Metoda obsługująca strzały statku gracza na podstawie klawiszy
        {
            if (key == Keys.Right) // Sprawdzenie, czy naciśnięty został klawisz strzału w prawo
            {
                Bullet bullet = new Bullet(  // Utworzenie nowego obiektu klasy Bullet reprezentującego pocisk
                    new Point(Position.X + 16, Position.Y + 12), // Początkowa pozycja pocisku (zależna od pozycji statku)
                    BulletType1.Normal, // Typ pocisku
                    GamePanel); // Panel, na którym znajduje się gra

                BulletsList.Add(bullet);  // Dodanie nowego pocisku do listy pocisków statku
            }

            else if (key == Keys.Left)
            {
                Bullet bullet = new Bullet(
                    new Point(Position.X, Position.Y + 12),
                    BulletType1.Normal,
                    GamePanel);

                BulletsList.Add(bullet);
            }

            else if (key == Keys.Up)
            {
                if (SuperShot >= 100)
                {
                    Bullet bullet = new Bullet(
                        new Point(Position.X + 12, Position.Y - 12),
                        BulletType1.Special,
                        GamePanel);

                    BulletsList.Add(bullet);
                    SuperShot = 0;
                }
            }
        }

        public bool RemoveBulletsOutsidePanel(Bullet bullet) // Metoda sprawdzająca i usuwająca pociski znajdujące się poza granicami panelu gry
        {
            Rectangle panelBounds = new Rectangle(GamePanel.Location, GamePanel.Size); // Utworzenie prostokąta reprezentującego granice panelu gry
            if (!panelBounds.Contains(bullet.Position)) // Sprawdzenie, czy pozycja pocisku jest poza granicami panelu gry
            {
                BulletsList.Remove(bullet); // Usunięcie pocisku z listy pocisków statku
                bullet.Clear(); // Usunięcie graficznego przedstawienia pocisku z panelu gry
                return true; // Zwrócenie wartości true, informującej o tym, że pocisk został usunięty
            }
            return false;  // Zwrócenie wartości false, informującej o tym, że pocisk pozostaje w granicach panelu
        }

        public bool MoveBullets(Point enemyPosition)  // Metoda obsługująca poruszanie się pocisków statku gracza oraz sprawdzająca, czy któryś z nich trafił w przeciwnika
        {
            bool inTarget = false; // Zmienna informująca, czy jakiś pocisk trafił w przeciwnika
            if (BulletsList != null)
            {
                var isOutside = BulletsList.Any(bullet =>   // Sprawdzenie, czy któryś z pocisków opuścił granice panelu gry
                {

                    bullet.Move();  // Poruszanie pociskiem
                    var enemyWidth = 56; // Szerokość enemy
                    var enemyHeight = 52; // Wysokość enemy

                    var enemyLeft = enemyPosition.X; // Lewa krawędź przeciwnika
                    var enemyRight = enemyPosition.X + enemyWidth; // Prawa krawędź przeciwnika
                    var enemyTop = enemyPosition.Y; // Górna krawędź przeciwnika
                    var enemyBottom = enemyPosition.Y + enemyHeight; // Dolna krawędź przeciwnika

                    if (bullet.Position.X >= enemyLeft && bullet.Position.X + 10 <= enemyRight &&
                        bullet.Position.Y >= enemyTop && bullet.Position.Y <= enemyBottom)   // Sprawdzenie, czy pocisk trafił w przeciwnika
                    {
                        inTarget = true; // Ustawienie flagi, że pocisk trafił w przeciwnika
                        BulletsList.Remove(bullet); // Usuń trafiony pocisk z listy
                        bullet.Clear();
                        return true; // Zwraca true, jeśli pocisk trafił w wroga
                    }

                    return false; // Zwraca false, jeśli pocisk jest w granicach planszy, ale nie trafił w wroga
                });
            
            }
            return inTarget; // Zwraca informację, czy któryś z pocisków trafił w przeciwnika
        }
        private void collisions(Point distance) // Sprawdza kolizje i aktualizuje pozycję statku gracza w określonych granicach okna gry
                                               // Jeśli statek wykroczy poza te granice, zostaje przeniesiony na najbliższą poprawną pozycję
        {
            int windowWidth = GamePanel.Width; // Szerokość panelu
            int windowHeight = GamePanel.Height; // Wysokość panelu

            Point positionAux = new Point(Position.X + distance.X, Position.Y + distance.Y); // Tworzy tymczasową pozycję statku, uwzględniając przesunięcie od klawiszy

            // Sprawdzanie i aktualizowanie pozycji statku w zależności od granic panelu
            if (positionAux.X <= 0)
                positionAux.X = 0;
            if (positionAux.X + 6 >= windowWidth)
                positionAux.X = windowWidth - 7;
            if (positionAux.Y <= 0)
                positionAux.Y = 0;
            if (positionAux.Y + 2 >= windowHeight)
                positionAux.Y = windowHeight - 3;

            Position = positionAux; // Aktualizuje pozycję statku
        }

        public void Dead() // Metoda wywoływana po zniszczeniu statku gracza
        {
            MessageBox.Show("You are dead, try again");
        }
    }
}
