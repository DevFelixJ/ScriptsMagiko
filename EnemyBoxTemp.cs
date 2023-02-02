using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoxTemp : EnemyBox
{
    public RandomBridge texturaPuente;
    Color cambioAmarillo = Color.yellow;
    public Material renderMaterial;
    public Rigidbody rigidCaida;
    public GameObject cuboAmarillo;
    float tiempoCaida;
    public Vector3 posicionInicial;

    private void Start()
    {
        texturaPuente = FindObjectOfType<RandomBridge>();
        rigidCaida = GetComponent<Rigidbody>();
        posicionInicial = new Vector3(cuboAmarillo.transform.position.x, cuboAmarillo.transform.position.y, 
                                      cuboAmarillo.transform.position.z);
    }
    public override void PlayerInteractua()
    {
       // cuboAmarillo.GetComponent<Renderer>().material = renderMaterial;
      //  Debug.Log("Cambia Color amarillo 2 seg y destruye a 4 seg");
        Invoke("CambioAmarillo", 0.2f);
        Invoke("CaidaCubo", 1f);
       StartCoroutine(RestaurarCubo());


    }
    float RandomTiempoCaida() {
        tiempoCaida = Random.Range(0.1f, 5.5f);
        return tiempoCaida;
    }
    void CambioAmarillo()
     {
        cuboAmarillo.GetComponent<Renderer>().material = renderMaterial;
        //cuboAmarillo.GetComponent<Renderer>().material.color = cambioAmarillo;  
     }
    void CaidaCubo() 
    {
        
        rigidCaida.isKinematic = false;

    }

    public IEnumerator RestaurarCubo()
    {
        yield return new WaitForSeconds(2);
        rigidCaida.isKinematic = true;
        cuboAmarillo.transform.position = posicionInicial;
        cuboAmarillo.GetComponent<Renderer>().material = texturaPuente.sueloPuente;
        
        
    }

    void Update()
    {
        
    }
}
