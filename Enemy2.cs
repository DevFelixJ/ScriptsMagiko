using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.GetComponent<Animator>().SetTrigger("Morir2");
            StartCoroutine(MuertePlayer());
        }
    }
    IEnumerator MuertePlayer()
    {
        yield return new WaitForSeconds(4);
        GameObject.FindGameObjectWithTag("Player").GetComponent<DeadPlayer>().vivo2 = false;
    }
}
