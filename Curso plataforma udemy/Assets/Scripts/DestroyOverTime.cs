using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {//script anexado nos componentes que são destinados a morrer, 
                                              //pouco tempo depois de sua criação, como a animação de sangue na morte do Player

    
    public float lifeTime;      //float que recebe quanto tempo um objeto terá de vida enquanto o jogo estiver rodando

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        lifeTime = lifeTime - Time.deltaTime; //o valor de vida dele será calculado
                                              //pela diferenca entre o valor de vida desejado e a variação de tempo durante um frame e outro
        Debug.Log(Time.deltaTime); //
        if(lifeTime <= 0)
        {
            Destroy(gameObject);    
        }
	}
}
