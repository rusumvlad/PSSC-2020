using CSharp.Choices;
using LanguageExt.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Profile.Domain.CreateProfileWorkflow
{
    [AsChoice]
    public static partial class EmailAddress
    {

        public interface IEmailAddress { }

        public class UnverifiedEmail : IEmailAddress
        {
            public string Email { get; private set; }

            private UnverifiedEmail(string email)
            {
                Email = email;
            }

            public static Result<UnverifiedEmail> Create(string email)
            {
                if (IsEmailAddressValid(email))
                {
                    return new UnverifiedEmail(email);
                }
                else
                {
                    return new Result<UnverifiedEmail>(new InvalidEmailException(email));
                }
            }

            private static bool IsEmailAddressValid(string email)
            {
                //TODO: validate email address based on RegEx

                //validate 
                if (email.Length >= 6)
                {
                    return true;
                }
                return false;
            }
        }

        public class VerifiedEmail: IEmailAddress
        {
            public string Email { get; private set; }

            internal VerifiedEmail(string email)
            {
                Email = email;
            }
        }
    }
}
