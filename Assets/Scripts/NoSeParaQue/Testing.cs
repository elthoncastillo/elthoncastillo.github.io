//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Testing : MonoBehaviour
//{
//    SistemaDeDialogo dialogue;
//    // Start is called before the first frame update
//    void Start()
//    {
//        dialogue = SistemaDeDialogo.instance;
//    }

//    public string[] d = new string[]
//        {
//          

//        };

//    int index = 0;

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            if (!dialogue.isSpeaking || dialogue.isWaitingForUserInput)
//            {
//                if (index >= d.Length)
//                {
//                    return;
//                }
//                Di(d[index]);
//                index++;
//            }
//        }
//    }

//    void Di(string d)
//    {
//        string[] parts = d.Split(':');
//        string speech = parts[0];
//        string speaker = (parts.Length >= 2) ? parts[1] : "";

//        dialogue.Di(speech, speaker);
//    }
//}
