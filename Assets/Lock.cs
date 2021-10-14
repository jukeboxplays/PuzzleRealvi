using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Lock : MonoBehaviour
{

    //[System.NonSerialized]
    //public string selectTag;

    //[System.NonSerialized]
    //public List<string> tagArray;
    //public string[] tagArray;


    public List<string> tagArray;

    public Transform objectAction;

    //Animator anim;
    List<Animator> anim = new List<Animator>();

    public Transform dependentDoor;
    

    private void Start()
    {
        Animator animObj = objectAction.GetComponent<Animator>();
        anim.Add(animObj);

        
        if (dependentDoor != null)
        {
            Animator animDep = dependentDoor.GetComponent<Animator>();
            anim.Add(animDep);
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == selectTag)
        if(tagArray.Contains(other.gameObject.tag))
        {
            anim.ForEach(x => TriggerSet(x, "Open"));

            //objectAction.GetComponent<Animator>().enabled = true;
            //anim.SetTrigger("Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {

        //if (other.gameObject.tag == selectTag)
        if (tagArray.Contains(other.gameObject.tag))
        {
            anim.ForEach(x => TriggerSet(x, "Close"));
            //anim.SetTrigger("Close");

            //objectAction.GetComponent<Animator>().enabled = false;
        }
    }


    void TriggerSet(Animator selectedAnim, string triggerName)
    {
        selectedAnim.SetTrigger(triggerName);
    }

}




//Intervalo inutilizado pois mudei o approach por estar dando alguns erros
/*
[CustomEditor(typeof(Lock))]
public class LockEditor : Editor
{
    int tagIndex = 0;
    
    public override void OnInspectorGUI()
    {
        
        //string[] unityTags = UnityEditorInternal.InternalEditorUtility.tags;

        //Lock lockClass = target as Lock;
        ////tagIndex = EditorGUILayout.Popup("Tag to interact", tagIndex, UnityEditorInternal.InternalEditorUtility.tags);

        //tagIndex = EditorGUILayout.Popup("Tag to interact", tagIndex, unityTags);
        //lockClass.selectTag = unityTags[tagIndex];        

        //EditorUtility.SetDirty(target);
        

        Lock lockClass = target as Lock;

        string[] unityTags = UnityEditorInternal.InternalEditorUtility.tags;
        List<string> arrTags = new List<string>(unityTags);

        DrawDefaultInspector();

        tagIndex = EditorGUILayout.MaskField("Tag(s) to interact", tagIndex, unityTags);


        for (int i = 0; i < arrTags.Count; i++)
        {
            int layer = 1 << i;
            if ((tagIndex & layer) != 0)
            {
                //lockClass.tagArray.Add(arrTags[i]); //inutilizado
                //Debug.Log(arrTags[i]);
            }
        }

    }
}
*/
