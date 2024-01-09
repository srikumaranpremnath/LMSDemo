using MediatR;
using Responses;
using System.Collections.Generic;

namespace Application.User.GetByRollNum
{
    public class GetByRollNumQuery : IRequest<BaseResponse<List<UserDTO>>>
    {
        public string RollNum { get; set; }
    }
}
