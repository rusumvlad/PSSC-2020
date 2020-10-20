using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using Primitives.IO.Attributes;

namespace Primitives.IO.Mocking
{
    public class MockContext : IExecutionContext
    {
        private readonly object[] _ctx;
        private List<EffectOverride> _effects = new List<EffectOverride>();

        public static MockContext GetInstance(params object[] ctx) => new MockContext(ctx);

        private MockContext(params object[] ctx)
        {
            _ctx = ctx;
        }

        public MockContext CreateScope()
        {
            return new MockContext(_ctx);
        }

        public T Get<T>()
        {
            return _ctx.OfType<T>().SingleOrDefault();
        }

        public object[] GetAll()
        {
            return _ctx;
        }

        public TResult FindEffect<TFunc, TResult>(TFunc defaultAction, MethodInfo methodInfo, Func<TFunc, TResult> execute)
        {
            var sideEffectType = methodInfo.GetCustomAttribute<SideEffectAttribute>().Type;
            var currentCase = _ctx.Single(p => p.GetType().Equals(sideEffectType));

            var effect = _effects
                .Where(p => p.CaseType.Equals(sideEffectType))
                .SingleOrDefault(p => ((int)p.Case).Equals((int)currentCase));
            if (effect == null)
                throw new Exception("A mock context has been set but you haven't defined any mocks in your operations. Review your RegisterMocks() implementation");
            object method = effect.MethodInfo.CreateDelegate(typeof(TFunc), effect.Target);
            var result = execute((TFunc)method);
            return result;
        }

        public void AttributeDiscovery(object instance)
        {
            var adapterType = instance.GetType();
            _effects = adapterType.GetMethods().Where(p => Attribute.IsDefined(p, typeof(MockEffectAttribute)))
            .Select(p =>
            {
                var attribute = p.GetCustomAttribute<MockEffectAttribute>();
                return new EffectOverride(attribute.Effect, p, instance);
            })
            .ToList();
        }

        public void Dispose()
        {
        }
    }
}