using MediatR;
using Responses;

namespace Application.User.BatchDelete
{
    public class DeleteByBatchCommand : IRequest<BaseResponse<object>>
    {
        public string BatchYear { get; set; }
        public string LoggedUserName { get; set; }
    }
}
