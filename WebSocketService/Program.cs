using System;

namespace WebSocketService
{
    /// <summary>
    /// Main class for WebSocket service.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The WebSocket server URI
        /// </summary>
        private static readonly string WEBSOCKET_URI = "FILL THIS OUT";

        /// <summary>
        /// The main entry point for the program
        /// </summary>
        static void Main()
        {
            // Run service if not debug, else simulate service
            #if (!DEBUG)
                // Initialize services
                ServiceBase[] ServicesToRun = new ServiceBase[] 
                { 
                    new Service(WEBSOCKET_URI) 
                };

                // Run services
                ServiceBase.Run(ServicesToRun);
            #else
                // Initialize service
                Service service = new Service(WEBSOCKET_URI);

                // Log service started
                Console.WriteLine(DateTime.Now + " - Program.Main: Service started");

                // Initialize client connection
                service.InitializeClient();

                // Indefinitely block thread to simulate running service
                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            #endif
        }
    }
}
