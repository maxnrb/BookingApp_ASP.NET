using BookingApp.Data;
using BookingApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly AppContextDB _context;

        public TransactionController(AppContextDB context)
        {
            _context = context;
        }

        // Return true 
        public async Task<bool> DoTransaction(User sender, User receiver, double amount)
        {
            if (!(sender.Balance >= amount)) { return false; }
            
            sender.Balance -= amount;
            receiver.Balance += amount;

            _context.Update(sender);
            _context.Update(receiver);
            await _context.AddAsync(new Transaction { Sender = sender, Receiver = receiver, Amount = amount });

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
