using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyAction : MonoBehaviour
{
    [System.Serializable]
    public enum Type
    {
        PRESSED,
        DOWN,
        RELEASED,
        UP,
    }

    public KeyCode key;
    public UnityEvent onAction;
    public Type type;

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            default:
            case Type.PRESSED:
                if (Input.GetKeyDown(key))
                    onAction.Invoke();
                break;
            case Type.DOWN:
                if (Input.GetKey(key))
                    onAction.Invoke();
                break;
            case Type.RELEASED:
                if (Input.GetKeyUp(key))
                    onAction.Invoke();
                break;
            case Type.UP:
                if (!Input.GetKey(key))
                    onAction.Invoke();
                break;
        }
    }
}
