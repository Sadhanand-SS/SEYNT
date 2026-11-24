using SEYNT.Application.Features.Devices.Commands.CreateDevice;
using SEYNT.Application.Features.Devices.Commands.DeleteDevice;
using SEYNT.Application.Features.Devices.Commands.UpdateDevice;
using SEYNT.Application.Features.Devices.Dtos;
using SEYNT.Application.Features.Devices.Queries.GetDeviceById;
using SEYNT.Application.Features.Devices.Queries.GetDevices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SEYNT.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/[action]")]
public sealed class DevicesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<DeviceResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<DeviceResponseDto>>> GetDevices(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetDevicesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(DeviceResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<DeviceResponseDto>> GetDeviceById([FromQuery] GetDeviceByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> CreateDevice(CreateDeviceCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> UpdateDevice(UpdateDeviceCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> DeleteDevice(DeleteDeviceCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
