using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    [SerializeField] 
    private TypeRealations _relations;

    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private float _damage;

    protected void SetDamage(Collider collider)
    {
        if (collider.TryGetComponent(out Damageable damageable))
        {
            if (_relations != damageable.Relation)
            {
                damageable.ApplyDamage(_damage);
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