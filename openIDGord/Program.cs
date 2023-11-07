using AuthModelLib;

namespace openIDGord
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("AuthService started:");

            var graphAuth = new graphAuth();
            var googleAuth = new googleAuth();

            Console.WriteLine(graphAuth.AuthLogin());
            var r = await AuthModelLib.googleAuth.AuthLoginTest();
            Console.WriteLine(r);      
            
            Console.WriteLine(googleAuth.ServiceTest());            
            Console.WriteLine(graphAuth.ServiceTest());



            //await Auth.AuthTokenCSCAsync();
            Console.WriteLine("AuthService finished...");
            Console.ReadLine();

        }
    }
}