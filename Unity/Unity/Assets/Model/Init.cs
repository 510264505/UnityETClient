using System;
using System.Threading;
using UnityEngine;

namespace ETModel
{
	public class Init : MonoBehaviour
	{
		private void Start()
		{
            Screen.sleepTimeout = SleepTimeout.NeverSleep; //保持唤醒
            this.StartAsync().Coroutine();
		}
		
		private async ETVoid StartAsync()
		{
			try
			{
				SynchronizationContext.SetSynchronizationContext(OneThreadSynchronizationContext.Instance);

				DontDestroyOnLoad(gameObject);
                //通过反射机制，获取特性来加载DLL里面的类
				Game.EventSystem.Add(DLLType.Model, typeof(Init).Assembly);

				Game.Scene.AddComponent<TimerComponent>();
                //全局配置组件（热更新服务器地址等）
				Game.Scene.AddComponent<GlobalConfigComponent>();
                //网络连接组件（跟服务器通信）
				Game.Scene.AddComponent<NetOuterComponent>();
                //资源管理组件（热更新资源管理，AB包）
				Game.Scene.AddComponent<ResourcesComponent>();
				Game.Scene.AddComponent<PlayerComponent>();
				Game.Scene.AddComponent<UnitComponent>();
                //UI管理组件
				Game.Scene.AddComponent<UIComponent>();
                //协程管理组件
                Game.Scene.AddComponent<CoroutineComponent, Init>(this);

				// 下载ab包
				await BundleHelper.DownloadBundle();
                // 加载热更新代码DLL
				Game.Hotfix.LoadHotfixAssembly();

				// 加载配置
				Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("config.unity3d");
				Game.Scene.AddComponent<ConfigComponent>();
				Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle("config.unity3d");
                //协议类型组件，标识码
				Game.Scene.AddComponent<OpcodeTypeComponent>();
                //消息分发组件
				Game.Scene.AddComponent<MessageDispatcherComponent>();
                ////保存User信息及ID
                //Game.Scene.AddComponent<ClientComponent>();
                ////打印到本地日志组件
                //Game.Scene.AddComponent<LogComponent>();
                //安卓手机状态栏组件
                Game.Scene.AddComponent<AndroidStatusBarComponent>();
                //执行热更，热更层的入口
				Game.Hotfix.GotoHotfix();

				Game.EventSystem.Run(EventIdType.TestHotfixSubscribMonoEvent, "TestHotfixSubscribMonoEvent");

                //手动下载AB包
                Game.Scene.AddComponent<BundleSingleDownloaderComponent>();
            }
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		private void Update()
		{
			OneThreadSynchronizationContext.Instance.Update();
			Game.Hotfix.Update?.Invoke();
			Game.EventSystem.Update();
		}

		private void LateUpdate()
		{
			Game.Hotfix.LateUpdate?.Invoke();
			Game.EventSystem.LateUpdate();
		}

		private void OnApplicationQuit()
		{
			Game.Hotfix.OnApplicationQuit?.Invoke();
			Game.Close();
		}

        // 转到前台的委托事件
        public Action OnToReception;
        /// <summary>
        /// true: 游戏所有物体暂停
        /// false: 游戏取消暂停
        /// </summary>
        private void OnApplicationPause(bool isPause)
        {
            Debug.Log("当玩家暂停时发送到所有的游戏物体。" + isPause);
            if (!isPause)
            {
                OnToReception?.Invoke();
            }
        }
    }
}