using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KeybindManager : MonoBehaviour
{
    private static KeybindManager instance;

    public static KeybindManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<KeybindManager>();
            }

            return instance;
        }
    }

    public Dictionary<string, KeyCode> Keybinds { get; private set; }

    public Dictionary<string, KeyCode> Actionbinds { get; private set; }

    private string bindName;

     

    void Start()
    {
        

        Keybinds = new Dictionary<string, KeyCode>();
        Actionbinds = new Dictionary<string, KeyCode>();

        BindKey("UPB", KeyCode.W);
        BindKey("DOWNB", KeyCode.S);
        BindKey("RIGHTB", KeyCode.D);
        BindKey("LEFTB", KeyCode.A);

        BindKey("ACT1", KeyCode.Alpha1);
        BindKey("ACT2", KeyCode.Alpha2);
        BindKey("ACT3", KeyCode.Alpha3);

    }

    public void BindKey(string key, KeyCode keybind)
    {
        Dictionary<string, KeyCode> currentDictionary = Keybinds;

        if (key.Contains("ACT"))
        {
            currentDictionary = Actionbinds;
        }
        if (!currentDictionary.ContainsKey(key))
        {
            currentDictionary.Add(key, keybind);
            UiManager.MyInstance.UpdateKeyText(key, keybind);
            
        }
        else if (currentDictionary.ContainsValue(keybind))
        {
            string myKey = currentDictionary.FirstOrDefault(x => x.Value == keybind).Key;
            currentDictionary[myKey] = KeyCode.None;

            UiManager.MyInstance.UpdateKeyText(key, KeyCode.None);
        }

        currentDictionary[key] = keybind;
        UiManager.MyInstance.UpdateKeyText(key, keybind);
        bindName = string.Empty;
        
        
    }

    public void KeyBindOnClick(string bindName)
    {
        this.bindName = bindName;
    }

    private void OnGUI()
    {
        if (bindName != string.Empty)
        {
            Event e = Event.current;

            if (e.isKey)
            {
                BindKey(bindName, e.keyCode);
            }
        }
    }

}
