﻿using common.libs;
using common.libs.database;
using common.libs.extends;
using common.server.model;
using System;
using System.Buffers.Binary;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace client.service.vea
{
    /// <summary>
    /// 组网配置文件
    /// </summary>
    [Table("vea-appsettings")]
    public sealed class Config
    {
        public Config() { }
        private readonly IConfigDataProvider<Config> configDataProvider;


        public Config(IConfigDataProvider<Config> configDataProvider)
        {
            this.configDataProvider = configDataProvider;

            Config config = ReadConfig().Result;
            ListenEnable = config.ListenEnable;
            ProxyAll = config.ProxyAll;
            IP = config.IP;
            LanIPs = config.LanIPs;
            ListenPort = config.ListenPort;
            BufferSize = config.BufferSize;
            ConnectEnable = config.ConnectEnable;
            BroadcastBind = config.BroadcastBind;
            BroadcastEnable = config.BroadcastEnable;
            BroadcastList = config.BroadcastList;
            SaveConfig().Wait();
        }

        [JsonIgnore]
        public byte Plugin => common.vea.Config.plugin;

        /// <summary>
        /// 启用
        /// </summary>
        public bool ListenEnable { get; set; }
        /// <summary>
        /// 代理所有
        /// </summary>
        public bool ProxyAll { get; set; }

        /// <summary>
        /// 组网ip
        /// </summary>
        public IPAddress IP { get; set; } = IPAddress.Parse("192.168.54.1");
        /// <summary>
        /// 局域网网段ip列表
        /// </summary>
        public string[] LanIPs { get; set; } = Array.Empty<string>();

        [JsonIgnore]
        public VeaLanIPAddress[] VeaLanIPs { get; set; } = Array.Empty<VeaLanIPAddress>();

        /// <summary>
        /// 监听端口
        /// </summary>
        public int ListenPort { get; set; } = 5415;
        /// <summary>
        /// buffersize
        /// </summary>
        public EnumBufferSize BufferSize { get; set; } = EnumBufferSize.KB_8;
        /// <summary>
        /// 允许被连接
        /// </summary>
        public bool ConnectEnable { get; set; } = true; 

        public IPAddress BroadcastBind { get; set; } = IPAddress.Any;
        public bool BroadcastEnable { get; set; }
        public IPAddress[] BroadcastList { get; set; } = Array.Empty<IPAddress>();
        [JsonIgnore]
        public uint[] VeaBroadcastList { get; set; } = Array.Empty<uint>();

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <returns></returns>
        public async Task<Config> ReadConfig()
        {
            return await configDataProvider.Load() ?? new Config();
        }
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <returns></returns>
        public async Task<string> ReadString()
        {
            return await configDataProvider.LoadString();
        }
        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public async Task SaveConfig(string jsonStr)
        {
            var _config = jsonStr.DeJson<Config>();

            ListenEnable = _config.ListenEnable;
            ProxyAll = _config.ProxyAll;
            IP = _config.IP;
            LanIPs = _config.LanIPs;
            ListenPort = _config.ListenPort;
            BufferSize = _config.BufferSize;
            ConnectEnable = _config.ConnectEnable;
            BroadcastBind = _config.BroadcastBind;
            BroadcastEnable = _config.BroadcastEnable;
            BroadcastList = _config.BroadcastList;
            ParseLanIPs();
            ParseBroadcastList();

            await configDataProvider.Save(jsonStr).ConfigureAwait(false);

        }

        public async Task SaveConfig()
        {
            ParseLanIPs();
            ParseBroadcastList();

            await configDataProvider.Save(this).ConfigureAwait(false);

        }

        private void ParseLanIPs()
        {
            try
            {
                VeaLanIPs = LanIPs.Select(c => c.Split(Helper.SeparatorCharSlash)).Where(c => c[0] != IPAddress.Any.ToString()).Select(c =>
                {
                    byte maskLength = c.Length > 1 ? byte.Parse(c[1]) : (byte)0;
                    uint ip = BinaryPrimitives.ReadUInt32BigEndian(IPAddress.Parse(c[0]).GetAddressBytes());
                    //每填写掩码，自动计算
                    if (c.Length == 1)
                    {
                        maskLength = NetworkHelper.MaskLength(ip);
                    }
                    //掩码十进制
                    uint maskValue = NetworkHelper.MaskValue(maskLength);
                    return new VeaLanIPAddress
                    {
                        IPAddress = ip,
                        MaskLength = maskLength,
                        MaskValue = maskValue,
                        NetWork = ip & maskValue,
                        Broadcast = ip | (~maskValue),
                    };

                }).ToArray();
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
        }

        private void ParseBroadcastList()
        {
            VeaBroadcastList = BroadcastList.Where(c => c.GetIsBroadcastAddress()).Select(c => BinaryPrimitives.ReadUInt32BigEndian(c.GetAddressBytes())).ToArray();
        }
    }

    public sealed class VeaLanIPAddress
    {
        /// <summary>
        /// ip，存小端
        /// </summary>
        public uint IPAddress { get; set; }
        public byte MaskLength { get; set; }
        public uint MaskValue { get; set; }
        public uint NetWork { get; set; }
        public uint Broadcast { get; set; }

    }

   
}
