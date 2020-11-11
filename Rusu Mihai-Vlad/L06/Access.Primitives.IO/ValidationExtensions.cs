

using System.Linq;
using LanguageExt.Common;

namespace Access.Primitives.Extensions.ObjectExtensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using LanguageExt;

    public static class ValidationExtensions
    {
        public static IEnumerable<ValidationResult> ValidateObject(this object target, out bool isValid)
        {
            var validationContext = new ValidationContext(target);
            var results = new List<ValidationResult>();

            isValid = Validator.TryValidateObject(target, validationContext, results, true);
            return results;
        }

        public static bool ValidateObject(this object target)
        {
            target.ValidateObject(out var isValid);
            return isValid;
        }

        public static IEnumerable<ValidationResult> ValidateObject<T>(this Option<T> target)
        {
            return target.Match(
                p => p.ValidateObject(out bool isValid),
                () => new ValidationResult[] { new ValidationResult("Received [None] instead of [Some]") } as IEnumerable<ValidationResult>);
        }

        public static IEnumerable<ValidationResult> ValidateObject<T>(this Option<IEnumerable<T>> target)
        {
            return target.Match(
                e => e.SelectMany(p => p.ValidateObject(out bool isValid)),
                () => new ValidationResult[] { new ValidationResult("Received [None] instead of [Some]") } as IEnumerable<ValidationResult>);
        }

        public static TryAsync<bool> TryValidate(this object target)
        {
            return new TryAsync<bool>(async () =>
            {
                var errors = target.ValidateObject(out bool isValid);
                return isValid ?
                    new Result<bool>(isValid) :
                    new Result<bool>(new ValidationException(string.Join(Environment.NewLine, errors.Select(p => p.ErrorMessage))));
            }).Memo();
        }

    }
}
