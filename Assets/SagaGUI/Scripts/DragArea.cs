using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image)), ExecuteInEditMode]
public class DragArea : MonoBehaviour, IBeginDragHandler, IDragHandler
{
	private Vector2 dragDelta;

	private void OnEnable ()
	{
		GetComponent<Image>().color = Color.clear;
		gameObject.name = "drag-area";
		transform.SetSiblingIndex(0);
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		dragDelta = (Vector2)transform.position - eventData.position;
	}

	public void OnDrag (PointerEventData eventData)
	{
		transform.parent.position = eventData.position + dragDelta;
	}
}