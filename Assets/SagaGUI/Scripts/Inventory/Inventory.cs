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
		/// Adds item to the character inventory.
		/// </summary>
		/// <param name="item">The item to add.</param>
		/// <param name="bag">Bag ID. Using deafult will place the item in the first free bag.</param>
		/// <param name="slot">Slot ID. Using default will place in the first free slot of a bag.</param>
		public void AddItem (Item item, int bag = -1, int slot = -1)
		{
			
		}

		/// <summary>
		/// Removes item from the character inventory.
		/// </summary>
		/// <param name="item">The item to remove.</param>
		public void RemoveItem (Item item)
		{

		}

		/// <summary>
		/// Removes item from the character inventory.
		/// </summary>
		/// <param name="bag">ID of a bag, where item placed.</param>
		/// <param name="slot">ID of a slot, where item placed.</param>
		public void RemoveItem (int bag, int slot)
		{

		}

		public void HandleUseItem (UnityAction<int, int> action)
		{

		}

		public void HandleMoveItem (UnityAction<int, int, int, int> action)
		{

		}

		public void HandleDropItem (UnityAction<int, int> action)
		{

		}
	}
}