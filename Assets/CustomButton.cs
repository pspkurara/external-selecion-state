using UnityEngine;
using UnityEngine.UI;
using Pspkurara.UI;
using Pspkurara.UI.ESS;

namespace Pspkurara.UI.Samples
{

	public class CustomButton : Button, ISelectableWithESS
	{
		// Use UnityEvent to register in the inspector and execute in the editor.
		[SerializeField]
		private OnDoStateTransitionEvent m_OnDoStateTransitionEvent = new OnDoStateTransitionEvent();

		public ExternalSelectionState currentExternalSelectionState {   // Convert protected Selectable.SelectionState to public.
			get { return (ExternalSelectionState)(int)base.currentSelectionState; }
		}

		public OnDoStateTransitionEvent onDoStateTransition {   // Callback executed when DoStateTransition is called.
			get { return m_OnDoStateTransitionEvent; }
		}

		protected override void DoStateTransition(SelectionState state, bool instant)
		{
			base.DoStateTransition(state, instant);
			// Trigger from UnityEngine.UI.Selectable.
			this.DoStateTransitionFromSelectable((int)state, instant);
		}
	}

}
