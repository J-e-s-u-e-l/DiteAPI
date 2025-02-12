using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskStatusEnum = DiteAPI.infrastructure.Infrastructures.Utilities.Enums.TaskStatusEnum;

namespace DiteAPI.Infrastructure.Data.Entities
{
    public class Tasks : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTimeOffset DueDate { get; set; }

        public string CourseTag { get; set; }

        [Required]
        public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Pending;


        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public virtual GenericUser User { get; set; }
    }
}
