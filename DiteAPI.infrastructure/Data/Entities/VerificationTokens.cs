using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
using DiteAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Data.Entities
{
    public class VerificationTokens : BaseEntity
    {
        [ForeignKey(nameof(LinkedUser))]
        public Guid UserId { get; set; }
        public string Token { get; set; } = default!;
        public string Recipient { get; set; }
        public VerificationPurposeEnum Purpose { get; set; }
        public DateTimeOffset ConfirmedOn { get; set; }
        public OtpTokenStatusEnum Status { get; set; }

        public virtual GenericUser LinkedUser { get; set; }

    }
}
