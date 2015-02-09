using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace SagaGUI
{
	public class GUIManager : MonoBehaviour
	{
		#region SINGLETON_MANAGEMENT
		private static GUIManager _instance;
		public static GUIManager I
		{
			get
			{
				if (_instance == null)
					_instance = Initialize();
				return _instance;
			}
		}

		public static bool Initialized { get { return _instance != null; } }

		public static GUIManager Initialize ()
		{
			if (FindObjectOfType<Canvas>())
				foreach (var canvas in FindObjectsOfType<Canvas>())
					Destroy(canvas.gameObject);

			if (!FindObjectOfType<EventSystem>())
				GameObject.Instantiate(Resources.Load<GameObject>("GUIEssentials/EventSystem")).name = "EventSystem";

			var guiManager = (Instantiate(Resources.Load<GameObject>("GUIEssentials/GUI")) as GameObject).GetComponent<GUIManager>();
			guiManager.gameObject.name = "GUI";

			return guiManager;
		}
		#endregion

		[HideInInspector]
		public List<GUISet> InitializedSets = new List<GUISet>();

		#region MONOBEHAVIOUR_CALLBACKS
		private void Awake ()
		{

		}

		private void Start ()
		{

		}

		private void Update ()
		{

		}

		private void OnApplicationQuit () 
		{ 
			_instance = null; 
		}
		#endregion

		public void ShowAllSets ()
		{
			foreach (var set in InitializedSets) set.Show();
		}

		public void HideAllSets ()
		{
			foreach (var set in InitializedSets) set.Hide();
		}
	}
}