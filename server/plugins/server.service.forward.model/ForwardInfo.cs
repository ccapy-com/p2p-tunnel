﻿using common.forward;
using common.libs.extends;
using System;
using System.ComponentModel;
using System.Net;

namespace server.service.forward.model
{
    public sealed class ForwardSignInInfo
    {
        public ForwardSignInInfo() { }

        public string SourceIp { get; set; }
        public ushort SourcePort { get; set; }
        public IPAddress TargetIp { get; set; } = IPAddress.Any;
        public ushort TargetPort { get; set; }
        public ForwardAliveTypes AliveType { get; set; } = ForwardAliveTypes.Web;

        public byte[] ToBytes()
        {
            var sipBytes = SourceIp.GetUTF16Bytes();

            byte[] bytes = new byte[
                1  //AliveType
                + 2  //SourcePort
                + 2  //TargetPort
                + 1 + 1 + sipBytes.Length  //SourceIp
                + 4  //TargetIp
             ];
            var memory = bytes.AsMemory();
            var span = bytes.AsSpan();

            int index = 0;

            bytes[index] = (byte)AliveType;
            index += 1;

            SourcePort.ToBytes(memory.Slice(index));
            index += 2;

            TargetPort.ToBytes(memory.Slice(index));
            index += 2;

            bytes[index] = (byte)sipBytes.Length;
            index += 1;
            bytes[index] = (byte)SourceIp.Length;
            index += 1;
            sipBytes.CopyTo(span.Slice(index));
            index += sipBytes.Length;


            TargetIp.TryWriteBytes(span.Slice(index), out int length);
            return bytes;

        }

        public void DeBytes(Memory<byte> data)
        {
            var span = data.Span;
            int index = 0;

            AliveType = (ForwardAliveTypes)span[index];
            index += 1;

            SourcePort = span.Slice(index, 2).ToUInt16();
            index += 2;

            TargetPort = span.Slice(index, 2).ToUInt16();
            index += 2;

            SourceIp = span.Slice(index + 2, span[index]).GetUTF16String(span[index + 1]);
            index += 1 + 1 + span[index];

            TargetIp = new IPAddress(span.Slice(index));
            index += 1 + 1 + span[index];

        }

    }
    public sealed class ForwardSignOutInfo
    {
        public ForwardSignOutInfo() { }
        public string SourceIp { get; set; }
        public ushort SourcePort { get; set; }
        public ForwardAliveTypes AliveType { get; set; }
        public byte[] ToBytes()
        {
            var ipBytes = SourceIp.GetUTF16Bytes();
            byte[] bytes = new byte[1 + 2 + 1 + ipBytes.Length];

            int index = 0;

            bytes[index] = (byte)AliveType;
            index += 1;

            SourcePort.ToBytes(bytes.AsMemory(index));
            index += 2;

            bytes[index] = (byte)SourceIp.Length;
            index += 1;
            ipBytes.CopyTo(bytes.AsSpan(index));
            index += ipBytes.Length;

            return bytes;
        }
        public void DeBytes(Memory<byte> data)
        {
            var span = data.Span;
            int index = 0;

            AliveType = (ForwardAliveTypes)span[index];
            index += 1;

            SourcePort = span.Slice(index, 2).ToUInt16();
            index += 2;

            SourceIp = span.Slice(index + 1).GetUTF16String(span[index]);
        }
    }
    public sealed class ForwardSignInResultInfo
    {
        public ForwardSignInResultCodes Code { get; set; }
        public ulong ID { get; set; }
        public string Msg { get; set; } = string.Empty;
        public byte[] ToBytes()
        {
            var msgBytes = Msg.GetUTF16Bytes();
            var bytes = new byte[
                1
                + 8
                + 1 + 1 + msgBytes.Length];

            int index = 0;

            bytes[index] = (byte)Code;
            index += 1;

            ID.ToBytes(bytes.AsMemory(index));
            index += 8;

            bytes[index] = (byte)msgBytes.Length;
            index += 1;
            bytes[index] = (byte)Msg.Length;
            index += 1;
            msgBytes.CopyTo(bytes.AsSpan(index));

            return bytes;
        }
        public void DeBytes(ReadOnlyMemory<byte> data)
        {
            var span = data.Span;
            int index = 0;

            Code = (ForwardSignInResultCodes)span[index];
            index += 1;

            ID = span.Slice(index, 8).ToUInt64();
            index += 8;

            Msg = span.Slice(index + 2, span[index]).GetUTF16String(span[index + 1]);
        }
    }

    [Flags]
    public enum ForwardSignInResultCodes : byte
    {
        [Description("成功")]
        OK = 0,
        [Description("插件未开启")]
        DISABLED = 1,
        [Description("已存在")]
        EXISTS = 2,
        [Description("端口超出范围")]
        OUT_RANGE = 4,
        [Description("未知")]
        UNKNOW = 8,
    }
}
