using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBoxTemp : EnemyBox
{
    public RandomBridge texturaPuente;
    Color cambioAmarillo = Color.yellow;
    public Material renderMaterial;
    public Rigidbody rigidCaida;
    public GameObject cuboAmarillo;
    float tiempoCaida;
    public DeadPlayer DeadPlayer;
    public List<Vector3> cuboGuardado;
    public int contador = 0;
    public EnemyBoxPista enemyBoxPista;

    private void Start()
    {
        DeadPlayer = FindObjectOfType<DeadPlayer>();
        texturaPuente = FindObjectOfType<RandomBridge>();
        enemyBoxPista= FindObjectOfType<EnemyBoxPista>();
        rigidCaida = GetComponent<Rigidbody>();


    }
    public override void PlayerInteractua()
    {
        if(cuboAmarillo.tag.Equals("enemyTime"))
        {
            Invoke("CambioAmarillo", 0.5f);
            Invoke("CaidaCubo", 2);
        }
        else
        {
            Invoke("CambioAmarillo", 0.2f);
            Invoke("CaidaCubo", 0.3f);
        }

    }
       
    void CambioAmarillo()
    {//Cambio de textura al pisar.
        cuboAmarillo.GetComponent<Renderer>().material = renderMaterial;
    }
    void CaidaCubo()
    {//Aqui le quitamos la malla y el collider.
        cuboAmarillo.GetComponent<MeshRenderer>().enabled = false;
        cuboAmarillo.GetComponent<Collider>().enabled = false;
    }

    public void RestaurarCubo()
    {//Cada vez que el jugador se cae, se restaura todos los cubos a su posicion inicial con la textura del puente.
        if (contador >= 1 )
        {
            Transform[] losasPuente = GameObject.FindGameObjectWithTag("puente").GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < losasPuente.Length; i++)
            {
                if (losasPuente[i].gameObject.GetComponent<Renderer>() != null && losasPuente[i].gameObject.GetComponent<Rigidbody>() != null)
                {
                    losasPuente[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
                    losasPuente[i].gameObject.GetComponent<Collider>().enabled = true;
                    losasPuente[i].gameObject.GetComponent<Renderer>().material = texturaPuente.sueloPuente;
                    losasPuente[i].gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    losasPuente[i].gameObject.transform.position = cuboGuardado[i];
                }
                
            }
            

        }
    }
    public void GuardarCubos()
    {
        //Cuando el contador que aparece en pantalla sea = 5. Se guarda en una lista todas las posiciones de los cubos del puente.
        if (DeadPlayer.contador == 5)
        {
            
            cuboGuardado = new List<Vector3>();
            Transform[] posicionesLosas = GameObject.FindGameObjectWithTag("puente").GetComponentsInChildren<Transform>(true);
            foreach (Transform posicionLosa in posicionesLosas)
            {
                cuboGuardado.Add(posicionLosa.gameObject.transform.position);

            }
            contador = 1;

        }
    }
   

    void Update()
    {
        GuardarCubos();
    }
}
