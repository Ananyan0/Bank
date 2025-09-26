using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.HelperExtensions
{
    public static class AccountExtension
    {
        public static string GenerateAccountNumber()
        {
            return $"{DateTime.UtcNow.Year}{new Random().Next(100000, 999999)}";
        }
    }
}
