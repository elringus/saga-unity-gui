using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace SagaGUI
{
	/// <summary>
	/// Character inventory system (bags).
	/// </summary>
	public class Inventory : GUISet<Inventory>
	{
		/// <summary>
		/// Player used an item.
		/// T1 = item to use.
		/// </summary>
		public event UnityAction<Item> OnUseItem = delegate { };
		/// <summary>
		/// Player dragged item out of the bag to drop it.
		/// T1 = item to drop.
		/// </summary>
		public event UnityAction<Item> OnDropItem = delegate { };
		/// <summary>
		/// Player dragged item to another slot.
		/// T1 = item moved, 
		/// T2 = target location.
		/// </summary>
		public event UnityAction<Item, InventoryLocation> OnMoveItem = delegate { };

		private InventoryWindow inventoryWindow;

		protected override void Awake ()
		{
			base.Awake();

			inventoryWindow = transform.Find("panel_inventory-window").GetComponent<InventoryWindow>();
		}

		/// <summary>
		/// Adds item to the character inventory at the first avaliable empty slot.
		/// </summary>
		/// <param name="item">The item to add.</param>
		public void AddItem (Item item)
		{
			var freeSlot = inventoryWindow.GetEmptySlot();

			if (!freeSlot || freeSlot.InventoryItem != null)
			{
				Debug.LogError("SagaGUI: Can't add an item to the bag — no free slots available.");
				return;
			}

			freeSlot.InventoryItem = InventoryItem.Initialize(item);
		}

		/// <summary>
		/// Adds item to the character inventory at the specific location.
		/// </summary>
		/// <param name="item">The item to add.</param>
		/// <param name="location">Specific location to place item.</param>
		public void AddItem (Item item, InventoryLocation location)
		{
			var freeSlot = inventoryWindow.FindSlot(location);

			if (!freeSlot || freeSlot.InventoryItem != null)
			{
				Debug.LogError("SagaGUI: Can't add an item to the specific slot — it is not empty.");
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
			var location = inventoryWindow.LocateItem(item);
			if (!location.Valid)
			{
				Debug.LogError("SagaGUI: Can't remove item from inventory — can't find this item in bags.");
				return;
			}

			inventoryWindow.FreeSlot(inventoryWindow.FindSlot(location));
		}

		/// <summary>
		/// Removes item from the character inventory.
		/// </summary>
		/// <param name="location">Location of item.</param>
		public void RemoveItem (InventoryLocation location)
		{
			if (!location.Valid)
			{
				Debug.LogError("SagaGUI: Can't remove item from inventory — specified inventory location is not valid.");
				return;
			}

			inventoryWindow.FreeSlot(inventoryWindow.FindSlot(location));
		}

		/// <summary>
		/// Moves item to another slot.
		/// Will swap items if moved to already occupied slot.
		/// </summary>
		/// <param name="item">Item to move.</param>
		/// <param name="location">Target location.</param>
		public void MoveItem (Item item, InventoryLocation location)
		{
			var initialItemLocation = inventoryWindow.LocateItem(item);
			RemoveItem(item);

			if (!inventoryWindow.FindSlot(location).Empty)
			{
				var itemInTargetSlot = inventoryWindow.FindSlot(location).InventoryItem.Item;
				AddItem(itemInTargetSlot, initialItemLocation);
				RemoveItem(location);
			}

			AddItem(item, location);
		}

		internal void FireUseItem (Item item) { OnUseItem(item); }
		internal void FireDropItem (Item item) { OnDropItem(item); }
		internal void FireMoveItem (Item item, InventoryLocation location) { OnMoveItem(item, location); }
	}
}