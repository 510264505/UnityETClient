using System;
using System.IO;
using System.Text;
using UnityEngine;

public class LogPrint : MonoBehaviour
{
    public enum GameLogType
    {
        None,
        print,
        save,
        printAndSave
    }

    private byte[] bytes;
    public GameLogType m_GameLogType;
    private string logName = DateTime.Now.ToString("yyyy-MM-dd");
    private string logHeadStyle = "HH-mm-ss:   ";
    private string a = "", b = "";
    private string DEBUG_LOG = "";
    private string persistentDataPath;

    private void Start()
    {
        persistentDataPath = Application.persistentDataPath;
        if (m_GameLogType >= GameLogType.print)
        {
            if (m_GameLogType >= GameLogType.save)
            {
                FileStream fs = new FileStream(persistentDataPath + "/" + logName + ".txt", FileMode.OpenOrCreate);
                fs.Seek(0, SeekOrigin.End);
                bytes = Encoding.UTF8.GetBytes(DateTime.Now.ToString("\n\n\n==========================打开游戏==========================\n"));
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }
            Application.logMessageReceivedThreaded += OnMessageReceived;
        }
    }
    /// <summary>
    /// 打印日志，线程安全的
    /// </summary>
    protected void OnMessageReceived(string condition, string stackTrace, UnityEngine.LogType type)
    {
        if (m_GameLogType == GameLogType.None) return;
        FileStream fs = null;
        if (m_GameLogType >= GameLogType.save)
        {
            fs = new FileStream(persistentDataPath + "/" + logName + ".txt", FileMode.OpenOrCreate);
            fs.Seek(0, SeekOrigin.End);
        }
        if (type == UnityEngine.LogType.Error || type == UnityEngine.LogType.Assert || type == UnityEngine.LogType.Exception)
        {
            a = condition + '\n' + stackTrace;
            b = "---------------------报错---------------------\n";
            if (m_GameLogType == GameLogType.print || m_GameLogType == GameLogType.printAndSave)
            {
                DEBUG_LOG += a;
                DEBUG_LOG += b;
            }
            if (m_GameLogType >= GameLogType.save)
            {
                if (fs == null) return;
                bytes = Encoding.UTF8.GetBytes(DateTime.Now.ToString(logHeadStyle) + a);
                fs.Write(bytes, 0, bytes.Length);

                bytes = Encoding.UTF8.GetBytes(DateTime.Now.ToString(logHeadStyle) + b);
                fs.Write(bytes, 0, bytes.Length);
            }
        }
        else
        {
            a = condition + '\n';
            if (m_GameLogType == GameLogType.print || m_GameLogType == GameLogType.printAndSave) DEBUG_LOG += a;
            if (m_GameLogType >= GameLogType.save)
            {
                if (fs == null) return;
                bytes = Encoding.UTF8.GetBytes(DateTime.Now.ToString(logHeadStyle) + a);
                fs.Write(bytes, 0, bytes.Length);
            }
        }
        if (m_GameLogType >= GameLogType.save)
            if (fs != null) fs.Close();
    }
}
