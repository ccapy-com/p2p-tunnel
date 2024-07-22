﻿using common.libs.database;
using common.libs.extends;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace common.user
{
    [Table("users-appsettings")]
    public sealed class Config
    {
        public Config() { }
        private readonly IConfigDataProvider<Config> configDataProvider;
        public Config(IConfigDataProvider<Config> configDataProvider)
        {
            this.configDataProvider = configDataProvider;

            Config config = ReadConfig().Result;
            Enable = config.Enable;
            ForceOffline = config.ForceOffline;
            SaveConfig().Wait();
        }

        /// <summary>
        /// 启用账号验证
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// 强制显现
        /// </summary>
        public bool ForceOffline { get; set; }

        [JsonIgnore]
        public byte NetFlowBit = 0x01;

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
            Enable = _config.Enable;
            ForceOffline = _config.ForceOffline;

            config.Enable = Enable;
            config.ForceOffline = ForceOffline;

            await configDataProvider.Save(config).ConfigureAwait(false);
        }
        public async Task SaveConfig()
        {
            await configDataProvider.Save(this).ConfigureAwait(false);
        }
    }

}
