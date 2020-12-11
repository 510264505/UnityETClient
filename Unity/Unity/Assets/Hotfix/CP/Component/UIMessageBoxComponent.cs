using System;
using System.Collections.Generic;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UIMessageBoxComponentAwake : AwakeSystem<UIMessageBoxComponent>
    {
        public override void Awake(UIMessageBoxComponent self)
        {
            self.Awake();
        }
    }
    public class UIMessageBoxComponent : Component
    {
        public class MessageBoxs
        {
            public GameObject messageBox;
            public Text tipsMsg;
            public Button comfirmBtn;
            public Button cancelBtn;
            private Dictionary<string, string> errorCode = new Dictionary<string, string>()
            {
                {"3", "登录失败，验证码错误！" },
                {"4", "登录失败，账号或密码错误！" },
                {"5", "投注失败，请从新投注！" },
                {"10000", "此游戏已关盘，请切换其他游戏!" },
            };
            public MessageBoxs()
            {
                ResourcesComponent res = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                res.LoadBundle(CPABName.Prefab);
                messageBox = UnityEngine.Object.Instantiate((GameObject)res.GetAsset(CPABName.Prefab, CPUIType.MessageBox1));
                res.UnloadBundle(CPABName.Prefab);
                messageBox.transform.SetParent(Game.Scene.GetComponent<UIComponent>().Get(CPUIType.UIMessageCanvas).GameObject.transform, false);
                tipsMsg = messageBox.transform.Find("Box/TipsMsg").GetComponent<Text>();
                comfirmBtn = messageBox.transform.Find("Box/ComfirmBtn").GetComponent<Button>();
            }
            /// <summary>
            /// 错误提示
            /// </summary>
            public void InitBox(string code)
            {
                tipsMsg.text = code;
                comfirmBtn.onClick.Add(Destroy); // 确定退出
                messageBox.SetActive(true);
            }
            /// <summary>
            /// 验证失败，用户名或密码错误
            /// </summary>
            public void InitBox1(string code, Action<string> action = null)
            {
                if (errorCode.ContainsKey(code))
                {
                    tipsMsg.text = errorCode[code];
                }
                else
                {
                    tipsMsg.text = "未知错误" + code;
                }
                comfirmBtn.onClick.Add(() => { // 确定退出
                    action?.Invoke(code); //账号或密码不正确，清空密码
                    Destroy();
                });
                messageBox.SetActive(true);
            }
            private void Destroy()
            {
                // 同时弹两个窗的时候，有一个窗口被隐藏了，所以点击不到
                messageBox.SetActive(false);
                comfirmBtn.onClick.RemoveAllListeners();
                Game.Scene.GetComponent<UIComponent>().Get(CPUIType.UIMessageCanvas).GetComponent<UIMessageBoxComponent>().messageBoxs.Enqueue(this);
            }
        }
        public class MessageWindows
        {
            public GameObject messageBox;
            public Text tipsMsg;
            public Button comfirmBtn;
            public Button cancelBtn;
            public MessageWindows()
            {
                ResourcesComponent res = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                res.LoadBundle(CPABName.Prefab);
                messageBox = UnityEngine.Object.Instantiate((GameObject)res.GetAsset(CPABName.Prefab, CPUIType.MessageBox0));
                res.UnloadBundle(CPABName.Prefab);
                messageBox.transform.SetParent(Game.Scene.GetComponent<UIComponent>().Get(CPUIType.UIMessageCanvas).GameObject.transform, false);
                tipsMsg = messageBox.transform.Find("Box/TipsMsg").GetComponent<Text>();
                comfirmBtn = messageBox.transform.Find("Box/ComfirmBtn").GetComponent<Button>();
                cancelBtn = messageBox.transform.Find("Box/CancelBtn").GetComponent<Button>();
            }
            public void InitBox(string message, Action action = null)
            {
                tipsMsg.text = message;
                comfirmBtn.onClick.Add(() => { // 确定退出
                    action?.Invoke();
                    Destroy();
                });
                cancelBtn.onClick.Add(Destroy);// 取消退出
                messageBox.SetActive(true);
            }
            private void Destroy()
            {
                comfirmBtn.onClick.RemoveAllListeners();
                cancelBtn.onClick.RemoveAllListeners();
                messageBox.SetActive(false);
                Game.Scene.GetComponent<UIComponent>().Get(CPUIType.UIMessageCanvas).GetComponent<UIMessageBoxComponent>().messageWindows.Enqueue(this);
            }
        }

        private Queue<MessageBoxs> messageBoxs = new Queue<MessageBoxs>();
        private Queue<MessageWindows> messageWindows = new Queue<MessageWindows>();
        
        public void Awake()
        {

        }
        private void ShowMessage()
        {
            Game.Scene.GetComponent<UIComponent>().Get(CPUIType.UIMessageCanvas).GameObject.transform.SetAsLastSibling();
        }
        public void MessageBox(string message, Action<string> action = null)
        {
            this.ShowMessage();
            MessageBoxs mb;
            if (messageBoxs.Count > 0)
            {
                mb = messageBoxs.Dequeue();
            }
            else
            {
                mb = new MessageBoxs();
            }
            mb.InitBox1(message, action);
        }
        public void MessageBox(string message)
        {
            this.ShowMessage();
            MessageBoxs mb;
            if (messageBoxs.Count > 0)
            {
                mb = messageBoxs.Dequeue();
            }
            else
            {
                mb = new MessageBoxs();
            }
            mb.InitBox(message);
        }
        public void MessageWindow(string message, Action action = null)
        {
            this.ShowMessage();
            MessageWindows mw;
            if (messageWindows.Count > 0)
            {
                mw = messageWindows.Dequeue();
            }
            else
            {
                mw = new MessageWindows();
            }
            mw.InitBox(message, action);
        }
    }
}
