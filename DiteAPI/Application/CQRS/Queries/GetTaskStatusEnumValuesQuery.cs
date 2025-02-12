using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Queries
{
    public class GetTaskStatusEnumValuesQuery : IRequest<BaseResponse<GetTaskStatusEnumValuesResponse>>
    {

    }
}
