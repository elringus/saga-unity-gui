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
		private Text spaceText;

		private void Awake ()
		{
			for (int i = 0; i < 5; i++)
			{
				foreach (Transform slot in transform.Find("panel_bag" + (i + 1)))
					Slots.Add(new InventoryLocation(i, slot.GetSiblingIndex()), slot.GetComponent<InventorySlot>());
			}

			inventory = FindObjectOfType<Inventory>();
			closeButton = transform.Find("button_close").GetComponent<Button>();
			spaceText = transform.Find("text_space").GetComponent<Text>();

			closeButton.OnClick(inventory.Hide);
		}

		private void Start ()
		{
			UpdateSpaceText();
		}

		public void UpdateSpaceText ()
		{
			int totalSlots = Slots.Count;
			int filledSlots = Slots.Where(s => !s.Value.Empty).Count();

			spaceText.text = string.Format("{0}/{1}", filledSlots, totalSlots);
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