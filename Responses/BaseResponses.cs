using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Responses
{
    public class BaseResponse<T> : IBaseResponse<T> where T : class
    {
        public T Result { get; private set; }
        public IList<ResponseErrors> Errors { get; private set; }
        public BaseResponse(T result) : this()
        {
            Result = result;
        }
        public BaseResponse(ResponseErrors error) : this()
        {
            Errors = new List<ResponseErrors>() { error };
        }
        public BaseResponse(IEnumerable<ResponseErrors> errors) : this()
        {
            Errors = new List<ResponseErrors>(errors);
        }
        public BaseResponse()
        {
            Errors = new List<ResponseErrors>();
        }
    }




}
