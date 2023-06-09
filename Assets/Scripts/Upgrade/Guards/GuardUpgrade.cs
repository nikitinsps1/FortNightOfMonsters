using UnityEngine;

public class GuardUpgrade : Upgrader
{
    [SerializeField]
    private Guard _guard;


    public override void Upgrade()
    {

        base.Upgrade();
        if (_guard.gameObject.activeSelf == false)
        {
            _guard.gameObject.SetActive(true);
        }

        _guard.ThisArsenal
            .Change((TypeWeapons)Level);

    }
}
