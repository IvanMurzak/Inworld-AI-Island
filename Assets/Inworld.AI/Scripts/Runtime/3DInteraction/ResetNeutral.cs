using UnityEngine;
namespace Inworld.Animation
{
    /// <summary>
    ///     This script attached to Neutral gesture.
    ///     Neutral is the main status of animation. Whenever some states enters it,
    ///     it'll reset all the flags.
    /// </summary>
    public class ResetNeutral : StateMachineBehaviour
    {
        static readonly int s_Motion = Animator.StringToHash("MainStatus");
        static readonly int s_Emotion = Animator.StringToHash("Emotion");
        static readonly int s_Gesture = Animator.StringToHash("Gesture");

        /// <summary>
        ///     This function is called in animator, bound to State Idle.
        /// </summary>
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetInteger(s_Motion, 0);
            animator.SetInteger(s_Emotion, 0);
            animator.SetInteger(s_Gesture, 0);
        }
    }
}
