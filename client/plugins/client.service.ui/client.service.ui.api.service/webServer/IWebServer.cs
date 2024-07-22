﻿using common.libs;
using System;
using System.IO;
using System.Net;

namespace client.service.ui.api.service.webServer
{
    /// <summary>
    /// web服务
    /// </summary>
    public interface IWebServer
    {
        /// <summary>
        /// 开始
        /// </summary>
        public void Start();
    }

    /// <summary>
    /// web服务文件读取器
    /// </summary>
    public interface IWebServerFileReader
    {
        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] Read(string fileName,out DateTime lastModified);
    }

    /// <summary>
    /// web服务文件读取器
    /// </summary>
    public sealed class WebServerFileReader : IWebServerFileReader
    {
        private readonly Config config;
        public WebServerFileReader(Config config)
        {
            this.config = config;
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] Read(string fileName, out DateTime lastModified)
        {
            fileName = Path.Join(config.Web.Root, fileName);
            lastModified = DateTime.UtcNow;
            if (File.Exists(fileName))
            {
                lastModified = File.GetLastWriteTimeUtc(fileName);
                return File.ReadAllBytes(fileName);
            }
            return Helper.EmptyArray;
        }
    }
}
