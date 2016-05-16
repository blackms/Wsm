using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MulticastProject
{
    public class Multicast : IDisposable
    {
        // The address of the multicast group to join.
        // Must be in the range from 224.0.0.0 to 239.255.255.255
        private IPAddress _multicastAddress, _unicastAddress;

        // The port over which to communicate to the multicast group
        private int _port;

        // A client receiver for multicast traffic from any source
        private UdpClient _client = null;

        // true if we have joined the multicast group; otherwise, false
        bool _joined = false;

        // Buffer for incoming data
        private byte[] _receiveBuffer;

        // Maximum size of a message in this communication
        private const int _messageSize = 512;

        //time to live
        private readonly int _ttl;

        //received messages
        public List<string> messages = new List<string>();

        private bool disposed = false; // to detect redundant calls

        public Multicast(IPAddress mcastAdrs, IPAddress localaddress, int mcastPort, int ttl)
        {
            _multicastAddress = mcastAdrs;
            _unicastAddress = localaddress;
            _port = mcastPort;
            _ttl = ttl;
        }

        /// <summary>
        /// Joins this instance.
        /// </summary>
        public void JoinMulticastGroup()
        {
            // Initialize the receive buffer
            _receiveBuffer = new byte[_messageSize];

            // client receiver for multicast traffic from any source, also known as Any Source Multicast (ASM)
            _client = new UdpClient();
            // Make a request to join the group.
            _client.JoinMulticastGroup(_multicastAddress, _unicastAddress);

            _joined = true;

            _client.Ttl = (short)_ttl;

            //send data t
            _client.MulticastLoopback = true;
        }

        /// <summary>
        /// Send the given message to the multicast group.
        /// </summary>
        /// <param name="message">The message to send</param>
        public void Send(string message)
        {
            // Attempt the send only if you have already joined the group.
            if (_joined == false)
                throw new Exception("Please join a group first");

            byte[] data = Encoding.UTF8.GetBytes(string.Format("{0}{1}", Environment.MachineName, message));

            _client.Send(data, data.Length, new IPEndPoint(_multicastAddress, _port));
        }

        /// <summary>
        /// Receives this instance.
        /// </summary>
        /// <exception cref="System.Exception">Please join a group first</exception>
        public void Receive()
        {
            try
            {
                // Only attempt to receive if you have already joined the group
                if (_joined == false)
                    throw new Exception("Please join a group first");

                Array.Clear(_receiveBuffer, 0, _receiveBuffer.Length);
                _client.BeginReceive(new AsyncCallback(MulticastReceiveCallback), null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //debug
            }
        }

        /// <summary>
        /// Multicasts the receive callback.
        /// </summary>
        /// <param name="res">The resource.</param>
        private void MulticastReceiveCallback(IAsyncResult res)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(_multicastAddress, _port);
            _receiveBuffer = _client.EndReceive(res, ref RemoteIpEndPoint);
            //convert to data to string
            string dataReceived = Encoding.UTF8.GetString(_receiveBuffer, 0, _receiveBuffer.Length);

            // Create a log entry.
            messages.Add(dataReceived);

            //start a new call back~!
            _client.BeginReceive(new AsyncCallback(MulticastReceiveCallback), null);
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            _client.Close();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    var dispose = _client as IDisposable;
                    if (dispose != null)
                    {
                        Close(); //Close connection before disposing
                        dispose.Dispose();
                        _client = null;
                    }

                    dispose = messages as IDisposable;
                    if (dispose != null)
                    {
                        dispose.Dispose();
                        messages = null;
                    }
                }

                disposed = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

