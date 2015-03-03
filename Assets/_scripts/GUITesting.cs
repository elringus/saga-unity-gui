using UnityEngine;
using SagaGUI;

public class GUITesting : MonoBehaviour
{
	private void Awake () 
	{
		for (int i = 0; i < 85; i++)
			Inventory.I.AddItem(new Item() { ID = 0, CurStack = 1, MaxStack = 99 });

		Inventory.I.AddItem(new Item() { ID = 0, MaxStack = 99, CurStack = 32 }, new InventoryLocation(0, 1));
		Inventory.I.AddItem(new Item() { ID = 0, MaxStack = 99, CurStack = 12 }, new InventoryLocation(0, 2));
		Inventory.I.AddItem(new Item() { ID = 0, MaxStack = 99, CurStack = 1 }, new InventoryLocation(0, 3));
		Inventory.I.AddItem(new Item() { ID = 0, MaxStack = 99, CurStack = 2 }, new InventoryLocation(0, 4));

		for (int i = 1; i < 30; i++)
			Inventory.I.AddItem(new Item() { ID = i });

		
		Inventory.I.RemoveItem(new InventoryLocation(0, 5));

		Inventory.I.OnUseItem += (Item i) => { print("Used " + i.ID); Inventory.I.RemoveItem(i); };
		Inventory.I.OnDropItem += (Item i) => Inventory.I.RemoveItem(i);
		Inventory.I.OnMoveItem += (Item i, InventoryLocation l) => Inventory.I.MoveItem(i, l);

		Tooltip.Initialize();
	}
}