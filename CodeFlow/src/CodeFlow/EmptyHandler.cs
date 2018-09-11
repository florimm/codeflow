using System;
using System.Threading.Tasks;

namespace CodeFlow
{
    public class EmptyHandler : IHandler
    {
        public Task<Result<TR>> Handle<TR>(Func<Task<Result<TR>>> func) => func();
    }
}
