using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float velocidad = 1.5f;
    private void OnTriggerEnter(Collider other)
    {
       bool esjugador = other.CompareTag ("Jugador") ||
       (other.attachedRigidbody!=null && other.attachedRigidbody.CompareTag("Jugador"));
       if(!esjugador)return;
       if(CompareTag("Obstaculo"))PuntajeManager.Instance.QuitarVida();
       else if (CompareTag("Moneda")) PuntajeManager.Instance.SumarPunto();
       Destroy (gameObject);
    }
    void Update()
    {
        transform.position += -transform.parent.forward * velocidad * Time.deltaTime;
    }
}
