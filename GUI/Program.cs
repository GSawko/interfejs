using System;
using System.Windows.Forms;

namespace GUI
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 widokLogowania = new Form1();
            if (widokLogowania.ShowDialog() == DialogResult.OK)
                DajWidokNaPodstLoginu(widokLogowania.Login);
        }

        private static void DajWidokNaPodstLoginu(string login)
        {
            var prefiks = login.Substring(0, 1);
            if (prefiks == "p")
            {
                Form3 widokPracownika = new Form3();
                Application.Run(widokPracownika);

            }
            else if (prefiks == "k")
            {
                Form2 widokKlienta = new Form2();
                Application.Run(widokKlienta);
            }
            else if (prefiks == "w")
            {
                //toDo
            }

        }
    }
}
