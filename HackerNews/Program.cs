using System;

namespace HackerNews
{
    class Program
    {
        static int Main(string[] args)
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


            return (int) ExitCodeEnum.Success;
        }
    }
}
