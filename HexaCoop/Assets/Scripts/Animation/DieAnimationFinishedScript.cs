using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAnimationFinishedScript : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ActionEvents.DieAnimationFinished?.Invoke(animator);
    }

}
