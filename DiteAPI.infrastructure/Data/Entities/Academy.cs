using DiteAPI.infrastructure.Infrastructure.Services.Implementations;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiteAPI.infrastructure.Data.Entities
{
    public class Academy : BaseEntity
    {
        /*private Academy()
        {
            AcademyCode = GenerateUniqueString();
        }

        private string? GenerateUniqueString()
        {

        }

        // This method is called for instantiating this class
        public static Academy Create()
        {
            return new Academy();
        }*/

        private readonly IHelperMethods _helperMethods;

        protected Academy() { }

        public Academy(IHelperMethods helperMethods)
        {
            _helperMethods = helperMethods;
            AcademyCode = _helperMethods.GenerateUniqueString();
        }


        // Class Properties:
        public string AcademyName { get; set; }
        public string AcademyCode { get; private set; }

        public string? Description{ get; set; }

        // FKs
        [Required]
        [ForeignKey(nameof(GenericUser))]
        public Guid CreatorId { get; set; }

        // Navigation properties
        public virtual List<Course> Courses { get; set; }               // 1 academy TO MANY courses
        public virtual List<GenericUser> GenericUser { get; set; }      // 1 academy TO MANY users
    }
}
