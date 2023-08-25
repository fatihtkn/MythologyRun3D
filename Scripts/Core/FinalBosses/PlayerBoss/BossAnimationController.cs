using UnityEngine;

public class BossAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetAnimation(string paramName)
    {
        animator.SetTrigger(paramName);
    }
    public void SetAnimation(string paramName,bool condition)
    {
        animator.SetBool(paramName, condition); 
    }




}
