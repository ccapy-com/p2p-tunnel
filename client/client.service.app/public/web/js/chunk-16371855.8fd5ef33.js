(window["webpackJsonp"]=window["webpackJsonp"]||[])?.Invoke([["chunk-16371855"],{1581:function(e,t,c){"use strict";c("d37a")},3476:function(e,t,c){"use strict";c.r(t);c("b0c0");var n=c("7a23"),a=function(e){return Object(n["pushScopeId"])("data-v-9aace542"),e=e(),Object(n["popScopeId"])(),e},o={class:"socks5-wrap"},r={class:"title t-c"},l={class:"form"},u={class:"w-100"},i={class:"w-100"},s={class:"w-100 t-c",style:{"line-height":"1.8rem"}},d=a((function(){return Object(n["createElementVNode"])("p",null,"自动设置代理有可能失败，可以手动配置系统代理“使用设置脚本”",-1)})),b=Object(n["createTextVNode"])("预置pac规则文件地址 "),f=Object(n["createTextVNode"])("自定义pac规则文件地址 "),m={class:"w-100 t-c"},O=Object(n["createTextVNode"])("确 定"),j=Object(n["createTextVNode"])("配置插件"),p={class:"w-100"};function V(e,t,c,a,V,N){var x=Object(n["resolveComponent"])("el-alert"),h=Object(n["resolveComponent"])("el-input"),C=Object(n["resolveComponent"])("el-form-item"),w=Object(n["resolveComponent"])("el-col"),g=Object(n["resolveComponent"])("el-option"),v=Object(n["resolveComponent"])("el-select"),_=Object(n["resolveComponent"])("el-row"),k=Object(n["resolveComponent"])("el-checkbox"),E=Object(n["resolveComponent"])("el-tooltip"),P=Object(n["resolveComponent"])("el-button"),S=Object(n["resolveComponent"])("ConfigureModal"),I=Object(n["resolveComponent"])("el-form");return Object(n["openBlock"])(),Object(n["createElementBlock"])("div",o,[Object(n["createElementVNode"])("h3",r,Object(n["toDisplayString"])(e.$route.meta.name),1),Object(n["createVNode"])(x,{class:"alert",type:"warning","show-icon":"",closable:!1,title:"socks5代理，如果服务端开启，则也可以代理到服务端",description:"适用于ftp双通道"}),Object(n["createElementVNode"])("div",l,[Object(n["createVNode"])(I,{ref:"formDom",model:a.state.form,rules:a.state.rules,"label-width":"80px"},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(C,{label:"","label-width":"0"},{default:Object(n["withCtx"])((function(){return[Object(n["createElementVNode"])("div",u,[Object(n["createVNode"])(_,{gutter:10},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(w,{xs:24,sm:6,md:6,lg:6,xl:6},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(C,{label:"监听端口",prop:"ListenPort"},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(h,{modelValue:a.state.form.ListenPort,"onUpdate:modelValue":t[0]||(t[0]=function(e){return a.state.form.ListenPort=e})},null,8,["modelValue"])]})),_:1})]})),_:1}),Object(n["createVNode"])(w,{xs:24,sm:6,md:6,lg:6,xl:6},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(C,{label:"buffersize",prop:"BufferSize"},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(h,{modelValue:a.state.form.BufferSize,"onUpdate:modelValue":t[1]||(t[1]=function(e){return a.state.form.BufferSize=e})},null,8,["modelValue"])]})),_:1})]})),_:1}),Object(n["createVNode"])(w,{xs:24,sm:6,md:6,lg:6,xl:6},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(C,{label:"目标端",prop:"TargetName"},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(v,{modelValue:a.state.form.TargetName,"onUpdate:modelValue":t[2]||(t[2]=function(e){return a.state.form.TargetName=e}),placeholder:"选择目标"},{default:Object(n["withCtx"])((function(){return[(Object(n["openBlock"])(!0),Object(n["createElementBlock"])(n["Fragment"],null,Object(n["renderList"])(a.targets,(function(e,t){return Object(n["openBlock"])(),Object(n["createBlock"])(g,{key:t,label:e.label,value:e.Name},null,8,["label","value"])})),128))]})),_:1},8,["modelValue"])]})),_:1})]})),_:1})]})),_:1})])]})),_:1}),Object(n["createVNode"])(C,{label:"","label-width":"0"},{default:Object(n["withCtx"])((function(){return[Object(n["createElementVNode"])("div",i,[Object(n["createVNode"])(_,{gutter:10},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(w,{xs:12,sm:6,md:6,lg:6,xl:6},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(C,{"label-width":"0",prop:"ListenEnable"},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(E,{class:"box-item",effect:"dark",content:"不勾选表示关闭socks5监听",placement:"top-start"},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(k,{modelValue:a.state.form.ListenEnable,"onUpdate:modelValue":t[3]||(t[3]=function(e){return a.state.form.ListenEnable=e}),label:"开启监听"},null,8,["modelValue"])]})),_:1})]})),_:1})]})),_:1}),Object(n["createVNode"])(w,{xs:12,sm:6,md:6,lg:6,xl:6},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(C,{"label-width":"0",prop:"ConnectEnable"},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(E,{class:"box-item",effect:"dark",content:"作为目标端时，是否允许被访问",placement:"top-start"},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(k,{modelValue:a.state.form.ConnectEnable,"onUpdate:modelValue":t[4]||(t[4]=function(e){return a.state.form.ConnectEnable=e}),label:"允许访问"},null,8,["modelValue"])]})),_:1})]})),_:1})]})),_:1}),Object(n["createVNode"])(w,{xs:12,sm:6,md:6,lg:6,xl:6},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(C,{"label-width":"0",prop:"IsPac"},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(E,{class:"box-item",effect:"dark",content:"勾选则设置系统代理，不勾选则需要自己设置",placement:"top-start"},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(k,{modelValue:a.state.form.IsPac,"onUpdate:modelValue":t[5]||(t[5]=function(e){return a.state.form.IsPac=e}),label:"设置系统代理"},null,8,["modelValue"])]})),_:1})]})),_:1})]})),_:1}),Object(n["createVNode"])(w,{xs:12,sm:6,md:6,lg:6,xl:6},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(C,{"label-width":"0",prop:"IsCustomPac"},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(E,{class:"box-item",effect:"dark",content:"自定义pac还是使用预制的pac规则",placement:"top-start"},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(k,{modelValue:a.state.form.IsCustomPac,"onUpdate:modelValue":t[6]||(t[6]=function(e){return a.state.form.IsCustomPac=e}),label:"自定义pac"},null,8,["modelValue"])]})),_:1})]})),_:1})]})),_:1})]})),_:1})])]})),_:1}),Object(n["createVNode"])(C,{"label-width":"0"},{default:Object(n["withCtx"])((function(){return[Object(n["createElementVNode"])("div",s,[d,Object(n["createElementVNode"])("p",null,[b,Object(n["createElementVNode"])("strong",null,Object(n["toDisplayString"])(a.state.localtion)+"/socks.pac",1)]),Object(n["createElementVNode"])("p",null,[f,Object(n["createElementVNode"])("strong",null,Object(n["toDisplayString"])(a.state.localtion)+"/socks-custom.pac",1)])])]})),_:1}),Object(n["createVNode"])(C,{"label-width":"0"},{default:Object(n["withCtx"])((function(){return[Object(n["createElementVNode"])("div",m,[Object(n["createVNode"])(P,{type:"primary",loading:a.state.loading,onClick:a.handleSubmit,class:"m-r-1"},{default:Object(n["withCtx"])((function(){return[O]})),_:1},8,["loading","onClick"]),Object(n["createVNode"])(S,{className:"Socks5ClientConfigure"},{default:Object(n["withCtx"])((function(){return[Object(n["createVNode"])(P,null,{default:Object(n["withCtx"])((function(){return[j]})),_:1})]})),_:1})])]})),_:1}),Object(n["createVNode"])(C,{"label-width":"0",class:"hidden-xs-only",style:{"margin-bottom":"0"}},{default:Object(n["withCtx"])((function(){return[Object(n["createElementVNode"])("div",p,[Object(n["createVNode"])(h,{modelValue:a.state.pac,"onUpdate:modelValue":t[7]||(t[7]=function(e){return a.state.pac=e}),rows:16,type:"textarea",placeholder:"写pac内容",resize:"none"},null,8,["modelValue"])])]})),_:1})]})),_:1},8,["model","rules"])])])}c("99af"),c("d81d"),c("a9e3"),c("e9c4");var N=c("a1e9"),x=c("97af"),h=function(){return Object(x["b"])("socks5/get")},C=function(e){return Object(x["b"])("socks5/set",e)},w=function(){return Object(x["b"])("socks5/getpac")},g=function(e){return Object(x["b"])("socks5/setpac",e)},v=c("5c40"),_=c("3ef4"),k=c("3fd2"),E=c("8286"),P=c("49f5"),S={components:{ConfigureModal:P["a"]},setup:function(){var e=Object(k["a"])(),t=Object(E["a"])(),c=function(){h().then((function(e){r.form.ListenEnable=e.ListenEnable,r.form.ListenPort=e.ListenPort,r.form.BufferSize=e.BufferSize,r.form.ConnectEnable=e.ConnectEnable,r.form.IsCustomPac=e.IsCustomPac,r.form.IsPac=e.IsPac,r.form.TargetName=e.TargetName}))},n=function(){w().then((function(e){r.pac=e}))},a=function(){g({IsCustom:r.form.IsCustomPac,Pac:r.pac}).then((function(){}))};Object(v["rb"])((function(){c(),n()}));var o=Object(N["c"])((function(){return[{Name:"",label:"服务器"}].concat(e.clients.map((function(e){return{Name:e.Name,label:e.Name}})))})),r=Object(N["p"])({loading:!1,pac:"",localtion:window.location.origin,form:{ListenEnable:!1,ListenPort:5412,ConnectEnable:!1,IsPac:!1,IsCustomPac:!1,BufferSize:8192,TargetName:""},rules:{ListenPort:[{required:!0,message:"必填",trigger:"blur"},{type:"number",min:1,max:65535,message:"数字 1-65535",trigger:"blur",transform:function(e){return Number(e)}}],BufferSize:[{required:!0,message:"必填",trigger:"blur"},{type:"number",min:1,max:1048576,message:"数字 1-1048576",trigger:"blur",transform:function(e){return Number(e)}}]}}),l=Object(N["r"])(null),u=function(){l.value.validate((function(e){if(!e)return!1;r.loading=!0;var t=JSON.parse(JSON.stringify(r.form));t.ListenPort=Number(t.ListenPort),t.BufferSize=Number(t.BufferSize),console.log(t),C(t).then((function(){r.loading=!1,t.IsPac&&a(),_["a"].success("操作成功！")})).catch((function(e){r.loading=!1}))}))};return{targets:o,shareData:t,state:r,formDom:l,handleSubmit:u}}},I=(c("1581"),c("6b0d")),L=c.n(I);const y=L()(S,[["render",V],["__scopeId","data-v-9aace542"]]);t["default"]=y},"8e03":function(e,t,c){var n=c("24fb");t=n(!1),t?.Invoke([e.i,".socks5-wrap[data-v-9aace542]{padding:2rem}.alert[data-v-9aace542]{margin-bottom:1rem}.form[data-v-9aace542]{border:1px solid #eee;padding:2rem;border-radius:.4rem}@media screen and (max-width:768px){.el-col[data-v-9aace542]{margin-top:.6rem}}",""]),e.exports=t},d37a:function(e,t,c){var n=c("8e03");n.__esModule&&(n=n.default),"string"===typeof n&&(n=[[e.i,n,""]]),n.locals&&(e.exports=n.locals);var a=c("499e").default;a("d339ef5c",n,!0,{sourceMap:!1,shadowMode:!1})}}]);