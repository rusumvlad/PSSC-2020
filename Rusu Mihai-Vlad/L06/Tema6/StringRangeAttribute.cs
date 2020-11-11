using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tema6
{
    [AttributeUsage(AttributeTargets.Property)]
    public class StringRangeAttribute : ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;

        public StringRangeAttribute(int min, int max) : base($"The field length has to be between {min} and {max}")
        {
            _min = min;
            _max = max;
        }

        public override bool IsValid(object value)
        {
            var strLen = value.ToString().Length;
            return strLen > _min && strLen < _max;
        }
    }
}
