namespace G03Trinh_eQLBHDabiLocal.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cart")]
    public partial class cart
    {
        public int id { get; set; }
        public int customer { get; set; }
        public int product { get; set; }

        public int? quantity { get; set; }
        public int? color { get; set; }

        public virtual customer customer1 { get; set; }

        public virtual product product1 { get; set; }
    }
}
