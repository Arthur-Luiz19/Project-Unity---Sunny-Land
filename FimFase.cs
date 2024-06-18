using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FimFase : MonoBehaviour
{
    public  string              Cena;
    
    private void OnTriggerEnter2D(Collider2D coll){
        if(coll.CompareTag("Player")){
            
            
            VoltarSelecao();
            
        }
        
    }

    public void VoltarSelecao(){
        
        SceneManager.LoadScene(Cena);
    }
}
