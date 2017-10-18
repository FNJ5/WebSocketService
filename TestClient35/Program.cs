using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;
using SuperSocket.ClientEngine;

namespace TestClient35
{
    class Program
    {
        static void Main(string[] args)
        {
            var ts = new TaskCompletionSource<bool>();

            using (var client = new WebSocket("wss://ws.gonines.com"))
            {
                client.Security.AllowNameMismatchCertificate = true;
                var openTask = ts.Task;

                client.Opened += (s, e) =>
                {
                    ts.SetResult(true);
                };

                client.Error += (s, e) =>
                {
                    Console.WriteLine(e.Exception.Message);
                    ts.SetResult(false);
                };

                client.Closed += (s, e) =>
                {
                    ts.SetResult(false);
                };

                client.Open();

                Console.WriteLine("OpenResult:" + openTask.Result);
            }

            Console.ReadKey();
        }
    }
}
