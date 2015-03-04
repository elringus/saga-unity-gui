using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

namespace SagaGUI
{
	public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
	{
		public readonly Color32 HOVER_COLOR = new Color32(255, 245, 200, 255);
		public readonly Color32 FOCUS_COLOR = new Color32(255, 235, 150, 255);

		public Item Item;

		private Transform parentSet;
		private Inventory inventory;
		private InventoryWindow inventoryWindow;
		private Image image;
		private Text stackText;
		private Vector2 dragDelta;
		private Camera canvasCamera;

		public static InventoryItem Initialize (Item item)
		{
			var newItem = (Instantiate(Resources.Load<GameObject>("GUIEssentials/InventoryItem")) as GameObject).GetComponent<InventoryItem>();
			newItem.gameObject.name = "InventoryItem";
			newItem.Item = item;

			return newItem;
		}

		private void Awake ()
		{
			inventory = FindObjectOfType<Inventory>();
			inventoryWindow = FindObjectOfType<InventoryWindow>();
			image = GetComponent<Image>();
			stackText = GetComponentInChildren<Text>();
			canvasCamera = FindObjectOfType<Canvas>().worldCamera;
		}

		private void Start ()
		{
			parentSet = transform.parent;
		}

		private void Update ()
		{
			stackText.text = Item.CurStack <= 1 ? string.Empty : Item.CurStack.ToString();
		}

		private void OnDisable ()
		{
			if (Tooltip.Initialized) Tooltip.I.Hide();
		}

		public void OnBeginDrag (PointerEventData eventData)
		{
			if (!inventory.Visible || !Input.GetMouseButton(0)) return;

			dragDelta = (Vector2)transform.position - eventData.position;
				//canvasCamera.ScreenToWorldPoint(eventData.position);
			transform.SetParent(GameObject.Find("GUI").transform, false);
		}

		public void OnDrag (PointerEventData eventData)
		{
			if (!inventory.Visible || !Input.GetMouseButton(0)) return;

			image.color = FOCUS_COLOR;
			transform.position = eventData.position + dragDelta;
				//(Vector2)canvasCamera.ScreenToWorldPoint(eventData.position) + dragDelta;

			// module returns null if invoked offscreen
			if (!eventData.pointerCurrentRaycast.module) return;
			var hoveredItems = new List<RaycastResult>();
			eventData.pointerCurrentRaycast.module.Raycast(eventData, hoveredItems);
			if (hoveredItems.Exists(i => i.gameObject.name == "Checkmark"))
				hoveredItems.Find(i => i.gameObject.name == "Checkmark")
					.gameObject.transform.parent.parent.GetComponent<Toggle>().isOn = true;
		}

		public void OnEndDrag (PointerEventData eventData)
		{
			if (!inventory.Visible || !Input.GetMouseButtonUp(0)) return;

			transform.SetParent(parentSet, false);
			transform.localPosition = Vector3.zero;
			image.color = Color.white;

			// module returns null if invoked offscreen
			if (!eventData.pointerCurrentRaycast.module) return;

			var hoveredItems = new List<RaycastResult>();
			eventData.pointerCurrentRaycast.module.Raycast(eventData, hoveredItems);

			if (!hoveredItems.Exists(i => i.gameObject.GetComponent<InventoryWindow>()))
			{
				inventory.FireDropItem(Item);
				return;
			}

			if (!hoveredItems.Exists(i => i.gameObject.GetComponent<InventorySlot>())) return;
			var targetSlot = hoveredItems.Find(i => i.gameObject.GetComponent<InventorySlot>()).gameObject.GetComponent<InventorySlot>();
			if (targetSlot.InventoryItem == this) return;
			inventory.FireMoveItem(Item, inventoryWindow.LocateSlot(targetSlot));
		}

		public void OnPointerEnter (PointerEventData eventData)
		{
			if (!inventory.Visible) return;

			image.color = HOVER_COLOR;

			if (Tooltip.Initialized)
				Tooltip.I.ShowTooltip(title: "Item with ID " + Item.ID);
		}

		public void OnPointerExit (PointerEventData eventData)
		{
			if (!inventory.Visible) return;

			image.color = Color.white;

			if (Tooltip.Initialized) Tooltip.I.Hide();
		}

		public void OnPointerDown (PointerEventData eventData)
		{
			if (!inventory.Visible) return;

			image.color = FOCUS_COLOR;
		}

		public void OnPointerClick (PointerEventData eventData)
		{
			if (!inventory.Visible) return;

			if (Input.GetMouseButtonUp(1)) inventory.FireUseItem(Item);
		}
	}
}