using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//AVALIAR UTILIZACAO DO MESMO enum DA ResetLevel.cs
public enum LevelSwitcher
{
    level1,
    level2,
    level3
}

public class SwitchLevel : MonoBehaviour
{

    public LevelSwitcher level;
    public CinemachineBlendListCamera blendCam;

    public Transform timer;

    public bool execute;

    //provisorio para entrega, depois passar para CustomEditor e montar GUI onde poderá dar assign no Transform publico apenas se for nivel 2. Também verificar de nular esses Transforms caso nao seja nivel 2, para evitar possíveis bugs.
    public Transform mainDoor;
    public Transform mainDoorExit;


    //int last;
    int next;


    void Start()
    {
        //blendCam = this.GetComponent<CinemachineBlendListCamera>();
        //last = 0;
    }

    //TIRAR DO UPDATE FUTURAMENTE E DEIXAR MAIS EFICIENTE
    void Update()
    {
        //Debug.Log(last);

        switch (level)
        {
            //NAO NECESSARIO NO MOMENTO, MAS FICA DEFINIDO CASO PRECISAR UTILIZAR
            case LevelSwitcher.level1:
                if (execute) {

                    execute = false;
                }
                break;

            case LevelSwitcher.level2:

                if (execute)
                {
                    next = 0;

                    for (int c = 0; c < blendCam.m_Instructions.Length; c++)
                    {
                        blendCam.m_Instructions[c].m_VirtualCamera = blendCam.ChildCameras[next];
                        next++;
                        //blendCam.m_Instructions[c].m_VirtualCamera = blendCam.ChildCameras[last];
                        //last++;
                        //DEPOIS VOLTAR LAST QUANDO FOR DESENVOLVER PARA UM OBJETO MANAGER SÓ
                    }

                    //temporario para entrega
                    timer.localPosition = new Vector3(2f, 2f, 25f);
                    mainDoor.gameObject.SetActive(false);
                    mainDoorExit.GetComponent<Animator>().enabled = true; ;

                    execute = false;
                }
                
                break;

            case LevelSwitcher.level3:

                if (execute) 
                {
                    next = 1;
                    //last -= 1;

                    for (int c = 0; c < blendCam.m_Instructions.Length; c++)
                    {
                        blendCam.m_Instructions[c].m_VirtualCamera = blendCam.ChildCameras[next];
                        next++;
                        //blendCam.m_Instructions[c].m_VirtualCamera = blendCam.ChildCameras[last];
                        //last++;
                    }

                    timer.localPosition = new Vector3(-10f, 10f, 30f);

                    execute = false;
                }
                
                break;
        }
    }
}
