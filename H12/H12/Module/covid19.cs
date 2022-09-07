using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using H12.Enum;
using Newtonsoft.Json.Linq;

namespace H12.Module
{
    public class covid19 : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("코로나현황", "코로나통계를 보여줍니다")]
        public async Task COVID19Command([Summary("지역", "현황을 보고싶은 지역을 선택하여주세요")]COVIDEnum region)
        {
            string apikey = "tTmaNgOR7CsKrPDl98kucj4GHAQwpSBbM";
            try
            {
                string url = "https://api.corona-19.kr/korea/beta/?serviceKey=" + apikey;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string text = reader.ReadToEnd();

                JObject obj = JObject.Parse(text);

                EmbedBuilder embed = new EmbedBuilder();
                EmbedBuilder embed1 = new EmbedBuilder();

                string updatetime = obj["API"]["updateTime"].ToString();

                if (region == COVIDEnum.통합)
                {
                    string totalCnt = obj["korea"]["totalCnt"].ToString();
                    string deathCnt = obj["korea"]["deathCnt"].ToString();
                    string incDec = obj["korea"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"통합", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : 통합\n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (region == COVIDEnum.서울)
                {
                    string totalCnt = obj["seoul"]["totalCnt"].ToString();
                    string deathCnt = obj["seoul"]["deathCnt"].ToString();
                    string incDec = obj["seoul"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"서울", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : 서울\n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (region == COVIDEnum.부산)
                {
                    string totalCnt = obj["busan"]["totalCnt"].ToString();
                    string deathCnt = obj["busan"]["deathCnt"].ToString();
                    string incDec = obj["busan"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"부산", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : 부산\n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (region == COVIDEnum.대구)
                {
                    string totalCnt = obj["daegu"]["totalCnt"].ToString();
                    string deathCnt = obj["daegu"]["deathCnt"].ToString();
                    string incDec = obj["daegu"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"대구", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : 대구\n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (region == COVIDEnum.인천)
                {
                    string totalCnt = obj["incheon"]["totalCnt"].ToString();
                    string deathCnt = obj["incheon"]["deathCnt"].ToString();
                    string incDec = obj["incheon"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"인천", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : 인천\n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (region == COVIDEnum.광주)
                {
                    string totalCnt = obj["gwangju"]["totalCnt"].ToString();
                    string deathCnt = obj["gwangju"]["deathCnt"].ToString();
                    string incDec = obj["gwangju"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"광주", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : 광주\n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (region == COVIDEnum.대전)
                {
                    string totalCnt = obj["daejeon"]["totalCnt"].ToString();
                    string deathCnt = obj["daejeon"]["deathCnt"].ToString();
                    string incDec = obj["daejeon"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"대전", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : 대전\n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (region == COVIDEnum.울산)
                {
                    string totalCnt = obj["ulsan"]["totalCnt"].ToString();
                    string deathCnt = obj["ulsan"]["deathCnt"].ToString();
                    string incDec = obj["ulsan"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"울산", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : 울산\n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (region == COVIDEnum.세종)
                {
                    string totalCnt = obj["sejong"]["totalCnt"].ToString();
                    string deathCnt = obj["sejong"]["deathCnt"].ToString();
                    string incDec = obj["sejong"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"세종", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : 세종\n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (region == COVIDEnum.경기)
                {
                    string totalCnt = obj["gyeonggi"]["totalCnt"].ToString();
                    string deathCnt = obj["gyeonggi"]["deathCnt"].ToString();
                    string incDec = obj["gyeonggi"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"경기", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : 경기\n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (region == COVIDEnum.강원)
                {
                    string totalCnt = obj["gangwon"]["totalCnt"].ToString();
                    string deathCnt = obj["gangwon"]["deathCnt"].ToString();
                    string incDec = obj["gangwon"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"강원", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : 강원\n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (region == COVIDEnum.충북)
                {
                    string totalCnt = obj["chungbuk"]["totalCnt"].ToString();
                    string deathCnt = obj["chungbuk"]["deathCnt"].ToString();
                    string incDec = obj["chungbuk"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"충북", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : 충북\n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (region == COVIDEnum.충남)
                {
                    string totalCnt = obj["chungnam"]["totalCnt"].ToString();
                    string deathCnt = obj["chungnam"]["deathCnt"].ToString();
                    string incDec = obj["chungnam"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"충남", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : 충남\n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (region == COVIDEnum.전북)
                {
                    string totalCnt = obj["jeonbuk"]["totalCnt"].ToString();
                    string deathCnt = obj["jeonbuk"]["deathCnt"].ToString();
                    string incDec = obj["jeonbuk"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"전북", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : 전북\n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (region == COVIDEnum.전남)
                {
                    string totalCnt = obj["jeonnam"]["totalCnt"].ToString();
                    string deathCnt = obj["jeonnam"]["deathCnt"].ToString();
                    string incDec = obj["jeonnam"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"전남", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : 전남\n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (region == COVIDEnum.경북)
                {
                    string totalCnt = obj["gyeongbuk"]["totalCnt"].ToString();
                    string deathCnt = obj["gyeongbuk"]["deathCnt"].ToString();
                    string incDec = obj["gyeongbuk"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"경북", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : 경북\n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (region == COVIDEnum.경남)
                {
                    string totalCnt = obj["gyeongnam"]["totalCnt"].ToString();
                    string deathCnt = obj["gyeongnam"]["deathCnt"].ToString();
                    string incDec = obj["gyeongnam"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"경남", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : 경남\n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
                else if (region == COVIDEnum.제주)
                {
                    string totalCnt = obj["jeju"]["totalCnt"].ToString();
                    string deathCnt = obj["jeju"]["deathCnt"].ToString();
                    string incDec = obj["jeju"]["incDec"].ToString();

                    embed.WithTitle($"{updatetime}");
                    embed.AddField($"제주", $"확진자 수 : {totalCnt}\n사망자 수 : {deathCnt}\n일일 확진자 : {incDec}");
                    embed.WithCurrentTimestamp();
                    embed.WithColor(Color.Magenta);

                    await RespondAsync(embed: embed.Build());

                    embed1.WithTitle("명령어 사용로그");
                    embed1.WithDescription($"{updatetime} \n사용자 : {Context.User.Mention}\n사용채널 : {Context.Channel} \n지역 : \n");
                    embed1.WithCurrentTimestamp();
                    embed1.WithColor(Color.DarkGrey);

                    await Context.Guild.GetTextChannel(1011534133996036186).SendMessageAsync("", false, embed1.Build());
                }
            }
            catch
            {
                await RespondAsync("error");
            }
        }
    }
}
