using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class ConversionRate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string From { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        
        public string To { get; set; }
        
        public string Rate { get; set; }
    }
}
