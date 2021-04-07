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
    /// Interaction logic for EditInvoice.xaml
    /// </summary>
    public partial class EditInvoice : Window
    {
        public InvoiceItem item;
        public EditInvoice(InvoiceItem it)
        {
            InitializeComponent();
            item = it;

            _id.Text = item.Id.ToString();
            _invoiceId.Text = item.InvoiceId.ToString();
            _name.Text = item.Name.ToString();
            _ammount.Text = item.Ammount.ToString();
            _price.Text = item.Price.ToString();

        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            item.Id = Convert.ToInt32(_id.Text);
            item.InvoiceId = Convert.ToInt32(_invoiceId.Text);
            item.Name = _name.Text;
            item.Ammount = float.Parse(_ammount.Text);
            item.Price = float.Parse(_price.Text);

            this.DialogResult = true;
        }
    }
}
