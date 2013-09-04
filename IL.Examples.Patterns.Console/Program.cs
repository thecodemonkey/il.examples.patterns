using IL.Examples.Patterns.Logic;
using IL.Examples.Patterns.Model;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IL.Examples.Patterns.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = UnityConfig.Register();
            UserService userService = container.Resolve<UserService>();

            while (true)
            {
                string userName = GetRandomText();
                string password = GetRandomText();
                User user = userService.Register(userName, password);

                System.Console.WriteLine("user created id:{0} nam:{1} password:{2}", user.ID, user.Name, user.Password);

                Thread.Sleep(3000);
            }
        }

        private static string GetRandomText()
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < 10; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * new Random((int)DateTime.Now.Ticks).NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

    }
}
