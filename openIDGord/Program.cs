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

            await graphAuth.GetProfile();
            await googleAuth.GetProfile();
            await ringCentralAuth.SendSMS("2369713928", "Daniil Zlenko sent that message from his C# program");
                  
            
            Console.WriteLine(googleAuth.ServiceTest());            
            Console.WriteLine(graphAuth.ServiceTest());

            Console.WriteLine("AuthService finished...");
            Console.ReadLine();

        }
    }
}