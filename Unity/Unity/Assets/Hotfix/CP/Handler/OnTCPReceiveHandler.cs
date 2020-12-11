using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETModel;
using LitJson;

namespace ETHotfix
{
    [Event(MessageEventType.TCPOnReceive)]
    public class OnTCPReceiveHandler : AEvent<string>
    {
        public override void Run(string json)
        {
            //先解析出第一个命令，然后再执行事件
            //JsonData js = JsonMapper.ToObject(json);
            //Game.EventSystem.Run(js["cmd"].ToString(),js["data"]);
            //Game.Scene.GetComponent<UIComponent>().Get(CPUIType.UILoginCanvas).GetComponent<UICPLoginComponent>().Login(json);
        }
    }
}

