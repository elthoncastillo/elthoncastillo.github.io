using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Code for characther manager; stil incomplete
/// Code inspired and modeled after this: https://www.youtube.com/watch?v=nnxZVU0qe5I&list=PLGSox0FgA5B7mApF1vhbspLj5NpzKedU6&index=1
/// </summary>

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;



    public RectTransform characterPanel; //to use with character since its more organize 

    public List<Character> characters = new List<Character>(); // characters in our scenes and far better than normal arrays because we don't have to specify the members of the array, we can simply add them like a constructor

    public Dictionary<string, int> characterDictionary = new Dictionary<string, int>(); //for easy access to the characters in the inspector so that we don't need to look at the code so much

     void Awake() // for the use of instantiating this class as a object
    {
        instance = this;
    }

    public Character GetCharacter(string characterName, bool createCharacterIfDoesNotExist = true, bool enableCreatedCharacterOnStart = true) //to return the characters if it's already on the scene and find the character
    {
        int index = -1;
        if (characterDictionary.TryGetValue (characterName, out index)) //to get the int associated to each character
        {
            return characters[index];
        }
        else if (createCharacterIfDoesNotExist)
        {  
            return CreateCharacter(characterName, enableCreatedCharacterOnStart);
        }

        return null;
    }

    public Character CreateCharacter(string characterName, bool enableOnStart = true) //if it does not exist, with this method of the CharacterManager will create one and assign a number to the Dictionary
    {
        Character newCharacter = new Character(characterName, enableOnStart);

        characterDictionary.Add(characterName, characters.Count);
        characters.Add(newCharacter);

        return newCharacter;
    }   
}
