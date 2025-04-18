﻿using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
using DiteAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Data.Entities
{
    public class OtpVerification : BaseEntity
    {
        [ForeignKey(nameof(LinkedUser))]
        public Guid UserId { get; set; }
        public string Recipient { get; set; }
        public OtpRecipientTypeEnum RecipientType { get; set; }
        public string Code { get; set; } = default!;
        public OtpTokenStatusEnum Status { get; set; }
        public VerificationPurposeEnum Purpose { get; set; }
        public DateTimeOffset ConfirmedOn { get; set; }

        public virtual GenericUser LinkedUser { get; set; }
    }
}
