using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {//Script utilizado para controlar o jogador


    public GameObject stomp;//gameObject referente ao colisor que checa quando o Player encostou na parte de cima do inimigo, para matá-lo

    public float moveSpeed; // Velocidade de movimento do jogador
    private Rigidbody2D myRigidBody; //variavel do tipo Rigidbody2d. Será usada para acessar os atributos desse componente no objeto 
    private SpriteRenderer myRenderer;//variável do tipo Sprite Renderer. Será usada para acessar os atributos desse componente, asssim como flipar o jogador

    public float jumpSpeed;//variável que ditará a força do pulo do jogador

    public Transform groundCheck;//posição de GroundCheck
    public float groundCheckRadius;//
    public LayerMask whatIsGround;//
    public bool isGrounded;

    private Animator myAnim;// Variavel do tipo animator, que será usada para acessar seus atributos e mudar os bools para mudar animações
    public Vector3 respawnPosition; //vetor de 3 posições, referente a posição de ressurgimento do jogador

    public LevelManager levelManager; //variável que reeberá o Objeto level manager, usado para controlar o funcionamento da fase

    public float knockbackForce;//força adicionada ao player para quando ele for atingido por um inimigo 
    public float knockbackLenght;//duração do 'ricochete' quando o player for atingido

    private float knockbackCounter;

	// Use this for initialization  
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>(); //Linka o RigidBody2D do gameObject escolhido á variável myRigidBody . 
        myAnim = GetComponent<Animator>();//Linka o Animator do gameObject escolhido á variável myAnim .
        myRenderer = GetComponent<SpriteRenderer>();//Associa o Sprite Renderer do objeto á variavel myRenderer, no comeco do jogo
        respawnPosition = transform.position;//Posição inicial do RespawnPoint será a posição incial do Player. (Caso não atinja os checkpoints).
        levelManager = FindObjectOfType<LevelManager>();//Linka o objeto Level Manager á variável levelManager, para poder manipular seus atributos
        respawnPosition = transform.position; //posição de Respawn inicial (caso não tenha atingido checkpoints) é a posição inicial do jogador no jogo. Ou seja, volta pra posição default.
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics2D.OverlapCircle //OverlapCircle é um circulo imaginário desenhado no pé do personagem
            (groundCheck.position, //recebe a posição do object que está pregado no pé do player, para saber que o check deve ocorrer nessa área
             groundCheckRadius,//raio definido para o gameobject que servirá de sensor de toque no chao
               whatIsGround);//variável que define o que pode ser considerado como Ground ou terreno andável.



        if (knockbackCounter <= 0)
        {

            //TRECHOS DE CÓDIGO PARA REPRESENTAR MOVIMENTO DO PLAYER


            //DIREITA
            if (Input.GetAxisRaw("Horizontal") > 0f) // Caso o Input na horizontal (eixo X) seja maior que 0, o personagem anda pra direita
            {
                myRigidBody.velocity = new Vector3(moveSpeed, myRigidBody.velocity.y, 0f);
                myRenderer.flipX = false;//não flipa, pois o sprite  do player é naturalmente virado para a direita
            }

            //ESQUERDA
            else if (Input.GetAxisRaw("Horizontal") < 0f) // Caso o Input na horizontal (eixo X) seja menor que 0, o personagem anda pra direita
            {
                myRigidBody.velocity = new Vector3(-moveSpeed, myRigidBody.velocity.y, 0f);
                myRenderer.flipX = true; //flipa o personagem, pois o sprite está virado para a esquerda.
            }
            //PARADO NO EIXO X
            else
            {
                myRigidBody.velocity = new Vector3(0, myRigidBody.velocity.y, 0f); //fica parado, não mexe o eixo X

            }

            //PULO DO PLAYER
            if (Input.GetButtonDown("Jump") && isGrounded == true)// Caso o botão associado ao pulo no Input for pressionado e o jogador estiver no solo.
            {
                myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, jumpSpeed, 0f); //pega a velocidade no X, e junta com a força do pulo. Num Vetor de 2 variáveis.

            }
        }

        if (knockbackCounter > 0)
        {
            knockbackCounter -= Time.deltaTime;


            if (myRenderer.flipX == false)
            {
                myRigidBody.velocity = new Vector3(-knockbackForce, knockbackForce, 0f);
            }
            else
            {
                myRigidBody.velocity = new Vector3(knockbackForce, knockbackForce, 0f);
            }
        }

        myAnim.SetFloat("Speed", Mathf.Abs(myRigidBody.velocity.x));//Faz com que a variável Speed retorne apenas valores verdadeiros.
        myAnim.SetBool("Grounded", isGrounded);

        if (myRigidBody.velocity.y < 0) // caso a velocidade no eixo Y do jogador for menor que 0, ele estará caindo
        {
            stomp.SetActive(true);//a caixinha Stomp será ativada, de modo que o inimigo só será morto na queda do Player
        }
        else
        {
            stomp.SetActive(false); //velocidade acima ou igual a 0, então a caixa Stomp permanece desligada
        }

    }
        //CÓDIGO PARA RICOCHETE DO PLAYER, QUANDO LEVAR DANO
        public void KnockBack()
        {
            knockbackCounter = knockbackLenght;

        }


    
    private void OnTriggerEnter2D(Collider2D collision)//Quando o player entra em algum trigger
    {
        if(collision.tag == "KillPlane") //caso encoste no colisor referente ao Chão/abismo
        {

            levelManager.Respawn(); // Invoca o método respawn do Level Manager
            // gameObject.SetActive(false);
            //transform.position = respawnPosition; // A posição inicial do player passa a ser a posição do último checkpoint
        }
        if(collision.tag == "Checkpoint")
        {
            respawnPosition = collision.transform.position; // a posição do choque do player e o checkpoint fica salva na variável respawnPoint;
            levelManager.waitToRespawn = 2;//tempo que demora para o jogador respawnar quando for voltar para um checkpoint

        }

       

    }

    //TRECHO DE CÓDIGO REFERENTE A PLATAFORMA QUE SE MOVE

    private void OnCollisionEnter2D(Collision2D collision) //código para quando o player estiver em cima da plataforma que anda
    {
        if(collision.gameObject.tag == "MovingPlatform")  //casp a tag do colisor que entrará em contato com o player, for MovingPlaform
        {
            transform.parent = collision.transform;   //Posição do colisor (jogador) vira filha da posição do objeto que se mexe
                                                   //para que a posição do player acompanhe a da plataforma, de forma que ele se mova junto.
        }
    }
    private void OnCollisionExit2D(Collision2D collision)//quando o player sair da plataforma (pulando ou outro jeito)
    {
        if(collision.gameObject.tag == "MovingPlatform")//caso a tag do colisor que perderá contato com o player, for MovingPlatform
        {
            transform.parent = null; //a posição do player deixa de ser filha da posição do objeto flutuante

        }
    }


}
