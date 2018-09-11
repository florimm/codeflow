using System.Threading.Tasks;

namespace CodeFlow
{
    public delegate Task<Result<T>> CodeFlowResult<T>(IHandler handler);

    public class Flow
    {
	    private Flow() { }

	    public static CodeFlowResult<Flow> Init()
	    {
		    var fw = new Flow();
		    return (_) => Task.FromResult(Result.Ok(fw));
	    }
    }
}
