using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DY.Entity.Plugin
{
    [Table("P_RndData")]
    public class RndData:BaseEntity
    {
        public long Id { get; set; }
        public int CharactersId  { get; set; }
    }
}
