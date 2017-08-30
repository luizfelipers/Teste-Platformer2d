using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public float waitToRespawn; //valor referente ao atraso para o jogador voltar em cena quando mover  
    public PlayerController thePlayer; // variável do tipo PlayerController, que será usada para achar o gameObject.
    public float waitToDie;//valor referente ao atraso para o jogador sair de cena quando morre

    public GameObject deathExplosion;



	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerController>(); // Encontra o componente PlayerController (script), e com isso, retorna o único objeto que está ligado nele.
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Respawn() //Ativada quando o player morre
    {
        StartCoroutine("RespawnCo");//começa a coroutine
    }

    public IEnumerator RespawnCo()
    {
        Instantiate(deathExplosion, thePlayer.transform.position, thePlayer.transform.rotation); //cria uma instancia do objeto com ParticleSystems (sangue) na posição em que o player morreu.

        yield return new WaitForSeconds(waitToDie);
        thePlayer.gameObject.SetActive(false); // desliga o gameObject quando a função for chamada





        thePlayer.transform.position = thePlayer.respawnPosition; //A nova posição do jogador será a posição do último respawn position
        yield return new WaitForSeconds(waitToRespawn);


        thePlayer.gameObject.SetActive(true); //liga o jogador

    }


}
