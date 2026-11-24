using MediatR;

namespace SEYNT.Application.Features.Organisations.Commands.DeleteOrganisation;

public sealed class DeleteOrganisationCommand : IRequest<Guid>
{
    public Guid OrganisationId { get; set; }
}
