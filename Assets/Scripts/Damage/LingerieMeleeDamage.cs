using UnityEngine;

public class LingerieMeleeDamage : MeleeDamage
{
    private void OnTriggerStay(Collider other)
    {
        SetDamage(other);
    }
}