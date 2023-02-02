using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBridge : MonoBehaviour
{
    public GameObject[] puentes;
    public Material sueloPuente;
    public GameObject puenteElegido;
    public GameObject puenteActivado;
    

    void Start()
    {
        ActivarPuenteRandom();
        EncontrarPuente();


    }
    private void Update()
    {
            StartCoroutine(EncontrarPuente());

        
    }

    //Update is called once per frame
    public void ActivarPuenteRandom()
    {
        int n = Random.Range(0, puentes.Length);
        puenteElegido = puentes[n];
        puenteElegido.SetActive(true);

    }
    public void restaurarPuente()
    {
        Transform puenteCubos;
        puenteCubos = this.GetComponent<Transform>();
        Destroy(puenteActivado);
        puenteActivado = Instantiate(puenteElegido) as GameObject;
        puenteActivado.transform.SetParent(puenteCubos, false);
        puenteActivado.name = puenteElegido.name;
        puenteElegido = puenteActivado;
    }

    public IEnumerator EncontrarPuente()
    {
        yield return new WaitForSeconds(1);
        puenteActivado = GameObject.FindGameObjectWithTag("puente");

    }
}
