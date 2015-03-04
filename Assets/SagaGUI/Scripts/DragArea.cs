using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image)), ExecuteInEditMode]
public class DragArea : MonoBehaviour, IBeginDragHandler, IDragHandler
{
	private Vector2 dragDelta;
	private Camera canvasCamera;

	private void OnEnable ()
	{
		GetComponent<Image>().color = Color.clear;
		gameObject.name = "drag-area";
		transform.SetAsFirstSibling();
	}

	private void Start ()
	{
		canvasCamera = FindObjectOfType<Canvas>().worldCamera;
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		if (!Input.GetMouseButton(0)) return;

		dragDelta = (Vector2)transform.position - eventData.position;
			//canvasCamera.ScreenToWorldPoint(eventData.position);
	}

	public void OnDrag (PointerEventData eventData)
	{
		if (!Input.GetMouseButton(0)) return;

		transform.parent.position = eventData.position + dragDelta;
			//(Vector2)canvasCamera.ScreenToWorldPoint(eventData.position) + dragDelta;
		transform.parent.localPosition = new Vector3(transform.parent.localPosition.x, transform.parent.localPosition.y, 0);
	}
}