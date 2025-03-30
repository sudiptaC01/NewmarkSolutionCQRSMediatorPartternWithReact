using MediatR;
using Newmark_Technical_Assessment.Entity;

namespace Newmark_Technical_Assessment.Query
{
    public class GetAllPropertyQuery : IRequest<List<PropertyDetails>>
    {
    }
}
