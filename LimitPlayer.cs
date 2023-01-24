using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitPlayer : MonoBehaviour
{
    void OnTriggerEnter(Collider player){
        DeadPlayer scriptDeath = player.GetComponent<DeadPlayer>();
        if(scriptDeath !=null){
            Debug.Log("Entro al collider fuera de limites y murio");
            scriptDeath.Morir();//daba error al intentar coger el script
            Time.timeScale = 0;
        }
    }
}
