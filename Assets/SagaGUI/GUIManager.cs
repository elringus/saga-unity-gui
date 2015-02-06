using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace SagaGUI
{
	public class GUIManager : MonoBehaviour
	{
		#region SINGLETON
		private static GUIManager _instance;
		private static GUIManager i
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<GUIManager>();
					if (_instance == null) Initialize();
				}
				return _instance;
			}
		}
		private void OnApplicationQuit () { _instance = null; }
		#endregion

		public static bool Initialized { get { return i.initialized; } }
		public static List<GUISet> InitializedSets { get { return i.initializedSets; } }

		private bool initialized;
		private List<GUISet> initializedSets = new List<GUISet>();

		public static void Initialize ()
		{
			if (FindObjectOfType<Canvas>()) 
				foreach (var canvas in FindObjectsOfType<Canvas>()) 
					Destroy(canvas.gameObject);

			GameObject.Instantiate(Resources.Load<GameObject>("GUIEssentials/GUI")).name = "GUI";

			if (!FindObjectOfType<EventSystem>())
				GameObject.Instantiate(Resources.Load<GameObject>("GUIEssentials/EventSystem")).name = "EventSystem";

			i.initialized = true;
		}

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