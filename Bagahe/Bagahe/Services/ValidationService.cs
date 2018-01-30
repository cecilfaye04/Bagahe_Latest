using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bagahe.Interfaces;
using System.Text.RegularExpressions;

namespace Bagahe.Services
{
    public class ValidationService : IValidation
    {
        private bool IsNumeric(string value)
        {
            long num;
            return Int64.TryParse(value, out num);
        }
        
        private bool IsLengthValid(string value, int length)
        {
            return value.Length == length;
        }

        public bool IsPNRValid(string PNR)
        {
            return !string.IsNullOrEmpty(PNR) && IsLengthValid(PNR, 6) && Regex.IsMatch(PNR, @"^[A-Z\d]{6}$");           
        }
        
        public bool IsBagtagValid(string bagtag)
        {
            return !string.IsNullOrEmpty(bagtag) && IsLengthValid(bagtag, 10) && IsNumeric(bagtag);
        }
    }
}
