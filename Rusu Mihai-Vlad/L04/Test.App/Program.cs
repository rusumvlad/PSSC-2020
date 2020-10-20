using Profile.Domain.CreateProfileWorkflow;
using Question.Domain.CreateQuestionWorkflow;
using System;
using System.Collections.Generic;
using static Profile.Domain.CreateProfileWorkflow.CreateProfileResult;
using static Question.Domain.CreateQuestionWorkflow.CreateQuestionResult;



namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {
            //Profile
            var cmdProfile = new CreateProfileCmd("Ion", string.Empty, "Ionescu", "ion.inonescu@company.com");
            var resultProfile = CreateProfile(cmdProfile);
            //Question
            var cmdQuestion = new CreateQuestionCmd("How to convert int to string in C#?", "I try so hard to convert int to string but i can't, the language that I use is C#.", "C#");
            var resultQuestion = CreateQuestion(cmdQuestion);

            resultProfile.Match(
                    ProcessProfileCreated,
                    ProcessProfileNotCreated,
                    ProcessInvalidProfile
                );
            resultQuestion.Match(
                ProcessQuestionCraeted,
                ProcessQuestionNotCreated,
                ProcessInvalidQuestion
                );
            Console.ReadLine();
        }
        //Profile invalid
        private static ICreateProfileResult ProcessInvalidProfile(ProfileValidationFailed validationErrors)
        {
            Console.WriteLine("Profile validation failed: ");
            foreach (var error in validationErrors.ValidationErrors)
            {
                Console.WriteLine(error);
            }
            return validationErrors;
        }

        //Question invalid
        private static ICreateQuestionResult ProcessInvalidQuestion(QuestionValidationFailed validationErrors)
        {
            Console.WriteLine("Question validation failed: ");
            foreach (var error in validationErrors.ValidationErrors)
            {
                Console.WriteLine(error);
            }
            return validationErrors;
        }

        //Profile not Created
        private static ICreateProfileResult ProcessProfileNotCreated(ProfileNotCreated profileNotCreatedResult)
        {
            Console.WriteLine($"Profile not created: {profileNotCreatedResult.Reason}");
            return profileNotCreatedResult;
        }

        //Question not Created
        private static ICreateQuestionResult ProcessQuestionNotCreated(QuestionNotCreated questionNotCreatedResult)
        {
            Console.WriteLine($"Question not created: {questionNotCreatedResult.Reason}");
            return questionNotCreatedResult;
        }

        //Profile Created
        private static ICreateProfileResult ProcessProfileCreated(ProfileCreated profile)
        {
            Console.WriteLine($"Profile {profile.ProfileId}");
            return profile;
        }

        //Question Created
        private static ICreateQuestionResult ProcessQuestionCraeted(QuestionCreated question)
        {
            Console.WriteLine($"Question {question.QuestionId}");
            Console.WriteLine($"Title: {question.Title}");
            return question;
        }

        //Create Profile
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
            var resultsProfile = new ProfileCreated(profileId, createProfileCommand.EmailAddress);

            //execute logic
            return resultsProfile;
        }

        //Create Question
        public static ICreateQuestionResult CreateQuestion(CreateQuestionCmd createQuestionCommand)
        {
            if (string.IsNullOrWhiteSpace(createQuestionCommand.Title))
            {
                var errors = new List<string>() { "Invalid title" };
                return new QuestionValidationFailed(errors);
            }
            else if (string.IsNullOrWhiteSpace(createQuestionCommand.Body))
            {
                var errors = new List<string>() { "Invalid body" };
                return new QuestionValidationFailed(errors);
            }
            else if (string.IsNullOrWhiteSpace(createQuestionCommand.Tags))
            {
                var errors = new List<string>() { "Invalid tag" };
                return new QuestionValidationFailed(errors);
            }

            if (new Random().Next(10) > 1)
            {
                return new QuestionNotCreated("Question could not be verified");
            }

            var questionId = Guid.NewGuid();
            var resultsQuestion = new QuestionCreated(questionId, createQuestionCommand.Title);

            return resultsQuestion;
        }
    }
}
