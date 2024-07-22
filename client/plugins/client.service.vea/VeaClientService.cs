﻿using client.messengers.clients;
using client.service.ui.api.clientServer;
using common.libs;
using common.libs.extends;
using common.proxy;
using System;
using System.Buffers.Binary;
using System.Linq;
using System.Threading.Tasks;

namespace client.service.vea
{
    /// <summary>
    /// 组网前端接口
    /// </summary>
    public sealed class VeaClientService : IClientService
    {
        private readonly Config config;
        private readonly VeaTransfer VeaTransfer;
        private readonly VeaMessengerSender veaMessengerSender;
        private readonly IClientInfoCaching clientInfoCaching;
        private readonly IProxyServer proxyServer;

        public VeaClientService(Config config, VeaTransfer VeaTransfer, VeaMessengerSender veaMessengerSender, IClientInfoCaching clientInfoCaching, IProxyServer proxyServer)
        {
            this.config = config;
            this.VeaTransfer = VeaTransfer;
            this.veaMessengerSender = veaMessengerSender;
            this.clientInfoCaching = clientInfoCaching;
            this.proxyServer = proxyServer;
        }

        public async Task<bool> Run(ClientServiceParamsInfo arg)
        {
            try
            {
                return await VeaTransfer.Run();
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                arg.SetCode(ClientServiceResponseCodes.Error, ex.Message);
            }
            return false;
        }

        public async Task<EnumProxyCommandStatusMsg> Test(ClientServiceParamsInfo arg)
        {
            TestTargetInfo fmodel = arg.Content.DeJson<TestTargetInfo>();
            return await VeaTransfer.Test(fmodel.Host, fmodel.Port);
        }

        /// <summary>
        /// 去获取在线设备
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public async Task<bool> Online(ClientServiceParamsInfo arg)
        {
            ulong id = ulong.Parse(arg.Content);
            if (clientInfoCaching.Get(id, out ClientInfo client))
            {
                return await veaMessengerSender.GetOnLine(client.Connection);
            }
            return false;
        }
        /// <summary>
        /// 在线设备
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public VeaLanIPAddressOnLine Onlines(ClientServiceParamsInfo arg)
        {
            ulong id = ulong.Parse(arg.Content);
            if (VeaTransfer.Onlines.TryGetValue(id, out VeaLanIPAddressOnLine online))
            {
                return online;
            }
            return new VeaLanIPAddressOnLine();
        }

        /// <summary>
        /// 各个客户端
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public object List(ClientServiceParamsInfo arg)
        {
            EnumProxyCommandStatusMsg lastError = EnumProxyCommandStatusMsg.Success;
            proxyServer.LastError((ushort)config.ListenPort, out lastError);

            return VeaTransfer.IPList.ToDictionary(c => c.Value.Client.ConnectionId, d => new
            {
                IP = string.Join(".", BinaryPrimitives.ReverseEndianness(d.Value.IP).ToBytes()),
                LanIPs = d.Value.LanIPs.Select(c => new { IPAddress = string.Join(".", BinaryPrimitives.ReverseEndianness(c.IPAddress).ToBytes()), Mask = c.MaskLength }),
                d.Value.NetWork,
                Mask = d.Value.MaskLength,
                LastError = lastError
            });
        }

        /// <summary>
        /// 重装其网卡
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public async Task<bool> Reset(ClientServiceParamsInfo arg)
        {
            ulong id = ulong.Parse(arg.Content);
            if (clientInfoCaching.Get(id, out ClientInfo client))
            {
                await veaMessengerSender.Reset(client.Connection, id);
            }
            return true;
        }

        /// <summary>
        /// 刷新ip列表
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public bool Update(ClientServiceParamsInfo arg)
        {
            VeaTransfer.UpdateIp();
            return true;
        }
    }

    public sealed class TestTargetInfo
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }

}
