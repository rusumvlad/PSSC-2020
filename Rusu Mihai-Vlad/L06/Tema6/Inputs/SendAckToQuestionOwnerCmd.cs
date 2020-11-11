using System.ComponentModel.DataAnnotations;

namespace Tema6.Inputs
{
    public class SendAckToQuestionOwnerCmd
    {
        [Required]
        public int ReplyId { get; }

        [Required]
        public int QuestionId { get; }

        [Required]
        public string Answer { get; }
        public string Text { get; internal set; }

        public SendAckToQuestionOwnerCmd(int replyId, int questionId, string answer)
        {
            ReplyId = replyId;
            QuestionId = questionId;
            Answer = answer;
        }
    }
}