using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevador : MonoBehaviour
{
   
   public   Transform   pontoA, pontoB, Plataforma;
   public   float       Velocidade = 5f;
   private  Vector3     Destino;
   public   GameObject  Player;

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
