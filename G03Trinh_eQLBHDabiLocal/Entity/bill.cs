namespace G03Trinh_eQLBHDabiLocal.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("bill")]
    public partial class bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public bill()
        {
            bill_info = new HashSet<bill_info>();
        }

        public int id { get; set; }
        public int customer { get; set; }

        [Column(TypeName = "date")]
        public DateTime? booking_date { get; set; }

        public int? status { get; set; }
        [StringLength(500)]
        public string name { get; set; }
        [StringLength(10)]
        public string phone { get; set; }
        public string address { get; set; }
        public int? payment { get; set; }

        public virtual customer customer1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bill_info> bill_info { get; set; }
    }
}
