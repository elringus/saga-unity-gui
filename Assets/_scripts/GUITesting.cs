using UnityEngine;
using SagaGUI;

public class GUITesting : MonoBehaviour
{
	private void Awake () 
	{
		for (int i = 0; i < 30; i++)
			Inventory.I.AddItem(new Item() { ID = i });
		Inventory.I.AddItem(new Item() { ID = 101 }, 4, 15);

		Inventory.I.RemoveItem(new Item() { ID = 10 });
		Inventory.I.RemoveItem(0, 1);
	}

	private void Update () 
	{
		
	}
}