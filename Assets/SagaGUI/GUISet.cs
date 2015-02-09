using UnityEngine;
using UnityEngine.UI;

namespace SagaGUI
{
	public abstract class GUISet : MonoBehaviour
	{
		public bool Visible
		{
			get { return _visible; }
			set { if (value) Show(); else Hide(); }
		}

		private bool _visible = true;
		private CanvasGroup canvasGroup;

		#region MONOBEHAVIOUR_CALLBACKS
		protected virtual void Awake ()
		{
			canvasGroup = GetComponent<CanvasGroup>();
		}

		protected virtual void Start ()
		{

		}

		protected virtual void Update ()
		{

		}

		protected virtual void OnEnable ()
		{

		}

		protected virtual void OnDisable ()
		{

		}

		protected virtual void OnApplicationQuit ()
		{
			
		}
		#endregion

		public virtual void Show ()
		{
			if (Visible) return;

			canvasGroup.alpha = 1;
			canvasGroup.interactable = true;

			_visible = true;
		}

		public virtual void Hide ()
		{
			if (!Visible) return;

			canvasGroup.alpha = 0;
			canvasGroup.interactable = false;

			_visible = false;
		}
	}

	public abstract class GUISet<T> : GUISet where T : GUISet
	{
		#region SINGLETON_MANAGEMENT
		private static T _instance;
		public static T I
		{
			get
			{
				if (_instance == null) 
					_instance = Initialize();
				return _instance;
			}
		}

		public static bool Initialized { get { return _instance != null; } }

		public static T Initialize ()
		{
			var duplicate = FindObjectOfType<T>();
			if (duplicate != null)
			{
				GUIManager.I.InitializedSets.Remove(duplicate);
				Destroy(duplicate.gameObject);
			}

			var set = (Instantiate(Resources.Load<GameObject>("GUISets/" + typeof(T).Name)) as GameObject).GetComponent<T>();
			set.gameObject.name = typeof(T).Name;
			set.transform.SetParent(GUIManager.I.transform, false);
			GUIManager.I.InitializedSets.Add(set);

			return set;
		}

		protected override void OnApplicationQuit ()
		{
			base.OnApplicationQuit();

			_instance = null;
		}
		#endregion
	}
}