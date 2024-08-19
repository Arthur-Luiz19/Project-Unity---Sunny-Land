using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    
[Header("Referências e Alvos")]
public  Transform       chave;
public  Transform[]     Position;

[Header("Configurações de Movimento")]
private float           speed = 1f;

[Header("Componentes")]
private int             IdTarget;
private SpriteRenderer  keySprite;

    

    // Start is called before the first frame update
    void Start()
    {
        keySprite = chave.gameObject.GetComponent<SpriteRenderer>();
        chave.position = Position[0].position;
        IdTarget = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(keySprite != null){
            chave.position = Vector3.MoveTowards(chave.position, Position[IdTarget].position, speed * Time.deltaTime);
            

            if(chave.position == Position[IdTarget].position){
                IdTarget +=1;

                if(IdTarget == Position.Length){
                    IdTarget = 0;
                }
            
            }
        }
    }
}
