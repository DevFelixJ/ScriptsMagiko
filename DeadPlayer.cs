using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayer : MonoBehaviour
{
    Animator animatorPlayer;
    bool vivo = true;
    bool vivo2 = true;
    void Start(){
        animatorPlayer = GetComponent<Animator>();
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
  
}
