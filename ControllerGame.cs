using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerGame : MonoBehaviour
{

[Header("Pontuação e Vida")]
private int         score;
private int         key;
private int         vidas = 3;
public  Text        TextoScore;
public  Text        TextKey;
public  Text        TextLife;
public  Sprite[]    imagemVidas;
public  Image       barraVidas;

[Header("Efeitos Sonoros")]
public  AudioSource JumpFX;
public  AudioClip   fxCenouraColetada;
public  AudioClip   fxChave;
public  AudioClip   fxExplosão;
public  AudioClip   fxDie;

[Header("Efeitos Visuais")]
public  GameObject  hitExplosão;

    
    public void Pontuacao(int qtdScore){
        score += qtdScore;
        TextoScore.text = score.ToString();

        JumpFX.PlayOneShot(fxCenouraColetada);
    }

    public void GanhaChave(int qtdKey){
        key += qtdKey;
        TextKey.text = key.ToString();

        JumpFX.PlayOneShot(fxChave);
    }

    

    public void LifeUp(){
        if(score >= 100){
            vidas +=1;
            score -= 100;
        }
    }

    public void BarrasVida(int health){

        barraVidas.sprite = imagemVidas[health];

    }

    public int GetPontuacao(){
        return score;
    }
        
    
}
