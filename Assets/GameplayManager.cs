using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Type
{
    Interactable,
    Playable
}

public enum EventToPlay
{
    CurtainOpener,
    Test
}


[CustomEditor(typeof(GameplayManager))]
public class GameplayManager_Editor : Editor
{
    override public void OnInspectorGUI()
    {
        GameplayManager gameplayManager = target as GameplayManager;


        gameplayManager.mainCamera = (Camera)EditorGUILayout.ObjectField("Main Camera", gameplayManager.mainCamera, typeof(Camera), true);
        gameplayManager.timer = (Transform)EditorGUILayout.ObjectField("Timer", gameplayManager.timer, typeof(Transform), true);
        gameplayManager.type = (Type)EditorGUILayout.EnumPopup("Type", gameplayManager.type);

        switch (gameplayManager.type)
        {
            case Type.Interactable:
                break;

            case Type.Playable:
                gameplayManager.eventToPlay = (EventToPlay)EditorGUILayout.EnumPopup("Event To Play", gameplayManager.eventToPlay);


                //MAPEAR EVENTOS PARA MOSTRAR EM EDITOR
                switch (gameplayManager.eventToPlay)
                {
                    case EventToPlay.CurtainOpener:
                        //gameplayManager.type = (Type)EditorGUILayout.EnumPopup("Type", gameplayManager.type);
                        gameplayManager.selectedObject = (Transform)EditorGUILayout.ObjectField("Object of interest", gameplayManager.selectedObject, typeof(Transform), true);
                        gameplayManager.curtain_Main = (Transform)EditorGUILayout.ObjectField("Curtain Main", gameplayManager.curtain_Main, typeof(Transform), true);
                        gameplayManager.curtain_L = (Transform)EditorGUILayout.ObjectField("Curtain L", gameplayManager.curtain_L, typeof(Transform), true);
                        gameplayManager.curtain_R = (Transform)EditorGUILayout.ObjectField("Curtain R", gameplayManager.curtain_R, typeof(Transform), true);
                        break;


                    case EventToPlay.Test:
                        gameplayManager.selectedObject = (Transform)EditorGUILayout.ObjectField("Object of interest", gameplayManager.selectedObject, typeof(Transform), true);
                        break;
                }
                break;
        }

        EditorUtility.SetDirty(target);

    }
}


public class GameplayManager : MonoBehaviour
{
    public EventToPlay eventToPlay;
    public Transform curtain_Main;
    public Transform curtain_L;
    public Transform curtain_R;
    public Camera mainCamera;

    public Transform timer;

    public Type type;

    public Transform selectedObject;

    private bool interactFollow = false;
    private Transform objFollow;


    void Update()
    {
        //Habilitar if somente quando dublagem acaba
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.collider.name);

                //if(hit.collider.tag == "Interactable")
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Interactable")) //ALTERADO PARA LAYER P/ NN PRECISAR DE TAG E LAYER IGUAL
                {
                    interactFollow = !interactFollow;
                    objFollow = hit.transform;
                    
                    
                }

                //if (hit.collider.tag == "Playable" && (hit.collider.name == selectedObject.name)) //ALTERADO PARA LAYER P/ NN PRECISAR DE TAG E LAYER IGUAL
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Playable") && (hit.collider.name == selectedObject.name))
                {
                    EventPlayer(hit.collider);
                }


                //___________________________________________________________________
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Reset"))
                {
                    hit.transform.gameObject.GetComponent<ResetLevel>().LevelReset();
                }

                if(hit.transform.tag == "MazeEnter")
                {
                    SceneManager.LoadScene("mazescene", LoadSceneMode.Additive);
                    timer.position = new Vector3(-12, -202, 6);
                    timer.rotation = new Quaternion(90f, 0f, 0f, 90f);
                }

                if(hit.transform.tag == "NextLevel")
                {
                    hit.transform.GetComponent<SwitchLevel>().execute = true;
                }

            }
        }


        if (interactFollow)
        {
            //objFollow.transform.position = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.WorldToScreenPoint(objFollow.transform.position).z));
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int layerMask = 1 << 6;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layerMask)) 
            {

                //RaycastHit heightHit;
                //var h = objFollow.GetComponent<MeshCollider>().bounds.min;
                //Physics.Raycast(h, Vector3.down, out heightHit, 4f, ~layerMask);
                //objFollow.transform.position = new Vector3(hit.point.x, hit.point.y + heightHit.distance, hit.point.z);



                //objFollow.transform.position = hit.point;
                objFollow.transform.position = Vector3.MoveTowards(objFollow.transform.position, hit.point, 25f * Time.deltaTime);

                objFollow.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation; ;

                //objFollow.transform.position = Vector3.MoveTowards(objFollow.transform.position, hit.point, 2f);
                //objFollow.transform.position += Vector3.Normalize(hit.point - objFollow.transform.position) * 25f * Time.deltaTime;


            }
        }

        else if(objFollow)
        {
            objFollow.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
        }

    }


    void EventPlayer(Collider hit)
    {
        //REGRAS DE OBJETOS_______________________________________
        
        if (hit.name == curtain_Main.name)
        {
            curtain_Main.transform.GetComponent<BoxCollider>().enabled = false;
        }
        //________________________________________________________
        



        //INVOKE PADRAO____
        Invoke(eventToPlay.ToString(), 0.0f);     

    }



    //EVENTOS__________________________

    public void CurtainOpener()
    {
        StartCoroutine("ICurtainOpener", 0.0f);
        //StartCoroutine(ICurtainOpener().ToString(), 0.0f);
        //this.enabled = false;
    }

    public IEnumerator ICurtainOpener()
    {

        var child = curtain_Main.GetComponentInChildren<Light>();
        var anim = child.transform.GetComponent<Animator>();
        anim.enabled = true;
        child.enabled = true;

        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);

        anim.enabled = false;

        Vector3 offset = new Vector3(0.1f, 0.0f, 0.0f);

        float seconds = 2.0f;

        for (int i = 0; i < 40; i++)
        {
            curtain_L.position -= offset;
            curtain_R.position += offset;
            yield return new WaitForSeconds(0.03f);
        }

        child.enabled = false;

        yield return new WaitForSeconds(seconds);;

    }





}

