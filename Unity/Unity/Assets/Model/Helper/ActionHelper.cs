using System;
using UnityEngine.UI;

namespace ETModel
{
	public static class ActionHelper
	{
		public static void Add(this Button.ButtonClickedEvent buttonClickedEvent, Action action)
		{
			buttonClickedEvent.AddListener(() => { action(); });
		}
        public static void Add(this InputField.SubmitEvent submitEvent, Action<string> action)
        {
            submitEvent.AddListener((s) => action(s));
        }
        public static void Add(this Toggle.ToggleEvent toggleEvent, Action<bool> action)
        {
            toggleEvent.AddListener((b) => action(b));
        }
	}
}