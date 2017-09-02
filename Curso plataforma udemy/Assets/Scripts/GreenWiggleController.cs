using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWiggleController : MonoBehaviour {

    public Transform leftPoint; //variável do tipo Transform, pois só precisamos saber a posição do gameObject, para servir de limite para o patroller 
    public Transform rightPoint; //variável do tipo Transform, pois só precisamos saber a posição do gameObject, para servir de limite para o patroller 

    public float moveSpeed; //velocidade que o inimigo andará no seu intervalo de espaço

    private Rigidbody2D myRigidBody;//variável que controlará o componente RigidBody2D do gameObject anexado


    public bool movingRight;

    // Use this for initialization
    void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (movingRight == true && transform.position.x > rightPoint.position.x)//caso o greenwiggle esteja andando pra direita e tenha passado pelo rightPoint no eixoX
        {

            movingRight = false;
        }
        if (movingRight == false && transform.position.x < leftPoint.position.x)
        {

            movingRight = true;

        }
        if(movingRight == true)
        {
            myRigidBody.velocity = new Vector3(moveSpeed, myRigidBody.velocity.y, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector3(-moveSpeed, myRigidBody.velocity.y, 0f);
        }
    }
}
