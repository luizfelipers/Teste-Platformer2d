using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

    private LevelManager theLevelManager; //declaração da variável theLevelManager do tipo Level Manager, para poder acessar seus atributos
                                            //e métodos

    public int coinValue; //valor inteiro referente ao valor que cada moeda terá. diferentes tipos de moeda podem ter dif valores


	// Use this for initialization
	void Start () {
        theLevelManager = FindObjectOfType<LevelManager>();//associa o objeto do tipo Level Manager á variável theLevelManager
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)//quando algo entra no colisor da moeda
    {
       if(collision.tag == "Player") //checa se o objeto colisor tem a tag Player
        {
            theLevelManager.AddCoins(coinValue);//aciona o método AddCoins do script LevelManager. Com isso a soma do valor da moeda com a pontuação atual é realizada
            Destroy(gameObject);//Destroi o gameObject da moeda na hierarquia do jogo

        }
    }
}
