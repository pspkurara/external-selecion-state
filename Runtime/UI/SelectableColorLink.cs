using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Pspkurara.UI
{

	/// <summary>
	/// <see cref="selectable"/>上で<see cref="Selectable.colors"/>での色をリンクさせる
	/// </summary>
	[AddComponentMenu("UI/Selectable Color Link")]
	[RequireComponent(typeof(Selectable))]
	[ExecuteInEditMode]
	public sealed class SelectableColorLink : UIBehaviour
	{

		/// <summary>
		/// 伝播対象となる<see cref="Graphic"/>
		/// </summary>
		[SerializeField] private Graphic[] m_TargetGraphics = System.Array.Empty<Graphic>();

		public Graphic[] targetGraphics
		{
			get
			{
				return m_TargetGraphics;
			}
			set
			{
				m_TargetGraphics = value;
			}
		}

		private ISelectableWithESS m_Selectable;

		/// <summary>
		/// 自身の<see cref="Selectable"/>
		/// 継承されている必要がある
		/// </summary>
		public ISelectableWithESS selectable
		{
			get
			{
				if (m_Selectable == null)
				{
					m_Selectable = GetComponent<ISelectableWithESS>();
				}
				return m_Selectable;
			}
		}

		protected override void OnEnable()
		{
			if (selectable == null) return;

			// 最初はすぐ反映させる
			var tintColor = GetTintColor(selectable.currentExternalSelectionState);
			foreach (var g in m_TargetGraphics)
			{
				StartColorTween(g, tintColor, true);
			}

			// コールバック登録
			selectable.onDoStateTransition.AddListener(OnDoStateTransition);
		}

		protected override void OnDisable()
		{
			// コールバックをはがす
			if (selectable == null) return;
			selectable.onDoStateTransition.RemoveListener(OnDoStateTransition);
		}

		private void OnDoStateTransition(ExternalSelectionState state, bool instant)
		{
			// 色でない場合は何もしない
			if (selectable.transition != Selectable.Transition.ColorTint) return;

			// 色を取得
			var tintColor = GetTintColor(state);

			// 色を子に伝播する
			foreach (var g in m_TargetGraphics)
			{
				StartColorTween(g, tintColor, instant);
			}
		}

		/// <summary>
		/// ステートに応じた色を取得
		/// </summary>
		/// <param name="state">ステート</param>
		/// <returns>対応する色</returns>
		private Color GetTintColor(ExternalSelectionState state)
		{
			Color tintColor;
			switch (state)
			{
				case ExternalSelectionState.Normal:
					tintColor = selectable.colors.normalColor;
					break;
				case ExternalSelectionState.Highlighted:
					tintColor = selectable.colors.highlightedColor;
					break;
				case ExternalSelectionState.Pressed:
					tintColor = selectable.colors.pressedColor;
					break;
				case ExternalSelectionState.Selected:
					tintColor = selectable.colors.selectedColor;
					break;
				case ExternalSelectionState.Disabled:
					tintColor = selectable.colors.disabledColor;
					break;
				default:
					tintColor = Color.black;
					break;
			}
			// Selectableでやってるのと同様に乗算する
			tintColor = tintColor * selectable.colors.colorMultiplier;
			return tintColor;
		}

		/// <summary>
		/// 色をクロスフェードさせる
		/// </summary>
		/// <param name="graphic">対象</param>
		/// <param name="targetColor">色</param>
		/// <param name="instant">即時</param>
		private void StartColorTween(Graphic graphic, Color targetColor, bool instant)
		{
			if (graphic == null)
				return;

			graphic.CrossFadeColor(targetColor, instant ? 0f : selectable.colors.fadeDuration, true, true);
		}

	}

}
