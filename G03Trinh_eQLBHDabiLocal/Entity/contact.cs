namespace G03Trinh_eQLBHDabiLocal.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("contact")]
    public partial class contact
    {
        public int id { get; set; }
        public int customer { get; set; }

        [StringLength(1000)]
        public string title { get; set; }

        [Column(TypeName = "ntext")]
        public string content { get; set; }

        public int? status { get; set; }

        public virtual customer customer1 { get; set; }
    }
}
