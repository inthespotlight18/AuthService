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
            var RingCentralAuth = new RingCentralAuth();

            await graphAuth.AuthLogin();
            await googleAuth.AuthLogin();

            await RingCentralAuth.test();
                  
            
            Console.WriteLine(googleAuth.ServiceTest());            
            Console.WriteLine(graphAuth.ServiceTest());




            Console.WriteLine("AuthService finished...");
            Console.ReadLine();

        }
    }
}