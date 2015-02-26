using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace SagaGUI
{
	public class InventoryWindow : MonoBehaviour
	{
		public Dictionary<InventoryLocation, InventorySlot> Slots = new Dictionary<InventoryLocation, InventorySlot>();

		private Inventory inventory;
		private Button closeButton;

		private void Awake ()
		{
			for (int i = 0; i < 5; i++)
			{
				foreach (Transform slot in transform.Find("panel_bag" + (i + 1)))
					Slots.Add(new InventoryLocation(i, slot.GetSiblingIndex()), slot.GetComponent<InventorySlot>());
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

		public InventorySlot GetEmptySlot ()
		{
			return Slots.OrderBy(s => s.Key.BagID)
				.FirstOrDefault(s => s.Value.InventoryItem == null).Value;
		}

		public InventorySlot FindSlot (InventoryLocation location)
		{
			return Slots[location];
		}

		public InventoryLocation LocateSlot (InventorySlot slot)
		{
			return Slots.FirstOrDefault(s => s.Value == slot).Key;
		}

		public void FreeSlot (InventorySlot slot)
		{
			Destroy(slot.InventoryItem.gameObject);
			slot.InventoryItem = null;
		}

		public InventoryLocation LocateItem (Item item)
		{
			return Slots.FirstOrDefault(s => s.Value.InventoryItem && s.Value.InventoryItem.Item == item).Key;
		}
	}
}