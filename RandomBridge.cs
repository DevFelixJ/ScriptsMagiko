using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBridge : MonoBehaviour
{
    public GameObject[] puentes;
    void Start()
    {
        ActivarPuenteRandom();
    }

    // Update is called once per frame
    public void ActivarPuenteRandom()
    {
        int n = Random.Range(0, puentes.Length);
        puentes[n].SetActive(true);

    }
}
