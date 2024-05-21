using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Data.Entities
{
    public class OtpVerification : BaseEntity
    {
        public string Recipient { get; set; }
        public OtpRecipientTypeEnum RecipientType { get; set; }
        public Guid UserId { get; set; }
        public string Code { get; set; } = default!;
        public OtpCodeStatusEnum Status { get; set; }
        public OtpVerificationPurposeEnum Purpose { get; set; }
        public DateTimeOffset ConfirmedOn { get; set; }
    }
}
