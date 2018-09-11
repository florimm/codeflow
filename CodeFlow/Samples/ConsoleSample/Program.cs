using ConsoleSample.Handlers;
using System;
using CodeFlow;
using System.Threading.Tasks;

namespace ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var pipeline = new MethodFormatSeperatorHandler(new LoggerHandler(new TryCatchHandler()));
	        
            var result = Flow.Init()
	        .Run(_ => GetEmail("joe_doe"))
	        .Run(response => SendEmail(response))
            .Run(_ => DoDbManipulation())
	        .ProcessWithPipelines(pipeline)
	        .GetAwaiter().GetResult();
	        
            if(result.IsSuccess) {
		        Console.WriteLine("Result is: " + result.Value);
	        } else {
		        Console.WriteLine("Result is not valid: " + result.Error);
	        }
            Console.ReadLine();
        }

        public static async Task<Result<string>> GetEmail(string name)
        {
            await Task.Delay(2000);
            Console.WriteLine($"Getting email for {name}");
            return Result.Ok($"{name}@gmail.com");
        }

        public static async Task<Result<bool>> SendEmail(string email)
        {
            await Task.Delay(2000);
            Console.WriteLine($"Sending email to {email}");
            return Result.Ok(true);
        }

        public static Result<bool> DoDbManipulation()
        {
            Console.WriteLine($"DB changes");
            return Result.Ok(true);
        }
    }
}
