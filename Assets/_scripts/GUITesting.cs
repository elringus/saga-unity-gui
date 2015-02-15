using UnityEngine;
using SagaGUI;

public class GUITesting : MonoBehaviour
{
	private void Awake () 
	{
		//Inventory.Initialize();
		for (int i = 0; i < 115; i++)
			Inventory.I.AddItem(new Item());
	}

	private void Update () 
	{
		
	}
}