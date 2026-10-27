using UnityEngine;
using System.Collections;


public class RandomManager : MonoBehaviour

{
    public ObstaculoManager[] obstacleParent;
    public float tiempoSpawn = 1.5f;
    private bool isSpawning=true;
    void Start()
    {
      StartCoroutine(RandomSpawn());
    }
    private IEnumerator RandomSpawn()
    {
        while(isSpawning)
        {
            yield return new WaitForSeconds (tiempoSpawn);
            if (PuntajeManager.Instance.juegoActivo)
            {
                int randomIndex = Random.Range (0, obstacleParent.Length);
                obstacleParent[randomIndex].InstantiateObstaculo();
            }
        }
    } 
}
