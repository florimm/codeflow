using CodeFlow;
using System;
using System.Threading.Tasks;

namespace ConsoleSample.Handlers
{

    public class MethodFormatSeperatorHandler : IHandler
    {
	    public IHandler next;
	    public MethodFormatSeperatorHandler(IHandler handler = null)
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
