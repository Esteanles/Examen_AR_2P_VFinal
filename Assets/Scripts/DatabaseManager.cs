using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField]
    private string url = "https://haphwdgrerlahaeqhitu.supabase.co/rest/v1/";
    private const string PublishableKey = "sb_publishable_5ySq9HLCTt1TG07iqG_NOw_yGttj4A-";


    public void GuardarPuntuacion (string nombre, int pts)
    {
        PuntuacionExamen p = new PuntuacionExamen
        {
            usuario = nombre,
            puntuacion = pts,
            fecha = System.DateTime.UtcNow.ToString("o")
        };
        StartCoroutine(GuardarRequest(p));
    }
    private IEnumerator GuardarRequest (PuntuacionExamen p)
    {
        string json = JsonUtility.ToJson (p);
        byte[]body= System.Text.Encoding.UTF8.GetBytes(json);
        UnityWebRequest www= new UnityWebRequest (url+ "PuntuacionesExamen", "POST");
        www.uploadHandler= new UploadHandlerRaw(body);
        www.downloadHandler= new DownloadHandlerBuffer();
        www.SetRequestHeader("apikey", PublishableKey);
        www.SetRequestHeader ("Content-Type","application/json");
        www.SetRequestHeader("Prefer","return= representation");
        yield return www.SendWebRequest();
        if(www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Guardando:" + www.downloadHandler.text);
        }
        else
        {
           Debug.LogError(www.responseCode + ":" + www.error + "In" + www.downloadHandler.text); 
        }
    }
    
[System.Serializable]
public class PuntuacionExamen
{
    public string usuario;
    public int puntuacion;
    public string fecha;
}
   
}
