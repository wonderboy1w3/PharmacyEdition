using PharmacyEdition.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyEdition.Domain.Entities
{
    public class CreditCard
    {
        public int CreditCardId { get; set; }
        public string CardNumber { get; set; }
        private DateTime _defaultDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        public DateTime Expiration
        {
            get { return _defaultDate; }
            set { _defaultDate = value; }
        }

        public string CardHolderName { get; set; }

        public PaymentType PaymentTip { get; set; } 

    }
}
