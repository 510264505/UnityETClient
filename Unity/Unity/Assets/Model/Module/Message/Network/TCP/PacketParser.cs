﻿using System;
using System.IO;

namespace ETModel
{
	public enum ParserState
	{
		PacketSize,
		PacketBody
	}
	
	public static class Packet
	{
		public const int PacketSizeLength2 = 2;
		public const int PacketSizeLength4 = 4;
		public const int MinPacketSize = 2;
		public const int OpcodeIndex = 0;
		public const int MessageIndex = 2;
	}

	public class PacketParser
	{
		private readonly CircularBuffer buffer;
		private int packetSize;
		private ParserState state;
		public MemoryStream memoryStream;
		private bool isOK;
		private readonly int packetSizeLength;

		public PacketParser(int packetSizeLength, CircularBuffer buffer, MemoryStream memoryStream)
		{
			this.packetSizeLength = packetSizeLength;
			this.buffer = buffer;
			this.memoryStream = memoryStream;
		}

		public bool Parse()
		{
			if (this.isOK)
			{
				return true;
			}

			bool finish = false;
			while (!finish)
			{
				switch (this.state)
				{
					case ParserState.PacketSize:
						if (this.buffer.Length < this.packetSizeLength)
						{
							finish = true;
						}
						else
						{
							this.buffer.Read(this.memoryStream.GetBuffer(), 0, this.packetSizeLength);
                            //在NetworkComponent 两个Awake方法中设置包长度参数
                            switch (this.packetSizeLength)
							{
								case Packet.PacketSizeLength4:
									this.packetSize = BitConverter.ToInt32(this.memoryStream.GetBuffer(), 0);
                                    byte[] b4 = new byte[this.packetSize];
                                    Buffer.BlockCopy(this.memoryStream.GetBuffer(), 0, b4, 0, b4.Length);
                                    if (this.packetSize > ushort.MaxValue * 16 || this.packetSize < Packet.MinPacketSize)
									{
										throw new Exception($"recv packet size error: {this.packetSize}");
									}
									break;
								case Packet.PacketSizeLength2:
                                    this.packetSize = BitConverter.ToUInt16(this.memoryStream.GetBuffer(), 0);
                                    byte[] b2 = new byte[this.packetSize];
                                    Buffer.BlockCopy(this.memoryStream.GetBuffer(), 0, b2, 0, b2.Length);
                                    if (this.packetSize > ushort.MaxValue || this.packetSize < Packet.MinPacketSize)
									{
										throw new Exception($"recv packet size error: {this.packetSize}");
									}
									break;
								default:
									throw new Exception("packet size byte count must be 2 or 4!");
							}
							this.state = ParserState.PacketBody;
						}
						break;
					case ParserState.PacketBody:
						if (this.buffer.Length < this.packetSize)
						{
							finish = true;
						}
						else
						{
							this.memoryStream.Seek(0, SeekOrigin.Begin);
							this.memoryStream.SetLength(this.packetSize);
							byte[] bytes = this.memoryStream.GetBuffer();
							this.buffer.Read(bytes, 0, this.packetSize);
							this.isOK = true;
							this.state = ParserState.PacketSize;
							finish = true;
						}
						break;
				}
			}
			return this.isOK;
		}

		public MemoryStream GetPacket()
		{
			this.isOK = false;
			return this.memoryStream;
		}
	}
}