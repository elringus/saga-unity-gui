using UnityEngine;

namespace SagaGUI
{
	public abstract class GUISet : MonoBehaviour
	{
		public bool Visible
		{
			get { return _visible; }
			set
			{
				if (_visible == value) return;

				if (value) Show();
				else Hide();

				_visible = value;
			}
		}

		private bool _visible;

		#region MONOBEHAVIOUR_CALLBACKS
		protected virtual void Awake ()
		{

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

		}

		public virtual void Hide ()
		{
			if (!Visible) return;

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
				{
					_instance = FindObjectOfType<T>();
					if (_instance == null) _instance = Initialize();
				}
				return _instance;
			}
		}

		public static bool Initialized { get { return _instance != null; } }

		public static T Initialize ()
		{
			var duplicate = GUIManager.I.InitializedSets.Find(s => s.name == typeof(T).Name);
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