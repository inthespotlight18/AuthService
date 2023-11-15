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
            var ringCentralAuth = new RingCentralAuth();

            await graphAuth.AuthLogin();
            await googleAuth.AuthLogin();
            await ringCentralAuth.AuthLogin();
                  
            
            Console.WriteLine(googleAuth.ServiceTest());            
            Console.WriteLine(graphAuth.ServiceTest());




            Console.WriteLine("AuthService finished...");
            Console.ReadLine();

        }
    }
}