using UnityEngine;

public class DragoAnimation : MonoBehaviour
{
    //Script Preso dall'Asset Store per risolvere un Bug, non andavano le animazioni
    [SerializeField] Animator eyeAnimator;
    public void Eyes(AnimationEvent animationEvent)
    {
        int receivedInt = animationEvent.intParameter;
        eyeAnimator.SetInteger("Eyes", receivedInt);
    }
}
