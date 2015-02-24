﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace SagaGUI
{
	/// <summary>
	/// Character inventory system (bags).
	/// </summary>
	public class Inventory : GUISet<Inventory>
	{
		private InventoryWindow inventoryWindow;

		protected override void Awake ()
		{
			base.Awake();

			inventoryWindow = transform.Find("panel_inventory-window").GetComponent<InventoryWindow>();
		}

		/// <summary>
		/// Adds item to the character inventory.
		/// </summary>
		/// <param name="item">The item to add.</param>
		/// <param name="bag">Bag ID. Using deafult will place the item in the first free bag.</param>
		/// <param name="slot">Slot ID. Using default will place in the first free slot of a bag.</param>
		public void AddItem (Item item, int bag = -1, int slot = -1)
		{
			var freeBag = bag == -1 ? inventoryWindow.FindFreeBag() : bag;
			if (freeBag == -1)
			{
				Debug.LogError("Can't add an item — no free bags available.");
				return;
			}

			var freeSlot = slot == -1 ? inventoryWindow.FindFreeSlot(freeBag) : inventoryWindow.Bags[freeBag][slot];
			if (freeSlot == null || freeSlot.InventoryItem != null)
			{
				Debug.LogError("Can't add an item to the bag — no free slots available.");
				return;
			}

			freeSlot.InventoryItem = InventoryItem.Initialize(item);
		}

		/// <summary>
		/// Removes item from the character inventory.
		/// </summary>
		/// <param name="item">The item to remove.</param>
		public void RemoveItem (Item item)
		{
			var itemToRemove = inventoryWindow.FindItem(item);
			if (itemToRemove == null)
			{
				Debug.LogError("Can't remove item form inventory — can't find this item in bags.");
				return;
			}

			inventoryWindow.RemoveItem(itemToRemove);
		}

		/// <summary>
		/// Removes item from the character inventory.
		/// </summary>
		/// <param name="bag">ID of a bag, where item placed.</param>
		/// <param name="slot">ID of a slot, where item placed.</param>
		public void RemoveItem (int bag, int slot)
		{
			var itemToRemove = inventoryWindow.FindItem(bag, slot);
			if (itemToRemove == null)
			{
				Debug.LogError("Can't remove item form inventory — can't find this item in bags.");
				return;
			}

			inventoryWindow.RemoveItem(itemToRemove);
		}

		public void HandleUseItem (UnityAction<int, int> action)
		{

		}

		public void HandleUseItem (UnityAction<Item> action)
		{

		}

		public void HandleMoveItem (UnityAction<int, int, int, int> action)
		{

		}

		public void HandleDropItem (UnityAction<int, int> action)
		{

		}

		public void HandleDropItem (UnityAction<Item> action)
		{

		}
	}
}