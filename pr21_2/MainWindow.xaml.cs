using pr21_2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace pr_21_2
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Product> products = new ObservableCollection<Product>();

        public MainWindow()
        {
            InitializeComponent();

            products.Add(new Product { Name = "Видеокарта AMD RX 6600xt", Price = 50000, Count = 5 });
            products.Add(new Product { Name = "Процессор AMD Ryzen 5 5600", Price = 25000, Count = 10 });
            products.Add(new Product { Name = "SSD Kingston 1024GB NVMe", Price = 11000, Count = 4 });
            products.Add(new Product { Name = "Мышь ATK A9 Air", Price = 7000, Count = 32 });

            tableProduct.ItemsSource = products;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text) || string.IsNullOrWhiteSpace(txtCount.Text))
            {
                MessageBox.Show("Заполните все поля!!!!");
                return;
            }

            int price = 0;
            int count = 0;

            try
            {
                price = Convert.ToInt32(txtPrice.Text);
                count = Convert.ToInt32(txtCount.Text);
            }
            catch
            {
                MessageBox.Show("Цена и количество должны быть числами!!!!");
                return;
            }

            if (price < 0 || count < 0)
            {
                MessageBox.Show("Цена и количество не могут быть отрицательными!!!!");
                return;
            }

            products.Add(new Product { Name = txtName.Text, Price = price, Count = count });

            txtName.Text = "";
            txtPrice.Text = "";
            txtCount.Text = "";
        }

        private void tableProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tableProduct.SelectedItem is Product p)
            {
                txtName.Text = p.Name;
                txtPrice.Text = p.Price.ToString();
                txtCount.Text = p.Count.ToString();
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (tableProduct.SelectedItem is Product p)
            {
                MessageBoxResult q = MessageBox.Show("Вы точно хотите удалить этот товар?",
                    "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (q == MessageBoxResult.Yes)
                {
                    products.Remove(p);
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для удаления!");
            }
        }

        private void Buy1_Click(object sender, RoutedEventArgs e)
        {
            // Получаем товар из DataContext нажатой кнопки
            if ((sender as Button).DataContext is Product p)
            {
                int buyCount = 0;
                try
                {
                    buyCount = Convert.ToInt32(txtCount.Text);
                }
                catch
                {
                    MessageBox.Show("Введите корректное количество для покупки в поле 'Кол-во'!");
                    return;
                }

                if (buyCount <= 0)
                {
                    MessageBox.Show("Количество должно быть больше нуля!");
                    return;
                }

                if (p.Count >= buyCount)
                {
                    p.Count -= buyCount; 
                    MessageBox.Show($"Куплен {p.Name} по цене {p.Price} в кол-ве {buyCount} шт");

                    tableProduct.ItemsSource = null;
                    tableProduct.ItemsSource = products;
                }
                else
                {
                    MessageBox.Show("Недостаточно товара на складе");
                }
            }
        }
    }
}