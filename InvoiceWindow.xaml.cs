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

namespace Lab11
{
    /// <summary>
    /// Interaction logic for InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : Window
    {
        public Invoice inv;
        public InvoiceWindow()
        {
            InitializeComponent();
            inv = new Invoice();

        }

        public InvoiceWindow(Invoice i)
        {
            InitializeComponent();
            inv = i;

            _id.IsEnabled = false;
            EditInvoices.IsEnabled = true;

            _address.Text = inv.Address;
            _customer.Text = inv.Customer;
            _date.Text = inv.Date.ToString();
            _id.Text = inv.Id.ToString();
            _value.Text = inv.Value.ToString();
            
        }

        private void GenerateInvoices_Unchecked(object sender, RoutedEventArgs e)
        {
            _numberOfInvoices.IsEnabled = false;
        }

        private void GenerateInvoices_Checked(object sender, RoutedEventArgs e)
        {
            _numberOfInvoices.IsEnabled = true;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            inv.Address = string.IsNullOrEmpty(_address.Text) ? "Brak" : _address.Text;
            inv.Customer = string.IsNullOrEmpty(_customer.Text) ? "Brak" : _customer.Text; ;
            inv.Date = Convert.ToDateTime(_date.Text);
            inv.Id = Convert.ToInt32(_id.Text);
            inv.Value = float.Parse(_value.Text);

            if (GenerateInvoices.IsChecked == true)
                inv.Items = AutoGenerateInvoices(int.Parse(_numberOfInvoices.Text));
            else if (inv.Items != null) { }
            else
                inv.Items = null;


                this.DialogResult = true;
        }

        private ICollection<InvoiceItem> AutoGenerateInvoices(int n)
        {
            ICollection<InvoiceItem> list = new List<InvoiceItem>();
            Random rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            for (int i = 0; i < n; i++)          
                list.Add(new InvoiceItem() { Id = i, InvoiceId = inv.Id, Ammount = rnd.Next(1, 150), Name = new string(Enumerable.Repeat(chars, rnd.Next(3, 15)).Select(s => s[rnd.Next(s.Length)]).ToArray()), Price = (float)(rnd.Next(100, 10000) / rnd.NextDouble()), Invoice = inv });
            return list;
            
        }

        private void EditInvoices_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new EditInvoicesWindow(inv.Items.ToList());
            if(dialog.ShowDialog() == true)        
                inv.Items = dialog.invoices;
            
        }
    }
}
