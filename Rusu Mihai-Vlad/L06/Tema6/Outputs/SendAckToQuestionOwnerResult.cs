using System;
using System.Collections.Generic;
using System.Text;

namespace Tema6.Outputs
{
    public static partial class SendAckToQuestionOwnerResult
    {
        public interface ISendAckToQuestionOwnerResult { };
        public class ReplyReceived : ISendAckToQuestionOwnerResult
        {
            public string Text { get; }
            public ReplyReceived(string text)
            {
                Text = text;
            }
        }
        public class InvalidReplyReceived : ISendAckToQuestionOwnerResult
        {
            public string ErrorMessage { get; }
            public InvalidReplyReceived(string errormessage)
            {
                ErrorMessage = errormessage;
            }
        }

    }
}