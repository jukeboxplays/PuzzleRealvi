using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;




//___________________________________________
//
//
//MUDAR TUDO APENAS PARA CLASSE Timer DEPOIS
//
//___________________________________________




public class GameOverTimer : MonoBehaviour
{
    float timer;
    TextMeshPro timerDisplay;

    void Start()
    {
        timer = 10f;
        timerDisplay = this.GetComponent<TextMeshPro>();
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
            SceneManager.LoadScene("mainscene");

        }

        float sec = Mathf.FloorToInt(timer % 60);

        timerDisplay.text = sec.ToString();



    }
}

