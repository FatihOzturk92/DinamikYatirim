using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DY.Entity.Common
{
    [Table("CMN_Languages")]

    public class Languages:BaseEntity
    {
        public Int16 Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
    }
}
