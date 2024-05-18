using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Data.Entities
{
    public class Course : BaseEntity
    {
        public string CourseName { get; set; }

        //FKs
        [Required]
        [ForeignKey(nameof(Academy))] 
        public Guid AcademyId { get; set; }

        // Navigation properties
        public virtual Academy Academy { get; set; }        // MANY courses TO 1 Academy
    }
}
