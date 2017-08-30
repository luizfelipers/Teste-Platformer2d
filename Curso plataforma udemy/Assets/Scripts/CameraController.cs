using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector2 velocity;//Vector do eixo X e Y, referente a velocidade em cada eixo


    public float smoothTimeY;  //Atraso para acompanhar o player no eixo Y

    public float smoothTimeX; //Atraso para acompanhar o player no eixo X


    public GameObject target; // Alvo que será o Foco da camera principal

    // Variáveis que definem os limites da câmera
    public bool maxMin = false;
    public float yMaxValue = 0;
    public float yMinValue = 0;
    public float xMaxValue = 0;
    public float xMinValue = 0;

    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
	}

    // Update is called once per frame

    void FixedUpdate()
    {


        float posX = Mathf.SmoothDamp(transform.position.x,
target.transform.position.x, ref velocity.x, smoothTimeX);

        float posY = Mathf.SmoothDamp(transform.position.y, target.transform.position.y, ref velocity.y, smoothTimeY);


        transform.position = new Vector3(posX, posY, transform.position.z);
        if (target)//se houver um foco
        {


            

            if (maxMin)//Se houver um limite definido, MathF Clamp executa os limites da tela
            {

                transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMinValue, xMaxValue),
                    Mathf.Clamp(transform.position.y, yMinValue, yMaxValue), -10);
            }


        }
    }
}
