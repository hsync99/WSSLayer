using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fleck;
namespace WSSLayer
{
    public class WebSocketServer
    {
        //127.0.0.1:13579
        private const int bufferSize = 1024;
        private static readonly byte[] buffer = new byte[bufferSize];

        public async Task StartWebSocketServer(string listenerPrefix)
        {
            string certPath = "C:\\cert\\cert.pem"; // Replace with your certificate file path
            string certKeyPath = "C:\\cert\\key.pem"; // Replace with your private key file path

            var server = new Fleck.WebSocketServer("wss://127.0.0.1:13579");
            server.Certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(certPath, certKeyPath, System.Security.Cryptography.X509Certificates.X509KeyStorageFlags.Exportable);

            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine($"Client {socket.ConnectionInfo.ClientIpAddress} connected");
                };

                socket.OnClose = () =>
                {
                    Console.WriteLine($"Client {socket.ConnectionInfo.ClientIpAddress} disconnected");
                };

                socket.OnMessage = message =>
                {
                    Console.WriteLine($"Received: {message}");

                    // Echo back the received message to the client
                    socket.Send(message);
                };
            });
        }
        
    
    
    
    
    }
}

