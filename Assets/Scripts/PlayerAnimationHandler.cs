using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _anmtr;

    void Update()
    {
        UpdateAnimationWalk();
        UpdateAnimationHandWashing();
    }
    
    private void UpdateAnimationWalk()
    {
        _anmtr.SetFloat("Speed", Input.GetAxisRaw("Vertical"));
    }

    private void UpdateAnimationHandWashing()
    {
        _anmtr.SetBool("IsWashing", Global.HandsAreWashed);
    }
}
