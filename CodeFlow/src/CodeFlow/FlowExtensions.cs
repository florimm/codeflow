using System;
using System.Threading.Tasks;

namespace CodeFlow
{
    public static class FlowExtensions
    {
	    public static Task<Result<T>> ProcessWithPipelines<T>(this FlowResult<T> src, IHandler handler)
	    {
		    return src(handler);
	    }
	    public static Task<Result<T>> Process<T>(this FlowResult<T> src)
	    {
		    return src(new EmptyHandler());
	    }

	    public static FlowResult<TR> Run<T, TR>(this FlowResult<T> src, Func<T, Task<Result<TR>>> func)
	    {
		    return async (handler) =>
		    {
			    var data = await src(handler).ConfigureAwait(false);
			    if (data.IsSuccess)
			    {
				    return await handler.Handle(() => func(data.Value)).ConfigureAwait(false);
			    }
			    return Result.Fail<TR>(data.Error);
		    };
	    }

        public static FlowResult<TR> Run<T, TR>(this FlowResult<T> src, Func<T, Result<TR>> func)
	    {
		    return async (handler) =>
		    {
			    var data = await src(handler).ConfigureAwait(false);
			    if (data.IsSuccess)
			    {
				    return await handler.Handle(() => func(data.Value).ToAsync()).ConfigureAwait(false);
			    }
			    return Result.Fail<TR>(data.Error);
		    };
	    }

        public static Task<Result<T>> ToAsync<T>(this Result<T> result)
        {
            return Task.FromResult(result);
        }
    }
}
