using UnityEngine;
using System.Reflection;

namespace SagaGUI
{
	public class AbilityPanel : GUISet
	{
		#region SINGLETON
		private static AbilityPanel _instance;
		private static AbilityPanel i
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<AbilityPanel>();
					if (_instance == null) Initialize();
				}
				return _instance;
			}
		}
		private void OnApplicationQuit () { _instance = null; }
		#endregion

		public static bool Initialized { get { return i.initialized; } }

		private bool initialized;

		public static void Initialize ()
		{
			if (!GUIManager.Initialized) GUIManager.Initialize();

			var type = MethodBase.GetCurrentMethod().DeclaringType;

			var duplicate = GUIManager.InitializedSets.Find(s => s.GetType() == type);
			if (duplicate)
			{
				GUIManager.InitializedSets.Remove(duplicate);
				Destroy(duplicate.gameObject);
			}

			var set = (GameObject.Instantiate(Resources.Load<GameObject>("GUISets/" + type.Name)) as GameObject).GetComponent<GUISet>();
			GUIManager.InitializedSets.Add(set);
			set.gameObject.name = type.Name;

			i.initialized = true;
		}
	}
}