using DiteAPI.infrastructure.Data.Entities;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class RemoveMemberFromAcademyCommand : IRequest<BaseResponse>
    {
        public Guid MemberId { get; set; }
        public Guid AcademyId { get; set; }
    }
}
