using SEYNT.Application.Features.Organisations.Commands.CreateOrganisation;
using SEYNT.Application.Features.Organisations.Commands.DeleteOrganisation;
using SEYNT.Application.Features.Organisations.Commands.UpdateOrganisation;
using SEYNT.Application.Features.Organisations.Dtos;
using SEYNT.Application.Features.Organisations.Queries.GetOrganisations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SEYNT.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/[action]")]
public sealed class OrganisationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<GetOrganisationsResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<GetOrganisationsResponseDto>>> GetOrganisations(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetOrganisationsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> CreateOrganisation(CreateOrganisationCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> UpdateOrganisation(UpdateOrganisationCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> DeleteOrganisation(DeleteOrganisationCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
