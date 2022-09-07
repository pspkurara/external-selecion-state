#if ESS_UGUISKINNER_SUPPORT

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

namespace Pspkurara.UI
{

	/// <summary>
	/// <see cref="Selectable"/>を継承したコンポーネントのTransitionを<see cref="UISkinner"/>と紐付けられる
	/// </summary>
	[AddComponentMenu("UI/Selectable Skinner Link")]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Selectable))]
	public sealed class SelectableSkinnerLink : UIBehaviour
	{

		/// <summary>
		/// ステートごとの設定
		/// </summary>
		[System.Serializable]
		private sealed class StateContent
		{

			/// <summary>
			/// ステート
			/// </summary>
			[SerializeField]
			private ExternalSelectionState m_State = ExternalSelectionState.Disabled;

			/// <summary>
			/// ステートが変わった際に反映させるスキナーのスタイルキー
			/// </summary>
			[SerializeField]
			private string m_StyleKey = string.Empty;

			/// <summary>
			/// ステート
			/// </summary>
			public ExternalSelectionState state { get { return m_State; } }

			/// <summary>
			/// ステートが変わった際に反映させるスキナーのスタイルキー
			/// </summary>
			public string styleKey { get { return m_StyleKey; } }

		}

		/// <summary>
		/// ステートごとの設定
		/// </summary>
		[SerializeField]
		private StateContent[] m_StateContents = System.Array.Empty<StateContent>();

		/// <summary>
		/// 未設定のステートで反映されるスタイルキー
		/// </summary>
		[SerializeField]
		private string m_DefaultStyleKey = null;

		private ISelectableWithESS m_Selectable;

		/// <summary>
		/// 自身の<see cref="Selectable"/>
		/// 継承されている必要がある
		/// </summary>
		public ISelectableWithESS selectable
		{
			get {
				if (m_Selectable == null)
				{
					m_Selectable = GetComponent<ISelectableWithESS>();
				}
				return m_Selectable;
			}
		}

		[SerializeField]
		private UISkinner m_Skinner = null;

		/// <summary>
		/// 使われるスキナー
		/// </summary>
		public UISkinner Skinner { get { return m_Skinner; } }

		protected override void OnEnable()
		{
			// コールバックを設定
			if (selectable == null) return;
			selectable.onDoStateTransition.AddListener(OnDoStateTransition);
		}

		protected override void OnDisable()
		{
			// コールバックをはがす
			if (selectable == null) return;
			selectable.onDoStateTransition.RemoveListener(OnDoStateTransition);
		}

		/// <summary>
		/// ステートが変わった際に呼び出される
		/// </summary>
		/// <param name="state">変化後のステート</param>
		/// <param name="instant">即時反映</param>
		private void OnDoStateTransition(ExternalSelectionState state, bool instant)
		{
			if (m_Skinner == null) return;
			m_Skinner.SetSkin(GetStyleKey(state));
		}

		/// <summary>
		/// ステートに一致するスタイルキーを取得する
		/// </summary>
		/// <param name="state">ステート</param>
		/// <returns>スタイルキー</returns>
		private string GetStyleKey(ExternalSelectionState state)
		{
			// まず一致するものを探す
			var content = m_StateContents.FirstOrDefault(s => s.state == state);
			// なければデフォルト値
			if (content == null) return m_DefaultStyleKey;
			return content.styleKey;
		}

#if UNITY_EDITOR

		protected override void Reset()
		{
			// 1つはあったほうがいいだろう
			m_StateContents = new StateContent[]
			{
				new StateContent()
			};
			// 適当なスキナーを取ってくる
			m_Skinner = GetComponentInChildren<UISkinner>();
		}

#endif

	}

}

#endif
