using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public static class CalculaBytesLengthHelper
    {
        public static byte[] Calcula(byte[] bytes)
        {
            byte[] byteInt = BitConverter.GetBytes(bytes.Length);
            List<byte> datas = new List<byte>();
            datas.AddRange(byteInt);//包头长度
            datas.AddRange(bytes);//加上数据内容
            return datas.ToArray();
        }
        public static byte[] Calcula(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            byte[] byteInt = BitConverter.GetBytes(bytes.Length);
            List<byte> datas = new List<byte>();
            datas.AddRange(byteInt);//包头长度
            datas.AddRange(bytes);//加上数据内容
            return datas.ToArray();
        }
    }
}
