using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _anmtrWalk;

    void Update()
    {
        UpdateAnimationWalk();
    }

    private void UpdateAnimationWalk()
    {
        _anmtrWalk.SetFloat("Speed", Input.GetAxisRaw("Vertical"));
    }
}
