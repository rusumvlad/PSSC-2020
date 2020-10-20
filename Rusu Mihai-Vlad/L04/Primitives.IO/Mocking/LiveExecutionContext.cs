using System;
using System.Reflection;

namespace Primitives.IO.Mocking
{
    public class LiveExecutionContext : IExecutionContext
    {
        public static LiveExecutionContext Instance = new LiveExecutionContext();

        public TResult FindEffect<TFunc, TResult>(TFunc defaultAction, MethodInfo methodInfo, Func<TFunc, TResult> execute)
        {
            return execute(defaultAction);
        }

        public void Dispose()
        {
        }
    }
}