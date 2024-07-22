﻿using common.libs.database;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Threading.Tasks;

namespace client.service.ui.api
{
    /// <summary>
    /// 配置信息
    /// </summary>
    [Table("ui-appsettings")]
    public sealed class Config
    {
        public Config() { }
        private readonly IConfigDataProvider<Config> configDataProvider;
        public Config(IConfigDataProvider<Config> configDataProvider)
        {
            this.configDataProvider = configDataProvider;

            Config config = ReadConfig().Result;
            EnableWeb = config.EnableWeb;
            EnableCommand = config.EnableCommand;
            EnableApi = config.EnableApi;
            Websocket = config.Websocket;
            Web = config.Web;
            SaveConfig().Wait();
        }
        /// <summary>
        /// 启用web
        /// </summary>
        public bool EnableWeb { get; set; } = true;
        /// <summary>
        /// 启用命令行，具名管道
        /// </summary>
        public bool EnableCommand { get; set; } = true;
        /// <summary>
        /// 启用websocket
        /// </summary>
        public bool EnableApi { get; set; } = true;

        /// <summary>
        /// 本地websocket
        /// </summary>
        public WebsocketConfig Websocket { get; set; } = new WebsocketConfig();
        /// <summary>
        /// 本地web管理端配置
        /// </summary>
        public WebConfig Web { get; set; } = new WebConfig();

        /// <summary>
        /// 读取配置
        /// </summary>
        /// <returns></returns>
        public async Task<Config> ReadConfig()
        {
            return await configDataProvider.Load() ?? new Config();
        }
        public async Task SaveConfig()
        {
            await configDataProvider.Save(this);
        }

    }

    /// <summary>
    /// 本地web管理端配置
    /// </summary>
    public sealed class WebConfig
    {
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 5411;
        /// <summary>
        /// 根目录
        /// </summary>
        public string Root { get; set; } = "./public/web";
        /// <summary>
        /// 绑定ip
        /// </summary>
        public string BindIp { get; set; } = "+";

    }
    /// <summary>
    /// 本地websocket
    /// </summary>
    public sealed class WebsocketConfig
    {
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 5412;
        /// <summary>
        /// 绑定ip
        /// </summary>
        public IPAddress BindIp { get; set; } = IPAddress.Any;
    }
}
