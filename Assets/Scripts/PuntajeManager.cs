using UnityEngine;
using TMPro;


public class PuntajeManager : MonoBehaviour
{
public static PuntajeManager Instance;
[SerializeField] private TMP_Text txtVidas, txtPuntos;
[SerializeField] private UIManager uiManager;
private int vidas = 3;
private int puntos = 0;
public bool juegoActivo= false;
public int Puntos => puntos;
    void Awake()
    {
    Instance = this; 
    }
    public void Iniciar()
    {
       vidas=3;
       puntos =0;
       juegoActivo = true;
       Actualizar();
    }
    public void Reiniciar()
    {
        vidas = 3; puntos =0;
        juegoActivo= false;
        Actualizar();
    }
    public void QuitarVida()
    {
        if(!juegoActivo) return;
        vidas --;
        Actualizar();
        if (vidas <=0)
        {
            juegoActivo = false;
            uiManager.MostrarGameOver(puntos);
        }
    }
    public void SumarPunto()
    {
        if(!juegoActivo)return;
        puntos++;
        Actualizar();
    }
    private void Actualizar()
    {
        txtVidas.text="Vidas: " + vidas;
        txtPuntos.text = "Puntos: " + puntos;
    }

}