using System;
using System.Collections.Generic;
using System.Text;

namespace Question.Domain.CreateQuestionWorkflow
{
    public class InvalidVotes : Exception
    {
        public InvalidVotes()
        {
        }

        public InvalidVotes(int votes) : base($"The number of votes is invalid")
        {
        }

    }
}
