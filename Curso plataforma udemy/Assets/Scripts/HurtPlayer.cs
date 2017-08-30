using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {//Script referente a quando o jogaodr é machucado. É anexado aos objetos que machucam o jogaodr

    public LevelManager theLevelManager;//variavel do tipo level Manager, que será utilizada para acessar o Objeto levelManager e seus valores

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
            theLevelManager.Respawn();//chama a função de respawn quando o jogador for o objeto colisor com o objeto que machucará
        }
    }

}
