using System;
using WebSocket4Net;

namespace WebSocketService
{
    /// <summary>
    /// WebSocket client class for the service
    /// </summary>
    class Client
    {
        /// <summary>
        /// The client WebSocket
        /// </summary>
        private static WebSocket webSocket;

        /// <summary>
        /// Static constructor
        /// </summary>
        static Client()
        { }

        /// <summary>
        /// WebSocket closed handler
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="closedEvent">Unused</param>
        private static void Closed(object sender, EventArgs closedEvent)
        {
            // Log client closed
            Console.WriteLine(DateTime.Now + " - Client.Closed: Connection to server has been closed");
        }

        /// <summary>
        /// WebSocket error handler
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="errorEvent">Event containing client error</param>
        private static void Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs errorEvent)
        {
            // Log client error
            Console.WriteLine(DateTime.Now + " - Client.Error: " + errorEvent.Exception.Message);
        }

        /// <summary>
        /// WebSocket message received handler
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="messageEvent">Event containing message from the server</param>
        private static void Message(object sender, MessageReceivedEventArgs messageEvent)
        {
            // Log client message
            Console.WriteLine(DateTime.Now + " - Client.Message: " + messageEvent.Message);
        }

        /// <summary> 
        /// WebSocket connection opened handler
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="openedEvent">Unused</param>
        private static void Opened(object sender, EventArgs openedEvent)
        {
            // Log connected to WebSocket server
            Console.WriteLine(DateTime.Now + " - Client.Opened: Connected to server");
        }

        /// <summary>
        /// Open connection to the WebSocket server
        /// </summary>
        /// <param name="uri">The WebSocket connection URI</param>
        public static void Connect(object uri)
        {
            try
            {
                // Log connecting to server
                Console.WriteLine(DateTime.Now + " - Client.Connect: Connecting to server");

                // Initialize client WebSocket
                webSocket = new WebSocket4Net.WebSocket((string)uri);

                // Add handlers for WebSocket opened, message received, error, and closed events
                webSocket.Opened += new EventHandler(Opened);
                webSocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(Message);
                webSocket.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(Error);
                webSocket.Closed += new EventHandler(Closed);

                // Open WebSocket connection to server
                webSocket.Open();
            }
            catch (Exception exception)
            {
                // Log client connection exception
                Console.WriteLine(DateTime.Now + " - Client.Connect: Exception: " + exception.Message);
            }
        }

        /// <summary>
        /// Close connection to WebSocket server
        /// </summary>
        /// <param name="reason">The reason for disconnecting</param>
        public static void Disconnect(string reason)
        {
            // If WebSocket is initialized
            if (webSocket != null)
            {
                // Close WebSocket connection
                webSocket.Close();

                // Release WebSocket resources
                webSocket.Dispose();

                // Clear WebSocket instance
                webSocket = null;
            }
        }
    }
}
