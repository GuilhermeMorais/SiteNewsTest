﻿using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HackerNews.Managers;
using Microsoft.Extensions.Configuration;

namespace HackerNews
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            if (args.Length != 2
                || !args[0].Equals("--posts", StringComparison.InvariantCultureIgnoreCase)
                || !int.TryParse(args[1], out var qtdPosts))
            {
                Console.WriteLine("Invalid parameters, should be: --posts N (where N is a number between 1 and 100)");
                return (int)ExitCodeEnum.InvalidParameters;
            }

            if (qtdPosts < 1 || qtdPosts > 100)
            {
                Console.WriteLine("The quantity of posts should be between 1 and 100");
                return (int)ExitCodeEnum.InvalidParameters;
            }

            var configurations = BuildSettingsConfigurations();
            var manager = new NewsManager(configurations);
            var result = await manager.GetTopStoriesAsync(qtdPosts);

            if (result != null && result.Any())
            {
                var str = JsonSerializer.Serialize(result, new JsonSerializerOptions {WriteIndented = true});
                Console.WriteLine(str);
                return (int) ExitCodeEnum.Success;
            }

            Console.WriteLine("Unexpected behaviour");
            return (int) ExitCodeEnum.InternalError;
        }

        public static IConfiguration BuildSettingsConfigurations()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .AddEnvironmentVariables()
                .Build();
            return builder;
        }
    }
}
