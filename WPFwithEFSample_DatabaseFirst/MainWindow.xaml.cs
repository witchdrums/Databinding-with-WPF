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
using System.Data.Entity;
using System.Linq;
using System.Windows;
using WPFwithEFSample_DatabaseFirst;

namespace WPFwithEFSample_DatabaseFirst
{
    public partial class MainWindow : Window
    {
        private ProductContext _context = new ProductContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource categoryViewSource =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("categoriesViewSource")));

            _context.Categories.Load();

            categoryViewSource.Source = _context.Categories.Local;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var product in _context.Products.Local.ToList())
            {
                if (product.Categories == null)
                {
                    _context.Products.Remove(product);
                }
            }
            _context.SaveChanges();
            this.categoriesDataGrid.Items.Refresh();
            this.productsDataGrid.Items.Refresh();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            this._context.Dispose();
        }

    }

}