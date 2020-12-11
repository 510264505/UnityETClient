using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public class GameTypeConfig
    {
        public int EnumId { get; set; }
        public string NameEN { get; set; }
        public string NameZN { get; set; }
        public string Url { get; set; }
        public string LoginUrl { get; set; }
        public string CodeUrl { get; set; }
        public string IndexUrl { get; set; }
        public string LoseWinUrl { get; set; }
        public string ResultUrl { get; set; }
        public string CountDownUrl { get; set; }
        public string OddsUrl0 { get; set; } //赛车有三个
        public string OddsUrl1 { get; set; }
        public string OddsUrl2 { get; set; }
        public string BetsUrl { get; set; }
        public string LogOutUrl { get; set; }
    }
    public class GameRankingNameConfig
    {
        public List<string> Num = new List<string>();
    }
    public static class BetNameType
    {
        public static string None { get; } = "空";
        public static string Big { get; } = "大";
        public static string Small { get; } = "小";
        public static string Single { get; } = "单";
        public static string Double { get; } = "双";
        public static string Dragon { get; } = "龙";
        public static string Tiger { get; } = "虎";
        public static string Zero { get; } = "0";
        public static string One { get; } = "1";
        public static string Two { get; } = "2";
        public static string Three { get; } = "3";
        public static string Four { get; } = "4";
        public static string Five { get; } = "5";
        public static string Six { get; } = "6";
        public static string Seven { get; } = "7";
        public static string Eight { get; } = "8";
        public static string Nine { get; } = "9";
        public static string Tan { get; } = "10";
        public static string Eleven { get; } = "11";
        public static string Twelve { get; } = "12";
        public static string Thirteen { get; } = "13";
        public static string Fourteen { get; } = "14";
        public static string Fifteen { get; } = "15";
        public static string Sixteen { get; } = "16";
        public static string Seventeen { get; } = "17";
        public static string Eighteen { get; } = "18";
        public static string Nineteen { get; } = "19";
        public static string Tie { get; } = "和";
    }

    [Flags]
    public enum BetBit : uint
    {
        None = 0x0,
        Big = 0x1,
        Small = 0x2,
        Single = 0x4,
        Double = 0x8,
        Dragon = 0x10,
        Tiger = 0x20,
        Zero = 0x40,
        One = 0x80,
        Two = 0x100,
        Three = 0x200,
        Four = 0x400,
        Five = 0x800,
        Six = 0x1000,
        Seven = 0x2000,
        Eight = 0x4000,
        Nine = 0x8000,
        Tan = 0x10000,
        Eleven = 0x20000,
        Twelve = 0x40000,
        Thirteen = 0x80000,
        Fourteen = 0x100000,
        Fifteen = 0x200000,
        Sixteen = 0x400000,
        Seventeen = 0x800000,
        Eighteen = 0x1000000,
        Nineteen = 0x2000000,
        Tie = 0x4000000,
    }

    /// <summary>
    /// 用于拼接的字符串及数字
    /// </summary>
    public static class BetBitRule
    {
        public static Dictionary<BetBit, StringBuilder> betBitGame = new Dictionary<BetBit, StringBuilder>()
        {
            { BetBit.Big, new StringBuilder("DX_D") }, //冠亚和拼接 G
            { BetBit.Small, new StringBuilder("DX_X") },
            { BetBit.Single, new StringBuilder("DS_D") },
            { BetBit.Double, new StringBuilder("DS_S") },
            { BetBit.Dragon, new StringBuilder("LH_L") },
            { BetBit.Tiger, new StringBuilder("LH_H") },
            { BetBit.Zero, new StringBuilder("B_0") },
            { BetBit.One, new StringBuilder("B_1") },
            { BetBit.Two, new StringBuilder("B_2") },
            { BetBit.Three, new StringBuilder("B_3") },
            { BetBit.Four, new StringBuilder("B_4") },
            { BetBit.Five, new StringBuilder("B_5") },
            { BetBit.Six, new StringBuilder("B_6") },
            { BetBit.Seven, new StringBuilder("B_7") },
            { BetBit.Eight, new StringBuilder("B_8") },
            { BetBit.Nine, new StringBuilder("B_9") },
            { BetBit.Tan, new StringBuilder("B_10") },
            { BetBit.Tie, new StringBuilder("LH_T") },
        };
        public static Dictionary<BetBit, byte> betConditions = new Dictionary<BetBit, byte>()
        {
            { BetBit.Big, 21 },
            { BetBit.Small, 22 },
            { BetBit.Single, 23 },
            { BetBit.Double, 24 },
            { BetBit.Dragon, 25 },
            { BetBit.Tiger, 26 },
            { BetBit.Zero, 0 },
            { BetBit.One, 1 },
            { BetBit.Two, 2 },
            { BetBit.Three, 3 },
            { BetBit.Four, 4 },
            { BetBit.Five, 5 },
            { BetBit.Six, 6 },
            { BetBit.Seven, 7 },
            { BetBit.Eight, 8 },
            { BetBit.Nine, 9 },
            { BetBit.Tan, 10 },
            { BetBit.Eleven, 11 },
            { BetBit.Twelve, 12 },
            { BetBit.Thirteen, 13 },
            { BetBit.Fourteen, 14 },
            { BetBit.Fifteen, 15 },
            { BetBit.Sixteen, 16 },
            { BetBit.Seventeen, 17 },
            { BetBit.Eighteen, 18 },
            { BetBit.Nineteen, 19 },
            { BetBit.Tie, 20 },
        };
        public static Dictionary<int, string> betSCTypeName = new Dictionary<int, string>()
        {
            { 21, "GDX_D" },
            { 22, "GDX_X" },
            { 23, "GDS_D" },
            { 24, "GDS_S" },
        };
        public static Dictionary<string, string> betSSCTypeName = new Dictionary<string, string>()
        {
            { "DX_D", "ZDX_D" },
            { "DX_X", "ZDX_X" },
            { "DS_D", "ZDS_D" },
            { "DS_S", "ZDS_S" },
            { "LH_L", "LH_L" },
            { "LH_H", "LH_H" },
            { "LH_T", "LH_T" },
        };
        public static Dictionary<string, string> betRankName = new Dictionary<string, string>()
        {
            { "DX_D", "大" },
            { "DX_X", "小" },
            { "DS_D", "单" },
            { "DS_S", "双" },
            { "LH_L", "龙" },
            { "LH_H", "虎" },
            { "LH_T", "和" },
            { "B_0", "0" }, //球才有
            { "B_1", "1" },
            { "B_2", "2" },
            { "B_3", "3" },
            { "B_4", "4" },
            { "B_5", "5" },
            { "B_6", "6" },
            { "B_7", "7" },
            { "B_8", "8" },
            { "B_9", "9" },
            { "B_10", "10" }, //车才有
            { "GDX_D", "大" },
            { "GDX_X", "小" },
            { "GDS_D", "单" },
            { "GDS_S", "双" },
            { "GYH_3", "军和 3" },
            { "GYH_4", "军和 4" },
            { "GYH_5", "军和 5" },
            { "GYH_6", "军和 6" },
            { "GYH_7", "军和 7" },
            { "GYH_8", "军和 8" },
            { "GYH_9", "军和 9" },
            { "GYH_10", "军和 10" },
            { "GYH_11", "军和 11" },
            { "GYH_12", "军和 12" },
            { "GYH_13", "军和 13" },
            { "GYH_14", "军和 14" },
            { "GYH_15", "军和 15" },
            { "GYH_16", "军和 16" },
            { "GYH_17", "军和 17" },
            { "GYH_18", "军和 18" },
            { "GYH_19", "军和 19" },
            { "ZDX_D", "总和大" },
            { "ZDX_X", "总和小" },
            { "ZDS_D", "总和单" },
            { "ZDS_S", "总和双" },
        };
    }
}
