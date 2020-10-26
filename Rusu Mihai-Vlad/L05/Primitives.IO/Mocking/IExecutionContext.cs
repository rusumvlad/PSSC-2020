using System;
using System.ComponentModel;
using System.Reflection;

namespace Primitives.IO.Mocking
{
    public delegate TResult Effect0<TEnum, TResult>(TEnum defaultEffect, Func<TResult> func);

    public delegate TResult Effect1<TEnum, TIn1, TResult>(TEnum defaultEffect, Func<TIn1, TResult> func, TIn1 param1);

    public delegate TResult Effect2<TEnum, TIn1, TIn2, TResult>(TEnum defaultEffect, Func<TIn1, TIn2, TResult> func,
        TIn1 param1, TIn2 param2);

    public delegate TResult Effect3<TEnum, TIn1, TIn2, TIn3, TResult>(TEnum defaultEffect,
        Func<TIn1, TIn2, TIn3, TResult> func, TIn1 param1, TIn2 param2, TIn3 param3);

    public delegate TResult Effect4<TEnum, TIn1, TIn2, TIn3, TIn4, TResult>(TEnum defaultEffect,
        Func<TIn1, TIn2, TIn3, TIn4, TResult> func, TIn1 param1, TIn2 param2, TIn3 param3, TIn4 param4);

    public delegate TResult Effect<TEnum, TIn1, TIn2, TIn3, TIn4, TIn5, TResult>(TEnum defaultEffect, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TResult> func, TIn1 param1, TIn2 param2, TIn3 param3,
        TIn4 param4, TIn5 param5);

    public static class ExecutionContextEx
    {
        public static TResult Effect<TResult>(this IExecutionContext self, Func<TResult> func)
            => self.FindEffect(func, func.Method, eff => eff());
        public static TResult Effect<TEnum, TIn1, TResult>(this IExecutionContext self, Func<TIn1, TResult> func, TIn1 param1)
            => self.FindEffect(func, func.Method, eff => eff(param1));
        public static TResult Effect<TEnum, TIn1, TIn2, TResult>(this IExecutionContext self, Func<TIn1, TIn2, TResult> func, TIn1 param1, TIn2 param2)
            => self.FindEffect(func, func.Method, eff => eff(param1, param2));
        public static TResult Effect<TEnum, TIn1, TIn2, TIn3, TResult>(this IExecutionContext self, Func<TIn1, TIn2, TIn3, TResult> func, TIn1 param1, TIn2 param2, TIn3 param3)
            => self.FindEffect(func, func.Method, eff => eff(param1, param2, param3));
        public static TResult Effect<TEnum, TIn1, TIn2, TIn3, TIn4, TResult>(this IExecutionContext self, Func<TIn1, TIn2, TIn3, TIn4, TResult> func, TIn1 param1, TIn2 param2, TIn3 param3, TIn4 param4)
            => self.FindEffect(func, func.Method, eff => eff(param1, param2, param3, param4));
        public static TResult Effect<TEnum, TIn1, TIn2, TIn3, TIn4, TIn5, TResult>(this IExecutionContext self, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TResult> func, TIn1 param1, TIn2 param2, TIn3 param3, TIn4 param4, TIn5 param5)
            => self.FindEffect(func, func.Method, eff => eff(param1, param2, param3, param4, param5));
    }

    public interface IExecutionContext : IDisposable
    {
        TResult FindEffect<TFunc, TResult>(TFunc defaultAction, MethodInfo methodInfo, Func<TFunc, TResult> execute);
    }
}