using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjetWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClassOperation f = new ClassOperation();
        bool result = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;

            while(i < f.GetAdmins().Count && result==false)
            {
                if (f.GetAdmins().ElementAt(i).login.Equals(texbox1.Text))
                {
                    if (f.GetAdmins().ElementAt(i).mot_de_passe.Equals(textbox2.Password))
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                        erreur.Content = "Mot de passe incorrect !";
                    }
                }
                else
                {
                    result = false;
                    erreur.Content = "Login incorrect !";
                }
                i++;
            }

            if (result == false)
            {
                
            }
            else
            {
                this.Hide();
                Window1 w = new Window1();
                w.Show();
            }
        }
    }
}
