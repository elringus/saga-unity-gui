using UnityEngine;
using SagaGUI;

public class GUITesting : MonoBehaviour
{
	private void Awake () 
	{
		AbilityPanel.I.HandleTestButtonClick(() => {
			print("Clicked test button!");
			GUIManager.I.HideAllSets(); 
			Invoke("TestFoo", 3);
		});
	}

	private void Update () 
	{
		AbilityPanel.I.SetTestText(Time.time.ToString());
	}

	private void TestFoo ()
	{
		GUIManager.I.ShowAllSets();
	}
}