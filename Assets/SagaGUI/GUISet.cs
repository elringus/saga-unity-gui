using UnityEngine;

public abstract class GUISet : MonoBehaviour
{
	private bool _visible;
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

	public virtual void Initialize ()
	{

	}

	protected virtual void Awake () 
	{
    	
	}

	protected virtual void Start () 
	{
    	
	}

	protected virtual void Update () 
	{
    	
	}

	protected virtual void Show ()
	{

	}

	protected virtual void Hide ()
	{

	}
}