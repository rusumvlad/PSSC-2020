using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharp.Choices;

namespace Profile.Domain.CreateProfileWorkflow
{
    [AsChoice]
    public static partial class CreateProfileResult
    {
        public interface ICreateProfileResult { }

        public class ProfileCreated: ICreateProfileResult
        {
            public Guid ProfileId { get; private set; }
            public string Email { get; private set; }

            public ProfileCreated(Guid profileId, string email)
            {
                ProfileId = profileId;
                Email = email;
            }
        }

        public class ProfileNotCreated: ICreateProfileResult
        {
            public string Reason { get; set; }

            public ProfileNotCreated(string reason)
            {
                Reason = reason;
            }
        }

        public class ProfileValidationFailed: ICreateProfileResult
        {
            public IEnumerable<string> ValidationErrors { get; private set; }

            public ProfileValidationFailed(IEnumerable<string> errors)
            {
                ValidationErrors = errors.AsEnumerable();
            }
        }
    }
}
