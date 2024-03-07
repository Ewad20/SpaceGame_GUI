using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceGame
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();  // Włączenie stylów wizualnych dla aplikacji Windows Forms
            Application.SetCompatibleTextRenderingDefault(false); // Ustawienie domyślnego renderowania bez kompatybilności z tekstem
            Application.Run(new Form2());  // Uruchomienie aplikacji, rozpoczynając od obiektu Form2
        }
    }
}
