using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Tema6.Inputs
{
    public class SendAckToReplyAuthorCmd
    {
        [Required]
        public int ReplyId { get; }

        [Required]
        public int QuestionId { get; }

        [Required]
        public string Answer { get; }

        public SendAckToReplyAuthorCmd(int replyId, int questionId, string answer)
        {
            ReplyId = replyId;
            QuestionId = questionId;
            Answer = answer;
        }
    }
}