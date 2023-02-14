using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class DeadPlayer : MonoBehaviour
{
    public GameObject panelMuerte;
    public GameObject puenteRandom;
    public RandomBridge desactivarPuente;
    public Text contadorMuertes;
    Animator animatorPlayer;
    public int contador = 5;
    public bool vivo = true;
    public bool vivo2 = true;
    void Start(){
        
        animatorPlayer = GetComponent<Animator>();
        contadorMuertes.text = contador.ToString();
    }
    void Update()
    {
        muerteUI();
        desactivarPuente = FindObjectOfType<RandomBridge>();



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

    public void muerteUI()
    {
        if (vivo == false || vivo2 == false)
            {
            panelMuerte.SetActive(true);
            contador--;
            contadorMuertes.text = contador.ToString();
            puenteRandom.GetComponent<RandomBridge>().restaurarPuente();
            while (contador == 0)
                {
                    desactivarPuente.puenteActivado.SetActive(false);
                    puenteRandom.GetComponent<RandomBridge>().ActivarPuenteRandom();
                    contador = 5;
                    contadorMuertes.text = contador.ToString();
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
