using System;
using System.Threading.Tasks;

namespace CodeFlow
{
    public interface IPipeline
    {
	    Task<Result<TR>> Handle<TR>(Func<Task<Result<TR>>> func);
    }
}
