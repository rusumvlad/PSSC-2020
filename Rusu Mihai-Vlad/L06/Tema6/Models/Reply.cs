using System;
using System.Collections.Generic;
using System.Text;

namespace Tema6.Models
{
    public class Question
    {
        public string Title { get; }
        public string Body { get; }
        public string[] Tags { get; }

        public Question(string title, string body, string[] tags)
        {
            Title = title;
            Body = body;
            Tags = tags;
        }
    }

    public class Reply
    {
        public int QuestionId { get; }
        public string Answer { get; }
        public int AuthorId { get; }

        public Reply(int questionId, string answer, int authorId)
        {
            QuestionId = questionId;
            Answer = answer;
            AuthorId = authorId;
        }
    }
}
