using System.Linq;
using System.Threading.Tasks;
using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using LanguageExt;
using Tema6.Inputs;
using Tema6.Models;
using Tema6.Outputs;
using static LanguageExt.Prelude;

namespace Tema6.Adapters
{
    public class ValidateReplyAdapter : Adapter<CreateReplyCmd, CreateReplyResult.ICreateReplyResult, QuestionWriteContext>
    {
        public override Task PostConditions(CreateReplyCmd cmd, CreateReplyResult.ICreateReplyResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }

        public override async Task<CreateReplyResult.ICreateReplyResult> Work(CreateReplyCmd cmd, QuestionWriteContext state)
        {
            var wf = from isValid in cmd.TryValidate()
                     from validateReply in ValidateReply(cmd, state)
                     select validateReply;

            return await wf.Match(
                Succ: reply => reply,
                Fail: ex => new CreateReplyResult.InvalidRequest(ex.ToString()));
        }

        private TryAsync<CreateReplyResult.ICreateReplyResult> ValidateReply(CreateReplyCmd cmd, QuestionWriteContext state)
        {
            return TryAsync<CreateReplyResult.ICreateReplyResult>(async () =>
            {
                if (!state.AuthorIds.Any(p => p == cmd.AuthorId))
                    return new CreateReplyResult.ReplyInvalid("The provided AuthorId does not exist");
                if(!state.QuestionIds.Any(p=>p == cmd.QuestionId))
                    return new CreateReplyResult.ReplyInvalid($"The provided QUestionId [{cmd.QuestionId}] does not exist");

                return new CreateReplyResult.ReplyValid(new Reply(cmd.QuestionId, cmd.Reply, cmd.AuthorId));
            });
        }
    }
}
