using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MastermindManager : MonoBehaviour
{
    public Transform keySpawner;
    public GameObject modelKey;

    public Transform frontPanel;
    public Transform youWon;
    public GameObject timer;

    public Transform[] boxes;
    public List<Color> boxesColors;

    public Transform key1;
    public Transform key2;
    public Transform key3;
    public Transform key4;

    Transform[] keys;
    Color[] keysColors;

    Color[] colors;


    void Start()
    {
        keys = new Transform[] { key1, key2, key3, key4 };
        keysColors = new Color[keys.Length];
        colors = new Color[] { Color.blue, Color.cyan, Color.green, Color.red, Color.magenta, Color.yellow};

        /*
        for(var c = 0; c < colors.Length; c++)
        {
            Debug.Log(c + ":" + colors[c].ToString());
        }*/

        /*
        foreach (var k in keys)
        {
            k.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length - 1)];
        }
        */


        for(int k = 0; k < keys.Length; k++)
        {
            int random = Random.Range(0, colors.Length - 1);

            keys[k].GetComponent<Renderer>().material.color = colors[random];
            keysColors[k] = keys[k].GetComponent<Renderer>().material.color;
            
            //Debug.Log(keys[k].name);

        }

        Spawner(); 

    }


    public void Spawner()
    {
        float offsetX = 0f;

        foreach (var c in colors)
        {
            float offsetZ = 0f;
            //GameObject newKey = modelKey;
            //newKey.GetComponent<Renderer>().material.color = colors[random];

            for (int i = 0; i < 4; i++)
            {
                GameObject newKey = Instantiate(modelKey, keySpawner.position + new Vector3(offsetX, 0, offsetZ), new Quaternion(0f, -90f, 0f, 90f), keySpawner.transform);//Quaternion.identity);
                newKey.GetComponent<Renderer>().material.color = c;

                offsetZ += 1f;
            }

            offsetX += 2f;
        }
    }



    public void Receiver(Collider other, int keyNumber)
    {
        Color colliderColor = other.gameObject.GetComponent<Renderer>().material.color;

        List<Color> colorsList = new List<Color>();
        colorsList.AddRange(keysColors); ;


        //if (other.gameObject.GetComponent<Renderer>().material.color == keys[keyNumber - 1].GetComponent<Renderer>().material.color)
        if ( colliderColor == keysColors[keyNumber - 1])
        {
            boxes[keyNumber - 1].GetComponent<Renderer>().material.color = Color.green;
            boxes[keyNumber - 1].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green * 5f); ;
        }

        //FAZER LOGICA P/ VER SE CONTEM ESSA COR EM QUALQUER OUTRA, QUE NAO SEJA ESSA, CASO SIM AMARELO. CASO NAO TENHA EM NENHUMA MESMO, ENTÃO VERMELHO.
        else if (colorsList.Contains(colliderColor))
        {
            boxes[keyNumber - 1].GetComponent<Renderer>().material.color = Color.yellow;
            boxes[keyNumber - 1].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow * 5f); ;
        }

        else
        {
            boxes[keyNumber - 1].GetComponent<Renderer>().material.color = Color.red;
            boxes[keyNumber - 1].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red * 5f); ;
        }

        boxesColors = new List<Color>();

        foreach (var b in boxes)
        {
            boxesColors.Add(b.GetComponent<Renderer>().material.color);
        }

        if (boxesColors.TrueForAll(i => i.Equals(Color.green)))
        {
            frontPanel.GetComponent<Animator>().enabled = true;
            youWon.gameObject.SetActive(true);
            Destroy(timer);
        }


    }

}
