using UnityEngine;

[RequireComponent(typeof(Damageable))]
[RequireComponent(typeof(CharacterAnimator))]

public abstract class CharacterLogic : MonoBehaviour
{
    [SerializeField]
    private float _speedRotation;

    public Damageable ThisDamageable
    { get; protected set; }
    public Transform ThisTransform
    { get; private set; }
    public CharacterAnimator ThisCharacterAnimator
    { get; private set; }

    public float SpeedRotation => _speedRotation;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        ThisTransform = transform;
        ThisCharacterAnimator = GetComponent<CharacterAnimator>();
        ThisDamageable = GetComponent<Damageable>();
    }

    protected virtual void OnEnable()
    {
        ThisDamageable.OnApplyDamage +=
            ThisCharacterAnimator.ApplyHit;
    }

    protected virtual void OnDisable()
    {
        ThisDamageable.OnApplyDamage -=
            ThisCharacterAnimator.ApplyHit;
    }

    public void RotateTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - ThisTransform.position;
        direction.Normalize();

        float angleRotation =
            Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        ThisTransform.rotation =
            Quaternion.RotateTowards
            (ThisTransform.rotation, Quaternion.Euler(0, angleRotation, 0),
            SpeedRotation * Time.deltaTime);
    }

    public abstract void Attack();
    public abstract void StopAttack();
}