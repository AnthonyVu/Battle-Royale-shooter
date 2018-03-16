﻿using System;

namespace Networking
{
	public unsafe class Client
	{
		private IntPtr connection;
		private EndPoint server;
		

		public static Int32 SOCKET_NO_DATA = 0;
		public static Int32 SOCKET_DATA_WAITING = 1;

		public Client()
		{
			connection = ServerLibrary.Client_CreateClient();
		}

		public Int32 Init(string ipaddr, ushort port)
		{
			CAddr addr = new CAddr (ipaddr);
			server = new EndPoint (ipaddr, port);
			Int32 err = ServerLibrary.Client_initClient(connection, server);
			return err;
		}

		public Int32 Poll()
		{
			Int32 p = ServerLibrary.Client_PollSocket(connection);
			return Convert.ToBoolean (p);
		}


		public Int32 Recv(byte[] buffer, Int32 len)
		{
			fixed(byte* tmpBuf = buffer) 
			{
				fixed(EndPoint* p = &rcvEndPoint)
				{
					UInt32 bufLen = Convert.ToUInt32 (len);
					Int32 length = ServerLibrary.Server_recvBytes(connection, p, new IntPtr(tmpBuf), bufLen);
					return length;
				}
			}
		}

		public Int32 Send(byte[] buffer, Int32 len)
		{
			uint bufLen = Convert.ToUInt32 (len);

			fixed( byte* tmpBuf = buffer) 
			{
				Int32 ret = ServerLibrary.Server_sendBytes (connection, server, new IntPtr (tmpBuf), bufLen);
				return ret;
			}

		}
	}
}

