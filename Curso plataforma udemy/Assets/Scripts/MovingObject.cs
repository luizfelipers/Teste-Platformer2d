using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    //Os objeos são setados na própria Unity

    public GameObject objectToMove; //gameobject que será movido na cena
    public Transform startPoint;//Posição incial desejada. Será linkada com um gameObject vazio, apenas para pegar a sua posição
    public Transform endPoint; //Posição final desejada. Será linkada com um gameObject vazio, apenas para pegar a sua posição
    public float moveSpeed; //velocidade que o objeto será movido
    
    private Vector3 currentTarget;

	// Use this for initialization
	void Start () {
        currentTarget = endPoint.position; //O ponto de destino do objeto, no início do jogo, é o Ponto final. Quando encontra esse ponto, o novo ponto de destino vira o inicial. (Ver código abaixo)
	}

    // Update is called once per frame
    void Update()
    {
        //Chamada da função MoveTowards que manipula a posição de um objeto, até o seu destino, usando uma velocidade.


        //Vector3.MoveTowards(posição do objeto a ser movido, posição do objetivo, velocidade)

        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime); 
        
        //código de movimento automático do objeto. 1º recebe a posição do objeto a ser movido,
       //2º recebe a posição de destino, e por ultimo, calcula a velocidade do movimento, usando a velocidade moveSpeed e a variação de tempo.


        if (objectToMove.transform.position == endPoint.position)//Quando a plataforma/objeto chegar até o ponto Final, ele ganha um novo ponto de destino
        {
            currentTarget = startPoint.position;//O novo destino do objeto vira o ponto inicial
        }


        if (objectToMove.transform.position == startPoint.position)//Quando a plataforma/objeto chegar até o ponto Inicial, ganha um novo ponto de destino
        {

            currentTarget = endPoint.position;//Seta o destino do objeto pro ponto Final
        }


    }
   
}
