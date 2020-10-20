using LanguageExt;
using System;

namespace Primitives.IO
{
    public interface Port<out A>
    {
        Port<B> Bind<B>(Func<A, Port<B>> f);
    }

    public interface IAdapter { }

    public interface IAdapter<O, R> : IAdapter { }

    public class Port<O, R, A> : Port<A>, IAdapter<O, R>
    {
        public readonly O Cmd;
        public readonly Func<R, Port<A>> Do;

        public Port(O cmd, Func<R, Port<A>> @do)
        {
            Cmd = cmd;
            Do = @do;
        }

        public Port<B> Bind<B>(Func<A, Port<B>> f) => new Port<O, R, B>(Cmd, r => Do(r).Bind(f));
    }
      
    public class Return<A> : Port<A>
    {
        public readonly A Value;
        public Return(A value) =>
            Value = value;

        public Port<B> Bind<B>(Func<A, Port<B>> f) => f(Value);
    }

    public class Inline<A> : Port<A>
    {
        private readonly Func<A> _inline;

        public Inline(Func<A> inline)
        {
            _inline = inline;
        }
        public Port<B> Bind<B>(Func<A, Port<B>> f) => f(_inline());
    }
}
