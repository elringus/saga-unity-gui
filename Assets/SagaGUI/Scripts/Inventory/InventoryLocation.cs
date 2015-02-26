
namespace SagaGUI
{
	/// <summary>
	/// Unique location in the inventory system.
	/// </summary>
	public struct InventoryLocation
	{
		public int BagID;
		public int SlotID;

		/// <summary>
		/// Is location valid (bag and slot IDs are set).
		/// </summary>
		public bool Valid { get { return BagID != -1 && SlotID != -1; } }

		/// <summary>
		/// Init location.
		/// </summary>
		/// <param name="bagID">ID of the bag. Should be equal to or greater than zero.</param>
		/// <param name="slotID">ID of the slot. Should be equal to or greater than zero.</param>
		public InventoryLocation (int bagID = -1, int slotID = -1)
		{
			this.BagID = bagID;
			this.SlotID = slotID;
		}
	}
}