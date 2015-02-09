using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace SagaGUI
{
	public class AbilityPanel : GUISet<AbilityPanel>
	{
		private Text testText;
		private Button testButton;

		protected override void Awake ()
		{
			base.Awake();

			testText = transform.Find("text_test").GetComponent<Text>();
			testButton = transform.Find("button_test").GetComponent<Button>();
		}

		public void SetTestText (string text)
		{
			testText.text = text;
		}

		public void HandleTestButtonClick (UnityAction action)
		{
			testButton.OnClick(action);
		}
	}
}