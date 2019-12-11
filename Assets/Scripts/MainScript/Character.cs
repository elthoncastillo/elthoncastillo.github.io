using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Code insipired by this: https://www.youtube.com/watch?v=nnxZVU0qe5I&list=PLGSox0FgA5B7mApF1vhbspLj5NpzKedU6&index=1
//Character object and dialog control

[System.Serializable]
public class Character 
{
    public string characterName; //create a new character with its name
    public bool isMultiLayerCharacter { get { return renderers.singleLayerImage == null; } } //for multilayered

    public bool enabled { get { return root.gameObject.activeInHierarchy; } set { root.gameObject.SetActive(value); } } //to enabke show on the screen

    public Vector2 anchorPadding { get { return root.anchorMax - root.anchorMin; } } //this get the  total measurament between the anchors

    SistemaDeDialogo dialogue;

    public void Di(string speech, bool add = false)
    {
        if (!enabled)
            enabled = true;  //make the characters appears only when they talk
        if (!add)
            dialogue.Di(speech, characterName);
        else
            dialogue.DiAdd(speech, characterName);
    }

    //for characters movement
    Vector2 targetPosition;
    Coroutine moving;
    bool isMoving { get { return moving != null; } } // to detect whether or not the character is moving and stores it as a boolean
    public void moveTo(Vector2 Target, float speed, bool smooth = true)
    {
        StopMoving();
        moving = CharacterManager.instance.StartCoroutine(Moving(Target, speed, smooth));
    }

    public void StopMoving() //a method that allows to stop the mvement
    {  
        if (isMoving) //a boolean
        {
            CharacterManager.instance.StopCoroutine(moving);
        }

        moving = null; //if not set movement to none
    }

    IEnumerator Moving(Vector2 target, float speed, bool smooth) //to return the movement being registrated
    {
        targetPosition = target; // the movement in the anchors (the things that are on the inspector, that allow us to personalize the resolution
        Vector2 padding = anchorPadding; //this limit the space between the x position and y position on the screen
        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;

        Vector2 minAnchorTarget = new Vector2(maxX * targetPosition.x, maxY * targetPosition.y); // these are the minimum positions in the anchors, so they add as a collider for the characters 
        speed *= Time.deltaTime;

        while(root.anchorMin != minAnchorTarget ){ //the character moves until it reaches a certain position in the anchors
            root.anchorMin = (!smooth) ? Vector2.MoveTowards(root.anchorMin, minAnchorTarget, speed) : Vector2.Lerp(root.anchorMin, minAnchorTarget, speed);
            root.anchorMax = root.anchorMin + padding;
            yield return new WaitForEndOfFrame();
        }

        StopMoving();
            
    }


    //for characters movement
    [HideInInspector] //unity attribute for hidding things
    public RectTransform root; // the root object, the container for all the images
    public Character(string _name, bool enableOnStart = true)
    {
        CharacterManager cm = CharacterManager.instance;
        GameObject prefab = Resources.Load("Characters/Character["+_name+"]") as GameObject; //to locate the ccharacters' files and as a GameObject rather than a normal object
        GameObject ob = GameObject.Instantiate(prefab, cm.characterPanel); //to respawn the characters into the scene. it is better than having invoke because you can control this easier

        root = ob.GetComponent<RectTransform>();
        characterName = _name;

        renderers.singleLayerImage = ob.GetComponentInChildren<RawImage>();
        if (isMultiLayerCharacter)
        {
            renderers.cuerpoRenderer = ob.transform.Find("cuerpoLayer").GetComponent<Image>(); //to get the expresions
            renderers.expresionRenderer = ob.transform.Find("expresionLayer").GetComponent<Image>();

        }

        dialogue = SistemaDeDialogo.instance; //since this class is not a MonoBehavior, it is needed to store a reference point using this
        enabled = enableOnStart;
    }
    [System.Serializable]
    public class Renderers
    {
        public RawImage singleLayerImage; //single layer character only

        public Image cuerpoRenderer; //multiple character layer only
        public Image expresionRenderer;
    }

    public Renderers renderers = new Renderers();
}
