using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBoxTemp : EnemyBox
{
    public RandomBridge texturaPuente;
    Color cambioAmarillo = Color.yellow;
    public Material renderMaterial;
    public Rigidbody rigidCaida;
    public GameObject cuboAmarillo;
    float tiempoCaida;
    public DeadPlayer DeadPlayer;
    public List<Vector3> cuboGuardado;
    int contador = 0;

    private void Start()
    {
        DeadPlayer = FindObjectOfType<DeadPlayer>();
        texturaPuente = FindObjectOfType<RandomBridge>();
        rigidCaida = GetComponent<Rigidbody>();


    }
    public override void PlayerInteractua()
    {
        // cuboAmarillo.GetComponent<Renderer>().material = renderMaterial;
        //  Debug.Log("Cambia Color amarillo 2 seg y destruye a 4 seg");
        Invoke("CambioAmarillo", 0.2f);
        Invoke("CaidaCubo", 0.5f);
        Invoke("ColliderCubo", 1.5f);

    }
    float RandomTiempoCaida()
    {
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

        cuboAmarillo.GetComponent<MeshRenderer>().enabled = false;

    }
    void ColliderCubo()
    {
        cuboAmarillo.GetComponent<Collider>().enabled = false;
    }

    public void RestaurarCubo()
    {
        contador++;
        if (contador < 5)
        {
            Transform[] losasPuente = GameObject.FindGameObjectWithTag("puente").GetComponentsInChildren<Transform>();
            for (int i = 0; i < losasPuente.Length; i++)
            {
                if (losasPuente[i].gameObject.GetComponent<Renderer>() != null && losasPuente[i].gameObject.GetComponent<Rigidbody>() != null)
                {
                    losasPuente[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
                    losasPuente[i].gameObject.GetComponent<Collider>().enabled = true;
                    losasPuente[i].gameObject.GetComponent<Renderer>().material = texturaPuente.sueloPuente;
                    losasPuente[i].gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    losasPuente[i].gameObject.transform.position = cuboGuardado[i];
                }
            }

        }
    }
    void GuardarCubos()
    {
        if (contador == 0 || contador == 4)
        {
            cuboGuardado = new List<Vector3>();
            Transform[] posicionesLosas = GameObject.FindGameObjectWithTag("puente").GetComponentsInChildren<Transform>();
            foreach (Transform posicionLosa in posicionesLosas)
            {
                cuboGuardado.Add(posicionLosa.gameObject.transform.position);

            }
            contador = 0;

        }
    }

    void Update()
    {
        GuardarCubos();
    }
}
