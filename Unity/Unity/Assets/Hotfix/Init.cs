using System;
using ETModel;

namespace ETHotfix
{
	public static class Init
	{
		public static void Start()
		{
#if ILRuntime
			if (!Define.IsILRuntime)
			{
				Log.Error("mono层是mono模式, 但是Hotfix层是ILRuntime模式");
			}
#else
			if (Define.IsILRuntime)
			{
				Log.Error("mono层是ILRuntime模式, Hotfix层是mono模式");
			}
#endif
			
			try
			{
				// 注册热更层回调
				ETModel.Game.Hotfix.Update = () => { Update(); };
				ETModel.Game.Hotfix.LateUpdate = () => { LateUpdate(); };
				ETModel.Game.Hotfix.OnApplicationQuit = () => { OnApplicationQuit(); };
				
				Game.Scene.AddComponent<UIComponent>();
				Game.Scene.AddComponent<OpcodeTypeComponent>();
				Game.Scene.AddComponent<MessageDispatcherComponent>();

                // 读取保存在本地的账号密码
                Game.Scene.AddComponent<AccountPasswordComponent>();
				// 加载热更配置
				ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("config.unity3d");
				Game.Scene.AddComponent<ConfigComponent>();
                // 加载游戏配置
                Game.Scene.AddComponent<GameConfigComponent>();
                ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle("config.unity3d");

				UnitConfig unitConfig = (UnitConfig)Game.Scene.GetComponent<ConfigComponent>().Get(typeof(UnitConfig), 1001);
				Log.Debug($"config {JsonHelper.ToJson(unitConfig)}");


                //GameTypeConfig gameConfig = (GameTypeConfig)Game.Scene.GetComponent<ConfigComponent>().Get(typeof(UnitConfig), 1);
                //Log.Debug($"Hotfix:{gameConfig}");
                //Log.Debug($"config {JsonHelper.ToJson(gameConfig)}");

                //Game.EventSystem.Run(EventIdType.InitSceneStart);

                Game.EventSystem.Run(CPEventIdType.CreateMessageUI); //初始化弹窗面板
                Game.EventSystem.Run(CPEventIdType.InitScensStart); //初始化UI

            }
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		public static void Update()
		{
			try
			{
				Game.EventSystem.Update();
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		public static void LateUpdate()
		{
			try
			{
				Game.EventSystem.LateUpdate();
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		public static void OnApplicationQuit()
		{
			Game.Close();
		}
	}
}