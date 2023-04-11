using System;
using System.Net.Sockets;

namespace Sudoku
{
    class Communicator
    {
        Socket S1;

        private byte[] buff;

        private string buff_s;

        public string Status;

        public delegate void MethodDelegate();

        public event MethodDelegate StatusChange;

        public Communicator()
        {
            S1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Status = "";

            buff = new byte[1024];

            buff_s = "";
        }

        private void OnReceive(IAsyncResult R)
        {
            for (int i = 0; buff[i] != 0; i++)
            {
                buff_s += ((char)buff[i]).ToString();
                buff[i] = 0;
            }

            S1.BeginReceive(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
        }

    }
}
