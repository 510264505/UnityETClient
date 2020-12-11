namespace ETHotfix
{
    public static partial class CPABName
    {
        public const string Prefab = "cp_prefabs.unity3d";
        public const string Textrue2D = "cp_textrue2d.unity3d";
    }
    public static partial class CPUIType
    {
        public const string UILoginCanvas = "UILoginCanvas";
        public const string UIGameCanvas = "UIGameCanvas";
        public const string UIMessageCanvas = "UIMessageCanvas";
        public const string BallHistory = "BallHistory";
        public const string BetsOptionPanel = "BetsOptionPanel";
        public const string BetsToggle = "BetsToggle";
        public const string ConditionBtn = "ConditionBtn";
        public const string GamePanel = "GamePanel";
        public const string GameSelectBtn = "GameSelectBtn";
        public const string HelpDecPanel = "HelpDecPanel";
        public const string InputBetsInfo = "InputBetsInfo";
        public const string OnBetsConfig = "OnBetsConfig";
        public const string RacingCarHistory = "RacingCarHistory";
        public const string UniteBetsOptionPanel = "UniteBetsOptionPanel";
        public const string HistoryRecord = "HistoryRecord";
        public const string DataInfo = "DataInfo";
        public const string MessageBox1 = "MessageBox1";
        public const string MessageBox0 = "MessageBox0";
    }


    /// <summary>
    /// 游戏类型
    /// </summary>
    public enum GameType : int
    {
        BJPK10,
        PK10JSC,
        LUCKYSB,
        SGFT,
        SSCJSC,
        XYFT,
    }
    /// <summary>
    /// 游戏种类
    /// </summary>
    public enum GameKind : int
    {
        SC,
        SSC,
    }
    /// <summary>
    /// 下注或条件输入
    /// </summary>
    public enum InputBetType : int
    {
        Condition,
        Bet,
    }
}
