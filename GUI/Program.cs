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
            Form1 loginView = new Form1();
            Application.Run(loginView);

            if (loginView.LoginStatus == true)
                OpenUserView(loginView.UserType, loginView.UserLogin);
        }

        private static void OpenUserView(char userType, string userLogin)
        {
            switch (userType)
            {
                case 'k':
                    Form2 clientView = new Form2(userLogin);
                    Application.Run(clientView);
                    break;

                case 'p':
                    Form3 workerView = new Form3(userLogin);
                    Application.Run(workerView);
                    break;

                case 'w':
                    Form4 OwnerView = new Form4(userLogin);
                    Application.Run(OwnerView);
                    break;
            }
        }
    }
}
