// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.BotBuilderSamples.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            string replyText;
            if (turnContext.Activity.Text.ToUpper() == "HI")
            {
                replyText = $"HexaBot Says: Hi";
            }
            else if (turnContext.Activity.Text.ToUpper() == "TIME")
            {
                replyText = $"HexaBot Says: " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
            }
            else
            {
                replyText = $"HexaBot Says: Oh Good ! {turnContext.Activity.Text}";
            }
            await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            string welcomeText;//= turnContext.Activity.Entities.Count.ToString();// "Vasanthakumar";// = turnContext.Activity.From.Properties["userparam"].ToString();
            welcomeText = "turnContext.Activity.From.Id==>" + turnContext.Activity.From.Id + " ; turnContext.Activity.Recipient.Id==>" + turnContext.Activity.Recipient.Id + "membersAdded[1]==>" + membersAdded[1].Id;
            if (turnContext.Activity.Type == ActivityTypes.ConversationUpdate && !(string.IsNullOrEmpty(welcomeText)))
            {
                welcomeText = "Hello " + welcomeText;
            }
            else
            {
                welcomeText = "Hello and Welcome!";
            }
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}
