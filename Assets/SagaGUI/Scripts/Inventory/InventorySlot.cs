using UnityEngine;

namespace SagaGUI
{
	public class InventorySlot : MonoBehaviour
	{
		public InventoryItem Item
		{
			get { return _item; }
			set
			{
				value.transform.SetParent(transform, false);
				_item = value;
			}
		}

		private InventoryItem _item;
	}
}