using LanguageExt;
using LanguageExt.Common;
using Profile.Domain.CreateProfileWorkflow;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using static Profile.Domain.CreateProfileWorkflow.CreateProfileResult;
using static Profile.Domain.CreateProfileWorkflow.EmailAddress;
using static Question.Domain.CreateQuestionWorkflow.CreateQuestionResult;
using static Question.Domain.CreateQuestionWorkflow.Question;
using Question.Domain.CreateQuestionWorkflow;

namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var emailResult = UnverifiedEmail.Create("tete@st.com");


            emailResult.Match(
                    Succ: email =>
                    {
                        SendResetPasswordLink(email);

                        Console.WriteLine("Email address is valid.");
                        return Unit.Default;
                    },
                    Fail: ex =>
                    {
                        Console.WriteLine($"Invalid email address. Reason: {ex.Message}");
                        return Unit.Default;
                    }
                );
            //question

            var cmdQuestion = new CreateQuestionCmd("Ce se face in c#?", "Va rog sa ma ajutati ca nu stiu ce sa fac in c#", new string[] { "C#", "C", "C++" },3);
            var questionResult = InvalidateQuestion.Create(cmdQuestion);

            questionResult.Match(
                    Succ: question =>
                    {
                        PostedQuestion(question);
                        Console.WriteLine($"Question with title \"{question.Question.Title}\" is valid");
                        return Unit.Default;
                    },
                    Fail: ex =>
                    {
                        Console.WriteLine($"Invalid Question.\n Reason: {ex.Message}");
                        return Unit.Default;
                    }
            );

            Console.ReadLine();
        }

        private static void SendResetPasswordLink(UnverifiedEmail email)
        {
            var verifiedEmailResult = new VerifyEmailService().VerifyEmail(email);
            verifiedEmailResult.Match(
                    verifiedEmail =>
                    {
                        new RestPasswordService().SendRestPasswordLink(verifiedEmail).Wait();
                        return Unit.Default;
                    },
                    ex =>
                    {
                        Console.WriteLine("Email address could not be verified");
                        return Unit.Default;
                    }
                );
        }

        private static void PostedQuestion(InvalidateQuestion question)
        {
            var verifyQuestionResult = new VerifyQuestion().VerifiedQuestion(question);
            verifyQuestionResult.Match(
                    voteQuestion =>
                    {
                        Console.WriteLine("The question is published and can be voted");
                        return Unit.Default;
                    },
                    ex =>
                    {
                        Console.WriteLine("Number of votes could not be verified");
                        return Unit.Default;
                    }
                );
        }


    }
}
