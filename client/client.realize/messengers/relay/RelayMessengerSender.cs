﻿using client.messengers.signin;
using common.libs;
using common.server;
using common.server.model;
using System;
using System.Threading.Tasks;

namespace client.realize.messengers.relay
{
    public sealed class RelayMessengerSender
    {
        public Action<RelayInfo> OnRelay { get; set; } = (param) => { };
        private readonly MessengerSender messengerSender;
        private readonly SignInStateInfo signInStateInfo;
        public RelayMessengerSender(MessengerSender messengerSender, SignInStateInfo signInStateInfo)
        {
            this.messengerSender = messengerSender;
            this.signInStateInfo = signInStateInfo;
        }
        /// <summary>
        /// 通知其要使用中继
        /// </summary>
        /// <param name="relayids"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async Task<bool> Relay(Memory<ulong> relayids, IConnection connection)
        {
            var resp = await messengerSender.SendReply(new MessageRequestWrap
            {
                MessengerId = (ushort)RelayMessengerIds.Relay,
                Encode = false,
                Connection = connection,
                RelayId = relayids,
                Payload = new RelayInfo { RelayIds = relayids }.ToBytes()
            }).ConfigureAwait(false);

            return resp.Code == MessageResponeCodes.OK && resp.Data.Span.SequenceEqual(Helper.TrueArray);
        }

        /// <summary>
        /// 中继延迟
        /// </summary>
        /// <param name="relayids"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async Task<bool> Delay(ulong[] relayids, IConnection connection)
        {
            var resp = await messengerSender.SendReply(new MessageRequestWrap
            {
                MessengerId = (ushort)RelayMessengerIds.Delay,
                Encode = false,
                Connection = connection,
                Timeout = 1000,
                RelayId = relayids
            }).ConfigureAwait(false);
            return resp.Code == MessageResponeCodes.OK && resp.Data.Span.SequenceEqual(Helper.TrueArray);
        }

        public async Task<bool> AskConnects()
        {
            return await messengerSender.SendOnly(new MessageRequestWrap
            {
                MessengerId = (ushort)RelayMessengerIds.AskConnects,
                Encode = false,
                Connection = signInStateInfo.Connection
            }).ConfigureAwait(false);
        }

        public async Task<bool> Connects(ConnectsInfo connects)
        {
            return await messengerSender.SendOnly(new MessageRequestWrap
            {
                MessengerId = (ushort)RelayMessengerIds.Connects,
                Encode = false,
                Connection = signInStateInfo.Connection,
                Payload = connects.ToBytes(),
            }).ConfigureAwait(false);
        }
    }
}
