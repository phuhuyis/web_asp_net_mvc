using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace G03Trinh_eQLBHDabiLocal.Entity
{
    [Table("slide")]
    public class slide
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public slide()
        {
        }
        [Key]
        public int id { get; set; }
        [Column(TypeName = "text")]
        public string url { get; set; }
        public int position { get; set; }
        public string username { get; set; }
        public virtual account account { get; set; }
    }
}