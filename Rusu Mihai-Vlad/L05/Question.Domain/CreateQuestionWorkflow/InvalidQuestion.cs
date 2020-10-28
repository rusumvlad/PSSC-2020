using System;
using System.Collections.Generic;
using System.Text;

namespace Question.Domain.CreateQuestionWorkflow
{
    public class InvalidQuestion : Exception
    {
        public InvalidQuestion()
        {
        }

        public InvalidQuestion(CreateQuestionCmd question) : base($"You reached the maximum length of 1000 characters in body or you have more than 3 tags")
        {
        }

    }
}
