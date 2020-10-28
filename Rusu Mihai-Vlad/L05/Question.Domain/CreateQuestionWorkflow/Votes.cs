using CSharp.Choices;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Question.Domain.CreateQuestionWorkflow
{
    [AsChoice]
    public static partial class Votes
    {
        public interface IVotes { }
        public class InvalidNumberVotes : IVotes
        {
            public static int Votes { get; private set; }
            private InvalidNumberVotes(int votes)
            {
                Votes = votes;
            }

            public static Result<InvalidNumberVotes> Create(int votes)
            {
                if (IsVotesValid(votes))
                {
                    return new InvalidNumberVotes(votes);
                }
                else
                {
                    return new Result<InvalidNumberVotes>(new InvalidVotes(votes));
                }
            }
            private static bool IsVotesValid(int votes)
            {
                if (Votes == votes)
                {
                    return true;
                }
                return false;
            }
        }
        public class VerifiedVotes : IVotes
        {
            public InvalidNumberVotes Votes { get; private set; }
            internal VerifiedVotes(InvalidNumberVotes votes)
            {
                Votes = votes;
            }
        }
    }
}
