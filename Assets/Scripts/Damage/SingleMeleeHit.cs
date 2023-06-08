using UnityEngine;

public class SingleMeleeHit : MeleeDamage
{
    private void OnTriggerEnter(Collider other)
    {
        SetDamage(other);

    }
}
