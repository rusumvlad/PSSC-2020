
using System;
using System.Collections.Generic;
using System.Text;
using static Question.Domain.CreateQuestionWorkflow.Question;
using LanguageExt.Common;

namespace Question.Domain.CreateQuestionWorkflow
{
    public class VerifyQuestion
    {
        public Result<ValidateQuestion> VerifiedQuestion(InvalidateQuestion question)
        {
            //implement the verification for title
            return new ValidateQuestion(question);
        }
    }
}
