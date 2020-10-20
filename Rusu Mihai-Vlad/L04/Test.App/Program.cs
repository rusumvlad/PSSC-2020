using Profile.Domain.CreateProfileWorkflow;
using System;
using System.Collections.Generic;
using System.Net;
using static Profile.Domain.CreateProfileWorkflow.CreateProfileResult;

namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmd = new CreateProfileCmd("Ion", string.Empty, "Ionescu", "ion.inonescu@company.com");
            var result = CreateProfile(cmd);

            result.Match(
                    ProcessProfileCreated,
                    ProcessProfileNotCreated,
                    ProcessInvalidProfile
                );

            Console.ReadLine();
        }

        private static ICreateProfileResult ProcessInvalidProfile(ProfileValidationFailed validationErrors)
        {
            Console.WriteLine("Profile validation failed: ");
            foreach (var error in validationErrors.ValidationErrors)
            {
                Console.WriteLine(error);
            }
            return validationErrors;
        }

        private static ICreateProfileResult ProcessProfileNotCreated(ProfileNotCreated profileNotCreatedResult)
        {
            Console.WriteLine($"Profile not created: {profileNotCreatedResult.Reason}");
            return profileNotCreatedResult;
        }

        private static ICreateProfileResult ProcessProfileCreated(ProfileCreated profile)
        {
            Console.WriteLine($"Profile {profile.ProfileId}");
            return profile;
        }

        public static ICreateProfileResult CreateProfile(CreateProfileCmd createProfileCommand)
        {
            if (string.IsNullOrWhiteSpace(createProfileCommand.EmailAddress))
            {
                var errors = new List<string>() { "Invlaid email address" };
                return new ProfileValidationFailed(errors);
            }

            if(new Random().Next(10) > 1)
            {
                return new ProfileNotCreated("Email could not be verified");
            }

            var profileId = Guid.NewGuid();
            var result = new ProfileCreated(profileId, createProfileCommand.EmailAddress);

            //execute logic
            return result;
        }
    }
}
