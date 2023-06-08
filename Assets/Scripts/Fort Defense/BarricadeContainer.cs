using UnityEngine;

public class BarricadeContainer : MonoBehaviour
{
    [SerializeField]
    private Barricade[] _barricades;
    public Barricade[] Barricades => _barricades;

    public void Upgrade(Barricade barricade, int levelDefense, float addingHealth)
    {
        barricade.gameObject.SetActive(true);
        barricade.Solidify(levelDefense, addingHealth);
    }
}