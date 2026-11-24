using SEYNT.Application.Common.Models;
using SEYNT.Application.Features.UserRoles.Commands.CreateUserRole;
using SEYNT.Application.Features.UserRoles.Commands.DeleteUserRole;
using SEYNT.Application.Features.UserRoles.Commands.UpdateUserRole;
using SEYNT.Application.Features.UserRoles.Dtos;
using SEYNT.Application.Features.UserRoles.Queries.GetUserRoleById;
using SEYNT.Application.Features.UserRoles.Queries.GetUserRoles;
using SEYNT.Application.Features.UserRoles.Queries.SearchUserRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SEYNT.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/[action]")]
public sealed class RolesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<GetUserRolesResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<GetUserRolesResponseDto>>> GetUserRoles([FromQuery] GetUserRolesQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetUserRolesResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetUserRolesResponseDto>> GetUserRoleById([FromQuery] GetUserRoleByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<GetUserRolesResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<GetUserRolesResponseDto>>> SearchUserRoles([FromQuery] SearchUserRolesQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> CreateUserRole(CreateUserRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> UpdateUserRole(UpdateUserRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> DeleteUserRole(DeleteUserRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
