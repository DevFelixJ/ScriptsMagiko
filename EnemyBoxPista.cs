using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoxPista : EnemyBox
{
    Color cambioVerde = Color.green;

    public GameObject flecha;
    public Material renderMaterial;
    public GameObject cuboGira;
    public RandomBridge randomBridge;
    public DeadPlayer deadPlayer;
    public override void PlayerInteractua()
    {
            flecha.SetActive(true);
            cuboGira.GetComponent<Renderer>().material = renderMaterial;
            flecha.GetComponent<Renderer>().material.color = cambioVerde;

            //Destroy(flecha, 7);
    }

    void Start()
    {
        randomBridge = FindObjectOfType<RandomBridge>();
        deadPlayer = FindObjectOfType<DeadPlayer>();
        flecha.SetActive(false);
    }

    public void DesactivarFlecha()
    {
        if(flecha.activeSelf == true)
        {
            GameObject.Find("Cube.019").GetComponent<Renderer>().material = randomBridge.sueloPuente;
            cuboGira.GetComponent <Renderer>().material = randomBridge.sueloPuente;
            flecha.SetActive(false);

        }
    }
}
