using UnityEngine.UI;
using UnityEngine.Events;

namespace SagaGUI
{
	public static class ButtonExtension
	{
		public static void OnClick (this Button button, UnityAction callback)
		{
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(callback);
		}
	}
}