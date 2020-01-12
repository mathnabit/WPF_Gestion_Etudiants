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
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace ProjetWPF
{
    /// <summary>
    /// Logique d'interaction pour Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        ClassOperation cf = new ClassOperation();

        public Window2()
        {
            InitializeComponent();
          

            raddata1.ItemsSource = cf.GetEtudiantsRad();
        }

        private void Valide_Click(object sender, RoutedEventArgs e)
        {
           
            this.Hide();
            Window1 w = new Window1();
            w.Show();
        }


        private void deleting_item(object sender, System.ComponentModel.CancelEventArgs e)
        {
            etudiantRad e1 = raddata1.CurrentItem as etudiantRad;
            cf.deleteEtudiant(e1.cne);
        }

        private void editing_rad(object sender, System.ComponentModel.CancelEventArgs e)
        {
            etudiantRad et =raddata1.CurrentItem as etudiantRad;
            cf.modifier(et);
            /////modifier

        }

        private void adding_item(object sender, Telerik.Windows.Controls.Data.DataForm.AddingNewItemEventArgs e)
        {
            raddata1.AddNewItem();
        }
      
 
    }
}
