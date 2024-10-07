using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace G03Trinh_eQLBHDabiLocal.Entity
{
    [Table("bank")]
    public partial class bank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public bank()
        {
        }
        [Key]
        public int id { get; set; }
        [StringLength(100)]
        public string idBank { get; set; }

        [StringLength(100)]
        public string number { get; set; }
        [StringLength(1000)]
        public string name { get; set; }
        [Column(TypeName = "ntext")]
        public string content { get; set; }
    }
}