﻿using System;

namespace Server
{
	public unsafe class ServerWrapper
	{
		public static Int32 SOCKET_NO_DATA = 0;
		public static Int32 SOCKET_DATA_WAITING = 1;

		private IntPtr server;

		public ServerWrapper()
		{
			server = ServerLibrary.Server_CreateServer();

		}

		public Int32 Init(ushort port)
		{
			Int32 err = ServerLibrary.Server_initServer(server, port);
			return err;
		}

		public Int32 Poll()
		{
			return ServerLibrary.Server_PollSocket(server);
		}


		/**
         * Parameters: 
         * - EndPoint * ep: a pointer to an an
         * 
        */
		public Int32 Recv(ref EndPoint ep, byte[] buffer, Int32 len)
		{
			Int32 length;
			fixed (byte* tmpBuf = buffer)
			{
				fixed(EndPoint * p = &ep) 
				{
					UInt32 bufLen = Convert.ToUInt32(len);
					length = ServerLibrary.Server_recvBytes(server, p, new IntPtr(tmpBuf), bufLen);
				}
				return length;
			}
		}

		public Int32 Send(EndPoint ep, byte[] buffer, Int32 len)
		{
			fixed (byte* tmpBuf = buffer)
			{
				UInt32 bufLen = Convert.ToUInt32(len);
				Int32 ret = ServerLibrary.Server_sendBytes(server, ep, new IntPtr(tmpBuf), bufLen);
				return ret;
			}
		}
	}
}

