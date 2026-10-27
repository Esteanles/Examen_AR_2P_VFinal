using UnityEngine;

public class ObstaculoManager : MonoBehaviour
{
   [SerializeField] private GameObject prefabObstaculo;
    public void InstantiateObstaculo()
    {
       GameObject obj= Instantiate(prefabObstaculo, transform.position, transform.rotation, transform);
       Destroy(obj, 3f);
    }
}
