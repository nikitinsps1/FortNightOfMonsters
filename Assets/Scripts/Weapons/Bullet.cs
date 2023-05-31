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


    private float _defaultLifeTime;

    private Transform _transform;
    private GameObject ThisGameObject;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        ThisGameObject = gameObject;
        _defaultLifeTime = _lifeTime;
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

        _transform.Translate
            (Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        _lifeTime = _defaultLifeTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Damageable>(out Damageable damageble))
        {
            if (damageble.Relation != _relation)
            {
                damageble.ApplyDamage(_damage);
                ThisGameObject.SetActive(false);
            }
        }
    }
}