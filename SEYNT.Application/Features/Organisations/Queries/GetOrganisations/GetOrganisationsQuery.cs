using SEYNT.Application.Features.Organisations.Dtos;
using MediatR;

namespace SEYNT.Application.Features.Organisations.Queries.GetOrganisations;

public sealed class GetOrganisationsQuery : IRequest<List<GetOrganisationsResponseDto>>
{
}
