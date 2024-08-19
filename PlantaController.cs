using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaController : MonoBehaviour
{
[Header("Componentes")]
private SpriteRenderer  sprite;
private Animator        PA;

[Header("Referências")]
public  Transform       planta;
public  Transform       player;

[Header("Configurações de Ataque")]
private float           attackRange = 3f;
private bool            isPlayerRange = false;

    
    // Start is called before the first frame update
    void Start()
    {
        sprite = planta.gameObject.GetComponent<SpriteRenderer>();
        PA = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distancePlayer = Vector3.Distance(player.position, planta.position);

        if(distancePlayer <= attackRange){
            isPlayerRange = true;
        }
        else{
            isPlayerRange = false;
        }

        if(isPlayerRange){
            StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine(){

        PA.SetTrigger("Attack");
        yield return new WaitForSeconds(1.0f);

    }
}
