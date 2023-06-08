using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private TypeRealations _relation;

    [SerializeField]
    private float
        _damage,
        _speed,
        _lifeTime;

    private Transform _transform;
    private GameObject ThisGameObject;

    private float _defaultLifeTime;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _defaultLifeTime = _lifeTime;

        ThisGameObject = gameObject;
    }

    private void Update()
    {
        Fly();
    }

    private void Fly()
    {
        _lifeTime -= Time.deltaTime;

        if (_lifeTime <= 0)
        {
            gameObject.SetActive(false);
        }

        _transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        _lifeTime = _defaultLifeTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Damageable damageable))
        {
            if (damageable.Relation != _relation)
            {
                damageable.ApplyDamage(_damage);
                ThisGameObject.SetActive(false);
            }
        }
    }
}