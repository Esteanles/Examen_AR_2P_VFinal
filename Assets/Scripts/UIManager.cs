using UnityEngine;
using TMPro;


public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject panelInicio, panelAction, panelGameOver;
    [SerializeField] private TMP_Text txtFinal, txtMensaje;
    [SerializeField] private TMP_InputField ifNombre;
    [SerializeField] private ARManager arManager;
    [SerializeField] private DatabaseManager db;

    public void BtnIniciar()
    {
        panelInicio.SetActive(false);
        panelAction.SetActive(true);
        arManager.IniciarAR();
    }

    public void MostrarGameOver(int puntos)
    {
        panelAction.SetActive(false);
        panelGameOver.SetActive(true);
        txtFinal.text= "Puntuacion: " + puntos;
    }

    public void BtnGuardar()
    {
        if(string.IsNullOrWhiteSpace(ifNombre.text))
        {
            txtMensaje.text= "Ingresa tu nombre";
            return;
        }
        txtMensaje.text = "";
        db.GuardarPuntuacion(ifNombre.text, PuntajeManager.Instance.Puntos);
    }
        public void BtnReiniciar()
        {
            ifNombre.text="";
            txtMensaje.text="";
            panelGameOver.SetActive(false);
            panelAction.SetActive(true);
            PuntajeManager.Instance.Reiniciar();
            arManager.ResetEscena();
        }
    }
    


