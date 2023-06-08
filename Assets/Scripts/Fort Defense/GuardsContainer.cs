using UnityEngine;

public class GuardsContainer : MonoBehaviour
{
    [SerializeField]
    private Guard[] _guards;
    public Guard[] Guards => _guards;

    public void AddRankGuard(Guard guard, int rank)
    {
        if (guard.gameObject.activeSelf == false)
        {
            guard.gameObject.SetActive(true);
        }

        guard.ThisArsenal
            .Change((TypeWeapons)rank);
    }
}
