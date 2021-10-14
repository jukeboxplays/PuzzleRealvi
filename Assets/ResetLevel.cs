using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Level
{
    level1,
    level2,
    level3
}


public class ResetLevel : MonoBehaviour
{

    //FUTURAMENTE IMPLEMENTAR SERIALIZABLE PARA CONTROLAR DEFINICIOES DE FORMA MAIS PERSONALIZADA NO MENU, COM MENOS INFORMACOES
    //FAZ CADA LEVEL PODE ADICIONAR OBJETOS EM UM ARRAY SENDO QUE O CONTEUDO SERA ELE MESMO E O PONTO DE ORIGEM, TALVEZ USAR DICIONARIOS OU LISTA/ARRAY DENTRO DE LISTA/ARRAY
    //NO MOMENTO MANTER ASSIM POR QUESTAO DE TEMPO DE ENTREGA

    public Level currentLevel;
    
    public Transform level1_key;
    public Transform level1_key_origin;

    public Transform level1_moneybag;
    public Transform level1_moneybag_origin;



    public Transform level2_ball;
    public Transform level2_ball_origin;
    
    public Transform level2_cube;
    public Transform level2_cube_origin;



    public Transform level3_keys_origin;
    public Transform level3_mastermind;


    void Start()
    {
        //currentLevel = Level.level1; 
    }

    public void LevelReset()
    {
        switch (currentLevel)
        {
            case Level.level1:
                Level1Reset();
                break;

            case Level.level2:
                Level2Reset();
                break;

            case Level.level3:
                Level3Reset();
                break;
        }

    }

    void Level1Reset()
    {
        level1_key.position = level1_key_origin.position;
        level1_moneybag.position = level1_moneybag_origin.position;
    }


    void Level2Reset()
    {
        level2_cube.rotation = level2_cube_origin.rotation;
        level2_ball.position = level2_ball_origin.position;
    }
    void Level3Reset()
    {
        for (int i = level3_keys_origin.childCount - 1; i >= 0; i--)
        {
            Destroy(level3_keys_origin.GetChild(i).gameObject);
        }

        level3_mastermind.GetComponent<MastermindManager>().Spawner();


    }



}
