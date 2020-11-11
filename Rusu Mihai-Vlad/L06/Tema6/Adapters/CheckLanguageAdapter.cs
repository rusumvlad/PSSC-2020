using System.Threading.Tasks;
using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using LanguageExt;
using Tema6.Inputs;
using Tema6.Outputs;
using static LanguageExt.Prelude;

namespace Tema6.Adapters
{
    class CheckLanguageAdapter : Adapter<CheckLanguageCmd, CheckLanguageResult.ICheckLanguageResult, QuestionWriteContext>
    {
        public override Task PostConditions(CheckLanguageCmd cmd, CheckLanguageResult.ICheckLanguageResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }
        
        public override async Task<CheckLanguageResult.ICheckLanguageResult> Work(CheckLanguageCmd cmd, QuestionWriteContext state)
        {
            var wf = from isValid in cmd.TryValidate()
                     from checkLanguageResult in CheckLanguage(cmd, state)
                     select checkLanguageResult;
            return await wf.Match(
                  Succ: checklanguage => checklanguage,
                  Fail: ex => new CheckLanguageResult.CheckFailed(ex.ToString()));
        }

        private TryAsync<CheckLanguageResult.ICheckLanguageResult> CheckLanguage(CheckLanguageCmd cmd, QuestionWriteContext state)
        {

            return TryAsync<CheckLanguageResult.ICheckLanguageResult>(async () =>
            {
                return new CheckLanguageResult.TextChecked(50);
            });
        }
    }
}