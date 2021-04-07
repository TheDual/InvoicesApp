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

namespace Lab11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            using (var db = new db.InvoiceDbContext())
            {
                invoicesDataGrid.ItemsSource = db.Invoices.ToList();
                foreach (var col in invoicesDataGrid.Columns)
                    col.CanUserSort = true;
            }
            
        }

        private void AddInvoice_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new InvoiceWindow();
            if(dialog.ShowDialog() == true)
            {
                using (var db = new db.InvoiceDbContext())
                {
                    db.Invoices.Add(dialog.inv);
                    db.SaveChanges();
                    var dd = db.Invoices.ToList();
                    invoicesDataGrid.ItemsSource = db.Invoices.ToList();
                }
            }
        }

        private void InvoicesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (invoicesDataGrid.SelectedItem is Invoice s)
            {
                using (var db = new db.InvoiceDbContext())
                {
                    var list = db.Invoices.Include("Items").Single(x => x.Id == s.Id);
                    if (list.Items != null)
                        invoicesItemsDataGrid.ItemsSource = list.Items;
                    foreach (var col in invoicesItemsDataGrid.Columns)
                        col.CanUserSort = true;
                }
            } 
            

        }

        private void RemoveInvoice_Click(object sender, RoutedEventArgs e)
        {
            if(invoicesDataGrid.SelectedItem is Invoice s)
            {
                using (var db = new db.InvoiceDbContext())
                {
                    db.Invoices.Remove(db.Invoices.Single(x => x.Id == s.Id));
                    db.SaveChanges();
                    invoicesDataGrid.ItemsSource = db.Invoices.ToList();
                }
            }
            invoicesDataGrid.Items.Refresh();
        }

        private void EditInvoice_Click(object sender, RoutedEventArgs e)
        {
            if(invoicesDataGrid.SelectedItem is Invoice s)
            {
                using (var db = new db.InvoiceDbContext())
                {
                    var inv = db.Invoices.Include("Items").Single(x => x.Id == s.Id);

                    var dialog = new InvoiceWindow(inv);
                    if(dialog.ShowDialog() == true)
                    {
                        //db.Invoices.Include("items").ToList()[db.Invoices.Include("items").ToList().FindIndex(x => x.Id == s.Id)] = dialog.inv;
                        db.SaveChanges();
                        invoicesDataGrid.ItemsSource = db.Invoices.ToList();
                       
                    }
                }
            }
        }
    }
}
