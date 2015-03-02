using UnityEngine;
using UnityEngine.UI;

namespace SagaGUI
{
	/// <summary>
	/// Base class for all the sets.
	/// Provides generic functionality.
	/// </summary>
	public abstract class GUISet : MonoBehaviour
	{
		/// <summary>
		/// Is this set visible (alpha = 1)?
		/// </summary>
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

		/// <summary>
		/// Sets alpha of the set canvas group to 1, revealing all the objects in the set.
		/// Also enables interaction with the elements in the set.
		/// </summary>
		public virtual void Show ()
		{
			if (Visible) return;

			canvasGroup.alpha = 1;
			canvasGroup.interactable = true;

			_visible = true;
		}

		/// <summary>
		/// Sets alpha of the set canvas group to 0, hiding all the objects in the set.
		/// Also disables interaction with the elements in the set.
		/// </summary>
		public virtual void Hide ()
		{
			if (!Visible) return;

			canvasGroup.alpha = 0;
			canvasGroup.interactable = false;

			_visible = false;
		}
	}

	/// <summary>
	/// Forces singleton pattern for all the inherited classes.
	/// Also implements generic instance management.
	/// </summary>
	/// <typeparam name="T">Type of the GUI set. Type name should be identical to the name of the set prefab.</typeparam>
	public abstract class GUISet<T> : GUISet where T : GUISet
	{
		#region SINGLETON_MANAGEMENT
		private static T _instance;
		/// <summary>
		/// Active instance of the GUI set. 
		/// Will automatically Initialize() if there is none.
		/// </summary>
		public static T I
		{
			get
			{
				if (_instance == null) Initialize();
				return _instance;
			}
		}

		/// <summary>
		/// Is GUI set instantiated on the scene and ready to use?
		/// </summary>
		public static bool Initialized { get { return _instance != null; } }

		/// <summary>
		/// Forces re-initialization of the GUI set. 
		/// Already present instance of the set (if there is one) will be destroyed.
		/// </summary>
		/// <returns>Instance of the instantiated GUI set.</returns>
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

			_instance = set;

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