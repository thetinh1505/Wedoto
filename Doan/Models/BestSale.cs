namespace Doan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BestSale")]
    public partial class BestSale
    {
        [Key]
        public long ? IDPro { get; set; }

        [StringLength(250)]
        public string NamePro { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        [Required]
        [StringLength(128)]
        public string Discriminator { get; set; }
    }
}
