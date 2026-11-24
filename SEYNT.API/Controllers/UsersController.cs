using SEYNT.Application.Common.Models;
using SEYNT.Application.Features.Users.Commands.CreateUser;
using SEYNT.Application.Features.Users.Commands.DeleteUser;
using SEYNT.Application.Features.Users.Commands.UpdateUser;
using SEYNT.Application.Features.Users.Dtos;
using SEYNT.Application.Features.Users.Queries.GetUserById;
using SEYNT.Application.Features.Users.Queries.GetUsers;
using SEYNT.Application.Features.Users.Queries.SearchUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SEYNT.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/[action]")]
public sealed class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateUserResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateUserResponseDto>> CreateUser(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<GetUsersResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<GetUsersResponseDto>>> GetUsers([FromQuery] GetUsersQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetUsersResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetUsersResponseDto>> GetUserById([FromQuery] GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<GetUsersResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<GetUsersResponseDto>>> SearchUsers([FromQuery] SearchUsersQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> UpdateUser(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> DeleteUser(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
