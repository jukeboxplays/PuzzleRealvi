using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    float timer;
    TextMeshPro timerDisplay;

    //public bool openLevel2; //Totalmente provisorio devido ao tempo, depois isso deve ser colocado em algum outro script que poderá gerenciar variaveis entre cenas, em ideal colocar isso em MazeManager, ou GameplayManager (melhor esse p/ utilizar outras partes mencionadas nos comentarios).
    //Ou talvez usar PlayerPrefs (avaliar confiabilidade disso).

    // Start is called before the first frame update
    void Start()
    {
        timer = 15f * 60f;
        //timer = 5f;

        //DontDestroyOnLoad(this.gameObject); //POR ENQUANTO A CENA É CARREGADA EM MODO ADITIVO, ISSO NÃO SERÁ NECESSÁRIO
        timerDisplay =  this.GetComponent<TextMeshPro>();

        //openLevel2 = false;
        PlayerPrefs.SetInt("passLevel2", 0); //provisorio para a entrega

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
            SceneManager.LoadScene("gameoverscene");
            Destroy(this.gameObject);

        }

        //timerDisplay.text = timer.ToString("mm:ss");
        //timerDisplay.text = TimeSpan.FromSeconds(timer).ToString("mm:ss");

        float min = Mathf.FloorToInt(timer/60);
        float sec = Mathf.FloorToInt(timer%60);

        timerDisplay.text = string.Format("{0:00}:{1:00}", min, sec);



    }
}
