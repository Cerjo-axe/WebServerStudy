
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebServer
{
    public class Program
    {
         

        public static void Main(string[] args)
        {
            Start();
        }

        public static void Start()
        {
            Server.Start();
            Console.WriteLine("Press Enter to finish application");
            Console.ReadLine();
        }

    }
}