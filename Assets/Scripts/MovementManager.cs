using UnityEngine;


public class MovementManager : MonoBehaviour
{
    [SerializeField]
    private Transform modeloAR;
    [Header("Rango")]
    public float minX = -1;
    public float maxX = 1;
    private Vector3 posicionInicial;
    private Vector3 ejeX;

    public void SetModeloAR(Transform modeloTransform)
    {
        modeloAR= modeloTransform.GetChild(0);
        if (modeloAR == null)
        {
            Debug.LogError ("ModeloAR no definido");
            return;
        }
        posicionInicial = modeloAR.position;
        ejeX = modeloAR.right;
    }
    void Update()
    {
        if(modeloAR == null)
        return;
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch (0);
        if(touch.phase == TouchPhase.Began||
            touch.phase == TouchPhase.Moved||
            touch.phase == TouchPhase.Stationary)
        {
           MoverObjetoAR(touch.position);
        }
    }
    private void MoverObjetoAR(Vector2 posicionTouch)
    {
        float normalizedX = posicionTouch.x/Screen.width;
        float movementX = Mathf.Lerp(minX,maxX, normalizedX);
        modeloAR.position = posicionInicial + ejeX * movementX;
    }
}
