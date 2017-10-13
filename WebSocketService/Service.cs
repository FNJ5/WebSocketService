using System;
using System.ServiceProcess;

namespace WebSocketService
{
    /// <summary>
    /// Control class for the service
    /// </summary>
    public class Service : ServiceBase
    {
        /// <summary>
        /// The WebSocket server URI
        /// </summary>
        private string serverUri;

        /// <summary>
        /// Constructor
        /// </summary>
        public Service(string uri)
        {
            // Store server URI
            this.serverUri = uri;
        }

        /// <summary>
        /// Overridden parent method call when service starts
        /// </summary>
        /// <param name="args">Arguments passed on service start</param>
        protected override void OnStart(string[] args)
        {
            // Log service started
            Console.WriteLine(DateTime.Now + " - Service.OnStart: Service started");

            // Initialize client connection
            InitializeClient();
        }

        /// <summary>
        /// Overridden parent method call when service stops
        /// </summary>
        protected override void OnStop()
        {
            // Disconnect from WebSocket server since service is stopping
            Client.Disconnect("Service is being stopped");

            // Log service stopped
            Console.WriteLine(DateTime.Now + " - Service.OnStop: Service stopped");
        }

        /// <summary>
        /// Initialize client connection to WebSocket server
        /// </summary>
        public void InitializeClient()
        {
            // Connect to WebSocket server
            Client.Connect(serverUri);
        }
    }
}
