using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SpaceGame
{
    public enum BulletType1 // Rodzaje pocisków
    {
        Normal, Special, Enemy
    }
    internal class Bullet // Klasa reprezentująca pocisk w grze kosmicznej
    {
       // Właściwości do przechowywania pozycji, typu i etykiety pocisku
        public Point Position { get; set; }
        public BulletType1 BulletType { get; set; }
        public Label bulletLabel { get; set; } //  Wizualna reprezentacja pocisku na interfejsie 
        Panel Panel { get;set; } // Właściwość przechowująca referencję do panelu, na którym umieszczony zostanie pocisk

        public Bullet(Point position, BulletType1 type, Panel panel)  // Konstruktor do inicjalizacji pocisku pozycją, typem i panelem
        {
            this.Position = position;
            this.BulletType = type;
            this.bulletLabel = new Label(); // Inicjalizacja nowej etykiety dla pocisku
            ConfigureLabel(); // Konfiguracja wyglądu etykiety w zależności od typu pocisku
            this.Panel = panel;
            Panel.Controls.Add(bulletLabel);
        }

        public void Move()  // Metoda do poruszania pocisku w zależności od jego typu
        {
            int speed = 5; // Prędkość poruszania pocisku

            if (BulletType == BulletType1.Normal || BulletType == BulletType1.Special) 
            {
                Position = new Point(Position.X, Position.Y - speed); // Poruszanie w górę dla normalnych i specjalnych pocisków (-speed bo pocisk przesuwa się w kierunku przeciwnym do osi Y)
                bulletLabel.Location = new Point(Position.X, Position.Y);
            }
            else
            {
                Position = new Point(Position.X, Position.Y + speed);  // // Poruszanie w dół dla pocisków przeciwnika
                bulletLabel.Location = new Point(Position.X, Position.Y);
            }


        }

        public void Clear() // Metoda do usunięcia pocisku z panelu
        {
            Panel.Controls.Remove(bulletLabel);
        }

        private void ConfigureLabel()  // Metoda do konfiguracji etykiety pocisku na podstawie jego typu
        {
            bulletLabel.AutoSize = true;

            bulletLabel.ForeColor = System.Drawing.Color.Black; // Ustawienie koloru etykiety

            int x = Position.X; //  Przechowuje aktualną współrzędną X pocisku przed zmianą pozycji
            int y = Position.Y;

            switch (BulletType) // Ustawienie tekstu etykiety i czcionki na podstawie typu pocisku
            {
                case BulletType1.Normal:
                    bulletLabel.Text = "╬";
                    bulletLabel.Font = new Font("Arial", 10);
                    break;

                case BulletType1.Special:
                    bulletLabel.Text = "_\r\n( )\r\nW";
                    break;

                case BulletType1.Enemy:
                    bulletLabel.Text = "█";
                    break;
            }
            bulletLabel.Location = new Point(x, y); // Nowa lokalizacja (współrzędne) etykiety bulletLabel na ekranie 
        }
    }
}
