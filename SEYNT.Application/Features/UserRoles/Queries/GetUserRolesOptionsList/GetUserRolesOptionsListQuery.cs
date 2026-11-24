using SEYNT.Application.Features.UserRoles.Queries.GetUserRolesOptionsList.Dtos;
using MediatR;

namespace SEYNT.Application.Features.UserRoles.Queries.GetUserRolesOptionsList;

public class GetUserRolesOptionsListQuery : IRequest<List<UserRoleOptionDto>>
{
}
