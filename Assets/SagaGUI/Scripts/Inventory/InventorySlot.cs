using UnityEngine;

namespace SagaGUI
{
	public class InventorySlot : MonoBehaviour
	{
		public InventoryItem InventoryItem
		{
			get { return _inventoryItem; }
			set
			{
				if (value != null) value.transform.SetParent(transform, false);
				_inventoryItem = value;
			}
		}

		public bool Empty { get { return InventoryItem == null; } }

		private InventoryItem _inventoryItem;
	}
}