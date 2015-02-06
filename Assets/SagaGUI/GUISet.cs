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

		protected virtual void Awake ()
		{
			
		}

		protected virtual void Start ()
		{

		}

		protected virtual void Update ()
		{
			
		}

		public virtual void Show ()
		{
			if (Visible) return;

		}

		public virtual void Hide ()
		{
			if (!Visible) return;

		}
	}
}