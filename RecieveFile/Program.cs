using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace RecieveFile
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = null;
            TcpClient client = null;

            try
            {
                Console.WriteLine("Enter the name of the server:");
                string server = Console.ReadLine();
                Console.WriteLine("Enter the server port, press enter for 80:");
                string port = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(port))
                    port = "80";

                client = new TcpClient(server, int.Parse(port)); //http port
                NetworkStream ns = client.GetStream();
                sr = new StreamReader(ns);

                bool hasConnectedMsg = false;

                while (client.Connected)
                {
                    if (!hasConnectedMsg)
                    {
                        Console.WriteLine("Connected");
                        hasConnectedMsg = true;
                    }

                    if (ns.CanRead && ns.DataAvailable)
                    {
                        string line = sr.ReadLine();
                        Console.WriteLine(line);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }

            finally
            {
                Console.WriteLine("Connection lost");
                sr.Dispose();
                Console.ReadLine();
            }
        }
    }
}
