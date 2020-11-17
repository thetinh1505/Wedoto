namespace Doan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public long IDOrder { get; set; }

        public DateTime? OrderDate { get; set; }

        [StringLength(500)]
        public string Descriptions { get; set; }

        public long? CodeCus { get; set; }

        [StringLength(150)]
        public string Email_Cus { get; set; }

        [StringLength(50)]
        public string Password_cus { get; set; }

        [StringLength(25)]
        public string SDT_Cus { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
