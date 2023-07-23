using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.IO;
using TMPro;

public class CambiaMesh : MonoBehaviour
{
    ControlesPlatos controles;

    int id_plato_nuevo;
    int id_plato_actual;
    
    Object platoS;
    Object platoM;
    Object platoL;
    public Transform zonaS;
    public Transform zonaM;
    public Transform zonaL;
    public TMP_Text nombrePlato;
    public TMP_Text nombrePlatoAnterior;
    public TMP_Text nombrePlatoSiguente;
    public GameObject menuCompleto;

    public static bool menuAbierto;

    void Awake(){
        controles = new ControlesPlatos();

        controles.Acciones.AvanzaPlato.performed += ctx => AvanzaPlato();
        controles.Acciones.RetrocedePlato.performed += ctx => RetrocedePlato();
        controles.Acciones.MuestraMenu.performed += ctx => MuestraMenu();
    }

    void OnEnable(){
        controles.Acciones.Enable();
    }

    void OnDisable(){
        controles.Acciones.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        id_plato_nuevo = id_plato_actual = 0;
        
        CargaPlatos(ListaPlatos.nombresPlatos[id_plato_actual]);

        menuCompleto.SetActive(false);
    }

    public void CargaPlatos(string plato_actual){
        GameObject platoCargado = Resources.Load<GameObject>(plato_actual + "/" + plato_actual + "S");
        if (platoCargado is not null)
        {
            platoS = Instantiate(platoCargado, zonaS.position, platoCargado.transform.rotation, zonaS);
        }
        platoCargado = Resources.Load<GameObject>(plato_actual + "/" + plato_actual + "M");
        if (platoCargado is not null)
        {
            platoM = Instantiate(platoCargado, zonaM.position, platoCargado.transform.rotation, zonaM);
        }
        platoCargado = Resources.Load<GameObject>(plato_actual + "/" + plato_actual + "L");
        if (platoCargado is not null)
        {
            platoL = Instantiate(platoCargado, zonaL.position, platoCargado.transform.rotation, zonaL);
        }

        CargaNombresPlatos();
    }

    void CargaNombresPlatos()
    {
        nombrePlato.text = ListaPlatos.nombresPlatos[id_plato_actual];
        if (id_plato_actual > 0) nombrePlatoAnterior.text = "Anterior: " + ListaPlatos.nombresPlatos[id_plato_actual - 1];
        else nombrePlatoAnterior.text = "";

        if (id_plato_actual < ListaPlatos.nombresPlatos.Count - 1) nombrePlatoSiguente.text = "Siguiente: " + ListaPlatos.nombresPlatos[id_plato_actual + 1];
        else nombrePlatoSiguente.text = ""; 
    }

    // Update is called once per frame
    void Update()
    {
        ActivaPlato();
    }

    void DesactivaTodos(){
        Destroy(platoL);
        Destroy(platoS);
        Destroy(platoM);
    }

    void ActivaPlato(){
        if (id_plato_nuevo != id_plato_actual){
            id_plato_actual = id_plato_nuevo;
            DesactivaTodos();
            CargaPlatos(ListaPlatos.nombresPlatos[id_plato_actual]);
        }     
    }

    public void ActivaPlatoPorNombre(string nombre)
    {
        int index = ListaPlatos.nombresPlatos.FindIndex(a => a.Contains(nombre));
        if (index != id_plato_actual)
        {
            id_plato_actual = id_plato_nuevo = index;
            DesactivaTodos();
            CargaPlatos(ListaPlatos.nombresPlatos[id_plato_actual]);
        }
    }

    void AvanzaPlato(){
        if(id_plato_nuevo < ListaPlatos.nombresPlatos.Count - 1 && menuCompleto.activeSelf == false)
        {
            id_plato_nuevo += 1;
        }
    }

    void RetrocedePlato(){
        if(id_plato_nuevo > 0 && menuCompleto.activeSelf == false) {
            id_plato_nuevo -= 1;
        }
    }

    void MuestraMenu()
    {
        menuCompleto.SetActive(true);
        FindObjectOfType<ControlaMenu>().SetSeleccionado(id_plato_actual);
    }
}
