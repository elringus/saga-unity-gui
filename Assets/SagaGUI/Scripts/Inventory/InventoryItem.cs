using UnityEngine;
using UnityEngine.UI;

namespace SagaGUI
{
	public class InventoryItem : MonoBehaviour
	{
		public Item Item;

		public static InventoryItem Initialize (Item item)
		{
			var newItem = (Instantiate(Resources.Load<GameObject>("GUIEssentials/InventoryItem")) as GameObject).GetComponent<InventoryItem>();
			newItem.gameObject.name = "InventoryItem";
			newItem.Item = item;

			return newItem;
		}
	}
}