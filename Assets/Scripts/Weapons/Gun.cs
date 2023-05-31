using UnityEngine;

public class Gun : Weapon
{
    [SerializeField]
    private float 
        _damage, 
        _spread;

    [SerializeField] 
    private ParticleSystem _attackParticle;

    [SerializeField]
    private Transform _firePosition;

    [SerializeField]
    private int _bulletsOneShoot;

    private Vector3[] _spreadBullets;
    private Vector3 _thicknessRays;


    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
      

        _thicknessRays =
            new Vector3(1.5f, 1.5f, 1.5f);

        _spreadBullets =
            new Vector3[_bulletsOneShoot];
    }

    private void SetSpread()
    {
      
        float evenOrNot = (float)_bulletsOneShoot;
        int numberBullet;

        if ( evenOrNot % 2 > 0)
        {
            _spreadBullets[0] = 
                Quaternion.Euler(0, 0, 0) * _firePosition.forward;

             numberBullet = 1;
        }
        else
        {
            numberBullet = 0;
        }

        float spreadLeft = 0;
        float spreadRight = 0;

        for (; numberBullet < _bulletsOneShoot; numberBullet++)
        {
            if (numberBullet <= _bulletsOneShoot / 2)
            {
                spreadLeft -= _spread;
                _spreadBullets[numberBullet] =
                    Quaternion.Euler(0, spreadLeft, 0) * _firePosition.forward;
            }
            else
            {
                spreadRight += _spread;
                _spreadBullets[numberBullet] = 
                    Quaternion.Euler(0, spreadRight, 0) * _firePosition.forward;
            }
        }
    }

    public override void Attack()
    {
      
        base.Attack();
        _attackParticle.Play();
        SetSpread();

        for (int i = 0; i < _spreadBullets.Length; i++)
        {
            if (Physics.BoxCast(_firePosition.position, _thicknessRays, _spreadBullets[i], out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<Damageable>(out Damageable damageble))
                {
                    damageble.ApplyDamage(_damage);
                }
            }
        }
    }

    public override void StopAttack()
    {

    }
}