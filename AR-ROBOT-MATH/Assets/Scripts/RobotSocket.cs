using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
//using Internal;
using System;
//using System.Diagnostics;
//using System.Numerics;

public class RobotSocket : MonoBehaviour
{
    Thread thread;
    public int port = 25000;
    public string server = "localhost"; //"10.0.2.15";

    void Start()
    {
        // Start connection logic on a new thread so that main UI thread isn't blocked
        ThreadStart ts = new ThreadStart(Connection);
        thread = new Thread(ts);
        thread.Start();
    }

    void SendIncorrect()
    {
        SendData("ans/incorrect");
    }

    void SendCorrect()
    {
        SendData("ans/correct");
    }

    void SendDance()
    {
        SendData("dance");
    }

    void SendData(String message)
    {
        try
        {
            // Creates socket and ensures it's disposed of later
            using TcpClient client = new TcpClient(server, port);

            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            // Get a client stream for reading and writing.
            NetworkStream stream = client.GetStream();

            // Send the message to the connected TcpServer.
            stream.Write(data, 0, data.Length);

        }
        catch (ArgumentNullException e)
        {   
            Debug.Log("ArgumentNullException: " + e);
        }
        catch (SocketException e)
        {
            Debug.Log("SocketException: " + e);
        }

    }

    void Connection()
    {
        // On connection send some data
        SendDance();
    }
}