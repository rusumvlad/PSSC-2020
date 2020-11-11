using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;
using Access.Primitives.IO;
using LanguageExt;
using Microsoft.Extensions.DependencyInjection;
using Tema6.Adapters;
using Tema6.Outputs;

namespace Tema6
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var wf = from createReplyResult in BoundedContextDSL.ValidateReply(100, 1, "123hndbfjshdfjhsgdfhjsdgfhj")
                     let validReply = (CreateReplyResult.ReplyValid)createReplyResult
                     from checkLanguageResult in BoundedContextDSL.CheckLanguage(validReply.Reply.Answer)
                     from ownerAck in BoundedContextDSL.SendAckToOwner(checkLanguageResult)
                     from authorAck in BoundedContextDSL.SendAckToAuthor(checkLanguageResult)
                     select (validReply, checkLanguageResult, ownerAck, authorAck);

            var serviceProvider = new ServiceCollection()
                .AddOperations(typeof(ValidateReplyAdapter).Assembly)
                .AddTransient<IInterpreterAsync>(sp => new LiveInterpreterAsync(sp))
                .BuildServiceProvider();

            var interpreter = serviceProvider.GetService<IInterpreterAsync>();

            var writeContext = new QuestionWriteContext(new List<int>() { 1, 2, 3 }, new List<int>() { 100, 101, 102 });
            var result = await interpreter.Interpret(wf, writeContext);


            Console.WriteLine("Hello World!");
        }

    }

    internal interface IReplyPosted
    {
    }
}
