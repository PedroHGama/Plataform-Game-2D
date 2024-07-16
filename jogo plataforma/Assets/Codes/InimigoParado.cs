using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InimigoParado : MonoBehaviour
{
    // Start is called before the first frame update

    public int municao;
    public float velocidadeBala;
    public GameObject balaMunicao;
    public int vida;
    private Animator anim;
    public Transform posicaoBala;
    void Start()
    {
        anim = GetComponent<Animator>();

        InvokeRepeating("atirarProjetil", 2, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        


    
    }
    public void atirarProjetil()
    {
        GameObject temporario = Instantiate(balaMunicao, posicaoBala.position, Quaternion.identity);

        temporario.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeBala * -1, 0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("municao"))
        {

            vida -= 1;
            Destroy (gameObject);
        }
    }

}
