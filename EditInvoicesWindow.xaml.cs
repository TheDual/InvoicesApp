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
    /// Interaction logic for EditInvoicesWindow.xaml
    /// </summary>
    public partial class EditInvoicesWindow : Window
    {
        public List<InvoiceItem> invoices;
        public EditInvoicesWindow(List<InvoiceItem> inv)
        {
            InitializeComponent();
            invoices = inv;

            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Id", Binding = new Binding("Id") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Id faktury", Binding = new Binding("InvoiceId") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Nazwa", Binding = new Binding("Name") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Ilość", Binding = new Binding("Ammount") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Cena", Binding = new Binding("Price") });

            dataGrid.AutoGenerateColumns = false;
            dataGrid.ItemsSource = invoices;
        }

        private void EditInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is InvoiceItem s)
            {
                var dialog = new EditInvoice(s);
                if (dialog.ShowDialog() == true)
                {
                    invoices[invoices.FindIndex(x => x.Id == s.Id)] = dialog.item;
                    dataGrid.Items.Refresh();
                }
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
