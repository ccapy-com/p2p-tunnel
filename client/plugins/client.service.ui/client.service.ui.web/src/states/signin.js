import { provide, inject, reactive } from "vue";
import { websocketState } from '../apis/request'
import { getSignInInfo } from '../apis/signin'
import { shareData } from './shareData'

const provideSignInKey = Symbol();
export const provideSignIn = () => {
    const state = reactive({
        ClientConfig: {
            ShortId: 0,
            GroupId: '',
            GroupIds: [],
            Name: '',
            Account: '',
            Password: '',
            AutoReg: false,
            Encode: false,
            EncodePassword: "",
            UsePunchHole: false,
            TimeoutDelay: 20000,
            UseUdp: false,
            UseTcp: false,
            UseRelay: false,
            UseReConnect: false,
            UdpUploadSpeedLimit: 0,
            HighConfig: false,
            UIPassword: '',
        },
        ServerConfig: {
            Ip: '',
            UdpPort: 0,
            TcpPort: 0,
            Encode: false,
            EncodePassword: "",
            Items: []
        },
        LocalInfo: {
            IsConnecting: false,
            Connected: false
        },
        RemoteInfo: {
            Ip: '',
            ConnectId: 0,
            Access: 0,
            NetFlow: 0,
            EndTime: new Date(),
            Relay: false,
        }
    });
    provide(provideSignInKey, state);

    const fn = () => {
        if (websocketState.connected) {
            getSignInInfo().then((json) => {
                state.LocalInfo.Connected = json.LocalInfo.Connected;

                state.ClientConfig.ShortId = json.ClientConfig.ShortId;
                state.ClientConfig.Name = json.ClientConfig.Name;
                state.ClientConfig.Args = json.ClientConfig.Args;
                state.ClientConfig.GroupIds = json.ClientConfig.GroupIds;
                state.ClientConfig.UseUdp = json.ClientConfig.UseUdp;
                state.ClientConfig.UseTcp = json.ClientConfig.UseTcp;
                state.ClientConfig.UseRelay = json.ClientConfig.UseRelay;
                state.ClientConfig.UdpUploadSpeedLimit = json.ClientConfig.UdpUploadSpeedLimit;
                state.ClientConfig.UIPassword = json.ClientConfig.UIPassword;


                state.ClientConfig.UsePunchHole = json.ClientConfig.UsePunchHole;
                state.ClientConfig.TimeoutDelay = json.ClientConfig.TimeoutDelay;

                state.RemoteInfo.Ip = json.RemoteInfo.Ip;
                state.RemoteInfo.ConnectId = json.RemoteInfo.ConnectId;
                state.RemoteInfo.Access = json.RemoteInfo.Access;
                state.RemoteInfo.Relay = shareData.serverAccessHasRelay(json.RemoteInfo.Access);

                state.LocalInfo.IsConnecting = json.LocalInfo.IsConnecting;
                if (state.ClientConfig.ShortId == 0) {
                    state.ClientConfig.ShortId = json.ClientConfig.ShortId;
                }
                if (!state.ClientConfig.GroupId) {
                    state.ClientConfig.GroupId = json.ClientConfig.GroupId;
                }
                if (!state.ServerConfig.Ip) {
                    state.ServerConfig.Ip = json.ServerConfig.Ip;
                    state.ServerConfig.UdpPort = json.ServerConfig.UdpPort;
                    state.ServerConfig.TcpPort = json.ServerConfig.TcpPort;
                }

                setTimeout(fn, 1000);
            }).catch(() => {
                setTimeout(fn, 1000);
            });
        } else {
            state.LocalInfo.Connected = false;
            state.LocalInfo.IsConnecting = false;

            state.RemoteInfo.Access = 0xffffffff;
            state.RemoteInfo.Relay = shareData.serverAccessHasRelay(state.RemoteInfo.Access);

            setTimeout(fn, 1000);
        }
    }
    fn();
}
export const injectSignIn = () => {
    return inject(provideSignInKey);
}