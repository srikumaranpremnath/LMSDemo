using MediatR;
using Responses;
using System.Collections.Generic;

namespace Application.User.GetBatchByNotReturned
{
    public class GetBatchByNotReturnedQuery : IRequest<BaseResponse<List<UserDTO>>>
    {
        public string batchYear { get; set; }
    }
}
