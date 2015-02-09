using UnityEngine;
using SagaGUI;

public class GUITesting : MonoBehaviour
{
	private void Awake () 
	{
		var foo = AbilityPanel.I;
		var boo = PlayerStatus.I;

		var foo2 = AbilityPanel.Initialize();

		foreach (var s in GUIManager.I.InitializedSets)
			print(s);
	}

	private void Update () 
	{
    	
	}
}