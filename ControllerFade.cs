using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ControllerFade : MonoBehaviour
{
    public static ControllerFade    _instanciaFade;
    private FimFase                  _FimFase; 
    public Image                    _imagemFade;
    public Color                    _corInicial;
    public Color                    _corFinal;
    public Color                    _corInicial2;
    public Color                    _corFinal2;
    public float                    _duracaoFade;
    public bool                     _isFade;
    private float                    _tempo;
    
    // Start is called before the first frame update
    void Awake()
    {
        _instanciaFade = this;
    }

    public IEnumerator inicioFade(){
        _isFade = true;
        _tempo = 0f;

        while(_tempo <= _duracaoFade){

            _imagemFade.color = Color.Lerp(_corInicial, _corFinal, _tempo / _duracaoFade);
            _tempo = _tempo + Time.deltaTime;
            yield return null;
        }
    
        _isFade = false;
    }

    public IEnumerator FinalFade(){
        _isFade = true;
        _tempo = 0f;

        while(_tempo <= _duracaoFade){

            _imagemFade.color = Color.Lerp(_corInicial2, _corFinal2, _tempo / _duracaoFade);
            _tempo += Time.deltaTime;
            yield return null;
        }

        _isFade = false;
    }


    
    void Start()
    {
        StartCoroutine(inicioFade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
