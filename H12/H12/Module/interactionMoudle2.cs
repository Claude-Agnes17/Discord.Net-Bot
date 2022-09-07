using Discord.Interactions;
using Discord;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using MySql.Data.MySqlClient;
using H12.Enum;
using System.Data;
using H12.Logger;

namespace H12.Module
{
    public class interactionMoudle2 : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("청소", "채팅을 청소합니다.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        public async Task CleanCommand([Summary("숫자", "청소 할 숫자를 입력해주세요(1~99까지만 입력해주세요)")]int amount)
        {
            await Context.Interaction.DeferAsync();

            if(amount <= 0 || amount >= 100)
            {
                var embed1 = new EmbedBuilder()
                    .WithTitle("실패!")
                    .WithDescription($"1~99의 숫자만 입력 해 주세요")
                    .WithColor(Color.Magenta)
                    .Build();

                EmbedBuilder embedd = new EmbedBuilder();

                embedd.WithTitle("명령어 사용로그");
                embedd.WithDescription($"청소 명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n**실패**");
                embedd.WithCurrentTimestamp();
                embedd.WithColor(Color.DarkGrey);

                await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embedd.Build());

                await ModifyOriginalResponseAsync(x => x.Embed = embed1);
                return;
            }

            var messages = await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync(); 

            var youngMessages = messages.Where(x => x.Timestamp > DateTime.Now.AddDays(-14)).Skip(1).ToList(); 
            await (Context.Channel as ITextChannel).DeleteMessagesAsync(youngMessages);

            var embed = new EmbedBuilder()
                .WithTitle("성공!")
                .WithDescription($"{youngMessages.Count}개의 메시지를 지웠습니다.")
                .WithColor(Color.Magenta)
                .Build();

            EmbedBuilder embed2 = new EmbedBuilder();
            
            embed2.WithTitle("명령어 사용로그");
            embed2.WithDescription($"청소 명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n 갯수 : {youngMessages.Count}");
            embed2.WithCurrentTimestamp();
            embed2.WithColor(Color.DarkGrey);

            await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed2.Build());

            // Because we used DeferAsync() now we should update the interaction.
            await ModifyOriginalResponseAsync(x => x.Embed = embed);
        }
    }
}
