using ETModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ETEditor
{
    public class GameRankingNameConfigEditor : EditorWindow
    {
        public class GameRankingNameConfig
        {
            public List<string> Num = new List<string>();
        }
        private const string path = @"./Assets/Res/Config/GameRankingNameConfig.txt";

        private const int LabelWidth = 100;
        private const int TextFieldWidth = 400;
        private Dictionary<string, GameRankingNameConfig> configs;
        private int selectedIndex = 0;
        private string curGameTypeName;
        private string newGameTypeName;

        [MenuItem("Tools/游戏种类")]
        public static void ShowWindow()
        {
            GetWindow<GameRankingNameConfigEditor>();
        }

        public void Awake()
        {
            if (File.Exists(path))
            {
                string fileContent = File.ReadAllText(path);
                if (string.IsNullOrEmpty(fileContent))
                {
                    this.configs = new Dictionary<string, GameRankingNameConfig>();
                }
                else
                {
                    this.configs = JsonHelper.FromJson<Dictionary<string, GameRankingNameConfig>>(File.ReadAllText(path));
                }
                Debug.Log(this.configs);
            }
            else
            {
                FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
                this.configs = new Dictionary<string, GameRankingNameConfig>();
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
                    this.newGameTypeName = this.curGameTypeName;
                }
                this.newGameTypeName = EditorGUILayout.TextField("游戏种类", this.newGameTypeName);
                if (GUILayout.Button("添加"))
                {
                    if (string.IsNullOrEmpty(this.newGameTypeName))
                    {
                        Debug.LogError("请输入一个key的名称!");
                        return;
                    }
                    if (!this.configs.ContainsKey(this.newGameTypeName))
                    {
                        this.configs.Add(this.newGameTypeName, new GameRankingNameConfig());
                        this.selectedIndex = this.configs.Values.Count - 1;
                    }
                    else
                    {
                        Debug.LogError("已存在这个游戏种类!");
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
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("添加一行号码标签"))
                    {
                        this.configs[this.newGameTypeName].Num.Add("");
                    }
                    if (GUILayout.Button("删除一行号码标签"))
                    {
                        if (this.configs[this.newGameTypeName].Num.Count > 0)
                        {
                            this.configs[this.newGameTypeName].Num.RemoveAt(this.configs[this.newGameTypeName].Num.Count - 1);
                        }
                        else
                        {
                            Debug.Log("已经没有数据可删除了");
                        }
                    }
                    GUILayout.EndHorizontal();
                    if (this.configs.ContainsKey(this.newGameTypeName))
                    {
                        for (int i = 0; i < this.configs[this.newGameTypeName].Num.Count; i++)
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label($"Num{i}:", GUILayout.Width(LabelWidth));
                            this.configs[this.newGameTypeName].Num[i] = EditorGUILayout.TextField(this.configs[this.newGameTypeName].Num[i], GUILayout.Width(TextFieldWidth));
                            GUILayout.EndHorizontal();
                        }
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
                    this.newGameTypeName = EditorGUILayout.TextField("游戏种类:", this.newGameTypeName);
                    if (GUILayout.Button("添加"))
                    {
                        if (string.IsNullOrEmpty(this.newGameTypeName))
                        {
                            Debug.LogError("请输入一个key的名称!");
                            return;
                        }
                        if (!this.configs.ContainsKey(this.newGameTypeName))
                        {
                            this.configs.Add(this.newGameTypeName, new GameRankingNameConfig());
                        }
                        else
                        {
                            Debug.LogError("已存在这个游戏种类!");
                        }
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}
