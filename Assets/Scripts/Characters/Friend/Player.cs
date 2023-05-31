using UnityEngine;
using Zenject;

[RequireComponent(typeof(Arsenal))]
[RequireComponent (typeof(Rigidbody))]
public class Player : Character
{
    [SerializeField]
    private float _moveSpeed;

    private Rigidbody _rigidBody;
    private DeadParticlesConteiner _deadParticles;
    private AudioContainer _audioEffects;

    private Quaternion _angleCamera;

    private Vector3
        _moveDirection,
        _inverseVector;

    public Arsenal ThisArsenal
    { get; private set; }

    [Inject]
    private void Construct(AudioContainer audioEffects, DeadParticlesConteiner deadParticles)
    {
        _deadParticles = deadParticles;
        _audioEffects = audioEffects;
    }


    protected override void OnEnable()
    {
        base.OnEnable();

        ThisArsenal.OnChangedWeapon +=
            ThisCharacterAnimator.ChangeWeapon;

        ThisCharacterAnimator.OnAnimationEvent
            += ThisArsenal.Attack;

        ThisCharacterAnimator.OnEndAnimationEvent
            += ThisArsenal.StopAttack;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        ThisArsenal.OnChangedWeapon
            -= ThisCharacterAnimator.ChangeWeapon;

        ThisCharacterAnimator.OnAnimationEvent
            -= ThisArsenal.Attack;

        ThisCharacterAnimator.OnEndAnimationEvent
            -= ThisArsenal.StopAttack;
    }

    public void Move(Vector3 _direction)
    {
        _moveDirection =  _angleCamera * _direction;

        _rigidBody
            .MovePosition
            (ThisTransform.position + _moveDirection * _moveSpeed * Time.fixedDeltaTime);

        _inverseVector =
            ThisTransform.InverseTransformVector(_direction);

        ThisCharacterAnimator.Move(_inverseVector);
    }

    protected override void Init()
    {
        base.Init();

        ThisArsenal = GetComponent<Arsenal>();
        _rigidBody = GetComponent<Rigidbody>();

        ThisArsenal.Init();

        ThisDamageable
         .Construct(ThisTransform, _deadParticles, _audioEffects);

        float cameraDegreeY = Camera.main.transform.rotation.eulerAngles.y;
        _angleCamera = Quaternion.Euler(0, cameraDegreeY, 0);
    }


    public override void StopAttack()
    {
    
        base.StopAttack();
        ThisArsenal .StopAttack();
    }

    public void Aim(float angleRotation)
    {
        ThisTransform.rotation = Quaternion
            .RotateTowards(
             ThisTransform.rotation, Quaternion.Euler(0, angleRotation, 0), 
             Time.deltaTime * SpeedRotation);
    }
}