using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //keyBoard contains all of the characters we'd like to pick random keys from
    private string keyBoard = "qwertyuiopasdfghjkl;zxcvbnm,./";

    //these help us keep track of active keys
    public KeyCode playerUp = KeyCode.W;
    public KeyCode playerLeft = KeyCode.A;
    public KeyCode playerRight = KeyCode.D;
    public KeyCode nextRandKey;

    public int lastDir = 0;
    public TMPro.TextMeshPro keySwitch;
    private bool isStuck = false;
    private float collisionTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(keyBoard[KeyGen()]);
        if (Input.GetKey(KeyCode.W))
        {
            //jump would potentially go here, 
            //but this is included so nothing happens when W is pressed
        }

        if (Input.GetKeyDown(playerRight))
        {
            GetComponent<CharacterController>().Move(new Vector3(2.5f, 0f, 0f));
        }
        if (Input.GetKeyDown(playerLeft))
        {
            GetComponent<CharacterController>().Move(new Vector3(-2.5f, 0f, 0f));
        }

        //Running in the 90's
        GetComponent<CharacterController>().Move(new Vector3(0f, 0f, 1f));

        //Debug.Log(collisionTimer);

        collisionTimer++;

        //if you go a certain amount of time without being stuck on on obstacle...
        if ((int)collisionTimer%400 == 0 && !isStuck)
        {
            //we change our keys!!

            //KeyGen gets a random character position
            int randNum = KeyGen();

            //TryGetValue uses that character position to get a key from the dictionary
            chartoKeycode.TryGetValue(keyBoard[randNum], out nextRandKey);

            //We check what direction was last and assign the key~
            if (lastDir == 0)
            {
                playerLeft = nextRandKey;
                lastDir = 1;
            } else if (lastDir == 1)
            {
                playerRight = nextRandKey;
                lastDir = 0;
            }
            Debug.Log(nextRandKey);

            //updating the text
            DisplayKey(keyBoard[randNum]);
        }
    }

    int KeyGen()
    {
        //this uses the keyboard string's length so you don't need to edit
        //any numbers in the method if you want to change the keys used
        int stringVal = (int)Random.Range(0f, (float)keyBoard.Length);
        return stringVal;
    }


    void DisplayKey(char keyIndex)
    {
        string dir;

        //this controls the alternation between left and right
        if (lastDir == 1)
        {
            dir = "left";
        } else if (lastDir == 0)
        {
            dir = "right";
        }
        else
        {
            dir = "this will never happen!";
        }
        //updates the textmesh
        keySwitch.text = dir + " is now " + keyIndex.ToString();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            //Debug.Log("we hit");
            isStuck = true;
        }


    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            collisionTimer = 0f;
            isStuck = false;
        }
    }


    //this dictionary was created by Brian Cancel, github user b-cancel 
    //NOTE: This is only a DICTIONARY with MOST character to keycode bindings... it is NOT a working cs file
    //ITS USEFUL: when you are reading in your control scheme from a file

    //NOTE: some characters SHOULD map to multiple keycodes (but this is impossible)
    //since this is a dictionary, only 1 character is bound to 1 keycode
    //EX: * from the keyboard will be read the same as * from the keypad... because they produce the same character in a text file

    Dictionary<char, KeyCode> chartoKeycode = new Dictionary<char, KeyCode>()
{
  //-------------------------LOGICAL mappings-------------------------
  
  //Lower Case Letters
  {'a', KeyCode.A},
  {'b', KeyCode.B},
  {'c', KeyCode.C},
  {'d', KeyCode.D},
  {'e', KeyCode.E},
  {'f', KeyCode.F},
  {'g', KeyCode.G},
  {'h', KeyCode.H},
  {'i', KeyCode.I},
  {'j', KeyCode.J},
  {'k', KeyCode.K},
  {'l', KeyCode.L},
  {'m', KeyCode.M},
  {'n', KeyCode.N},
  {'o', KeyCode.O},
  {'p', KeyCode.P},
  {'q', KeyCode.Q},
  {'r', KeyCode.R},
  {'s', KeyCode.S},
  {'t', KeyCode.T},
  {'u', KeyCode.U},
  {'v', KeyCode.V},
  {'w', KeyCode.W},
  {'x', KeyCode.X},
  {'y', KeyCode.Y},
  {'z', KeyCode.Z},
  
  //KeyPad Numbers
  {'1', KeyCode.Keypad1},
  {'2', KeyCode.Keypad2},
  {'3', KeyCode.Keypad3},
  {'4', KeyCode.Keypad4},
  {'5', KeyCode.Keypad5},
  {'6', KeyCode.Keypad6},
  {'7', KeyCode.Keypad7},
  {'8', KeyCode.Keypad8},
  {'9', KeyCode.Keypad9},
  {'0', KeyCode.Keypad0},
  
  //Other Symbols
  {'!', KeyCode.Exclaim}, //1
  {'"', KeyCode.DoubleQuote},
  {'#', KeyCode.Hash}, //3
  {'$', KeyCode.Dollar}, //4
  {'&', KeyCode.Ampersand}, //7
  {'\'', KeyCode.Quote}, //remember the special forward slash rule... this isnt wrong
  {'(', KeyCode.LeftParen}, //9
  {')', KeyCode.RightParen}, //0
  {'*', KeyCode.Asterisk}, //8
  {'+', KeyCode.Plus},
  {',', KeyCode.Comma},
  {'-', KeyCode.Minus},
  {'.', KeyCode.Period},
  {'/', KeyCode.Slash},
  {':', KeyCode.Colon},
  {';', KeyCode.Semicolon},
  {'<', KeyCode.Less},
  {'=', KeyCode.Equals},
  {'>', KeyCode.Greater},
  {'?', KeyCode.Question},
  {'@', KeyCode.At}, //2
  {'[', KeyCode.LeftBracket},
  {'\\', KeyCode.Backslash}, //remember the special forward slash rule... this isnt wrong
  {']', KeyCode.RightBracket},
  {'^', KeyCode.Caret}, //6
  {'_', KeyCode.Underscore},
  {'`', KeyCode.BackQuote},
  
  //-------------------------NON-LOGICAL mappings-------------------------
  
  //NOTE: all of these can easily be remapped to something that perhaps you find more useful
  
  //---Mappings where the logical keycode was taken up by its counter part in either (the regular keybaord) or the (keypad)
  
  //Alpha Numbers
  //NOTE: we are using the UPPER CASE LETTERS Q -> P because they are nearest to the Alpha Numbers
  {'Q', KeyCode.Alpha1},
  {'W', KeyCode.Alpha2},
  {'E', KeyCode.Alpha3},
  {'R', KeyCode.Alpha4},
  {'T', KeyCode.Alpha5},
  {'Y', KeyCode.Alpha6},
  {'U', KeyCode.Alpha7},
  {'I', KeyCode.Alpha8},
  {'O', KeyCode.Alpha9},
  {'P', KeyCode.Alpha0},
  
  //INACTIVE since I am using these characters else where
  {'A', KeyCode.KeypadPeriod},
  {'B', KeyCode.KeypadDivide},
  {'C', KeyCode.KeypadMultiply},
  {'D', KeyCode.KeypadMinus},
  {'F', KeyCode.KeypadPlus},
  {'G', KeyCode.KeypadEquals},
  
  //-------------------------CHARACTER KEYS with NO KEYCODE-------------------------
  
  //NOTE: you can map these to any of the OPEN KEYCODES below
  
  /*
  //Upper Case Letters (16)
  {'H', -},
  {'J', -},
  {'K', -},
  {'L', -},
  {'M', -},
  {'N', -},
  {'S', -},
  {'V', -},
  {'X', -},
  {'Z', -}
  */
  
  //-------------------------KEYCODES with NO CHARACER KEY-------------------------
  
  //-----KeyCodes without Logical Mappings
  //-Anything above "KeyCode.Space" in Unity's Documentation (9 KeyCodes)
  //-Anything between "KeyCode.UpArrow" and "KeyCode.F15" in Unity's Documentation (24 KeyCodes)
  //-Anything Below "KeyCode.Numlock" in Unity's Documentation [(28 KeyCodes) + (9 * 20 = 180 JoyStickCodes) = 208 KeyCodes]
  
  //-------------------------other-------------------------

  //-----KeyCodes that are inaccesible for some reason
  //{'~', KeyCode.tilde},
  //{'{', KeyCode.LeftCurlyBrace}, 
  //{'}', KeyCode.RightCurlyBrace}, 
  //{'|', KeyCode.Line},   
  //{'%', KeyCode.percent},
};
}
