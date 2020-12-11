using System.Collections.Generic;
using UnityEngine;
using ETModel;
using System;
using System.Linq;

namespace ETHotfix
{
    [ObjectSystem]
    public class GameConfigComponentAwake : AwakeSystem<GameConfigComponent>
    {
        public override void Awake(GameConfigComponent self)
        {
            self.Awake();
        }
    }
    [ObjectSystem]
    public class GameConfigComponentLoad : LoadSystem<GameConfigComponent>
    {
        public override void Load(GameConfigComponent self)
        {
            self.Load();
        }
    }
    /// <summary>
    /// 扫描所有的游戏配置特性
    /// </summary>
    public class GameConfigComponent : Component
    {
        private Dictionary<string, GameTypeConfig> gameTypeConfigs = new Dictionary<string, GameTypeConfig>();
        private Dictionary<string, GameRankingNameConfig> gameRankingNameConfig = new Dictionary<string, GameRankingNameConfig>();
        public void Awake()
        {
            this.Load();
        }
        public void Load()
        {
            this.gameTypeConfigs.Clear();
            TextAsset assetType = (TextAsset)ETModel.Game.Scene.GetComponent<ResourcesComponent>().GetAsset("config.unity3d", typeof(GameTypeConfig).Name);
            this.gameTypeConfigs = JsonHelper.FromJson<Dictionary<string, GameTypeConfig>>(assetType.text);

            this.gameRankingNameConfig.Clear();
            TextAsset assetName = (TextAsset)ETModel.Game.Scene.GetComponent<ResourcesComponent>().GetAsset("config.unity3d", typeof(GameRankingNameConfig).Name);
            this.gameRankingNameConfig = JsonHelper.FromJson<Dictionary<string, GameRankingNameConfig>>(assetName.text);
        }


        public GameTypeConfig GetGameTypeConfig(string gameName)
        {
            GameTypeConfig config;
            if (!this.gameTypeConfigs.TryGetValue(gameName, out config))
            {
                throw new Exception($"not find key:{gameName}");
            }
            return config;
        }
        public GameTypeConfig TryGetGameTypeConfig(string gameName)
        {
            GameTypeConfig config;
            if (!this.gameTypeConfigs.TryGetValue(gameName, out config))
            {
                return null;
            }
            return config;
        }
        public GameTypeConfig[] GetAllGameTypeConfig()
        {
            return this.gameTypeConfigs.Values.ToArray();
        }

        public GameRankingNameConfig GetGameRankingNameConfig(string gameName)
        {
            GameRankingNameConfig config;
            if (!this.gameRankingNameConfig.TryGetValue(gameName, out config))
            {
                throw new Exception($"not find key:{gameName}");
            }
            return config;
        }

        public GameRankingNameConfig TryGetGameRankingNameConfig(string gameName)
        {
            GameRankingNameConfig config;
            if (!this.gameRankingNameConfig.TryGetValue(gameName, out config))
            {
                return null;
            }
            return config;
        }
        public GameRankingNameConfig[] GetAllGameRankingNameConfig()
        {
            return this.gameRankingNameConfig.Values.ToArray();
        }
    }
}

