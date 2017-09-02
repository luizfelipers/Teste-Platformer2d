using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour {

    public float moveSpeed; //variavel referente a velocidade que a aranha se locomoverá
    public bool canMove;//variavel que autoriza o movimento

    private Rigidbody2D myRigidBody;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();//associa o componente RigidBody2d do gameObject anexado, á variavel myRigidBody
	}
	
	// Update is called once per frame
	void Update () {
		
        if(canMove== true)//caso possa se mexer
        {
            myRigidBody.velocity = new Vector3(-moveSpeed, myRigidBody.velocity.y, 0f);//anda apenas para a esquerda

        }
        

	}
    private void OnBecameVisible()//funão da classe MonoBehavior, que checa se o objeto está visivel na camera, e qd estiver, executa os comandos nas chaves
    {
        canMove = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)//quando o gameObjeto encostar no abismo, ele morre
    {
        if(collision.tag == "KillPlane")//caso a tag do colisor seja KillPlane
        {
            Destroy(gameObject);//gameObject é destru,ido

        }
    }
}
