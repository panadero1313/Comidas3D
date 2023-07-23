using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// Add the script to your Dropdown Menu Template Object via (Your Dropdown Button > Template)

[RequireComponent(typeof(ScrollRect))]
public class ControlaMenu : MonoBehaviour
{
    ControlesPlatos controles;

    // Set as Template Object via (Your Dropdown Button > Template)
    public ScrollRect m_templateScrollRect;

    // Set as Template Viewport Object via (Your Dropdown Button > Template > Viewport)
    public RectTransform m_templateViewportTransform;

    // Set as Template Content Object via (Your Dropdown Button > Template > Viewport > Content)
    public RectTransform m_ContentRectTransform;

    private RectTransform m_SelectedRectTransform;

    public GameObject botonTemplate;

    public GameObject menuCompleto;

    public GameObject mesaPrincipal;

    EventSystem m_EventSystem;

    int objetoSeleccionado = 0;
    public int nuevoSeleccionado = 0;
    int objetoInicial = 0;

    void Awake()
    {
        controles = new ControlesPlatos();

        controles.Acciones.AvanzaIndexMenu.performed += ctx => AvanzaIndex();
        controles.Acciones.RetrocedeIndexMenu.performed += ctx => RetrocedeIndex();
        controles.Acciones.MuestraMenu.performed += ctx => CambiaPlato();
        controles.Acciones.CancelaMenu.performed += ctx => CancelaMenu();
    }

    void OnEnable()
    {
        //Fetch the current EventSystem. Make sure your Scene has one.
        m_EventSystem = EventSystem.current;

        controles.Acciones.Enable();
    }

    void OnDisable()
    {
        controles.Acciones.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (string plato in ListaPlatos.nombresPlatos)
        {
            GameObject btn = Instantiate(botonTemplate, m_ContentRectTransform);
            Text textBoton = btn.GetComponentInChildren<Text>();
            textBoton.text = plato;
            btn.GetComponent<Button>().onClick.AddListener(delegate { CargaPlato(plato); });
        }
        m_EventSystem.SetSelectedGameObject(m_ContentRectTransform.GetChild(nuevoSeleccionado).gameObject);
        m_EventSystem.currentSelectedGameObject.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.green;
        m_EventSystem.currentSelectedGameObject.GetComponentInChildren<Text>().color = Color.black;
        m_EventSystem.currentSelectedGameObject.GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
    }

    void CargaPlato(string nombre)
    {
        mesaPrincipal.GetComponent<CambiaMesh>().ActivaPlatoPorNombre(nombre);
        menuCompleto.SetActive(false);
    }

    void CambiaPlato()
    {
        objetoInicial = nuevoSeleccionado = objetoSeleccionado;
        m_EventSystem.currentSelectedGameObject.GetComponentInChildren<Button>().onClick.Invoke();
    }

    void Update()
    {
        UpdateSelected();
    }

    void UpdateSelected()
    {
        if (objetoSeleccionado == nuevoSeleccionado)
        {
            return;
        }
        else
        {
            CambiaSelected();
        }
    }

    void CambiaSelected()
    {
        if (m_EventSystem.currentSelectedGameObject != null)
        {
            GameObject obj = m_ContentRectTransform.GetChild(objetoSeleccionado).gameObject;
            obj.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;
            obj.GetComponentInChildren<Text>().color = Color.black;
            obj.GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
        }

        m_EventSystem.SetSelectedGameObject(m_ContentRectTransform.GetChild(nuevoSeleccionado).gameObject);
        m_EventSystem.currentSelectedGameObject.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.green;
        m_EventSystem.currentSelectedGameObject.GetComponentInChildren<Text>().color = Color.black;
        m_EventSystem.currentSelectedGameObject.GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;

        objetoSeleccionado = nuevoSeleccionado;
    }

    void AvanzaIndex()
    {
        if (nuevoSeleccionado < ListaPlatos.nombresPlatos.Count - 1)
        {
            nuevoSeleccionado++;
        }
    }

    void RetrocedeIndex()
    {
        if (nuevoSeleccionado > 0)
        {
            nuevoSeleccionado--;
        }
    }

    void CancelaMenu()
    {
        nuevoSeleccionado = objetoInicial;
        CambiaSelected();
        menuCompleto.SetActive(false);
    }

    public void SetSeleccionado(int seleccionado)
    {
        nuevoSeleccionado = seleccionado;
    }
}
