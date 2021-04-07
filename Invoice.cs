using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;



namespace Lab11
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public string Address { get; set; }
        public float Value { get; set; }

        public ICollection<InvoiceItem> Items { get; set; }

    }
}
