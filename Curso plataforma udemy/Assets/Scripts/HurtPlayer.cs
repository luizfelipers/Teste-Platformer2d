using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {//Script referente a quando o jogaodr é machucado. É anexado aos objetos que machucam o jogaodr

    public LevelManager theLevelManager;//variavel do tipo level Manager, que será utilizada para acessar o Objeto levelManager e seus valores

    public int damageToGive;//valor inteiro referente ao dano que cada objeto causará no player

	// Use this for initialization
	void Start () {

        theLevelManager = FindObjectOfType<LevelManager>();//inicializa o objeto LevelManager na variável theLevelManager


	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)//OntriggerEnter para checar a morte
    {
        if(collision.tag == "Player")//se a tag do objeto colisor for "Player"
        {

            //theLevelManager.Respawn();

            theLevelManager.HurtPlayer(damageToGive);//chama a função de adição de dano ao Player. 
                                                     //Recebe como parâmetro o valor do dano recebido, setado na unity
        }
    }

}
