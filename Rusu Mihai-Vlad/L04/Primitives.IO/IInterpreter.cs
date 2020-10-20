using System;
using System.Threading.Tasks;
using Primitives.IO.Mocking;

namespace Primitives.IO
{
    public interface IInterpreter<S> : IInterpreter
    {
        Task<A> Mock<A>(MockContext mockContext, Port<A> ma, S state, Func<Port<A>, Task<A>> interpret);
    }

    public interface IInterpreter
    {
        Task<A> Interpret<A>(Port<A> ma, object state, Func<Port<A>, Task<A>> interpret);
    }
}