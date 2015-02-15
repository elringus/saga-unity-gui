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

		private InventoryItem _inventoryItem;
	}
}