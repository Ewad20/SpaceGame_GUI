using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceGame
{
    public partial class Form1 : Form
    {
        private Ship playerShip; // Obiekt reprezentujący statek gracza
        private Enemy enemy1;
        private Enemy enemy2;

        private Timer bulletsTimer; // Timer do obsługi pocisków
        bool active = true; // Flaga określająca, czy gra jest aktywna

        public Form1()
        {
            InitializeComponent(); // Inicjalizacja komponentów formularza (automatyczne)
            InitializeGameObjects(); // Inicjalizacja obiektów gry

            this.KeyPreview = true; // Ustawienie właściwości umożliwiającej obsługę zdarzeń klawiatury przez formularz
            this.KeyDown += new KeyEventHandler(Form1_KeyDown); // Dodanie obsługi zdarzenia naciśnięcia klawisza

            bulletsTimer = new Timer(); // Utworzenie obiektu Timer do obsługi czasu trwania gry
            bulletsTimer.Interval = 35; // Ustawienie interwału timera na 35 milisekund
            bulletsTimer.Tick += BulletsTimer_Tick;  // Dodanie obsługi zdarzenia tick (co każdy interwał) do timera
            bulletsTimer.Start(); // Rozpoczęcie działania timera
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            MoveShip(e.KeyCode); // Wywołanie metody obsługującej poruszanie statkiem na podstawie naciśniętego klawisza
            ShootShip(e.KeyCode); // Wywołanie metody obsługującej strzały statku na podstawie naciśniętego klawisza
    }

        private void InitializeGameObjectsLvl2()
        {
            pictureEnemy2.Visible = false; // Ukryj drugiego wroga na początku poziomu 
            pictureEnemy.Visible = true; // Pokaż pierwszego wroga na początku poziomu 
            playerShip = new Ship(new Point(315, 233), WindowPanel); // Utwórz statek gracza
            enemy1 = new Enemy(WindowPanel, pictureEnemy); // Utwórz pierwszego wroga
            enemy2 = new Enemy(WindowPanel, pictureEnemy2); // Utwórz drugiego wroga

            // Przypisanie tagów do kontrolki PictureBox dla identyfikacji obiektów w formularzu
            PictureShip.Tag = playerShip; // Przypisanie tagu dla statku gracza
            pictureEnemy.Tag = enemy1;  // Przypisanie tagu dla pierwszego wroga
            pictureEnemy2.Tag = enemy2;
        }

        private void InitializeGameObjects()
        {
            pictureEnemy2.Visible = false; // Ukryj drugiego wroga na początku gry
            pictureEnemy.Visible = true; // Pokaż pierwszego wroga na początku gry
            playerShip = new Ship(new Point(315, 233), WindowPanel);
            enemy1 = new Enemy(WindowPanel, pictureEnemy);
            enemy2 = new Enemy(WindowPanel, pictureEnemy2);
            PictureShip.Tag = playerShip;
            pictureEnemy.Tag = enemy1;
            pictureEnemy2.Tag = enemy2;

            label3.Visible = false; // Ukryj etykietę dla poziomu 2 na początku gry
            progressBar3.Visible = false; // Ukryj pasek postępu dla poziomu 2 na początku gry
        }

        bool LevelFinish = false; // Flaga oznaczająca zakończenie poziomu

        private bool CheckGameEnd()
        {
            if (playerShip.Health <= 0) // Sprawdź, czy zdrowie statku gracza jest mniejsze lub równe zero
                {
                active = false; // Ustaw flagę na false, oznaczając, że gra jest nieaktywna
                //playerShip.Dead();
                return false; // Zwróć false, ponieważ gra się zakończyła
            }

            return true; // Zwróć true, gdy gra nadal trwa
        }
  
        private void BulletsTimer_Tick(object sender, EventArgs e)
        {
            if (active)    // Sprawdź, czy gra jest aktywna
            {
                if (LevelFinish)    // Sprawdź, czy aktualny poziom gry został zakończony
                {
                    // Przywróć widoczność obiektów dla nowego poziomu
                    pictureEnemy.Visible = true;
                    pictureEnemy2.Visible = true;
                    label3.Visible = true;
                    progressBar3.Visible = true;
                    LevelFinish = Level2();  // Przejdź do drugiego poziomu
                }
                else
                {
                    LevelFinish = Level1();  // Kontynuuj aktualny poziom
                }

                if (!CheckGameEnd())  // Sprawdź, czy gra się zakończyła
                {
                    EndGame();  // Zakończ grę
                }
            }
        }

        private bool Level1()
        {
            enemy1.Move();  // Porusz wroga
            var inTargetEnemy = playerShip.MoveBullets(enemy1.Position);  // Sprawdź, czy pociski gracza trafiły w wroga
            var inTargetShip = enemy1.MoveBullets(PictureShip.Location); // Sprawdź, czy pociski wroga trafiły w gracza

            if (inTargetShip)  // Jeśli pociski trafiły w gracza, zmniejsz jego zdrowie i zaktualizuj pasek postępu
            {
                playerShip.Health = playerShip.Health - 10;
                OnHit(progressBar1, 10);
            }

            if (inTargetEnemy) // Jeśli pociski gracza trafiły w wroga, zmniejsz jego zdrowie i zaktualizuj pasek postępu
            {
                enemy1.Health = enemy1.Health - 15;
                OnHit(progressBar2, 15);
            }

            if (enemy1.Health <= 0)  // Jeśli zdrowie wroga spadło do zera
            {
                enemy1.Death();  // Zakończ życie wroga
                LevelFinish = true;

                // Przywróć ustawienia wroga do początkowych wartości
                enemy1.IsAlive = true;
                enemy1.Health = 100;
                progressBar2.Value = 100;
                bulletsTimer.Enabled = false;
                
                MessageBox.Show("Level 2", "Congratulations!", MessageBoxButtons.OK);  // Wyświetl komunikat o przejściu na poziom drugi

                var customDialog = new CustomDialog("               Do you want to go to level two?"); // Zapytaj użytkownika, czy chce przejść na poziom drugi
                var result = customDialog.ShowDialog(); 

                if (result == DialogResult.OK) // Potwierdzenie
                {
                    foreach (var bulletDelete in playerShip.BulletsList) // Usuń wszystkie pociski gracza
                    {
                        WindowPanel.Controls.Remove(bulletDelete.bulletLabel); 
                    }
                    // Poczekaj chwilę przed wznowieniem timera i kontynuacją gry
                    System.Threading.Thread.Sleep(50);
                    bulletsTimer.Enabled = true;
                    return true; // Kontynuacja gry - przejście na poziom drugi
                }
                else 
                {
                    EndGame();  // Zakończ grę, jeśli użytkownik nie chce przejść na poziom drugi
                    return false;
                }
            }

            if (playerShip.Health <= 0)  // Jeśli zdrowie gracza spadło do zera, zakończ grę
            {
                active = false;
                playerShip.Dead();
                LevelFinish = false;
            }

            return LevelFinish; // Zwróć informację o stanie poziomu
        }

        private bool Level2()
        {
            enemy1.Move();
            enemy2.Move();
            var inTargetShip2 = enemy2.MoveBullets(PictureShip.Location); // Sprawdzenie, czy strzał z drugiego poziomu trafił w statek gracza
            var inTargetShip = enemy1.MoveBullets(PictureShip.Location); // Sprawdzenie, czy strzał z pierwszego poziomu trafił w statek gracza
            var inTargetEnemy = playerShip.MoveBullets(enemy1.Position);  // Sprawdzenie, czy strzał gracza trafił w pierwszego przeciwnika
            var inTargetEnemy2 = playerShip.MoveBullets(enemy2.Position); // Sprawdzenie, czy strzał gracza trafił w drugiego przeciwnika

            if (inTargetShip || inTargetShip2)  // Sprawdzenie, czy gracz został trafiony przez strzał przeciwnika 
            {
                playerShip.Health = playerShip.Health - 20;
                OnHit(progressBar1, 20);
            }

            if (inTargetEnemy) // Sprawdzenie, czy pierwszy przeciwnik został trafiony przez strzał gracza
            {
                enemy1.Health = enemy1.Health - 25;
                OnHit(progressBar2, 25);
            }

            if (inTargetEnemy2)  // Sprawdzenie, czy drugi przeciwnik został trafiony przez strzał gracza
            {
                enemy2.Health = enemy2.Health - 25;
                OnHit(progressBar3, 25);
            }

            if (enemy1.Health <= 0) // Sprawdzenie, czy pierwszy przeciwnik został pokonany
            {
                enemy1.Death();
            }

            if (enemy2.Health <= 0) // Sprawdzenie, czy drugi przeciwnik został pokonany
            {
                enemy2.Death();
            }
            if (enemy2.Health <= 0 && enemy1.Health <= 0) // Sprawdzenie, czy obaj przeciwnicy zostali pokonani (wygrana)
            {
                bulletsTimer.Enabled = false;
                MessageBox.Show("    Congratulation \n" +
                    "           You win!!");
                Form2 form2 = new Form2(); // Tworzenie nowej instancji Form2
                form2.Show();
                this.Close(); // Zamknięcie bieżącej instancji Form1
                LevelFinish = true;

            }

            if (playerShip.Health <= 0)   // Sprawdzenie, czy gracz stracił wszystkie punkty zdrowia (przegrana)
            {
                active = false;
                playerShip.Dead(); 
                LevelFinish = false;

            }


            return LevelFinish; // Zwracanie flagi informującej, czy poziom gry został ukończony
        }
        public void MoveShip(Keys key)
        {
            playerShip.Move(2, key); // Wywołanie metody ruchu statku gracza z określoną prędkością
            PictureShip.Location = playerShip.Position; 
        }

        public void ShootShip(Keys key)
        {
            if (key == Keys.Left || key == Keys.Right || key == Keys.Up)
                playerShip.Shoot(key);  // Wywołanie metody strzału statku gracza w odpowiedzi na określony klawisz
        }

        private void OnHit(ProgressBar progressBar, int minusHP)
        {
            if (progressBar.Value - minusHP >= 0)  // Redukuje wartość paska postępu o określoną ilość punktów życia (minusHP), ale nie pozwala, aby wartość spadła poniżej zera
            {
                progressBar.Value -= minusHP;
            }
            else
            {
                progressBar.Value = 0; // Jeśli wartość spadłaby poniżej zera, ustawia ją na zero
            }
        }

        private void EndGame() // Koniec gry
        {
            MessageBox.Show("End of the game");
            Form2 form2 = new Form2();
            form2.Show();
            this.Close();
        }

        private void WindowPanel_Paint(object sender, PaintEventArgs e)
        {
        }
    }

    public class CustomDialog : Form
    {
        private Button okButton;
        private Button cancelButton;

        public CustomDialog(string message)  // Konstruktor klasy CustomDialog
        {
            InitializeComponent(message); // Inicjalizacja komponentów okna dialogowego
        }

        private void InitializeComponent(string message) // Metoda odpowiedzialna za inicjalizację i ustawienia komponentów formularza
        {
            // Ustawienia formularza okna dialogowego
            this.Text = "Confirm";
            this.Size = new Size(300, 150);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Tworzenie etykiety z przekazanym komunikatem
            Label labelMessage = new Label();
            labelMessage.Text = message;
            labelMessage.Location = new Point(20, 20);
            labelMessage.Size = new Size(260, 50);

            okButton = new Button();
            okButton.Text = "OK";
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(50, 80);
            okButton.Size = new Size(80, 30);
            okButton.Click += OkButton_Click;

            cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(150, 80);
            cancelButton.Size = new Size(80, 30);
            cancelButton.Click += CancelButton_Click;

            // Dodanie komponentów do formularza
            this.Controls.Add(labelMessage);
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);
        }

        private void OkButton_Click(object sender, EventArgs e)  // Obsługa zdarzenia kliknięcia przycisku OK
        {
            this.DialogResult = DialogResult.OK;  // Ustawienie wyniku dialogu na OK i zamknięcie formularza
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)  // Obsługa zdarzenia kliknięcia przycisku Anuluj
        {
            this.DialogResult = DialogResult.Cancel;  // Ustawienie wyniku dialogu na Anuluj i zamknięcie formularza
            this.Close();
        }
    }
}
