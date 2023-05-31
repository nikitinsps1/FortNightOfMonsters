using UnityEngine;

public enum AmountTargets
{
    One = 0,
    Many = 1
}

public class MelleDamage : MonoBehaviour
{
    [SerializeField] 
    private TypeRealations _relations;

    [SerializeField] 
    private AmountTargets _amountTargets;

    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private float _damage;


    private void OnTriggerEnter(Collider other)
    {
        SetDamage(other);
    }

    protected void SetDamage(Collider collider)
    {
        if (collider.TryGetComponent(out Damageable damageable))
        {
            if (_relations != damageable.Relation)
            {
                damageable.ApplyDamage(_damage);

                if (_amountTargets == AmountTargets.One)
                {
                    DamageOff();
                }
            }
        }
    }

    public void DamageOn()
    {
        _collider.enabled = true;
    }

    public void DamageOff()
    {
        _collider.enabled = false;
    }
}