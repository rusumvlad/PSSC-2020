using System;
using System.Linq;
using System.Threading.Tasks;
using OpenTracing.Util;

namespace Primitives.IO
{
    public interface IInterpreterAsync
    {
        Task<A> Interpret<A, TState>(Port<A> ma, TState state);
    }

    public class LiveInterpreterAsync : IInterpreterAsync
    {
        private readonly IServiceProvider _serviceProvider;

        public LiveInterpreterAsync(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<A> Interpret<A, TState>(Port<A> ma, TState state)
        {
            try
            {
                if (ma is Return<A> r)
                {
                    return r.Value;
                }
                else
                {
                    var tracer = GlobalTracer.Instance;
                    var opType = ma.GetType();
                    using (var scope = tracer
                        .BuildSpan($"Adapter<{opType.GenericTypeArguments[0].Name}, {opType.GenericTypeArguments[1].Name}, {state.GetType().Name}>")
                        .StartActive(true))
                    {
                        return await ResolveInterpreter<A, TState>(ma).Interpret(ma, state, (a) => Interpret(a, state));
                    }
                }
            }
            catch (Exception ex)
            {
                //Tracking.TrackException(ex);
                throw;
            }
        }

        private IInterpreter ResolveInterpreter<A, S>(Port<A> ma)
        {
            return (IInterpreter)_serviceProvider.GetService(GetTypeMarker(ma));
        }

        private readonly Type _nonGenericTypeMaker = typeof(IAdapter);
        private Type GetTypeMarker<A>(Port<A> ma) =>
            ma.GetType().GetInterfaces().Single(p => _nonGenericTypeMaker.IsAssignableFrom(p) && p.IsGenericType);
    }
}
