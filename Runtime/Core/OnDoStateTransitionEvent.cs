using UnityEngine.Events;

namespace Pspkurara.UI.ESS
{

	/// <summary>
	/// 伝播のコールバックを格納するイベント
	/// </summary>
	[System.Serializable]
	public sealed class OnDoStateTransitionEvent : UnityEvent<ExternalSelectionState, bool> { }

}
