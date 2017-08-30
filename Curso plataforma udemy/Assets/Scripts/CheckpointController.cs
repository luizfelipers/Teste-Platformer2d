using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

    public Sprite flagClosed; //sprite que receberá a figura da bandeira fechada (Quando o checkpoint não foi acionado)
    public Sprite flagOpened; //sprite que receberá a figura da bandeira fechada (Quando o checkpoint foi acionado)

    private SpriteRenderer mySpriteRenderer; //variavel que será usada para acessar os atributos do sprite renderer do checkpoint

    public bool checkPointActive; //booleano que verifica se o checkpoint está ativado

	// Use this for initialization
	void Start () {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")//caso o elemento colisor no checkpoint, seja um objeto com tag Player
        {
            mySpriteRenderer.sprite = flagOpened; //troca o sprite pelo sprite da bandeira aberta
            checkPointActive = true; //seta a variavel Checkpoint active como verdadeira no objeto anexado
        }
    }
}
