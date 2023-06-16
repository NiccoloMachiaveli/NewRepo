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
    /// Логика взаимодействия для Check.xaml
    /// </summary>
    public partial class Check : Window
    {
        BookClubBaseEntities entities = new BookClubBaseEntities();
        List<Product> OrderProductL = new List<Product>();
        public Check(List<Product> orderProductL, Orders order)
        {
            InitializeComponent();
            OrderProductL = orderProductL;

            txtCheckInfo.Text = $"Заказ №{order.idOrder} от {order.dateOrder.ToShortDateString()}";

            foreach (var product in orderProductL)
            {
                txtCheckInfo.Text += $"\n{product.nameProduct} ------------- {product.totalPrice}";
            }
            txtCheckInfo.Text += $"\n\nОбщая стоимость: {OrderProductL.Sum(product => product.totalPrice)} руб.";
            txtCheckInfo.Text += $"\nРазмер скидки: {OrderProductL.Sum(product => product.priceProduct) - OrderProductL.Sum(product => product.totalPrice)} руб.";
            txtCheckInfo.Text += $"\nПункт выдачи: {order.PickPoint.addresPickPoint}";
            txtCheckInfo.Text += $"\nКод получения: {order.codeOrder}";

            if (OrderProductL.Min(product => product.countProduct) > 3)
            {
                txtCheckInfo.Text += $"\nСрок доставки - 3 дня";
            }
            else
            {
                txtCheckInfo.Text += $"\nСрок доставки - 6 дней";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                IDocumentPaginatorSource ipd = CheckOrder;
                printDialog.PrintDocument(ipd.DocumentPaginator, "Flow Document");
            }
        }
    }
}
