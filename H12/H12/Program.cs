using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration.Yaml;
using Discord.Commands;
using Discord.Interactions;
using H12.Module;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using Lavalink4NET;
using Lavalink4NET.DiscordNet;
using H12.Logger;

namespace H12
{
    public class Program
    {
        private static System.Timers.Timer aTimer;
        private DiscordSocketClient _client;
        static void Main(string[] args) 
        {
            new Program().MainAsync().GetAwaiter().GetResult();

        }

        public async Task MainAsync()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddYamlFile("config.yml")
                .Build();

            using IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
            services
            .AddSingleton(config)
            .AddSingleton(x => new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = Discord.GatewayIntents.AllUnprivileged,
                LogGatewayIntentWarnings = false,
                UseInteractionSnowflakeDate = false,
                AlwaysDownloadUsers = true,
                LogLevel = LogSeverity.Debug
            }))
            .AddTransient<ConsoleLogger>()
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
            .AddSingleton<InteractionHandler>()
            .AddSingleton(x => new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Debug,
                DefaultRunMode = Discord.Commands.RunMode.Async
            }))
            .AddSingleton<PrefixHandler>())
            .Build();

            await RunAsync(host);
        }
        
        public async Task RunAsync(IHost host)
        {

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var commands = provider.GetRequiredService<InteractionService>();
            _client = provider.GetRequiredService<DiscordSocketClient>();
            var config = provider.GetRequiredService<IConfigurationRoot>();

            await provider.GetRequiredService<InteractionHandler>().InitializeAsync();

            var prefixCommands = provider.GetRequiredService<PrefixHandler>();
            prefixCommands.AddModule<Module.PrefixModule>();
            await prefixCommands.InitializeAsync();

            _client.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);
            commands.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);

            _client.Ready += async () =>
            {
                await commands.RegisterCommandsToGuildAsync(UInt64.Parse(config["testGuild"]), true);
                //await commands.RegisterCommandsGloballyAsync(true);
            };


            await _client.LoginAsync(TokenType.Bot, config["tokens:discord"]);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
    }
}


























/*var myTask = Task.Run(async () =>
{
    string url = "https://game.dr-score.com/api/powerball/getsect?gamecount=288";

    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

    Stream stream = response.GetResponseStream();
    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
    string text = reader.ReadToEnd();
    while (true)
    {
        Thread.Sleep(1000);

        string url2 = "https://game.dr-score.com/api/powerball/getsect?gamecount=288";
        HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(url2);
        HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();

        Stream stream2 = response2.GetResponseStream();
        StreamReader reader2 = new StreamReader(stream2, Encoding.UTF8);
        string text2 = reader2.ReadToEnd();
        if (text != text2)
        {
            JObject obj = JObject.Parse(text);

            string dayround = obj["data"]["datas"][0]["DAYROUND"].ToString();
            string resultnum = obj["data"]["datas"][0]["RESULTNUM"].ToString();
            string pb = obj["data"]["datas"][0]["PB"].ToString();

            EmbedBuilder embed = new EmbedBuilder();

            embed.WithTitle($"{dayround}회차 결과");
            embed.WithDescription($"숫자 {resultnum} + '{pb}'");
            embed.WithCurrentTimestamp();
            embed.WithColor(Discord.Color.Magenta);

            ((ITextChannel)_client.GetChannel(984617103267078235)).SendMessageAsync("", false, embed.Build());
            text = text2;
        }
        else
        {
            continue;
        }
    }
});*/