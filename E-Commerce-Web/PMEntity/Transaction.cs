using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMEntity
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public string BookId { get; set; }

        public string BookQuantity { get; set; }

        public string ClothId { get; set; }

        public string ClothQuantity { get; set; }

        public double TotalPrice { get; set; }

        public int UserId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateTime { get; set; }
    }
}
