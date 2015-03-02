using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace SagaGUI
{
	public class Tooltip : GUISet<Tooltip>
	{
		const float FADE_TIME = .1f;
		const float FADEOUT_DELAY = .15f;

		private Image icon;
		private Text titleText;
		private Text categoryText;
		private Text infoText;

		protected override void Awake ()
		{
			base.Awake();

			icon = transform.Find("image_icon").GetComponent<Image>();
			titleText = transform.Find("text_title").GetComponent<Text>();
			categoryText = transform.Find("text_category").GetComponent<Text>();
			infoText = transform.Find("text_info").GetComponent<Text>();
		}

		protected override void Start ()
		{
			base.Start();
			base.Hide();
		}

		protected override void Update ()
		{
			base.Update();

			CanvasGroup.alpha = Mathf.Lerp(CanvasGroup.alpha, Visible ? 1 : 0, Time.deltaTime / FADE_TIME);
		}

		/// <summary>
		/// Fades in set of the Tooltip and sets specified tooltip parameters.
		/// </summary>
		/// <param name="icon">Sprite to use as an icon of the tooltip.</param>
		/// <param name="title">Title of the tooltip.</param>
		/// <param name="category">Category of the tooltip.</param>
		/// <param name="info">Info text of the tooltip.</param>
		public void ShowTooltip (Sprite icon = null, string title = "", string category = "", string info = "")
		{
			StopCoroutine("DelayedHide");

			var curAlpha = CanvasGroup.alpha;
			base.Show();
			CanvasGroup.alpha = curAlpha;

			if (icon) this.icon.sprite = icon;
			if (title != "") this.titleText.text = title;
			if (category != "") this.categoryText.text = category;
			if (info != "") this.infoText.text = info;
		}

		/// <summary>
		/// Fades out the tooltip.
		/// </summary>
		public void HideTooltip ()
		{
			StopCoroutine("DelayedHide");
			StartCoroutine("DelayedHide");
		}

		private IEnumerator DelayedHide ()
		{
			yield return new WaitForSeconds(FADEOUT_DELAY);

			var curAlpha = CanvasGroup.alpha;
			base.Hide();
			CanvasGroup.alpha = curAlpha;
		}

		public override void Show ()
		{
			ShowTooltip();
		}

		public override void Hide ()
		{
			HideTooltip();
		}
	}
}