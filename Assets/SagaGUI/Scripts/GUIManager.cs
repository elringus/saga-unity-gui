using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace SagaGUI
{
	/// <summary>
	/// Manages all the GUI objects on the scene.
	/// Use GUIManager.I to get the instance and use it.
	/// </summary>
	public class GUIManager : MonoBehaviour
	{
		#region SINGLETON_MANAGEMENT
		private static GUIManager _instance;
		/// <summary>
		/// Active instance of the GUIManager. 
		/// Will automatically Initialize() if there is none.
		/// </summary>
		public static GUIManager I
		{
			get
			{
				if (_instance == null)
					_instance = Initialize();
				return _instance;
			}
		}

		/// <summary>
		/// Is GUI instantiated on the scene and ready to use?
		/// </summary>
		public static bool Initialized { get { return _instance != null; } }

		/// <summary>
		/// Forces re-initialization of the GUIManager. 
		/// All the present GUI hierarchy (if any) will be destroyed.
		/// </summary>
		/// <returns>Instance of the instantiated GUIManager.</returns>
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

		/// <summary>
		/// All the GUI sets, currently present on the scene.
		/// Do not modify this list directly!
		/// </summary>
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

		/// <summary>
		/// Calls Show() on every set currently present in the scene.
		/// </summary>
		public void ShowAllSets ()
		{
			foreach (var set in InitializedSets) set.Show();
		}

		/// <summary>
		/// Calls Hide() on every set currently present in the scene.
		/// </summary>
		public void HideAllSets ()
		{
			foreach (var set in InitializedSets) set.Hide();
		}
	}
}