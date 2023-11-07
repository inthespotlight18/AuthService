using AuthModelLib;

namespace openIDGord
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var googleAuth = new googleAuth();

            var s = googleAuth.ServiceTest();

            Console.WriteLine(s);

            await Auth.AuthTokenCSCAsync();
        }
    }
}

//Console.WriteLine("Hello, World!");
//await Auth.AuthTokenCSCAsync();