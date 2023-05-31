using UnityEngine;

public class LingerieMeleDamage : MelleDamage
{
    private void OnTriggerStay(Collider other)
    {
        SetDamage(other);
    }
}