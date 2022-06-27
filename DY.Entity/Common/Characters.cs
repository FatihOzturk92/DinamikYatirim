using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DY.Entity.Common
{
    [Table("CMN_Characters")]
    public class Characters:BaseEntity
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        [StringLength(1)]
        public string Name { get; set; }

    }
}
