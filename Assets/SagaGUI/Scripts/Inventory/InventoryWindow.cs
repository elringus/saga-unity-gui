using UnityEngine;
using UnityEngine.UI;

namespace SagaGUI
{
	public class InventoryWindow : MonoBehaviour
	{
		private Inventory inventory;
		private Button closeButton;

		private void Awake ()
		{
			inventory = FindObjectOfType<Inventory>();
			closeButton = transform.Find("button_close").GetComponent<Button>();

			closeButton.OnClick(inventory.Hide);
		}
	}
}