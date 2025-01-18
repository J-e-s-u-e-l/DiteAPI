using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using DiteAPI.infrastructure.Data.Models;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class CreateAcademyCommandHandler : IRequestHandler<CreateAcademyCommand, BaseResponse<CreateAcademyResponse>>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<RegistrationCommand> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHelperMethods _helperMethods;

        public CreateAcademyCommandHandler(
            DataDBContext dbContext,
            ILogger<RegistrationCommand> logger,
            IOptions<AppSettings> options,
            IHttpContextAccessor httpContextAccessor,
            IHelperMethods helperMethods)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = options.Value;
            _helperMethods = helperMethods;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<CreateAcademyResponse>> Handle(CreateAcademyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var newAcademyCode = _helperMethods.Generate6CharString();
                    var codeExists = await _dbContext.Academy.AnyAsync(x => x.AcademyCode == newAcademyCode);

                    while(codeExists)
                    {
                        newAcademyCode = _helperMethods.Generate6CharString();
                        codeExists = await _dbContext.Academy.AnyAsync(x => x.AcademyCode == newAcademyCode);
                    }
                    var userId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;
                    var newAcademy = new Academy
                    {
                        AcademyName = request.AcademyName,
                        Description = request.Description,
                        AcademyCode = newAcademyCode,
                        CreatorId = userId,
                    };
                    await _dbContext.Academy.AddAsync(newAcademy, cancellationToken);

                    for(int i=0; i < request.Tracks.Count; i++)
                    {
                        var newTrack = new Track
                        {
                            TrackName = request.Tracks[i],
                            AcademyId = newAcademy.Id
                        };
                        await _dbContext.Tracks.AddAsync(newTrack);
                    }

                    await _dbContext.SaveChangesAsync(cancellationToken);

                    var academyId = await _dbContext.Academy.Where(x => x.AcademyCode == newAcademyCode).Select(x => x.Id).FirstOrDefaultAsync();
                    
                    // Add Creator to the Academy
                    var newAcademyMember = new AcademyMembers
                    {
                        GenericUserId = userId,
                        AcademyId = academyId,
                        RoleId = new Guid(_appSettings.AdminRoleId)
                    };
                    await _dbContext.AcademyMembers.AddAsync(newAcademyMember, cancellationToken);

                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    var academyDetails = await _dbContext.Academy.FirstOrDefaultAsync();

                    CreateAcademyResponse createAcademyResponse = new CreateAcademyResponse()
                    {
                        AcademyId = academyId,
                        AcademyName = request.AcademyName,
                        TrackNames = request.Tracks,
                        AcademyDescription = request.Description
                    };

                    return new BaseResponse<CreateAcademyResponse>(true, _appSettings.AcademyCreatedSuccessfully, createAcademyResponse);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"CREATE_ACADEMY_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                    return new BaseResponse<CreateAcademyResponse>(false, _appSettings.ProcessingError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"CREATE_ACADEMY_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<CreateAcademyResponse>(false, _appSettings.ProcessingError);
            }
        }

        private Guid GetUserIdFromHttpContext(HttpContext context)
        {
            return (Guid)context.Items["UserId"]!;
        }
    }
}
