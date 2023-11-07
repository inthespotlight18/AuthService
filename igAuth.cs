using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace authModel.Interface
{
    public interface gAuth
    {
        string AuthLogin();
        string ServiceTest();


    }
}

//MAIN()
//{
// string OK = AuthLogin();
// if (! OK.Equals("OK")  { Console.WriteLine("AuthLogin : Error[{0}]", OK); Application.Exit(-1); }
// 
// string result = ServiceTest();
// ConsoleWriteLine("ServcieTest : Result [{0}]", result);
//}