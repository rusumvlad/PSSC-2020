using System;
using System.Collections.Generic;
using System.Text;

namespace Access.Primitives.IO.Attributes
{
    public class MockEffectAttribute : Attribute
    {
        public object Effect { get; }

        public MockEffectAttribute(object effect)
        {
            Effect = effect;
        }
    }
}
