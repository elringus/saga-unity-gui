using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SagaGUI
{
	public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
	{
		public readonly Color32 HOVER_COLOR = new Color32(255, 245, 200, 255);
		public readonly Color32 FOCUS_COLOR = new Color32(255, 235, 150, 255);

		public Item Item;

		private Image image;
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
			image = GetComponent<Image>();
			canvasCamera = FindObjectOfType<Canvas>().worldCamera;
		}

		public void OnBeginDrag (PointerEventData eventData)
		{
			dragDelta = transform.position - canvasCamera.ScreenToWorldPoint(eventData.position);
		}

		public void OnDrag (PointerEventData eventData)
		{
			image.color = FOCUS_COLOR;
			transform.position = (Vector2)canvasCamera.ScreenToWorldPoint(eventData.position) + dragDelta;
		}

		public void OnEndDrag (PointerEventData eventData)
		{
			transform.localPosition = Vector3.zero;
			image.color = Color.white;
		}

		public void OnPointerEnter (PointerEventData eventData)
		{
			image.color = HOVER_COLOR;
		}

		public void OnPointerExit (PointerEventData eventData)
		{
			image.color = Color.white;
		}

		public void OnPointerDown (PointerEventData eventData)
		{
			image.color = FOCUS_COLOR;
		}
	}
}