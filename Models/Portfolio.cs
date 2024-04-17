using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public string AppUserID { get; set; } = string.Empty;

        public string StockId { get; set; } = string.Empty;

        public AppUser AppUser { get; set; } = new AppUser();

        public Stock Stock { get; set; } = new Stock();
    }
}
