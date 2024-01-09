using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Responses
{
    public interface IBaseResponse<T> where T : class
    {
        public T Result { get; }
        public IList<ResponseErrors> Errors { get; }
    }
}
