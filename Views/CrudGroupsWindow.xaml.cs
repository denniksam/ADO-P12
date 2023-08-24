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

namespace ADO_P12.Views
{
    /// <summary>
    /// Interaction logic for CrudGroupsWindow.xaml
    /// </summary>
    public partial class CrudGroupsWindow : Window
    {
        public DAL.Entity.ProductGroup? ProductGroup { get; set; }   // посилання на редаговану групу

        public CrudGroupsWindow(DAL.Entity.ProductGroup productGroup)
        {
            InitializeComponent();
            this.ProductGroup = productGroup;
            this.DataContext = this.ProductGroup;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: перевірити поля на допустимість значень (на пустоту)
            DialogResult = true;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ProductGroup = null;
            Close();
        }
    }
}
