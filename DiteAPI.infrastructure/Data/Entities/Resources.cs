using DiteAPI.infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.Infrastructure.Data.Entities
{
    public class Resources : BaseEntity
    {
        public Guid AcademyId { get; set; }
        public string ResourceName { get; set; }
        public string ResourcePath { get; set; }
        public string ResourceType { get; set; }
        public Guid UploadedBy { get; set; }
    }
}
