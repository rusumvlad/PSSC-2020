using CSharp.Choices;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Question.Domain.CreateQuestionWorkflow
{
    [AsChoice]
    public static partial class Question
    {
        public interface IQuestion { }

        public class InvalidateQuestion : IQuestion
        {
            public CreateQuestionCmd Question { get; private set; }

            private InvalidateQuestion(CreateQuestionCmd question)
            {
                Question = question;
            }

            public static Result<InvalidateQuestion> Create(CreateQuestionCmd question)
            {
                if (IsQuestionValid(question))
                {
                    return new InvalidateQuestion(question);
                }
                else
                {
                    return new Result<InvalidateQuestion>(new InvalidQuestion(question));
                }
            }
            private static bool IsQuestionValid(CreateQuestionCmd question)
            {
                
                if (question.Body.Length <= 1000)
                {
                    if(question.Tags.Length >= 1 && question.Tags.Length <= 3)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public class ValidateQuestion : IQuestion
        {
            public InvalidateQuestion Question { get; private set; }


            internal ValidateQuestion(InvalidateQuestion question)
            {
                Question = question;
            }
        }

    }
}

