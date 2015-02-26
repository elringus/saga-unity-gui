using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace SagaGUI
{
	public class InventoryWindow : MonoBehaviour
	{
		public Dictionary<int, List<InventorySlot>> Bags = new Dictionary<int, List<InventorySlot>>();

		private Inventory inventory;
		private Button closeButton;

		private void Awake ()
		{
			for (int i = 0; i < 5; i++)
			{
				var slots = new List<InventorySlot>();
				foreach (Transform slot in transform.Find("panel_bag" + (i + 1)))
					slots.Add(slot.GetComponent<InventorySlot>());
				Bags.Add(i, slots);
			}

			inventory = FindObjectOfType<Inventory>();
			closeButton = transform.Find("button_close").GetComponent<Button>();

			closeButton.OnClick(inventory.Hide);
		}

		private void Start ()
		{
			//foreach (var bag in Bags)
			//	foreach (var slot in bag.Value)
			//		print("Bag " + bag.Key + ": Item " + (slot.InventoryItem != null ? slot.InventoryItem.Item.ID.ToString() : "empty"));
		}

		public int FindFreeBag ()
		{
			foreach (var bag in Bags)
				if (bag.Value.Exists(s => s.InventoryItem == null)) return bag.Key;

			// can't find free bag, return -1
			return -1;
		}

		public InventorySlot FindFreeSlot (int bag)
		{
			foreach (var slot in Bags[bag])
				if (slot.InventoryItem == null) return slot;

			// can't find free slot in the bag, return null
			return null;
		}

		public InventoryItem FindItem (Item item)
		{
			InventoryItem foundItem = null;

			foreach (var bag in Bags)
			{
				var slot = bag.Value.Find(s => s.InventoryItem && s.InventoryItem.Item.ID == item.ID);
				if (!slot) continue;
				foundItem = slot.InventoryItem;
				break;
			}

			return foundItem;
		}

		public InventoryItem FindItem (int bag, int slot)
		{
			return Bags[bag][slot].InventoryItem;
		}

		public void RemoveItem (InventoryItem inventoryItem)
		{
			foreach (var bag in Bags)
			{
				var slot = bag.Value.Find(s => s.InventoryItem == inventoryItem);
				if (slot != null)
				{
					slot.InventoryItem = null;
					Destroy(inventoryItem.gameObject);
					break;
				}
			}
		}
	}
}