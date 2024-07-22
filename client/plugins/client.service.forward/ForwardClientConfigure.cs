﻿using client.service.ui.api.clientServer;
using System.Threading.Tasks;

namespace client.service.forward
{
    /// <summary>
    /// 端口转发配置文件
    /// </summary>
    public sealed class ForwardClientConfigure : IClientConfigure
    {
        private common.forward.Config config;
        public ForwardClientConfigure(common.forward.Config config)
        {
            this.config = config;
        }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name => "TCP转发";
        /// <summary>
        /// 作者
        /// </summary>
        public string Author => "snltty";
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc => "白名单不为空时只允许白名单内端口";
        /// <summary>
        /// 启用
        /// </summary>
        public bool Enable => config.ConnectEnable;

        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        public async Task<string> Load()
        {
            return await config.ReadString();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public async Task<bool> Save(string jsonStr)
        {
            await config.SaveConfig(jsonStr).ConfigureAwait(false);
            return true;
        }
    }
}
