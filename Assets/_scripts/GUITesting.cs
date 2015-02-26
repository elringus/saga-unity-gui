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

		Inventory.I.OnUseItem += (Item i) => { print("Used " + i.ID); Inventory.I.RemoveItem(i); };
		Inventory.I.OnDropItem += (Item i) => Inventory.I.RemoveItem(i);
		Inventory.I.OnMoveItem += (Item i, int b, int s) => print(string.Format("Item {0} moved to bag {1} slot {2}", i.ID, b, s));
	}
}