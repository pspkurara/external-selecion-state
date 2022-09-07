using UnityEngine;
using Pspkurara.UI.ESS;
using UnityEngine.UI;

namespace Pspkurara.UI
{

	/// <summary>
	/// 拡張<see cref= "Selectable"/>用のインターフェース
	/// <see cref= "Selectable"/>を継承したコンポーネントに実装する
	/// </summary>
	public interface ISelectableWithESS
	{

		#region Selectable本体の実装

		/// <summary>
		/// <seealso cref="Component.gameObject"/>
		/// </summary>
		GameObject gameObject { get; }

		/// <summary>
		/// <seealso cref="Selectable.transition"/>
		/// </summary>
		Selectable.Transition transition { get; }

		/// <summary>
		/// <seealso cref="Selectable.colors"/>
		/// </summary>
		ColorBlock colors { get; }

		/// <summary>
		/// <seealso cref="Selectable.spriteState"/>
		/// </summary>
		SpriteState spriteState { get; }

		/// <summary>
		/// <seealso cref="Selectable.animationTriggers"/>
		/// </summary>
		AnimationTriggers animationTriggers { get; }

		#endregion

		#region 拡張の実装

		/// <summary>
		/// 外部に見せる用の<see cref="Selectable.currentSelectionState"/>>
		/// </summary>
		ExternalSelectionState currentExternalSelectionState { get; }

		/// <summary>
		/// <see cref="Selectable.DoStateTransition(Selectable.SelectionState, bool)">発生時のコールバック
		/// </summary>
		OnDoStateTransitionEvent onDoStateTransition { get; }

		#endregion

	}

}

