using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator: MonoBehaviour
{ 
    private Animator _animator;

    public event Action
        OnAnimationEvent,
        OnEndAnimationEvent;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.Play(0, 0, Random.value);
    }

    public void StartAttack()
    {
        _animator.SetBool(ContainerStrings.IsAttack, true);
    }

    public void StopAttack()
    {
        _animator.SetBool(ContainerStrings.IsAttack, false);
    }

    public void ApplyHit()
    {
        _animator.SetTrigger(ContainerStrings.Hit);
    }

    public void ChangeWeapon(TypeWeapons type)
    {
        _animator.SetFloat
            (ContainerStrings.NumberCurrentWeapon, (int)type);
    }

    public void Move(Vector3 blendTreeParameters)
    {
        _animator.SetFloat(
            ContainerStrings.InputMagnitude,
            blendTreeParameters.magnitude,
            0.1f,
            Time.deltaTime * 2);

        _animator.SetFloat(
            ContainerStrings.Horizontal,
            blendTreeParameters.x,
            0.1f,
            Time.deltaTime * 2);

        _animator.SetFloat(
            ContainerStrings.Vertical,
            blendTreeParameters.z,
            0.1f,
            Time.deltaTime * 2);
    }

    public void AnimationEvent()
    {
        OnAnimationEvent?.Invoke();
    }

    public void EndAnimationEvent()
    {
        OnEndAnimationEvent?.Invoke();
    }
}