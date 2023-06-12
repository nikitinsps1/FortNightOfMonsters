using UnityEngine;
using Zenject;

public class Gun : Weapon
{
    [SerializeField]
    private BulletTrace[] gunLine;

    [SerializeField]
    private ParticleSystem _attackParticle;

    [SerializeField]
    private Transform _firePosition;

    [SerializeField]
    private float
       _damage,
       _spread;

    [SerializeField]
    private int _bulletsOneShoot;

    private ParticlesContainer _particles;

    private Vector3[] _spreadBullets;
    private Ray _rayBulletTrace;

    private float _radiusBullet = 1.2f;
    private float _maxLengthTrace = 35f;

    [Inject]
    private void Construct(ParticlesContainer particles)
    {
        _particles = particles;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _rayBulletTrace = new Ray();
        _spreadBullets = new Vector3[_bulletsOneShoot];
    }

    private void SetSpread()
    {
        int numberBullet = 0;
        float angleLeft = 0;
        float angleRight = 0;

        if (_bulletsOneShoot % 2 > 0)
        {
            _spreadBullets[0] =
                Quaternion.Euler(0, 0, 0) * _firePosition.forward;

            numberBullet++;
        }

        for (; numberBullet < _bulletsOneShoot; numberBullet++)
        {
            if (numberBullet <= _bulletsOneShoot / 2)
            {
                SetShootVector
                    (ref _spreadBullets[numberBullet], ref angleLeft, -_spread);
            }
            else
            {
                SetShootVector
                    (ref _spreadBullets[numberBullet], ref angleRight, _spread);
            }
        }
    }

    private void SetShootVector(ref Vector3 vector, ref float angle, float spread)
    {
        angle += spread;
        vector = Quaternion.Euler(0, angle, 0) * _firePosition.forward;
    }

    private void DrawBulletTrace(BulletTrace lineLife, float length)
    {
        Vector3 hitPoint =
            _rayBulletTrace.origin + _rayBulletTrace.direction * length;

        _particles.GetObject(
            (int)TypeDeadParticles.Hit,
            hitPoint,
            Quaternion.identity);

        lineLife.DrawLine(_rayBulletTrace.origin, hitPoint);
    }

    public override void Attack()
    {
        base.Attack();
        _attackParticle.Play();
        SetSpread();

        for (int i = 0; i < _spreadBullets.Length; i++)
        {
            _rayBulletTrace.origin = _firePosition.position;
            _rayBulletTrace.direction = _spreadBullets[i];
            float length = _maxLengthTrace;

            if (Physics.SphereCast(_rayBulletTrace, _radiusBullet, out RaycastHit hit))
            {
                length = hit.distance;

                if (hit.collider.TryGetComponent(out Damageable damageable))
                {
                    if (damageable.Relation == TypeRelations.Enemy)
                    {
                        damageable.ApplyDamage(_damage);
                    }
                }
            }
            DrawBulletTrace(gunLine[i], length);
        }
    }
}