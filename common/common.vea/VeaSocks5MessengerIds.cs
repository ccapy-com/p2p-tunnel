﻿using common.server;
using System;

namespace common.vea
{
    /// <summary>
    /// 组网消息
    /// </summary>
    [Flags, MessengerIdEnum]
    public enum VeaSocks5MessengerIds : ushort
    {
        /// <summary>
        /// 最小
        /// </summary>
        Min = 1100,
        /// <summary>
        /// 更新ip
        /// </summary>
        UpdateIp = 1101,
        /// <summary>
        /// 重装网卡
        /// </summary>
        Reset = 1102,
        /// <summary>
        /// 获取在线设备
        /// </summary>
        GetOnLine = 1103,
        /// <summary>
        /// 在线设备数据
        /// </summary>
        OnLine = 1104,
        
        /// <summary>
        /// 获取网络组
        /// </summary>
        Network = 1105,
        /// <summary>
        /// 创建一个网络组
        /// </summary>
        AddNetwork = 1106,
        /// <summary>
        /// 获取一个可用ip
        /// </summary>
        AssignIP = 1107,
        /// <summary>
        /// 更新ip
        /// </summary>
        ModifyIP = 1108,
        /// <summary>
        /// 已更新IP
        /// </summary>
        ModifiedIP = 1109,
        /// <summary>
        /// 删除分配ip
        /// </summary>
        DeleteIP = 1110,
        /// <summary>
        /// 获取配置
        /// </summary>
        GetSetting = 1111,
        /// <summary>
        /// 设置配置
        /// </summary>
        Setting = 1112,

        /// <summary>
        /// 最大
        /// </summary>
        Max = 1199,
    }
}
