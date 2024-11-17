using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
namespace DiscordBot
{
    class Program
    {
        private static DiscordSocketClient client= new DiscordSocketClient(
            new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });
        // Main entry point
        public static async Task Main(string[] args)
        {
            // Register events
            client.Log += LogAsync;
            client.MessageReceived += MessageReceivedAsync;

            // Retrieve the bot token securely
            var token = "MTMwNjk3NzQ0NjUwNzA1NzE2Mw.GhrfJS.zQGBL3ZuH2tSDUG8beMPLSuO-6XwldEWMhIEno";
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Error: Bot token is not set. Please configure the DISCORD_BOT_TOKEN environment variable.");
                return;
            }

            // Log in and start the bot
            await client.LoginAsync(TokenType.Bot, token);  
            await client.StartAsync();

            Console.WriteLine("Bot is running. Press Ctrl+C to exit.");

            // Keep the application running
            await Task.Delay(Timeout.Infinite);
        }

        // Handle log messages
        private static Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        // Handle incoming messages
        private static async Task MessageReceivedAsync(SocketMessage message)
        {
            // Ignore bot messages
            if (message.Author.IsBot) return;

            // Example command: "!ping"
            if (message.Content.Equals("!ping", StringComparison.OrdinalIgnoreCase))
            {
                await message.Channel.SendMessageAsync("Pong!");
            }
        }
    }
}
