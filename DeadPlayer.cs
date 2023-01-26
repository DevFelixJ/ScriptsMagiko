using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadPlayer : MonoBehaviour
{
    public GameObject panelMuerte;
    public GameObject puenteRandom;
    public GameObject desactivarPuente;
    GameObject puente;
    public Text contadorMuertes;
    Animator animatorPlayer;
    private int contador = 0;
    bool vivo = true;
    bool vivo2 = true;
    void Start(){
        animatorPlayer = GetComponent<Animator>();
        

    }
    void Update()
    {
        muerteUI();
        puente = GameObject.FindGameObjectWithTag("puente");
        desactivarPuente = puente;


    }
   public void Morir()
    {
        if (vivo)
        { 
           animatorPlayer.SetTrigger("Morir");
         //   sonidoCaida.Play();//testear no suena
            Debug.Log("COLISIONADO PLAYER MUERE");
            vivo = false;

        }
    }
    public void Morir2()
    {
        if (vivo2)
        { 
           // sonidoCaida.Play();  //testear no suena
           animatorPlayer.SetTrigger("Morir2");
            Debug.Log("COLISIONADO PLAYER MUERE2");
            vivo2 = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag.Equals("Enemy"))
        {
            hit.collider.GetComponent<EnemyBox>().PlayerInteractua();
            
        }
    }
    public void muerteUI()
    {
        if (vivo == false || vivo2 == false)
        {
            panelMuerte.SetActive(true);
            contador++;
            contadorMuertes.text = contador.ToString();
            while (contador % 5 == 0)
            {
                desactivarPuente.SetActive(false);
                puenteRandom.GetComponent<RandomBridge>().ActivarPuenteRandom();
                break;
            }


            if (vivo == false)
            {
                animatorPlayer.ResetTrigger("Morir");
                vivo = true;
            }
            else
            {
                animatorPlayer.ResetTrigger("Morir2");
                vivo2 = true;
            }
        }
    }


}
