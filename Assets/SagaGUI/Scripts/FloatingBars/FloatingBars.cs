using UnityEngine;
using UnityEngine.UI;

namespace SagaGUI
{
	/// <summary>
	/// Manages floating status bars of the actors.
	/// </summary>
	public class FloatingBars : GUISet<FloatingBars>
	{
		private Transform target;
		private RectTransform sliderTrs;
		private Slider slider;

		protected override void Awake ()
		{
			base.Awake();

			slider = GetComponentInChildren<Slider>();
			sliderTrs = slider.transform as RectTransform;
		}

		protected override void Start ()
		{
			base.Start();

			transform.SetAsFirstSibling();
		}

		private void LateUpdate ()
		{
			if (target)
				sliderTrs.anchoredPosition = Camera.main.WorldToScreenPoint(target.transform.position);
		}

		/// <summary>
		/// Binds bar to the target, so it will follow it in screenspace.
		/// </summary>
		/// <param name="target">Transform component of the target.</param>
		public void BindBar (Transform target)
		{
			this.target = target;
		}

		/// <summary>
		/// Sets current HP value, in percents. 
		/// Should be in 0.0 to 1.0 interval.
		/// </summary>
		/// <param name="value">Current HP value, should be in 0.0 to 1.0 interval.</param>
		public void SetHP (float value)
		{
			slider.normalizedValue = value;
		}
	}
}