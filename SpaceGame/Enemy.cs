using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SpaceGame
{
    public enum TypeEnemy // Typy wroga
    {
        Normal, Boss, Menu
    }
    internal class Enemy   // Klasa reprezentująca wroga
    {
        enum Direction  // Enumeracja reprezentująca kierunki ruchu wroga
        {
            Right, Left, Up, Down
        }
        public bool IsAlive { get; set; } // Okresla, czy wrog jest zywy
        public float Health { get; set; } // Zdrowie wroga
        public Point Position { get; set; } // Pozycja wroga
        public Panel GamePanel { get; set; } // Okno gry, w ktorym wrog sie porusza

        public PictureBox EnemyPictureBox { get; set; } // Właściwość przechowująca obiekt PictureBox związanego z danym obiektem wroga
        public List<Bullet> Bullets { get; set; }  // Lista pociskow wrogow

        private Direction direction; // Kierunek poruszania

        private DateTime timeMovement; // Czas od ostatniego ruchu wroga
       
        private DateTime timeDirection; // Czas od ostatniej zmiany kierunku poruszania sie
        
        private float timeDirectionRandom; // Prywatna zmienna przechowująca czas między losowaniem nowego kierunku ruchu wroga
        public TypeEnemy TypeEnemyE { get; set; } // Typ wroga
        public List<Point> PositionsEnemy { get; set; } // Lista przechowujaca pozycje czesci wroga
        

        public Enemy(Panel panel, PictureBox enemyPictureBox)  // Konstruktor klasy Enemy
        {
            GamePanel = panel; // Przypisanie panelu gry i obiektu PictureBox do odpowiednich właściwości obiektu Enemy
            EnemyPictureBox = enemyPictureBox;
            Position = new Point(EnemyPictureBox.Location.X, EnemyPictureBox.Location.Y);  // Inicjalizacja pozycji wroga na podstawie aktualnej lokalizacji obiektu PictureBox
            IsAlive = true; // Ustawienie, że wróg jest żywy, a także inicjalizacja czasów i innych atrybutów
            timeDirection = DateTime.Now;
            timeDirectionRandom = 1500; // Ustawienie początkowej wartości interwału czasowego
            timeMovement = DateTime.Now; // Ustawienie zmiennej timeMovement na aktualny czas, co oznacza początek nowego interwału czasowego dla ruchu wroga
            Bullets = new List<Bullet>(); // Lista pocisków
            Health = 100; 

        }
        public void Move()  // Metoda odpowiedzialna za ruch wroga
        {
            if (!IsAlive)  // Jesli wrog nie jest zywy, wykonaj procedure smierci i zakoncz funkcje
            {
                GamePanel.Controls.Remove(EnemyPictureBox); // Usuń zdjęcie enemy
                EnemyPictureBox.Dispose(); // Zwolnienie zasobów używanych przez obiekt przed jego usunięciem
                return;
            }

            int time = 10; // Określenie co ile wrogowi jest dozwolone wykonać ruch

            if (DateTime.Now > timeMovement.AddMilliseconds(time))
            {
                randomDirection(); // Losowanie nowego kierunku ruchu wroga
                Point positionAux = Position; // Utworzenie kopii aktualnej pozycji wroga
                Movement(ref positionAux); // Wywołanie metody do przemieszczenia wroga



                if (GamePanel.ClientRectangle.Contains(positionAux)) // Upewnij się, że nowa pozycja mieści się w obszarze panelu
                {
                    Position = positionAux; // Aktualizacja pozycji obiektu Enemy
                    EnemyPictureBox.Location = Position; // Aktualizacja lokalizacji obiektu PictureBox reprezentującego wroga
                }

                bool allBulletsGone = true; // Flaga sprawdzająca, czy wszystkie pociski wroga opuściły obszar gry
                foreach (var bullet in Bullets)
                {
                    if (bullet.Position.Y < GamePanel.ClientRectangle.Bottom) // Sprawdzenie, czy pozycja Y pocisku wroga jest poniżej dolnej krawędzi obszaru gry
                    {
                        allBulletsGone = false; // Ustawienie flagi na false, gdy chociaż jeden pocisk nadal jest widoczny w obszarze gry
                        break;
                    }
                }

                if (allBulletsGone)
                {
                    Shoot(); // Jeśli wszystkie pociski wroga opuściły obszar gry, wrogowi zostaje pozwolone na strzał
                }
            }

        }


        private void randomDirection() // Losowy kierunek poruszania
        {
            // Warunek sprawdza, czy wystarczył już czas na losową zmianę kierunku ruchu w poziomie
            if (DateTime.Now > timeDirection.AddMilliseconds(timeDirectionRandom)
                && (direction == Direction.Right || direction == Direction.Left))
            {

                Random random = new Random(); // Generowanie losowych liczb
                int randomNumer = random.Next(1, 5); // Losowanie numeru od 1 do 4 

                switch (randomNumer) // Losowanie liczby od 1 do 4, aby określić nowy kierunek
                {
                    case 1: direction = Direction.Right; break;

                    case 2: direction = Direction.Left; break;

                    case 3: direction = Direction.Up; break;

                    case 4: direction = Direction.Down; break;
                }
                // Aktualizacja czasu i czasu losowania dla kolejnej zmiany kierunku
                timeDirection = DateTime.Now;
                timeDirectionRandom = random.Next(1000, 2000); // Przypisanie zmiennej timeDirectionRandom losowej liczby całkowitej z zakresu od 1000 do 1999

            }
            if (DateTime.Now > timeDirection.AddMilliseconds(150) // Warunek sprawdza, czy wystarczył już czas na losową zmianę kierunku w pionie
                && (direction == Direction.Up || direction == Direction.Down))
            {

                Random random = new Random(); // Generowanie losowych liczb
                int randomNumer = random.Next(1, 3); // Losowanie numeru od 1 do 2

                switch (randomNumer)
                {
                    case 1: direction = Direction.Right; break;

                    case 2: direction = Direction.Left; break; // Przypisanie losowego kierunku w pionie
                }

                timeDirection = DateTime.Now; // Ustawienie zmiennej na aktualny czas, co oznacza początek nowego interwału czasowego dla losowania kierunku ruchu wroga

            }
        }

        public void Movement(ref Point positionAux)  // Metoda przemieszczająca wroga w zależności od aktualnego kierunku
        {
            // Obliczenie nowej pozycji wroga w zaleznoci od aktualnego kierunku
            switch (direction)
            {
                case Direction.Right:
                    positionAux.X += 1; // Przesuń pozycję wroga o 1 w prawo
                    break;
                case Direction.Left:
                    positionAux.X -= 1;
                    break;
                case Direction.Up:
                    positionAux.Y -= 1; // Przesuń pozycję wroga o 1 w górę
                    break;
                case Direction.Down:
                    positionAux.Y += 1;
                    break;
            }
        }

        public void Shoot() // Metoda odpowiedzialna za wystrzeliwanie pocisków przez wroga
        {

            Bullet bullet = new Bullet( // Utworzenie nowego obiektu Bullet (pocisku) 
                new Point(Position.X + 16, Position.Y + 12), // Początkowa pozycja pocisku ustawiona zależnie od pozycji wroga
                BulletType1.Enemy, // Określenie, że to pocisk wroga
                GamePanel); // Przekazanie panelu gry, do którego zostanie dodany pocisk
            Bullets.Add(bullet);  // Dodanie pocisku do listy Bullets

        }
        private Bullet bulletOnPanel; // Zmienna przechowująca aktualny pocisk wroga, który jest na planszy
        public bool MoveBullets(Point Ship1Position)  // Metoda odpowiedzialna za poruszanie się pocisków wroga i sprawdzanie kolizji z statkiem gracza
        {
            
            bool inTarget = false; // Flaga informująca, czy pocisk trafił w statek gracza
            if (IsAlive) // Sprawdzenie, czy wróg jest nadal żywy
            {
                if (Bullets != null) // Sprawdzenie, czy istnieją jakiekolwiek pociski wroga
                {
                    try
                    {
                        bulletOnPanel = Bullets.Last(); // Pobranie ostatniego pocisku z listy

                        bulletOnPanel.Move(); // Poruszanie się ostatniego pocisku wroga

                        var shipWidth = 70; // Szerokość statku
                        var shipHeight = 71; // Wysokość statku

                        var shipLeft = Ship1Position.X - 15; // Lewa krawędź statku gracza
                        var shipRight = Ship1Position.X + shipWidth; // Prawa krawędź statku gracza
                        var shipTop = Ship1Position.Y; // Górna krawędź statku gracza
                        var shipBottom = Ship1Position.Y + shipHeight; // Dolna krawędź statku gracza

                        // Sprawdzenie kolizji pocisku z statkiem gracza
                        if (bulletOnPanel.Position.X >= shipLeft && bulletOnPanel.Position.X + 10 <= shipRight &&
                            bulletOnPanel.Position.Y >= shipTop && bulletOnPanel.Position.Y <= shipBottom)
                        {
                            inTarget = true; // Ustawienie flagi na true, gdy pocisk trafił w statek gracza
                            Bullets = new List<Bullet>();  // Wyczyszczenie listy pocisków wroga
                            bulletOnPanel.Clear(); // Usunięcie pocisku z planszy
                        }
                    }
                    catch (Exception ex)
                    {
                       // GamePanel.Controls.Clear();
                    }
                    
                }
            }
            return inTarget; // Zwrócenie informacji, czy pocisk trafił w statek gracza
        }
        public void Death() // Metoda obsługująca śmierć wroga
        {
            IsAlive= false; // Ustawienie flagi, oznaczającej, że wróg nie jest już żywy
            EnemyPictureBox.Visible = false; // Ukrycie obrazu reprezentującego wroga
            //GamePanel.Controls.Remove(EnemyPictureBox);
            bulletOnPanel.Clear(); // Usunięcie ostatniego pocisku wroga z planszy
        }
    }
}