using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciaMoedas : MonoBehaviour
{
    public int moedas;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
