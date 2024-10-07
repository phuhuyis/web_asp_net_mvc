namespace G03Trinh_eQLBHDabiLocal.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class bill_info
    {
        public int id { get; set; }
        public int bill { get; set; }
        public int product { get; set; }

        public int? quantity { get; set; }
        public int? color { get; set; }

        public virtual bill bill1 { get; set; }

        public virtual product product1 { get; set; }
    }
}
