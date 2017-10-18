using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var ts = new TaskCompletionSource<bool>();

            using (var client = new WebSocket("wss://ws.gonines.com"))
            {
                var openTask = ts.Task;

                client.Opened += (s, e) =>
                {
                    ts.SetResult(true);
                };

                client.Error += (s, e) =>
                {
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
