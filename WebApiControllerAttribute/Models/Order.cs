using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiControllerAttribute.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        [Required]
        public IEnumerable<string> ItemIds { get; set; }

        [Required]
        [StringLength(3)]
        [Currrency]
        public string Currency { get; set; }
        public void CopyOrderDetails(Order other)
        {
            ItemIds = other.ItemIds;
            Currency = other.Currency;
        }
    }
}
