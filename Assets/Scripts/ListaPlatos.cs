using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ListaPlatos : MonoBehaviour
{

    public static List<string> nombresPlatos = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        CargaListaPlatos();
    }

    void CargaListaPlatos()
    {
        //var textFile = Resources.Load<TextAsset>("Text/textFile01");
        //StreamReader reader = new StreamReader(Resources.Load<TextAsset>("Platos.txt"));
        string[] AllWords = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "Platos.txt"));
        nombresPlatos = new List<string>(AllWords);
    }
}
