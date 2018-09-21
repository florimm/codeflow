using CodeFlow;
using System;
using System.Threading.Tasks;

namespace ConsoleSample.Pipelines
{

    public class MethodFormatSeperatorHandler : IPipeline
    {
	    public IPipeline next;
	    public MethodFormatSeperatorHandler(IPipeline handler = null)
	    {
		    next = handler;
	    }
	    public async Task<Result<TR>> Handle<TR>(Func<Task<Result<TR>>> func)
	    {
            Console.WriteLine("----------------Method star-----------------------");
		    Result<TR> result = next != null ? await next.Handle(func).ConfigureAwait(false) : await func().ConfigureAwait(false);
            Console.WriteLine("----------------Method end------------------------");
		    return result;
	    }
    }
}
