using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTimeOffset TimeCreated { get; set; }
        public DateTimeOffset TimeUpdated { get; set; }
    }

    public class BaseResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }

        public BaseResponse(bool status, string message)
        {
            Status = status;
            Message = message;
        }
    }

    public class BaseResponse<T>
    {
        public bool Status { get; set; }
        public string? Message { get; set; }

        public T? Data { get; set; }
        public BaseResponse()
        {
            
        }
        public BaseResponse(bool status, string message)
        {
            Status = status;
            Message = message;
        }
        public BaseResponse(bool status, string message, T data)
        {
            Status = status;
            Message = message;
        }
    }

    public class JwtSettings
    {
#nullable disable
        public string Secret { get; set; }
        public int ExpiryMinutes { get; set; }
        public string Issuer { get; set; }
    }
}
