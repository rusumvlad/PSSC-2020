using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Question.Domain.CreateQuestionWorkflow
{
    public struct CreateQuestionCmd
    {
        [Required]
        public string Title { get; private set; }
        [Required]
        public string Body { get; private set; }
        [Required]
        public string Tags { get; private set; }

        public CreateQuestionCmd(string title, string body, string tags)
        {
            Title = title;
            Body = body;
            Tags = tags;
        }
    }
}
