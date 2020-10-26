using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Question.Domain.CreateQuestionWorkflow
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult
        {
        }

        public class QuestionCreated : ICreateQuestionResult
        {
            public Guid QuestionId { get; private set; }
            public string Title { get; private set; }

            public QuestionCreated(Guid questionId, string title)
            {
                QuestionId = questionId;
                Title = title;
            }

        }

        public class QuestionNotCreated : ICreateQuestionResult
        {
            public string Reason { get; set; }

            public QuestionNotCreated(string reason)
            {
                Reason = reason;
            }
        }

        public class QuestionValidationFailed : ICreateQuestionResult
        {
            public IEnumerable<string> ValidationErrors { get; private set; }

            public QuestionValidationFailed(IEnumerable<string> errors)
            {
                ValidationErrors = errors.AsEnumerable();
            }
        }
    }
}
