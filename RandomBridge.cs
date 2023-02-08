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
    public bool isAlive;

    private CharacterController _characterController;
    Animator animatorPlayer;
    //  public AudioSource sonidoCaida;

    Quaternion rotacionParado;

    void Start()
    {
        DeadPlayer = FindObjectOfType<DeadPlayer>();
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {//Cuando el player pisa un cubo con el tag ENEMY, se activa el script.
        if (hit.collider.tag.Equals("Enemy"))
        {//Cuando pisa la losa, activamos el script, que hace que el jugador vaya al centro de la losa sin poder moverse y caer. EnemyBoxTemp.
            hit.collider.GetComponent<EnemyBox>().PlayerInteractua();
            velocidad = 0;
            animatorPlayer.SetBool("Caminar", false);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, hit.collider.GetComponent<Renderer>().bounds.center.z + 0.2f);
            this.transform.rotation = Quaternion.Euler(0,-80,0);
            isAlive = false;

        }
        if (!hit.collider.tag.Equals("Enemy") && isAlive == false)
        {//Cuando se le va el collider del cubo trampa, se activa el script, RestaurarCubo() en EnemyBoxTemp y se le devuelve las animaciones y la velocidad, sumandole un
         //contador para que se pueda restaurar una vez que vuelve el player.
            enemyBoxTemp.RestaurarCubo();
            impulsoGravedad = -2f;
            velocidad = 2.5f;
            isAlive = true;
            enemyBoxTemp.contador++;
        }

     }//End OnControllerColliderHit

}//Fin Script
