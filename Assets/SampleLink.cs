using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Pspkurara.UI;

namespace Pspkurara.UI.Samples
{

	[AddComponentMenu("UI/Sample Link")]
	[RequireComponent(typeof(Selectable))]
	[ExecuteInEditMode]
	public class SampleLink : UIBehaviour
	{
		// Own Selectable.
		private ISelectableWithESS m_Selectable;

		/// <summary>
		/// Own Selectable.
		/// ISelectableWithESS must be inherited.
		/// </summary>
		public ISelectableWithESS selectable {
			get {
				// interface cannot be Serialized, so it should be acquired automatically.
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

			// Immediate reflection at initialization.
			OnDoStateTransition(selectable.currentExternalSelectionState, true);

			// Callback Registration.
			selectable.onDoStateTransition.AddListener(OnDoStateTransition);
		}

		protected override void OnDisable()
		{
			if (selectable == null) return;

			// Callback Release.
			selectable.onDoStateTransition.RemoveListener(OnDoStateTransition);
		}

		// Called when there is an actual change in state.
		private void OnDoStateTransition(ExternalSelectionState state, bool instant)
		{
			// any processing.
			Debug.Log(state);
		}

	}

}
