using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnRespawn : MonoBehaviour {

    public Vector3 startPosition; //variável referente a posição inicial do objeto no inicio do jogo
    public Quaternion startRotation;//variável referente a rotação inicial do objeto no inicio do jogo
    public Vector3 startLocalScale;//variável referente ao tamanho inicial do objeto no inicio do jogo

    private Rigidbody2D myRigidBody;//variável que receberá o RigidBody do objeto que o script estiver anexado


	// Use this for initialization
	void Start () {

        //INICIALIZAÇÃO

        //salva os valores iniciais de posição, rotação e tamanho nas variáveis, e depois, 
        //caso o gameobject houver um RigidBody2d, esse rigidbody é anexado na variável myRigidBody




        startPosition = transform.position;//no inicio do jogo, atribui o valor da posição do GameObject( que o script está anexado), á variável startPosition
        startRotation = transform.rotation;//no inicio do jogo, atribui o valor da rotação do GameObject( que o script está anexado), á variável startRotation
        startLocalScale = transform.localScale;//no inicio do jogo, atribui o valor da escala(Tamanhos) do GameObject( que o script está anexado), á variável startlocalscale


        if (GetComponent<Rigidbody2D>() != null)//só executa esse trecho, se o gameObject possui um componente Rigidbody/ se for diferente de nulo
        {
            myRigidBody = GetComponent<Rigidbody2D>();// linka o RigidBody2d á variável myRigidBody, para que possa ser feito alterações na fisica e velocidade
        }
       

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetObject()
        //resete o objeto com esse script

    //Passa os valores iniciais das variáveis, para o seu estado atual. De modo que os valores atuais, se tornem os valores iniciais, salvos no inicio do jogo


    {
        transform.position = startPosition;//a posição do GameObject volta a ser a posição inicial  /  posição passa a ser a posição salva na variável StartPosition, setada no método Start
        transform.rotation = startRotation;//a rotação do GameObject volta a ser a rotação inicial  /  posição passa a ser a rotação salva na variável StartRotation, setada no método Start
        transform.localScale = startLocalScale;//o tamanho do GameObject volta a ser o tamanho inicial  /  posição passa a ser a escala salva na variável StartLocalScale, setada no método Start

        if (myRigidBody != null)//caso tenha rigidBody
        {
            myRigidBody.velocity = Vector3.zero;//o vetor de velocidade do RigidBody do gameobject é zerado
        }


    }
}
