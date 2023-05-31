using UnityEngine;

[RequireComponent(typeof(Damageable))]
[RequireComponent (typeof(CharacterAnimator))]
public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float _speedRotation;

    public Damageable ThisDamageable
    { get; protected set; }  
    public Transform ThisTransform 
    { get; private set; }
    public CharacterAnimator ThisCharacterAnimator 
    { get; private set; }

    protected float SpeedRotation => _speedRotation;

    private void Awake()
    {
        Init();
    }


    private void OnApplyDamage()
    {
        ThisCharacterAnimator.ApplyHit();
    }

    protected virtual void OnEnable()
    {
        ThisDamageable.OnApplyDamage += OnApplyDamage;
    }

    protected virtual void OnDisable()
    {
        ThisDamageable.OnApplyDamage -= OnApplyDamage;
    }

    protected virtual void Init()
    {
        ThisTransform = transform;
        ThisDamageable = GetComponent<Damageable>();
        ThisCharacterAnimator = GetComponent<CharacterAnimator>();
    }

    public virtual void RotateTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - ThisTransform.position;
        direction.Normalize();

        float angleRotation 
            = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        ThisTransform.rotation =
            Quaternion.RotateTowards
            (ThisTransform.rotation, Quaternion.Euler(0, angleRotation, 0),
            SpeedRotation * Time.deltaTime);
    }

    public virtual void Attack()
    {
        ThisCharacterAnimator.StartAttack();
    }

    public virtual void StopAttack()
    {
        ThisCharacterAnimator.StopAttack();
    }
}