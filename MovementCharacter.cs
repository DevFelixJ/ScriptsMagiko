using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomBridge : MonoBehaviour
{
    public GameObject[] puentes;
    public Material sueloPuente;
    public GameObject puenteElegido;
    public GameObject puenteActivado;
    public int numeroPuente;

    void Start()
    {
        ActivarPuenteRandom();


    }
    private void Update()
    {
        puenteElegido = GameObject.FindGameObjectWithTag("puente");

        
    }

    //Update is called once per frame
    public void ActivarPuenteRandom()
    {//Coge del Array, un numero aleatorio de la posicion y lo activa.
     //Se guarda la posicion del array para que no se elimine al cambiar el puente o restaurarlo.

        int n = Random.Range(0, puentes.Length);
        puenteElegido = puentes[n];
        puenteElegido.SetActive(true);
        puenteActivado = puenteElegido;
        numeroPuente = n;
    }
    public void restaurarPuente()
    {//Destruye el puente y se instancia el gameObject del puenteElegido y
     //se guarda en el array el puente activado para no perderlo.
        Transform puenteCubos;
        puenteCubos = this.GetComponent<Transform>();
        Destroy(puenteActivado);
        puenteActivado = Instantiate(puenteElegido) as GameObject;
        puenteActivado.transform.SetParent(puenteCubos, false);
        puenteActivado.name = puenteElegido.name;
        puentes[numeroPuente] = puenteActivado;
       
    }

}
