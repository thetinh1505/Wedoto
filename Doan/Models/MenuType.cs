namespace Doan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuType")]
    public partial class MenuType
    {
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
    }
}
