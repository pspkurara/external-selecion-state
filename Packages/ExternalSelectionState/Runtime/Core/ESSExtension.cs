namespace Pspkurara.UI.ESS
{

	/// <summary>
	/// ESSの実装で使う拡張関数
	/// </summary>
	public static class ESSExtension
	{

		/// <summary>
		/// <see cref="ISelectableWithESS">の伝播を実行
		/// <see cref="UnityEngine.UI.Selectable.DoStateTransition(UnityEngine.UI.Selectable.SelectionState, bool)"/>内で呼び出すことを想定
		/// </summary>
		/// <param name="state">を表す数値</param>
		/// <param name="instant">かどうかを表す値</param>
		public static void DoStateTransitionFromSelectable(this ISelectableWithESS self, int state, bool instant)
		{
			if (!self.gameObject.activeInHierarchy) return;
			self.onDoStateTransition.Invoke((ExternalSelectionState)state, instant);
		}

	}

}
