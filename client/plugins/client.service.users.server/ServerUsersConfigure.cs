﻿using client.messengers.signin;
using client.service.ui.api.clientServer;
using common.libs;
using common.libs.extends;
using common.server;
using common.server.model;
using common.user;
using System;
using System.Threading.Tasks;

namespace client.service.users.server
{
    public sealed class ServerUsersConfigure : IClientConfigure
    {
        private readonly MessengerSender messengerSender;
        private readonly SignInStateInfo signInStateInfo;
        public ServerUsersConfigure(MessengerSender messengerSender, SignInStateInfo signInStateInfo)
        {
            this.messengerSender = messengerSender;
            this.signInStateInfo = signInStateInfo;
        }

        public string Name => "服务端用户管理";
        public string Author => "snltty";
        public string Desc => string.Empty;
        public bool Enable => true;

        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        public async Task<string> Load()
        {
            MessageResponeInfo resp = await messengerSender.SendReply(new MessageRequestWrap
            {
                MessengerId = (ushort)UsersMessengerIds.GetSetting,
                Connection = signInStateInfo.Connection,
            }).ConfigureAwait(false);
            if (resp.Code == MessageResponeCodes.OK)
            {
                return resp.Data.GetUTF8String();
            }
            return string.Empty;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public async Task<bool> Save(string jsonStr)
        {
            MessageResponeInfo resp = await messengerSender.SendReply(new MessageRequestWrap
            {
                MessengerId = (ushort)UsersMessengerIds.Setting,
                Connection = signInStateInfo.Connection,
                Payload = jsonStr.ToUTF8Bytes()
            }).ConfigureAwait(false);
            return resp.Code == MessageResponeCodes.OK && resp.Data.Span.SequenceEqual(Helper.TrueArray);
        }
    }
}
