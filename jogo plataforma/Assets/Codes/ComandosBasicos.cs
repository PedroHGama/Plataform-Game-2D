using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ComandosBasicos : MonoBehaviour
{
    private GerenciaMoedas GerenciaMoedas;
    public float velocidadePersonagem;
    private Rigidbody2D rbPlayer;
    private float movimentoHorizontal;
    private Animator anim;
    private bool direcaoPersonagem;
    public float jump;
    public Transform posicaoSensor;
    private bool sensor;
    public int vida;
    public int moeda;
    public GameObject painelGameObject;
   
    // textos
    public TextMeshProUGUI textoMunicao;
    public TextMeshProUGUI textoVida;
    public TextMeshProUGUI textoMoeda;

    // variáveis do projetil
    public int municao; 
    public float velocidadeBala;
    public Transform posicaoBala;
    public GameObject balaMunicao;
    public GameObject direcaoMunicao;

    // Sons
    private audios audios;

    private BoxCollider2D boxCollider;
    public int moedas;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rbPlayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        vida = 2;
        municao = 2;

        audios = FindObjectOfType(typeof(audios)) as audios;

        GerenciaMoedas = FindObjectOfType(typeof(GerenciaMoedas)) as GerenciaMoedas;
    }

    void Update()
    {
        //Movimentação do personagem de direita para esquerda
        movimentoHorizontal = Input.GetAxisRaw("Horizontal");
        rbPlayer.velocity = new Vector2 (movimentoHorizontal * velocidadePersonagem, rbPlayer.velocity.y);
        anim.SetInteger("run", (int)movimentoHorizontal);

        if(movimentoHorizontal>0 && direcaoPersonagem == true)
        {
            direcao();
        }

        else if(movimentoHorizontal < 0 && direcaoPersonagem==false)
        {
            direcao();
        }

        pular();
        detectarChao();

        // Mostrar pontos de vida e municao
        textoVida.text = vida.ToString();
        textoMunicao.text = municao.ToString();
        textoMoeda.text = GerenciaMoedas.moedas.ToString();

        // Animacao de morte 

        if(vida <= 0)
        {
            // boxCollider.enabled = false;
            boxCollider.size = new Vector2 (1.5f, 1);
            //rbPlayer.gravityScale = 0;
            anim.SetTrigger("morte");
            velocidadePersonagem = 0;

        }
        // comando para atirar caso o botão do mouse for pressionado
        if(Input.GetMouseButtonDown(0) && municao > 0) 
        {

            atirarProjetil();
            anim.SetTrigger("atirarParado");
            municao--;

            // Som do tiro
            audios.SFXManager(audios.sfxTiro, 1);

        }

        // Limitando munição
        if (municao <= 0)
        {
            municao = 0;
        }


    }

    // Função para o personagem virar para esquerda e direita
    public void direcao() 
    {
        direcaoPersonagem = !direcaoPersonagem; 

        float x = transform.localScale.x * -1;

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

        velocidadeBala *= -1;

        direcaoMunicao.GetComponent<SpriteRenderer>().flipX = direcaoPersonagem;

    }

    // Função para o personagem pular
    public void pular()
    {
        if (Input.GetButtonDown("Jump") && sensor == true)
        {
            rbPlayer.AddForce(new Vector2(0, jump));
        }

        anim.SetBool("sensor", sensor);
    }

    // Função para o personagem virar para detectar o chão e não dar pulo duplo
    public void detectarChao()
    {

        sensor = Physics2D.OverlapCircle(posicaoSensor.position, 0.25f);
    }

    // Função para o personagem destruir o item coletável assim que for encostado
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("coletavel"))
        {
            vida += 1;
            Destroy(collision.gameObject);

            audios.SFXManager(audios.sfxVida, 1);

        }

        if (collision.gameObject.CompareTag("coletavelMunicao"))
        {

            municao ++;
            Destroy(collision.gameObject);

            audios.SFXManager(audios.sfxMunicao, 1);
        }

        if (collision.gameObject.CompareTag("morte"))
        {

            vida -= 1;
        }

        if (collision.gameObject.CompareTag("moeda"))
        {
            GerenciaMoedas.moedas++;

            Destroy(collision.gameObject);

            audios.SFXManager(audios.sfxMoeda, 1);

        }

        if(collision.gameObject.CompareTag("ProximaFase"))
        {
            SceneManager.LoadScene("FASE2");
        }

        if (collision.gameObject.CompareTag("Final"))
        {
            SceneManager.LoadScene("FIM");
        }



    }

    public void reiniciarJogo()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void sairJogo()
    {
        Application.Quit();
    }

public void PainelGameover()
    {
        painelGameObject.SetActive(true);

        audios.SFXManager(audios.sfxGameOver, 1);
    }


    public void atirarProjetil()
    {
        GameObject temporario = Instantiate(balaMunicao);

        temporario.transform.position = posicaoBala.transform.position;

        temporario.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeBala, 0);
    }
}
