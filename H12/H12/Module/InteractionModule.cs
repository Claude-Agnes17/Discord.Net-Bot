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
    public class InteractionModule : InteractionModuleBase<SocketInteractionContext>
    {
        

        [SlashCommand("ping", "Receivce a ping message!")]
        public async Task HandlePingCommand()
        {
            EmbedBuilder embed = new EmbedBuilder();
            
            embed.WithTitle("PING");
            embed.WithDescription($"{Context.Client.Latency}ms");
            embed.WithAuthor(Context.Client.CurrentUser);
            embed.WithCurrentTimestamp();
            embed.WithColor(Color.Magenta);
            await RespondAsync(embed: embed.Build());

            EmbedBuilder embed1 = new EmbedBuilder();

            embed1.WithTitle("명령어 사용로그");
            embed1.WithDescription($"PING명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel}");
            embed1.WithCurrentTimestamp();
            embed.WithColor(Color.DarkGrey);

            await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
        }
        [SlashCommand("추방", "Kick select user")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task HandleKickCommand([Summary("유저","추방 하고 싶은 유저를 선택해주세요")]IGuildUser user, [Summary("사유", "추방 하는 이유를 적어주세요")]string reason = "No reason provided")
        {
            EmbedBuilder embed = new EmbedBuilder();

            embed.WithTitle("추방");
            embed.AddField($"{user}", $"사유 : { reason }", false);   
            embed.WithAuthor(Context.Client.CurrentUser);
            embed.WithCurrentTimestamp();
            embed.WithColor(Color.Magenta);

            await user.KickAsync();
            await RespondAsync(embed: embed.Build(), ephemeral: true);

            EmbedBuilder embed1 = new EmbedBuilder();

            embed1.WithTitle("명령어 사용로그");
            embed1.WithDescription($"추방명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n추방유저 : {user}\n사유 : {reason}");
            embed1.WithCurrentTimestamp();
            embed.WithColor(Color.DarkGrey);

            await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
        }
        [SlashCommand("밴", "ban select user")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task HandleBanCommand([Summary("유저", "밴 하고 싶은 유저를 선택해주세요")]IGuildUser user, [Summary("사유", "밴 하는 이유를 적어주세요")]string reason = "No reason provided")
        {
            EmbedBuilder embed = new EmbedBuilder();

            embed.WithTitle("밴");
            embed.AddField($"{user}", $"사유 : {reason}", false);
            embed.WithAuthor(Context.Client.CurrentUser);
            embed.WithCurrentTimestamp();
            embed.WithColor(Color.Magenta);

            await user.Guild.AddBanAsync(user, reason: reason);
            await RespondAsync(embed: embed.Build(), ephemeral: true);

            EmbedBuilder embed1 = new EmbedBuilder();

            embed1.WithTitle("명령어 사용로그");
            embed1.WithDescription($"밴명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel}\n차단유저 : {user}\n사유 : {reason}");
            embed1.WithCurrentTimestamp();
            embed.WithColor(Color.DarkGrey);

            await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
        }
        [SlashCommand("channel", "Send Channel message")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task HandleChannelCommand(IMessageChannel channel,string msg)
        {
            try
            {
                await channel.SendMessageAsync(msg);

                await RespondAsync("전송성공", ephemeral: true);
            }
            catch
            {
                await RespondAsync("전송 실패", ephemeral: true);
            }

            EmbedBuilder embed1 = new EmbedBuilder();

            embed1.WithTitle("명령어 사용로그");
            embed1.WithDescription($"channel명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n채널 : {channel}\n메시지 : {msg}");
            embed1.WithCurrentTimestamp();
            embed1.WithColor(Color.DarkGrey);

            await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
        }
        [SlashCommand("유저정보", "view your account info")] 
        public async Task HandlemyinfoCommand([Summary("유저", "정보를 확인하고 싶은 유저를 선택해주세요")]SocketUser user)
        {

            EmbedBuilder embed = new EmbedBuilder();

            embed.WithTitle($"{user}님의 정보");
            embed.AddField("계정생성", user.CreatedAt);
            embed.AddField("유저이름", user.Username + "#" + user.Discriminator);   
            embed.WithCurrentTimestamp();
            embed.WithColor(Color.Magenta);
            embed.ThumbnailUrl = user.GetAvatarUrl();

            await RespondAsync(embed: embed.Build());

            EmbedBuilder embed1 = new EmbedBuilder();

            embed1.WithTitle("명령어 사용로그");
            embed1.WithDescription($"유저정보명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n검색유저 : {user}");
            embed1.WithCurrentTimestamp();
            embed.WithColor(Color.DarkGrey);

            await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
        }
        [SlashCommand("dm", "send user dm")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task HandleDmCommand(IGuildUser user, string msg)
        {
            await user.SendMessageAsync(msg);
            await RespondAsync("전송완료", ephemeral: true);

            EmbedBuilder embed1 = new EmbedBuilder();

            embed1.WithTitle("명령어 사용로그");
            embed1.WithDescription($"DM명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \nDM유저 : {user}\n내용 : {msg}");
            embed1.WithCurrentTimestamp();
            embed1.WithColor(Color.DarkGrey);

            await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
        }
        [SlashCommand("프로필", "view user avatar")]
        public async Task ViewAvatarCommand([Summary("유저", "프로필사진을 보고 싶은 유저를 선택해주세요")]IGuildUser user)
        {
            var subject = user.GetAvatarUrl();

            EmbedBuilder embed = new EmbedBuilder();

            embed.WithTitle($"{user}님의 프로필사진");
            embed.WithColor(Color.Magenta);
            embed.WithImageUrl(subject);

            await RespondAsync(embed: embed.Build());

            EmbedBuilder embed1 = new EmbedBuilder();

            embed1.WithTitle("명령어 사용로그");
            embed1.WithDescription($"프로필명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n유저 : {user}");
            embed1.WithCurrentTimestamp();
            embed1.WithColor(Color.DarkGrey);

            await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
        }
 

        [SlashCommand("급식검색", "초,중,고등학교 급식 검색")]
        public async Task Serchschoolfood([Summary("학교이름","학교이름을 적어주세요")]string schoolname, [Summary("시간", "아침, 점심, 저녁중 보고 싶은 시간대를 골라주세요")]FoodEnum time)
        {
            try
            {
                string url = "https://scmeal.ml/" + schoolname;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string text = reader.ReadToEnd();

                JObject obj = JObject.Parse(text);

                string breakfast = obj["breakfast"].ToString();
                string lunch = obj["lunch"].ToString();
                string dinner = obj["dinner"].ToString();
                string[] remove = new string[] { "[", "]", ",", "\"","*" };

                foreach (var c in remove)
                {
                    breakfast = breakfast.Replace(c, string.Empty);
                    lunch = lunch.Replace(c, string.Empty);
                    dinner = dinner.Replace(c, string.Empty);
                }
                EmbedBuilder embed = new EmbedBuilder();
                EmbedBuilder embed1 = new EmbedBuilder();

                if (time == FoodEnum.아침)
                {
                    if (breakfast == null)
                    {
                        embed.WithTitle($"{schoolname} 아침밥");
                        embed.WithDescription($"{DateTime.Now.ToString("yyyy-MM-dd")}");
                        embed.AddField("아침", $"밥이 없습니다");
                        embed.WithCurrentTimestamp();
                        embed.WithColor(Color.Magenta);

                        await RespondAsync(embed: embed.Build());

                        embed1.WithTitle("명령어 사용로그");
                        embed1.WithDescription($"급식검색명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n학교 : {schoolname}\n아침");
                        embed1.WithCurrentTimestamp();
                        embed1.WithColor(Color.DarkGrey);

                        await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                    }

                    embed.WithTitle($"{schoolname} 아침밥");
                    embed.WithDescription($"{DateTime.Now.ToString("yyyy-MM-dd")}");
                    embed.AddField("아침", $"{breakfast}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"급식검색명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n학교 : {schoolname}\n아침");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (time == FoodEnum.점심)
                {
                    if (lunch == null)
                    {
                        embed.WithTitle($"{schoolname} 점심밥");
                        embed.WithDescription($"{DateTime.Now.ToString("yyyy-MM-dd")}");
                        embed.AddField("점심", $"밥이 없습니다");
                        embed.WithCurrentTimestamp();
                        embed.WithColor(Color.Magenta);

                        await RespondAsync(embed: embed.Build());

                        await RespondAsync(embed: embed.Build());

                        embed1.WithTitle("명령어 사용로그");
                        embed1.WithDescription($"급식검색명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n학교 : {schoolname}\n점심");
                        embed1.WithCurrentTimestamp();
                        embed1.WithColor(Color.DarkGrey);

                        await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                    }

                    embed.WithTitle($"{schoolname} 점심밥");
                    embed.WithDescription($"{DateTime.Now.ToString("yyyy-MM-dd")}");
                    embed.AddField("점심", $"{lunch}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"급식검색명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n학교 : {schoolname}\n점심");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else
                {
                    if (dinner == null)
                    {
                        embed.WithTitle($"{schoolname} 저녁밥");
                        embed.WithDescription($"{DateTime.Now.ToString("yyyy-MM-dd")}");
                        embed.AddField("저녁", $"밥이 없습니다");
                        embed.WithCurrentTimestamp();
                        embed.WithColor(Color.Magenta);

                        await RespondAsync(embed: embed.Build());

                        embed1.WithTitle("명령어 사용로그");
                        embed1.WithDescription($"급식검색명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n학교 : {schoolname}\n저녁");
                        embed1.WithCurrentTimestamp();
                        embed1.WithColor(Color.DarkGrey);

                        await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                    }

                    embed.WithTitle($"{schoolname} 저녁밥");
                    embed.WithDescription($"{DateTime.Now.ToString("yyyy-MM-dd")}");
                    embed.AddField("저녁", $"{dinner}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"급식검색명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n학교 : {schoolname}\n저녁");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                      await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
            }
            catch
            {
                EmbedBuilder embed = new EmbedBuilder();
                EmbedBuilder embed1 = new EmbedBuilder();

                embed.WithTitle("에러");
                embed.WithDescription("검색된 학교가 없습니다. 다시 검색하여주세요");
                embed.WithCurrentTimestamp();
                embed.WithColor(Color.DarkGrey);

                await RespondAsync(embed: embed.Build());

                embed1.WithTitle("명령어 사용로그");
                embed1.WithDescription($"급식검색명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n학교 : {schoolname}\n검색실패");
                embed1.WithCurrentTimestamp();
                embed1.WithColor(Color.DarkGrey);

                await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
            }
        }

        [SlashCommand("타임아웃", "타임아웃 지급")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.Administrator)]
        public async Task SetTimeout([Summary("유저","타임아웃을 하고자 하는 유저를 선택해주세요")]IGuildUser user, [Summary("기간", "타임아웃 기간을 정해주세요")]TimeEnum time)
        {
            EmbedBuilder embed = new EmbedBuilder();

            if(time == TimeEnum.오분)
            {
                TimeSpan span = TimeSpan.FromMinutes(5);

                await user.SetTimeOutAsync(span);

                embed.WithTitle("타임아웃");
                embed.AddField($"{user}", $"기간 : {time}", false);
                embed.WithAuthor(Context.Client.CurrentUser);
                embed.WithCurrentTimestamp();
                embed.WithColor(Color.Magenta);

                await RespondAsync(embed: embed.Build());

                EmbedBuilder embed1 = new EmbedBuilder();

                embed1.WithTitle("명령어 사용로그");
                embed1.WithDescription($"타임아웃명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n유저 : {user}\n기간 : {time}");
                embed1.WithCurrentTimestamp();
                embed1.WithColor(Color.DarkGrey);

                await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
            }
            else if (time == TimeEnum.십분)
            {
                TimeSpan span = TimeSpan.FromMinutes(10);

                await user.SetTimeOutAsync(span);

                embed.WithTitle("타임아웃");
                embed.AddField($"{user}", $"기간 : {time}", false);
                embed.WithAuthor(Context.Client.CurrentUser);
                embed.WithCurrentTimestamp();
                embed.WithColor(Color.Magenta);

                await RespondAsync(embed: embed.Build());

                EmbedBuilder embed1 = new EmbedBuilder();

                embed1.WithTitle("명령어 사용로그");
                embed1.WithDescription($"타임아웃명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n유저 : {user}\n기간 : {time}");
                embed1.WithCurrentTimestamp();
                embed1.WithColor(Color.DarkGrey);

                await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
            }
            else if (time == TimeEnum.삼십분)
            {
                TimeSpan span = TimeSpan.FromMinutes(30);

                await user.SetTimeOutAsync(span);

                embed.WithTitle("타임아웃");
                embed.AddField($"{user}", $"기간 : {time}", false);
                embed.WithAuthor(Context.Client.CurrentUser);
                embed.WithCurrentTimestamp();
                embed.WithColor(Color.Magenta);

                await RespondAsync(embed: embed.Build());

                EmbedBuilder embed1 = new EmbedBuilder();

                embed1.WithTitle("명령어 사용로그");
                embed1.WithDescription($"타임아웃명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n유저 : {user}\n기간 : {time}");
                embed1.WithCurrentTimestamp();
                embed1.WithColor(Color.DarkGrey);

                await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
            }
            else if (time == TimeEnum.한시간)
            {
                TimeSpan span = TimeSpan.FromHours(1);

                await user.SetTimeOutAsync(span);

                embed.WithTitle("타임아웃");
                embed.AddField($"{user}", $"기간 : {time}", false);
                embed.WithAuthor(Context.Client.CurrentUser);
                embed.WithCurrentTimestamp();
                embed.WithColor(Color.Magenta);

                await RespondAsync(embed: embed.Build());

                EmbedBuilder embed1 = new EmbedBuilder();

                embed1.WithTitle("명령어 사용로그");
                embed1.WithDescription($"타임아웃명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n유저 : {user}\n기간 : {time}");
                embed1.WithCurrentTimestamp();
                embed1.WithColor(Color.DarkGrey);

                await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
            }
            else if (time == TimeEnum.두시간)
            {
                TimeSpan span = TimeSpan.FromHours(2);

                await user.SetTimeOutAsync(span);

                embed.WithTitle("타임아웃");
                embed.AddField($"{user}", $"기간 : {time}", false);
                embed.WithAuthor(Context.Client.CurrentUser);
                embed.WithCurrentTimestamp();
                embed.WithColor(Color.Magenta);

                await RespondAsync(embed: embed.Build());

                EmbedBuilder embed1 = new EmbedBuilder();

                embed1.WithTitle("명령어 사용로그");
                embed1.WithDescription($"타임아웃명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n유저 : {user}\n기간 : {time}");
                embed1.WithCurrentTimestamp();
                embed1.WithColor(Color.DarkGrey);

                await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
            }
            else if (time == TimeEnum.다섯시간)
            {
                TimeSpan span = TimeSpan.FromHours(5);

                await user.SetTimeOutAsync(span);

                embed.WithTitle("타임아웃");
                embed.AddField($"{user}", $"기간 : {time}", false);
                embed.WithAuthor(Context.Client.CurrentUser);
                embed.WithCurrentTimestamp();
                embed.WithColor(Color.Magenta);

                await RespondAsync(embed: embed.Build());

                EmbedBuilder embed1 = new EmbedBuilder();

                embed1.WithTitle("명령어 사용로그");
                embed1.WithDescription($"타임아웃명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n유저 : {user}\n기간 : {time}");
                embed1.WithCurrentTimestamp();
                embed1.WithColor(Color.DarkGrey);

                await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
            }
            else if (time == TimeEnum.하루)
            {
                TimeSpan span = TimeSpan.FromDays(1);

                await user.SetTimeOutAsync(span);

                embed.WithTitle("타임아웃");
                embed.AddField($"{user}", $"기간 : {time}", false);
                embed.WithAuthor(Context.Client.CurrentUser);
                embed.WithCurrentTimestamp();
                embed.WithColor(Color.Magenta);

                await RespondAsync(embed: embed.Build());

                EmbedBuilder embed1 = new EmbedBuilder();

                embed1.WithTitle("명령어 사용로그");
                embed1.WithDescription($"타임아웃명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n유저 : {user}\n기간 : {time}");
                embed1.WithCurrentTimestamp();
                embed1.WithColor(Color.DarkGrey);

                await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
            }
            else if (time == TimeEnum.일주일)
            {
                TimeSpan span = TimeSpan.FromDays(7);

                await user.SetTimeOutAsync(span);

                embed.WithTitle("타임아웃");
                embed.AddField($"{user}", $"기간 : {time}", false);
                embed.WithAuthor(Context.Client.CurrentUser);
                embed.WithCurrentTimestamp();
                embed.WithColor(Color.Magenta);

                await RespondAsync(embed: embed.Build());

                EmbedBuilder embed1 = new EmbedBuilder();

                embed1.WithTitle("명령어 사용로그");
                embed1.WithDescription($"타임아웃명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n유저 : {user}\n기간 : {time}");
                embed1.WithCurrentTimestamp();
                embed1.WithColor(Color.DarkGrey);

                await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
            }
        }
        [SlashCommand("타임아웃해제", "타임아웃해제")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.Administrator)]
        public async Task CansleTimeoutCommand([Summary("유저", "타임아웃을 해제하고자 하는 유저를 골라주세요")]IGuildUser user)
        {
            EmbedBuilder embed = new EmbedBuilder();

            try
            {
                await user.RemoveTimeOutAsync();

                embed.WithTitle("타임아웃 해제");
                embed.WithDescription($"{user}님의 타임아웃을 해제 하였습니다");
                embed.WithAuthor(Context.Client.CurrentUser);
                embed.WithCurrentTimestamp();
                embed.WithColor(Color.Magenta);

                await RespondAsync(embed: embed.Build());

                EmbedBuilder embed1 = new EmbedBuilder();

                embed1.WithTitle("명령어 사용로그");
                embed1.WithDescription($"타임아웃해제 명령어 \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n유저 : {user}");
                embed1.WithCurrentTimestamp();
                embed1.WithColor(Color.DarkGrey);

                await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
            }
            catch
            {
                await RespondAsync("해제 실패");
            }
        }


        [SlashCommand("신고", "Demonstrate buttons and select menus.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.Administrator)]
        public async Task HandleComponentsCommand()
        {
            var button = new ButtonBuilder()
            {
                Label = "신고하기",
                CustomId = "button",
                Style = ButtonStyle.Primary
            };

            var component = new ComponentBuilder();
            component.WithButton(button);

            EmbedBuilder embed = new EmbedBuilder();

            embed.WithTitle("버튼을 누르면 익명으로 신고를 할 수 있습니다");
            embed.WithDescription("장난으로 보내다가 적발시 서버 영구차단.");

            await RespondAsync(embed: embed.Build(), components: component.Build());
        }
        
        [ComponentInteraction("button")]
        public async Task HandleButtonInput()
        {
            await RespondWithModalAsync<DemoModal>("demo_modal");
        }

        [ModalInteraction("demo_modal")]
        public async Task HandleModalInput(DemoModal modal)
        {

            string input = modal.Greeting;


            await Context.Guild.GetTextChannel(1011237092132655134).SendMessageAsync("**!!신고가 들어왔습니다!!**\n" + input + "\n신고자 : " + Context.User.Mention);

            await RespondAsync("신고했습니다.", ephemeral: true);
/*            bool valid = Regex.IsMatch(input, @"^([0-9a-zA-Z]+)@([0-9a-zA-Z]+)(\.[0-9a-zA-Z]+){1,}$");

            if (valid)
            {
                await RespondAsync(input);
            }
            else
            {
                await RespondAsync("Please keep the format of the Email format");
            }*/
        }
    }

    public class DemoModal : IModal
    {
        public string Title => "신고하기";
        [InputLabel("내용")]
        [ModalTextInput("greeting_input", TextInputStyle.Short, placeholder: "내용을 입력해주세요", maxLength:100)]
        public string Greeting { get; set; }
    }
}







/*[SlashCommand("급식", "경소고 급식")]
public async Task HandleFoodCommand(FoodEnum time)
{
    try
    {
        string url = "https://scmeal.ml/%EA%B2%BD%EB%B6%81%EC%86%8C%ED%94%84%ED%8A%B8%EC%9B%A8%EC%96%B4%EA%B3%A0%EB%93%B1%ED%95%99%EA%B5%90";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        Stream stream = response.GetResponseStream();
        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
        string text = reader.ReadToEnd();

        JObject obj = JObject.Parse(text);

        string breakfast = obj["breakfast"].ToString();
        string lunch = obj["lunch"].ToString();
        string dinner = obj["dinner"].ToString();
        string[] remove = new string[] { "[", "]", ",", "\"" };
        foreach (var c in remove)
        {
            breakfast = breakfast.Replace(c, string.Empty);
            lunch = lunch.Replace(c, string.Empty);
            dinner = dinner.Replace(c, string.Empty);
        }
        EmbedBuilder embed = new EmbedBuilder();
        if (time == FoodEnum.아침)
        {
            embed.WithTitle($"경소고 아침밥");
            embed.WithDescription($"{DateTime.Now.ToString("yyyy-MM-dd")}");
            embed.AddField("아침", $"{breakfast}");
            embed.WithCurrentTimestamp();
            embed.WithColor(Color.Magenta);

            await RespondAsync(embed: embed.Build());
        }
        else if (time == FoodEnum.점심)
        {
            embed.WithTitle($"경소고 점심밥");
            embed.WithDescription($"{DateTime.Now.ToString("yyyy-MM-dd")}");
            embed.AddField("점심", $"{lunch}");
            embed.WithCurrentTimestamp();
            embed.WithColor(Color.Magenta);

            await RespondAsync(embed: embed.Build());
        }
        else
        {
            embed.WithTitle($"경소고 저녁밥");
            embed.WithDescription($"{DateTime.Now.ToString("yyyy-MM-dd")}");
            embed.AddField("저녁", $"{dinner}");
            embed.WithCurrentTimestamp();
            embed.WithColor(Color.Magenta);

            await RespondAsync(embed: embed.Build());
        }
    }
    catch
    {
        await RespondAsync("가져오기 실패");
    }
}*/