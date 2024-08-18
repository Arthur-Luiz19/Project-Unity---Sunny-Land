using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevador : MonoBehaviour
{
   
[Header("Componentes e Referências")]
public GameObject Player;
public Transform  pontoA;
public Transform  pontoB;
public Transform  Plataforma;

[Header("Configurações de Movimento")]
public float      Velocidade = 5f;
private Vector3   Destino;


    // Start is called before the first frame update
    void Start()
    {
        Plataforma.position = pontoA.position;
        Destino = pontoB.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Plataforma != null){
            Plataforma.position = Vector3.MoveTowards(Plataforma.position, Destino, Velocidade * Time.deltaTime);
        }
        if(Plataforma.position == pontoB.position){
            Destino = pontoA.position;
        }
        else if(Plataforma.position == pontoA.position){
            Destino = pontoB.position;
        }
    }
}
