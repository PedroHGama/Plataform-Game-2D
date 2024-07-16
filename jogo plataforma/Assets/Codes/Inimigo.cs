using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocidadeInimigo;
    public float velocidadeAtual;
    public bool direcaoPersonagem;
    public float limiteL, limiteR;
    public int vida;
    public TextMeshProUGUI textoVida;
    private Animator anim;
    public float velocidadePersonagem;
    void Start()
    {
        velocidadeInimigo = velocidadeAtual;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <=limiteL && direcaoPersonagem == true)
        {
            velocidadeInimigo = velocidadeAtual;
            direcao();
        }

        else if(transform.position.x >=limiteR && direcaoPersonagem == false)
        {
           
            velocidadeInimigo = -velocidadeAtual;
            direcao();
        }
        transform.Translate(new Vector2(velocidadeInimigo, 0)*Time.deltaTime);

        if (vida <= 0)
        {
            anim.SetTrigger("morte");
            velocidadePersonagem = 0;

        }
        textoVida.text = vida.ToString();
    }

    public void direcao()
    {
        direcaoPersonagem = !direcaoPersonagem;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("municao"))
        {

            vida -= 1;
            Destroy(gameObject);
        }
    }
}
