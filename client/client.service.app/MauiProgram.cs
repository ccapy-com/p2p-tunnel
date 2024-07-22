﻿using client.messengers.singnin;
using client.realize.messengers.punchHole;
using client.service.logger;
//using client.service.socks5;
using client.service.ui.api.service.clientServer;
using client.service.ui.api.service.webServer;
using common.libs;
using common.libs.database;
using common.server;
using System.Reflection;
using client.service.users.server;
using client.service.forward;
//using client.service.httpProxy;
using System.Net;
using common.user;
using common.proxy;
using client.service.forward.server;
using client.service.proxy;
//using client.service.httpProxy.server;
//using client.service.socks5.server;

namespace client.service.app
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            ThreadPool.SetMinThreads(150, 150);

            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureMauiHandlers(handlers =>
            {
            });
            // builder.Services.AddMauiBlazorWebView();

            MauiApp app = builder.Build();

            return app;
        }
    }

    public sealed class Startup
    {
        static ServiceProvider serviceProvider = null;
        public static void Start()
        {
            AppDomain.CurrentDomain.UnhandledException += (a, b) =>
            {
                Logger.Instance.Error(b.ExceptionObject + "");
            };

            Logger.Instance.PaddingWidth = 10;
            Logger.Instance.Info("正在启动...");

            ServiceCollection serviceCollection = new ServiceCollection();
            //注入 依赖注入服务供应 使得可以在别的地方通过注入的方式获得 ServiceProvider 以用来获取其它服务
            serviceCollection.AddSingleton((e) => serviceProvider);


            //加载插件程序集，当单文件发布或者动态加载dll外部插件时需要，否则如果本程序集没有显式的使用它的相关内容的话，会加载不出来
            //可以改为从dll文件加载
            Assembly[] assemblys = new Assembly[] {
                 typeof(ClientServer).Assembly,
                typeof(LoggerClientService).Assembly,
                typeof(PunchHoleMessenger).Assembly,

                typeof(ProxyMessenger).Assembly,

                typeof(ForwardClientService).Assembly,
                typeof(ServerForwardClientService).Assembly,

               // typeof(HttpProxyClientService).Assembly,
                //typeof(ServerHttpProxyClientService).Assembly,

                //typeof(Socks5ClientService).Assembly,
                //typeof(ServerSocks5ClientService).Assembly,

                //typeof(VeaClientService).Assembly,

                typeof(UsersClientService).Assembly,
                typeof(ServerUsersClientService).Assembly,

                typeof(ProxyClientService).Assembly,

                //以下是为了获取信息
                typeof(common.server.model.SignInMessengerIds).Assembly,
                typeof(ProxyMessengerIds).Assembly,
                typeof(common.httpProxy.HttpProxyMessengerIds).Assembly,
               // typeof(common.socks5.Socks5MessengerIds).Assembly,
                typeof(common.forward.ForwardMessengerIds).Assembly,

                typeof(UsersMessengerIds).Assembly,
            }.Concat(AppDomain.CurrentDomain.GetAssemblies()).ToArray();


            IPlugin[] plugins = PluginLoader.LoadBefore(serviceCollection, assemblys);

            //覆盖几个实现，由于平台实现不一样
            serviceCollection.AddTransient(typeof(IConfigDataProvider<>), typeof(ConfigDataFileProvider<>));
            serviceCollection.AddSingleton<IWebServerFileReader, WebServerFileReader>();
            serviceCollection.AddSingleton<IIPv6AddressRequest, IPv6AddressRequest>();

            serviceProvider = serviceCollection.BuildServiceProvider();
            PluginLoader.LoadAfter(plugins, serviceProvider, assemblys);

            SignInStateInfo signInStateInfo = serviceProvider.GetService<SignInStateInfo>();
            Logger.Instance.Warning(string.Empty.PadRight(Logger.Instance.PaddingWidth, '='));
            Logger.Instance.Info("获取外网距离ing...");
            signInStateInfo.LocalInfo.RouteLevel = NetworkHelper.GetRouteLevel(out List<IPAddress> ips);
            signInStateInfo.LocalInfo.RouteIps = ips.ToArray();
            Logger.Instance.Info($"外网距离:{signInStateInfo.LocalInfo.RouteLevel}");
            Logger.Instance.Warning(string.Empty.PadRight(Logger.Instance.PaddingWidth, '='));

            Logger.Instance.Warning(string.Empty.PadRight(Logger.Instance.PaddingWidth, '='));
            Logger.Instance.Warning("没什么报红的，就说明运行成功了");
            Logger.Instance.Warning(string.Empty.PadRight(Logger.Instance.PaddingWidth, '='));


            var config = serviceProvider.GetService<Config>();
            //自动注册
            if (config.Client.AutoReg)
            {
                serviceProvider.GetService<ISignInTransfer>().SignIn(config.Client.AutoReg);
            }
        }
    }
}