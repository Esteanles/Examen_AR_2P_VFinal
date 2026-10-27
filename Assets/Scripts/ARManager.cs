using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ARManager : MonoBehaviour
{
[Header("componenete AR")]

[SerializeField]
private ARRaycastManager raycastManager;
[SerializeField]
private ARPlaneManager planeManager;
[SerializeField]
private Camera camaraAR;
[Header("Elementos en escena")]
[SerializeField]
private GameObject prefabIndicador;
[SerializeField]
private GameObject prefabModelo;
[Header("UI")]
public Button btnColocar;
public Button btnReiniciar;
private GameObject indicador;
private GameObject modelo;
private bool planosDetectados=false;
private bool modeloColocado=false;
private List <ARRaycastHit> hits = new List<ARRaycastHit>();
[SerializeField]
private MovementManager movementManager; 
void Start()
{
    indicador=Instantiate(prefabIndicador);
    indicador.SetActive(false);
    planeManager.planesChanged += OnPlanesChanged;
    planeManager.enabled = false;
    btnColocar.interactable = false;
}
private void OnDestroy()
{
    planeManager.planesChanged -= OnPlanesChanged;
}
void OnPlanesChanged (ARPlanesChangedEventArgs eventArgs)
{
    if(eventArgs.added.Count>0)
    {
        planosDetectados = true;
    }
}

    // Update is called once per frame
    void Update()
    {
      if (modeloColocado) return;
      if (!planosDetectados) return;
      Vector2 centroPantalla = new Vector2 (Screen.width/ 2, Screen.height / 2 );
      if(raycastManager.Raycast(centroPantalla, hits, TrackableType.PlaneWithinPolygon))
    {
        Pose hitPose= hits [0].pose;
        indicador. SetActive (true);
        indicador.transform.position = hitPose.position;
        indicador.transform.rotation = Quaternion.Euler (0f, 0f, 0f);
        btnColocar.interactable = true;
    }
    else
    {
        indicador.SetActive (false);
        btnColocar. interactable = false; 
    }
    }
    public void ColocarModelo()
{
    if (!indicador.activeSelf) return;
    Vector3 posicionSpawn = indicador.transform.position;
    Vector3 rotacionCamara = camaraAR.transform.eulerAngles;
    Quaternion rotacionSpawn = Quaternion.Euler (0f,rotacionCamara.y, 0f);
    modelo=Instantiate (prefabModelo, posicionSpawn, rotacionSpawn);
    indicador.SetActive(false);
    modeloColocado = true;
    movementManager.SetModeloAR (modelo.transform);
    Desactivarplanos();
    PuntajeManager.Instance.Iniciar();

}
private void Desactivarplanos()
{
    foreach (var plano in planeManager.trackables)
    {
        plano.gameObject.SetActive (false);
    }
    planeManager.enabled = false;
}
public void ResetEscena()
{
    if (modelo != null)
    {
        Destroy(modelo);
        modelo = null;
    }
    modeloColocado = false;
    planeManager.enabled = true;
    foreach (var plano in planeManager.trackables)
    {
        plano.gameObject.SetActive(true);
    }
    indicador.SetActive(false);
    btnColocar.interactable = false;
}
public void IniciarAR()
{
    planeManager.enabled=true;
}
}

