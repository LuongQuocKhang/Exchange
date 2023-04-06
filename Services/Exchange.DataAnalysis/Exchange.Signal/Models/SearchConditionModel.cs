using Exchange.Data.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Exchange.Signal.Models
{
    public class SearchConditionModel
    {
        [DefaultValue("BTC")]
        [Required]
        public string Symbol { get; set; }

        [DefaultValue("USDT")]
        [Required]
        public string Quote_Name { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        [DefaultValue("1h")]
        public string Interval { get; set; } = "1h";

        [DefaultValue(200)]
        public int Limits { get; set; }

        public TechnicalIndicator indicator { get; set; }
    }
}
