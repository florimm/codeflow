using CodeFlow;
using System;
using System.Threading.Tasks;

namespace ConsoleSample.Pipelines
{
    public class LoggerHandler : IPipeline
    {
	    public IPipeline next;
	    public LoggerHandler(IPipeline handler = null)
	    {
		    next = handler;
	    }
	    public async Task<Result<TR>> Handle<TR>(Func<Task<Result<TR>>> func)
	    {
            Console.WriteLine("Logger started");
		    Result<TR> result;
		    if (next != null)
		    {
			    result = await next.Handle(func).ConfigureAwait(false);
		    }
		    else
		    {
			    result = await func().ConfigureAwait(false);
		    }
            Console.WriteLine("Logger => result has status: " + result.IsSuccess);
		    return result;
	    }
    }
}
