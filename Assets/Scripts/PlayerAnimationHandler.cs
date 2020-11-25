using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _anmtr;

    void Update()
    {
        UpdateAnimationWalk();
        UpdateAnimationHandWashing();
        UpdateAnimationGrabbing();
        UpdateAnimationWorkingOnLeg();
    }
    
    private void UpdateAnimationWalk()
    {
        _anmtr.SetFloat("Speed", Input.GetAxisRaw("Vertical"));
    }

    private void UpdateAnimationHandWashing()
    {
        if (Global.ForAnimHandsAreWashed)
        {
            _anmtr.SetBool("IsWashing", true);
            Global.ForAnimHandsAreWashed = false;
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

    private void UpdateAnimationWorkingOnLeg()
    {
        if (Global.PlayerIsWorkingOnleg)
        {
            _anmtr.SetBool("IsWorkingOnLeg", true);
            Global.PlayerIsWorkingOnleg = false;
        }
        else
            _anmtr.SetBool("IsWorkingOnLeg", false);
    }
}
