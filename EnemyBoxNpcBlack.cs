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
    public EnemyBoxPista enemyBoxPista;

    private void Awake()
    {
        enemigoBlackNpc.SetActive(false);
    }
    void Start()
    {
        texturaPuente = FindObjectOfType<RandomBridge>();
        enemyBoxPista= FindObjectOfType<EnemyBoxPista>();
    }
    public override void PlayerInteractua()
    {
            Invoke("CambioRojo", 0.2f);
            Invoke("ActivoEnemy", 0.3f);
            Invoke("RestaurarEnemigo",4.5f);

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
    public void RestaurarEnemigo()
    {
        enemigoBlackNpc.SetActive(false);
    }


}
