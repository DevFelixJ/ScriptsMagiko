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
    {
        if (hit.collider.tag.Equals("Enemy"))
        {
            hit.collider.GetComponent<EnemyBox>().PlayerInteractua();
            //velocidad = 0;
            isAlive = false;
            animatorPlayer.SetBool("Caminar", false);
            

        }
        if (!hit.collider.tag.Equals("Enemy") && isAlive == false)
        {
            enemyBoxTemp.RestaurarCubo();
            impulsoGravedad = -4f;
            velocidad = 2.5f;
            isAlive = true;
        }

     }//End OnControllerColliderHit

}//Fin Script
