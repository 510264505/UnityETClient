using System;

namespace ETHotfix
{
    public static class MessagePopupHelper
    {
        public static void MessageBox(string message)
        {
            Game.Scene.GetComponent<UIComponent>().Get(CPUIType.UIMessageCanvas).GetComponent<UIMessageBoxComponent>().MessageBox(message);
        }
        public static void MessageBox(string message, Action<string> action = null)
        {
            Game.Scene.GetComponent<UIComponent>().Get(CPUIType.UIMessageCanvas).GetComponent<UIMessageBoxComponent>().MessageBox(message, action);
        }
        public static void MeesageWindow(string message, Action action = null)
        {
            Game.Scene.GetComponent<UIComponent>().Get(CPUIType.UIMessageCanvas).GetComponent<UIMessageBoxComponent>().MessageWindow(message, action);
        }
    }
}
