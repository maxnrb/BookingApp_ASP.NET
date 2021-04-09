using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public string SenderId { get; set; }
        public User Sender { get; set; }

        public string ReceiverId { get; set; }
        public User Receiver { get; set; }

        public double Amount { get; set; }

        [Display(Name = "Date de transaction")]
        [DataType(DataType.DateTime)]
        public DateTime TransactionDateTime { get; set; }

        public Transaction()
        {
            this.TransactionDateTime = DateTime.Now;
        }
    }
}
