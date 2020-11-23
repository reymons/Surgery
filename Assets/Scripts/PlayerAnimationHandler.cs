using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _anmtr;

    void Update()
    {
        UpdateAnimationWalk();
        UpdateAnimationHandWashing();
        UpdateAnimationGrabbing();
    }
    
    private void UpdateAnimationWalk()
    {
        _anmtr.SetFloat("Speed", Input.GetAxisRaw("Vertical"));
    }

    private void UpdateAnimationHandWashing()
    {
        if (Global.HandsAreWashed)
        {
            _anmtr.SetBool("IsWashing", true);
            Global.HandsAreWashed = false;
        }
        else
            _anmtr.SetBool("IsWashing", false);
    }

    private void UpdateAnimationGrabbing()
    {
        if (Global.PlayerGrabbed)
        {
            _anmtr.SetBool("IsGrabbing", true);
            Global.PlayerGrabbed = false; 
        }
        else
            _anmtr.SetBool("IsGrabbing", false);
    }
}
