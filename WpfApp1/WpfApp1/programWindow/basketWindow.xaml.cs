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

namespace WpfApp1.programWindow
{
    /// <summary>
    /// Логика взаимодействия для basketWindow.xaml
    /// </summary>
    public partial class basketWindow : Window
    {
        BookClubBaseEntities entities = new BookClubBaseEntities();
        List<Product> OrderProductL = new List<Product>();
        public basketWindow(List<Product> orderproduct)
        {
            InitializeComponent();
            OrderProductL = orderproduct;
            foreach (var adress in entities.PickPoint)
            {
                cbPickPoint.Items.Add(adress);
            }
            dataGridProduct.ItemsSource = OrderProductL;
            lblTotalPrice.Content = $"Общая стоимтость: {OrderProductL.Sum(product => product.totalPrice)} руб.";
            lblDiscountPrice.Content = $"Размер скидки: {OrderProductL.Sum(product => product.priceProduct) - OrderProductL.Sum(product => product.totalPrice)} руб.";
        }

        private void btnDelet_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = dataGridProduct.SelectedItem as Product;
            var result = MessageBox.Show("Подтвердите удаление товара из корзины", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                OrderProductL.Remove(selectedProduct);
                dataGridProduct.ItemsSource = null;
                dataGridProduct.ItemsSource = OrderProductL;
                lblTotalPrice.Content = $"Общая стоимтость: {OrderProductL.Sum(product => product.totalPrice)} руб.";
                lblDiscountPrice.Content = $"Размер скидки: {OrderProductL.Sum(product => product.priceProduct) - OrderProductL.Sum(product => product.totalPrice)} руб.";
            }
        }

        private void btnOrderAccess_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            var selectedAddress = cbPickPoint.SelectedItem as PickPoint;
            if(selectedAddress != null)
            {
                Orders newOrder = new Orders();
                entities.Orders.Add(newOrder);
                newOrder.dateOrder = DateTime.Now;
                newOrder.statusOrder = "Новый";
                newOrder.codeOrder = random.Next(100, 999);
                newOrder.idPickPoint = (cbPickPoint.SelectedItem as PickPoint).idPickPoint;
                entities.SaveChanges();

                foreach (var product in OrderProductL)
                {
                    ProductOrder productOrder = new ProductOrder();
                    entities.ProductOrder.Add(productOrder);
                    productOrder.idOrder = newOrder.idOrder;
                    productOrder.idProduct = product.idProduct;
                    entities.SaveChanges();
                }
                MessageBox.Show("Заказ успешно сформирован", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                var window = new programWindow.Check(OrderProductL, newOrder);
                window.ShowDialog();
            }
        }
    }
}
