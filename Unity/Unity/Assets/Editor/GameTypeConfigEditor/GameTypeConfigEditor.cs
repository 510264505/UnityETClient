using System.IO;
using ETModel;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETEditor
{
    public class GameTypeConfigEditor : EditorWindow
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
        private const string path = @"./Assets/Res/Config/GameTypeConfig.txt";

        private const int LabelWidth = 100;
        private const int TextFieldWidth = 400;
        private Dictionary<string, GameTypeConfig> configs;
        private int selectedIndex = 0;
        private string curGameTypeName;
        private string newGameTypeName;
        

        [MenuItem("Tools/游戏类型配置")]
        public static void ShowWindow()
        {
            GetWindow<GameTypeConfigEditor>();
        }

        public void Awake()
        {
            if (File.Exists(path))
            {
                string fileContent = File.ReadAllText(path);
                if (string.IsNullOrEmpty(fileContent))
                {
                    this.configs = new Dictionary<string, GameTypeConfig>();
                }
                else
                {
                    this.configs = JsonHelper.FromJson<Dictionary<string, GameTypeConfig>>(File.ReadAllText(path));
                }
                Debug.Log(this.configs);
            }
            else
            {
                FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
                this.configs = new Dictionary<string, GameTypeConfig>();
            }
        }


        public void OnGUI()
        {
            if (this.configs.Count > 0)
            {
                GUILayout.BeginHorizontal();
                string[] gameNameArray = this.configs.Keys.ToArray();
                this.selectedIndex = EditorGUILayout.Popup(this.selectedIndex, gameNameArray);

                string lastName = this.curGameTypeName;
                this.curGameTypeName = gameNameArray[this.selectedIndex];
                if (lastName != curGameTypeName)
                {
                    this.newGameTypeName = curGameTypeName;
                    //切换了游戏类型，加载新的配置出来
                }
                //EditorGUILayout表示可编辑，GUILayout不可编辑
                this.newGameTypeName = EditorGUILayout.TextField("游戏类型英文名:", this.newGameTypeName);
                if (GUILayout.Button("添加"))
                {
                    if (string.IsNullOrEmpty(this.newGameTypeName))
                    {
                        Debug.LogError("请输入一个key的名称!");
                        return;
                    }
                    if (!this.configs.ContainsKey(this.newGameTypeName))
                    {
                        this.configs.Add(this.newGameTypeName, new GameTypeConfig());
                        this.selectedIndex = this.configs.Values.Count - 1;
                    }
                    else
                    {
                        Debug.LogError("已存在这个游戏类型!");
                    }
                }
                if (GUILayout.Button("删除"))
                {
                    if (!string.IsNullOrEmpty(gameNameArray[this.selectedIndex]))
                    {
                        this.configs.Remove(gameNameArray[this.selectedIndex]);
                        this.selectedIndex = 0;
                    }
                }

                GUILayout.EndHorizontal();

                GUILayout.BeginVertical();
                {
                    if (this.configs.ContainsKey(this.newGameTypeName))
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"EnumId:", GUILayout.Width(LabelWidth));
                        this.configs[this.newGameTypeName].EnumId = EditorGUILayout.IntField(this.configs[this.newGameTypeName].EnumId, GUILayout.Width(TextFieldWidth));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"NameEN:", GUILayout.Width(LabelWidth));
                        this.configs[this.newGameTypeName].NameEN = EditorGUILayout.TextField(this.configs[this.newGameTypeName].NameEN, GUILayout.Width(TextFieldWidth));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"NameZN:", GUILayout.Width(LabelWidth));
                        this.configs[this.newGameTypeName].NameZN = EditorGUILayout.TextField(this.configs[this.newGameTypeName].NameZN, GUILayout.Width(TextFieldWidth));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"Url:", GUILayout.Width(LabelWidth));
                        this.configs[this.newGameTypeName].Url = EditorGUILayout.TextField(this.configs[this.newGameTypeName].Url, GUILayout.Width(TextFieldWidth));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"LoginUrl:", GUILayout.Width(LabelWidth));
                        this.configs[this.newGameTypeName].LoginUrl = EditorGUILayout.TextField(this.configs[this.newGameTypeName].LoginUrl, GUILayout.Width(TextFieldWidth));
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"CodeUrl:", GUILayout.Width(LabelWidth));
                        this.configs[this.newGameTypeName].CodeUrl = EditorGUILayout.TextField(this.configs[this.newGameTypeName].CodeUrl, GUILayout.Width(TextFieldWidth));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"IndexUrl:", GUILayout.Width(LabelWidth));
                        this.configs[this.newGameTypeName].IndexUrl = EditorGUILayout.TextField(this.configs[this.newGameTypeName].IndexUrl, GUILayout.Width(TextFieldWidth));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"LoseWinUrl:", GUILayout.Width(LabelWidth));
                        this.configs[this.newGameTypeName].LoseWinUrl = EditorGUILayout.TextField(this.configs[this.newGameTypeName].LoseWinUrl, GUILayout.Width(TextFieldWidth));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"ResultUrl:", GUILayout.Width(LabelWidth));
                        this.configs[this.newGameTypeName].ResultUrl = EditorGUILayout.TextField(this.configs[this.newGameTypeName].ResultUrl, GUILayout.Width(TextFieldWidth));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"CountDownUrl:", GUILayout.Width(LabelWidth));
                        this.configs[this.newGameTypeName].CountDownUrl = EditorGUILayout.TextField(this.configs[this.newGameTypeName].CountDownUrl, GUILayout.Width(TextFieldWidth));
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"OddsUrl0:", GUILayout.Width(LabelWidth));
                        this.configs[this.newGameTypeName].OddsUrl0 = EditorGUILayout.TextField(this.configs[this.newGameTypeName].OddsUrl0, GUILayout.Width(TextFieldWidth));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"OddsUrl1:", GUILayout.Width(LabelWidth));
                        this.configs[this.newGameTypeName].OddsUrl1 = EditorGUILayout.TextField(this.configs[this.newGameTypeName].OddsUrl1, GUILayout.Width(TextFieldWidth));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"OddsUrl2:", GUILayout.Width(LabelWidth));
                        this.configs[this.newGameTypeName].OddsUrl2 = EditorGUILayout.TextField(this.configs[this.newGameTypeName].OddsUrl2, GUILayout.Width(TextFieldWidth));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"BetsUrl:", GUILayout.Width(LabelWidth));
                        this.configs[this.newGameTypeName].BetsUrl = EditorGUILayout.TextField(this.configs[this.newGameTypeName].BetsUrl, GUILayout.Width(TextFieldWidth));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label($"LogOutUrl:", GUILayout.Width(LabelWidth));
                        this.configs[this.newGameTypeName].LogOutUrl = EditorGUILayout.TextField(this.configs[this.newGameTypeName].LogOutUrl, GUILayout.Width(TextFieldWidth));
                        GUILayout.EndHorizontal();
                    }
                }
                GUILayout.EndVertical();

                GUILayout.Space(10);
                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("保存"))
                    {
                        File.WriteAllText(path, JsonHelper.ToJson(this.configs));
                        AssetDatabase.Refresh();
                    }
                }
                GUILayout.EndHorizontal();
            }
            else
            {
                //添加一行新的编辑
                GUILayout.BeginHorizontal();
                {
                    this.selectedIndex = EditorGUILayout.Popup(this.selectedIndex, new string[] { "请命名一个游戏" });
                    this.newGameTypeName = EditorGUILayout.TextField("游戏类型英文名:", this.newGameTypeName);
                    if (GUILayout.Button("添加"))
                    {
                        if (string.IsNullOrEmpty(this.newGameTypeName))
                        {
                            Debug.LogError("请输入一个key的名称!");
                            return;
                        }
                        if (!this.configs.ContainsKey(this.newGameTypeName))
                        {
                            this.configs.Add(this.newGameTypeName, new GameTypeConfig());
                        }
                        else
                        {
                            Debug.LogError("已存在这个游戏类型!");
                        }
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}
