using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoxNpcBlack : EnemyBox

{
    Color cambioVerde = Color.green;
    public GameObject enemigoBlackNpc;
    public GameObject Cubo5;
    public Material renderMaterial;
    public RandomBridge texturaPuente;

    private void Awake()
    {
        enemigoBlackNpc.SetActive(false);
        
        //posicionLosa = transform.position;
    }
    void Start()
    {
        texturaPuente = FindObjectOfType<RandomBridge>();
    }
    public override void PlayerInteractua()
    {
            Invoke("CambioRojo", 0.2f);
            Invoke("ActivoEnemy", 0.3f);
            Invoke("DesactivarCollider", 3);
            Invoke("RestaurarEnemigo",3.5f);

            //Debug.Log("Test enemigo aparece ok");
            //Destroy(enemigoBlackNpc, 7)
        }

    void ActivoEnemy()
    {
        enemigoBlackNpc.SetActive(true);
        

    }
    void CambioRojo()
    {
        Cubo5.GetComponent<Renderer>().material = renderMaterial;
    }
    void DesactivarCollider()
    {
        
        Cubo5.GetComponent<Collider>().enabled = false;
    }
    public void RestaurarEnemigo()
    {
        enemigoBlackNpc.SetActive(false);
        GameObject.FindGameObjectWithTag("enemyDmg").GetComponent<BoxCollider>().enabled = true;
    }


}


