using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Code inspired by this:https://www.youtube.com/watch?v=nnxZVU0qe5I&list=PLGSox0FgA5B7mApF1vhbspLj5NpzKedU6&index=1
//this code is for CharacterTesting purpose

public class CharacterTesting : MonoBehaviour
{
    public Character Detective1;
    // Start is called before the first frame update
    void Start()
    {
        Detective1 = CharacterManager.instance.GetCharacter("Detective1", enableCreatedCharacterOnStart: false);
    }

    public string[] speech;
    int i = 0;

    public Vector2 moveTarget;
    public float moveSpeed;
    public bool smooth;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (i< speech.Length)
                Detective1.Di(speech [i]);
            else
                SistemaDeDialogo.instance.Close ();

            i++;
        }

        if (Input.GetKey(KeyCode.L))
        {
            Detective1.moveTo(moveTarget, moveSpeed, smooth);
        }   
    }
}
