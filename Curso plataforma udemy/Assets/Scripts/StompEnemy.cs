using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour {

    public GameObject deathExplosion;//variável referente á animação de sangue e explosão

    private Rigidbody2D playerRigidBody;//variável que controlará o Bounce, NO RIGIDBODY DO PLAYER, e não no do Stomp

    public float bounceForce;//força que será adicionada ao eixo y quando o Player encostar no Inimigo

	// Use this for initialization
	void Start () {
        playerRigidBody = transform.parent.GetComponent<Rigidbody2D>();//associação da variável playerRigidBody ao componente RigidBody2D do PLAYER(objeto pai) 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")//Caso o colisor do Stomp (colisor abaixo do player), entre em contato com um inimigo
        {
            Destroy(collision.gameObject);//o inimigo é destruido

            Instantiate(deathExplosion, collision.transform.position, collision.transform.rotation);//instancia uma animação de sanguinho no lugar da morte do inimigo

            playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, bounceForce, 0f);//adiciona BounceForce no eixo Y, para fazer o player ricochetear quando matar o inimigo
        }
    }
}
