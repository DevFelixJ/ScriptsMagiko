using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementCharacter : MonoBehaviour
{
    [SerializeField]
    public float velocidad;
    [SerializeField]
    private float gravedad = 1f;
    [SerializeField]
    private float alturaSalto;
    private float impulsoGravedad;
    public DeadPlayer DeadPlayer;
    public EnemyBoxTemp enemyBoxTemp;
    public EnemyBoxNpc enemyBoxNpc;
    public bool isFalling;
    public bool isAlive;
    public bool Timer = false;
    public EnemyBoxPista enemyBoxPista;

    private CharacterController _characterController;
    Animator animatorPlayer;
    //  public AudioSource sonidoCaida;

    Quaternion rotacionParado;

    void Start()
    {
        DeadPlayer = FindObjectOfType<DeadPlayer>();
        isAlive = true;
        isFalling= false;
        //     sonidoCaida = GetComponent<AudioSource>();
        animatorPlayer = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        if (_characterController is null)
        {
            Debug.Log("Character controler es Nulo");
        }
        rotacionParado = gameObject.transform.rotation;
        // _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        enemyBoxTemp = FindObjectOfType<EnemyBoxTemp>();
        enemyBoxNpc = FindObjectOfType<EnemyBoxNpc>();
        enemyBoxPista= FindObjectOfType<EnemyBoxPista>();

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direccion = new Vector3(verticalInput, 0, -horizontalInput);
        Vector3 velocity = direccion * velocidad;

        if (_characterController.isGrounded) //si el charqacter controler esta tocando suelo
        {
            Quaternion rotacionInput;
            Vector3 dir = new Vector3(-horizontalInput, 0, -verticalInput).normalized;
            if (dir != Vector3.zero)
            {
                rotacionInput = Quaternion.LookRotation(dir, Vector3.up);
            }
            else
            {
                rotacionInput = rotacionParado;
            }
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotacionInput, Time.deltaTime * 10);

            if (Input.GetKeyDown(KeyCode.Space))//y pulsamos Espacio
            {
                Debug.Log("Debo de Saltar ");
                animatorPlayer.SetTrigger("Saltar");
                //animatorPlayer.SetBool("Caminar", false);
                //animatorPlayer.SetBool("Morir", false);
                //animatorPlayer.SetBool("Idle", false);
                Invoke("ImpulsoSalto", .5f);
            }

            if (direccion != Vector3.zero)
            {
                animatorPlayer.SetBool("Caminar", true);
                //    impulsoGravedad = alturaSalto;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Estoy intentando saltar dentro del IF de caminar");
                    animatorPlayer.SetTrigger("Saltar");
                    animatorPlayer.SetBool("Caminar", false);
                    Invoke("ImpulsoSalto", .5f);
                }
            }
            else
            {
                animatorPlayer.SetBool("Caminar", false);
            }
        }
        else
        {
            impulsoGravedad -= gravedad; //si el player esta en el suelo le quitamos la gravedad
        }
        velocity.y = impulsoGravedad;
        _characterController.Move(velocity * Time.deltaTime);
        //    _characterController.Move(velocity * Time.deltaTime);
    }

    private void ImpulsoSalto()
    {
        impulsoGravedad = alturaSalto;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)//Cuando el player pisa un cubo con el tag enemy o enemyTime, se activa el script.
    {
        if (hit.collider.tag.Equals("enemy") && isFalling == false || hit.collider.tag.Equals("enemyTime") && Timer == false)
        {//Cuando pisa la losa, activamos el script, que hace que el jugador vaya al centro de la losa sin poder moverse y caer. EnemyBoxTemp.
            hit.collider.GetComponent<EnemyBox>().PlayerInteractua();
            StartCoroutine(CaidaPlayer());
            if (hit.collider.tag.Equals("enemy") && isFalling == false)
            {
                isFalling = true;
            }
            else if(hit.collider.tag.Equals("enemyTime") && Timer == false)
            {
                Timer = true;
            }
        }

        
       else if (hit.collider.tag.Equals("enemyDmg") && isAlive == true)//Cuando entre en el collider del enemigo.EnemyBoxNpc.
        {
            hit.collider.GetComponent<EnemyBox>().PlayerInteractua();
            //StartCoroutine(EnemigoGolpe());
            isAlive = false;

        }
        else if (hit.collider.tag.Equals("greenBox"))//Cuando entre en la losa verde.EnemyBoxColor.
        {
            hit.collider.GetComponent<EnemyBox>().PlayerInteractua();

        }
        else if (hit.collider.tag.Equals("greenBoxArrow"))//Cuando entre en la losa de la pista.EnemyBoxPista.
        {
            hit.collider.GetComponent<EnemyBox>().PlayerInteractua();
        }
        //Cuando el player muere, se restaura a la posicion original el puente y el player.
        if (!hit.collider.tag.Equals("enemy") && isFalling == true || !hit.collider.tag.Equals("enemyTime") && Timer == true)//EnemyBoxTemp.
        {
            Resucitar();
        }
        if (!hit.collider.tag.Equals("enemyDmg") && isAlive == false)//EnemyBoxNpc.
        {
            Resucitar();
        }

    }//End OnControllerColliderHit

    IEnumerator CaidaPlayer()
    {
        yield return new WaitForSeconds(0.2f);
        velocidad = 0;
        animatorPlayer.SetBool("Caminar", false);
        this.transform.rotation = Quaternion.Euler(0, -80, 0);
    }
    IEnumerator EnemigoGolpe()
    {
        yield return new WaitForSeconds(0.2f);
        velocidad = 0;
    }
    public void Resucitar()
    {
        enemyBoxTemp.RestaurarCubo();
        impulsoGravedad = -2f;
        velocidad = 2.5f;
        isFalling = false;
        isAlive = true;
        Timer = false;
        enemyBoxTemp.contador++;
    }
}//Fin Script
