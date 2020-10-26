using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Text;
using static Profile.Domain.CreateProfileWorkflow.EmailAddress;

namespace Profile.Domain.CreateProfileWorkflow
{
    public class VerifyEmailService
    {
        public Result<VerifiedEmail> VerifyEmail(UnverifiedEmail email)
        {
            //send verification link
            //

            return new VerifiedEmail(email.Email);
        }
    }
}
