using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoGolpe : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Animator>().SetTrigger("Morir");
        StartCoroutine(MuertePlayer());
    }
    IEnumerator MuertePlayer()
    {
        yield return new WaitForSeconds(4);
        GameObject.FindGameObjectWithTag("Player").GetComponent<DeadPlayer>().vivo = false;
    }
}
