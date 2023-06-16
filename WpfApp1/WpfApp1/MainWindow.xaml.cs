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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BookClubBaseEntities entities = new BookClubBaseEntities();
        List<Product> OrderProductL = new List<Product>();
        public MainWindow()
        {
            InitializeComponent();
            dataGridProduct.ItemsSource = entities.Product.ToList();
            btnOpen.Visibility = Visibility.Hidden;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dataGridProduct.SelectedItem as Product;
            if (selectedItem != null)
            {
                OrderProductL.Add(selectedItem);
                btnOpen.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Выберите товар", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
