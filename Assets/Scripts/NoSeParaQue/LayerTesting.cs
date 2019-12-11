using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script is for backgrounds and movie controlling 
//Code inspired by this https://www.youtube.com/watch?v=nnxZVU0qe5I&list=PLGSox0FgA5B7mApF1vhbspLj5NpzKedU6&index=1

public class LayerTesting : MonoBehaviour
{
    BCFC controller; //initialize this as a reference

    public Texture text;
   // public MovieTexture mov;



    // Start is called before the first frame update
    void Start()
    {
       controller = BCFC.instance;
    }

    // Update is called once per frame
    void Update()
    {
        BCFC.LAYER layer = null;
        if (Input.GetKey(KeyCode.Z)) // if this key is pressed, it will load a new background
            layer = controller.background;
        if (Input.GetKey(KeyCode.X))
            layer = controller.cinematic; //key related to cinematics
        if (Input.GetKey(KeyCode.C))
            layer = controller.foreground; //key related to foreground

        if (Input.GetKey(KeyCode.V))
        {
            if (Input.GetKeyDown(KeyCode.A))
                layer.TransitionToTexture(text);
           // else if (Input.GetKeyDown(KeyCode.S))
               // layer.SetTexture(mov);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A))
                layer.SetTexture(text);
          //  else if (Input.GetKeyDown(KeyCode.S))
              //  layer.SetTexture(mov);
        }
    }


}
