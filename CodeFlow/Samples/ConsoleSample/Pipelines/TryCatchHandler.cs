using CodeFlow;
using System;
using System.Threading.Tasks;

namespace ConsoleSample.Pipelines
{
    public class TryCatchHandler : IPipeline
    {
	    public IPipeline next;
	    public TryCatchHandler(IPipeline handler = null)
	    {
		    next = handler;
	    }
	    public async Task<Result<TR>> Handle<TR>(Func<Task<Result<TR>>> func)
	    {
		    Result<TR> result;
		    try
		    {
			    if (next != null)
			    {
				    result = await next.Handle(func).ConfigureAwait(false);
			    }
			    else
			    {
				    result = await func().ConfigureAwait(false);
			    }
			    return result;
		    }
		    catch (Exception ex)
		    {
			    return Result.Fail<TR>(ex.Message);
		    }
	    }
    }
}
