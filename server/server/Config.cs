﻿using common.libs.database;
using common.libs.extends;
using common.server.model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace server
{
    /// <summary>
    /// 服务端配置文件
    /// </summary>
    [Table("appsettings")]
    public sealed class Config
    {
        public Config() { }
        private readonly IConfigDataProvider<Config> configDataProvider;
        public Config(IConfigDataProvider<Config> configDataProvider)
        {
            this.configDataProvider = configDataProvider;

            Config config = ReadConfig().Result;
            Udp = config.Udp;
            Tcp = config.Tcp;
            ConnectLimit = config.ConnectLimit;
            TcpBufferSize = config.TcpBufferSize;
            TimeoutDelay = config.TimeoutDelay;
            RegisterTimeout = config.RegisterTimeout;
            RegisterEnable = config.RegisterEnable;
            RelayEnable = config.RelayEnable;
            EncodePassword = config.EncodePassword;
            AdminGroup = config.AdminGroup;
            SaveConfig().Wait();
        }

        /// <summary>
        /// udp端口
        /// </summary>
        public int Udp { get; set; } = 5410;
        /// <summary>
        /// tcp端口
        /// </summary>
        public int Tcp { get; set; } = 5410;
        /// <summary>
        /// 连接频率每秒
        /// </summary>
        public int ConnectLimit { get; set; }
        public EnumBufferSize TcpBufferSize { get; set; } = EnumBufferSize.KB_8;
        /// <summary>
        /// 掉线超时
        /// </summary>
        public int TimeoutDelay { get; set; } = 20 * 1000;
        /// <summary>
        /// 注册超时
        /// </summary>
        public int RegisterTimeout { get; set; } = 5000;

        /// <summary>
        /// 允许注册
        /// </summary>
        public bool RegisterEnable { get; set; } = true;
        /// <summary>
        /// 允许中继
        /// </summary>
        public bool RelayEnable { get; set; }
        /// <summary>
        /// 加密密码
        /// </summary>
        public string EncodePassword { get; set; } = string.Empty;
        /// <summary>
        /// 管理员分组
        /// </summary>
        public string AdminGroup { get; set; } = string.Empty;

        public bool NoDelay { get; set; }



        private async Task<Config> ReadConfig()
        {
            return await configDataProvider.Load() ?? new Config();
        }
        public async Task<string> ReadString()
        {
            return await configDataProvider.LoadString();
        }
        public async Task SaveConfig(string jsonStr)
        {
            Config config = await ReadConfig().ConfigureAwait(false);
            Config _config = jsonStr.DeJson<Config>();
            Udp = _config.Udp;
            Tcp = _config.Tcp;
            ConnectLimit = _config.ConnectLimit;
            TcpBufferSize = _config.TcpBufferSize;
            TimeoutDelay = _config.TimeoutDelay;
            RegisterTimeout = _config.RegisterTimeout;
            RegisterEnable = _config.RegisterEnable;
            RelayEnable = _config.RelayEnable;
            EncodePassword = _config.EncodePassword;
            AdminGroup = _config.AdminGroup;

            config.Udp = Udp;
            config.Tcp = Tcp;
            config.ConnectLimit = ConnectLimit;
            config.TcpBufferSize = TcpBufferSize;
            config.TimeoutDelay = TimeoutDelay;
            config.RegisterTimeout = RegisterTimeout;
            config.RegisterEnable = RegisterEnable;
            config.RelayEnable = RelayEnable;
            config.EncodePassword = EncodePassword;
            config.AdminGroup = AdminGroup;

            await configDataProvider.Save(config).ConfigureAwait(false);
        }

        public async Task SaveConfig()
        {
            await configDataProvider.Save(this).ConfigureAwait(false);
        }
    }

}
