using UnityEngine;
using System.Collections.Generic;

namespace SagaGUI
{
	public static class GUIManager
	{
		public static List<GUISet> InitializedSets;

		public static void ShowAllSets ()
		{
			foreach (var set in InitializedSets) set.Visible = true;
		}

		public static void HideAllSets ()
		{
			foreach (var set in InitializedSets) set.Visible = false;
		}
	}
}