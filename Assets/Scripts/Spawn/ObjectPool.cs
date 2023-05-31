using UnityEngine;


public class  ObjectPool: MonoBehaviour 
{
    public Transform ThisTransform { get; private set; }
    public GameObject ThisGameObject { get; private set; }


    public  void Init()
    {
        ThisTransform = transform;
        ThisGameObject = gameObject;
    }
}
