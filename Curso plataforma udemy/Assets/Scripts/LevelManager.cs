using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public float waitToRespawn; //valor referente ao atraso para o jogador voltar em cena quando mover  
    public PlayerController thePlayer; // variável do tipo PlayerController, que será usada para achar o gameObject.
    public float waitToDie;//valor referente ao atraso para o jogador sair de cena quando morre

    public GameObject deathExplosion; //gameObject referente ao Particle Systems de sangue, quando o Player morre

    public int coinCount;//variavel responsavel por guardar a pontuação do jogador

    public Text coinText;//texto de UI que é responsável por mostrar na tela a quantidade de coins que o player tem

    //Imagens referentes aos corações
    public Image heart1;
    public Image heart2;
    public Image heart3;

    //Sprites referentes aos estados dos corações
    public Sprite heartFull;//cheio
    public Sprite heartHalf;//mid
    public Sprite heartEmpty;//vazio

    public int maxHealth;//variável responsável por setar o valor da vida máxima do player
    public int healthCount; //variável responsável por monitorar o nível de vida do PLayer

    public bool respawning;//booleano que indica se o Player está em fase de respawn no momento ou não

    public ResetOnRespawn[] objectsToReset; //array de objectos que terão seus atributos resetados quando o jogo reiniciar

    public bool invincible;


    public Text livesText;
    public int currentLives;
    public int startingLives;


    // Use this for initialization
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>(); // Encontra o componente PlayerController (script), e com isso, retorna o único objeto que está ligado nele.

        coinText.text = "Coins: 0";//texto inicial exibido pelo UI de pontuação

        healthCount = maxHealth;//vida inicial do jogador

        objectsToReset = FindObjectsOfType<ResetOnRespawn>();

        currentLives = startingLives;//no começo do jogo, o valor atual de vidas será o valor inicial de vidas, setado na Unity
        livesText.text = "Vidas x " + currentLives;//texto inicial exibido no contador de vidas
    }

    // Update is called once per frame
    void Update()
    {
        if (healthCount <= 0 && respawning == false)// só respawna se a vida chegar em 0 e o player não estiver respawnando no momento
        {
            Respawn();//está dentro do método update, pois a checagem pela quantidade de vida do Player e se esta respawnando, deve ser feita a cada frame
            respawning = true;//seta o bool respawning para verdadeiro
        }
    }

    public void Respawn() //Ativada quando o player morre
    {

        currentLives -= 1;
        livesText.text = "Vidas x " + currentLives;//texto do contador de vidas é atualizado no Respawn

        if(currentLives > 0)
        {
            StartCoroutine("RespawnCo");//começa a coroutine
        }
        else
        {
            Instantiate(deathExplosion, thePlayer.transform.position, thePlayer.transform.rotation); //COLOCADO POR MIM - PRO PLAYER EXPLODIR TB QUANDO MORRER PELA ULTIMA VEZ
            thePlayer.gameObject.SetActive(false); // desliga o gameObject quando a função for chamada
            respawning = false;
        }

        
    }

    public IEnumerator RespawnCo()
    {
        Instantiate(deathExplosion, thePlayer.transform.position, thePlayer.transform.rotation); //cria uma instancia do objeto com ParticleSystems (sangue) na posição em que o player morreu.

        yield return new WaitForSeconds(waitToDie);//tempo de espera pra ele morrer
        thePlayer.gameObject.SetActive(false); // desliga o gameObject quando a função for chamada




        healthCount = maxHealth;//no começo do jogo, o Player começa com vida cheia
        //seta o bool respawning para falso
        UpdateHeartMeter();//atualiza o valor da vida do usuário, com o Switch, no case 6, reiniciando o valor de vida para Total
        coinCount = 0;
        coinText.text = "Coins:" + coinCount;

        thePlayer.transform.position = thePlayer.respawnPosition; //A nova posição do jogador será a posição do último respawn position
        yield return new WaitForSeconds(waitToRespawn);//demora para respawnar depois que todas as operações acima forem realizadas
        respawning = false;

        thePlayer.gameObject.SetActive(true); //liga o jogador

        for (int i = 0; i < objectsToReset.Length; i++)
        {

            objectsToReset[i].gameObject.SetActive(true);
            objectsToReset[i].ResetObject();
        }


    }
    public void AddCoins(int coinsToAdd)//função que adiciona uma certa quantidade de pontos, passada como parametro pela variavel indicada
    {
        coinCount += coinsToAdd;//adiciona a pontuação referente ao gameObject, na pontuação total do Player
        coinText.text = "Coins: " + coinCount;//Atualiza o elemento de UI text, de modo que apareça a pontuação do usuário
    }
    public void HurtPlayer(int damageToTake)// função a ser chamada sempre que o Player morre
    {
        if (invincible == false)
        {
            //healthCount = healthCount - damageToTake;
            healthCount -= damageToTake;//a vida atual do player é subtraída peloo valor que o gameobject colisor atinge de dano
            UpdateHeartMeter();//atualiza UI da vida do usuário de acordo com o dano recebido
            if(healthCount > 0)
            {
                thePlayer.KnockBack();
            }
            
        }




    }
    public void UpdateHeartMeter()//função que controla os sprites de vida do personagem
    {
        switch (healthCount)//recebe a quantidade de vida atual do Player
        {


            //Contagem na ordem decrescente = começa em Vida cheia e vai esvaziando
            case 6:
                heart1.sprite = heartFull;//cheia 6/6
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                return;

            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;//5/6
                return;
            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;// 4/6
                heart3.sprite = heartEmpty;
                return;

            case 3:
                heart1.sprite = heartFull;// 3/6
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                return;

            case 2:
                heart1.sprite = heartFull;// 2/6
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            case 1:
                heart1.sprite = heartHalf;//  1/6
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            case 0:
                heart1.sprite = heartEmpty;// 0/6, vazia, Player morto
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;//caso não seja nenhuma das outras opções, o player está morto.
                heart3.sprite = heartEmpty;
                return;
        }

    }

}
