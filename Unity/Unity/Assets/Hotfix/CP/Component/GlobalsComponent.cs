using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class GlobalsComponentAwake : AwakeSystem<GlobalsComponent>
    {
        public override void Awake(GlobalsComponent self)
        {
            self.Awake();
        }
    }
    public class GlobalsComponent : Component
    {
        // 当前游戏类型
        public GameType GameType { get; set; }
        // 当前游戏种类
        public GameKind GameKind { get; set; }
        // 输入下注类型
        public InputBetType InputBetType { get; set; }
        // 用户额度
        public float Amount { get; set; }
        // 未结算金额
        public float UnSettlement { get; set; }
        // 今日输赢
        public float TodayLoseWin { get; set; }
        // 最新一期记录的期号
        public string NewIssue { get; set; }
        // 下期开奖期号
        public string NextIssue { get; set; }
        // 当前游戏配置
        public GameTypeConfig Config { get; set; }
        // 是否已封盘
        public bool IsSeal { get; set; } = false;
        // 是否已更新至最新数据，可以下注
        public bool IsNewData { get; set; } = false;
        // 历史排名列表
        public List<int[]> RankingList { get; set; } = new List<int[]>();
        // 记录历史排名的数量
        public const int RankingLen = 5;
        // 游戏赔率
        public Dictionary<string, float> Odds { get; set; } = new Dictionary<string, float>();
        // 格式
        public string FloatStyle { get; } = "0.00000";
        // 所有可以下注名次的长度
        public int AllBetsRankLen { get; set; }
        // 结果地址
        public string ResultUrl { get; } = "https://chwm08.com/member/result_popup?lottery=";

        public void Awake()
        {
            
        }

        public void ChangeConfig(GameTypeConfig config)
        {
            this.Config = config;
            this.GameType = (GameType)config.EnumId;
            switch (this.GameType)
            {
                case GameType.BJPK10:
                    this.GameKind = GameKind.SC;
                    break;
                case GameType.PK10JSC:
                    this.GameKind = GameKind.SC;
                    break;
                case GameType.LUCKYSB:
                    this.GameKind = GameKind.SC;
                    break;
                case GameType.SGFT:
                    this.GameKind = GameKind.SC;
                    break;
                case GameType.SSCJSC:
                    this.GameKind = GameKind.SSC;
                    break;
                case GameType.XYFT:
                    this.GameKind = GameKind.SC;
                    break;
            }
            switch (this.GameKind)
            {
                case GameKind.SC:
                    this.AllBetsRankLen = 11;
                    break;
                case GameKind.SSC:
                    this.AllBetsRankLen = 6;
                    break;
            }
        }

        public void AddRanking(int[] nums)
        {
            RankingList.Add(nums);
            while (RankingCount > RankingLen)
            {
                RankingList.RemoveAt(0);
            }
        }
        public int[] GetRanking(int n)
        {
            int[] num = null;
            if (n < RankingCount)
            {
                num = RankingList[n];
            }
            return num;
        }
        public int RankingCount
        {
            get
            {
                return RankingList.Count;
            }
        }
    }
}
