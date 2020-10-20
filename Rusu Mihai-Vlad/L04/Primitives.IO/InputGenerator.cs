using System;
using System.Collections.Generic;
using System.Text;

namespace Primitives.IO
{
    public class InputGenerator<T, TEnum> : IInputGenerator
    {
        protected IDictionary<TEnum, Func<T>> mappings = new Dictionary<TEnum, Func<T>>();

        public InputGenerator() { }

        public T Get(TEnum @case)
        {
            return mappings[@case]();
        }
    }

    public interface IInputGenerator
    {
    }
}
