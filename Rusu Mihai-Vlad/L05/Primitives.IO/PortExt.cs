using LanguageExt;
using System;
using Primitives.IO;

public static class PortExt
{
    public static Port<A> Port<A>(this A a) =>
       new Return<A>(a);
    public static Port<B> Select<A, B>(this Port<A> m, Func<A, B> f) =>
            m.Bind(a => f(a).Port());
    public static Port<C> SelectMany<A, B, C>(this Port<A> m, Func<A, Port<B>> f, Func<A, B, C> project) =>
            m.Bind(a => f(a).Bind(b => project(a, b).Port()));
    public static Port<R> NewPort<O, R>(O op) => new Port<O, R, R>(op, Port);
}
