using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour
{
    public List<GameObject> stack = new List<GameObject>();
    
    public void Push(GameObject obj)
    {
        stack.Add(obj);
    }

    public GameObject Pop()
    {
        if (stack == null)
            return null;
        GameObject obj = stack[stack.Count - 1];
        if (obj != null)
            stack.Remove(obj);
        return obj;
    }

    public GameObject lastObj()
    {
        if (stack.Count <= 0)
            return null;
        else
            return stack[stack.Count - 1];
    }
    public int Length()
    {
        return stack.Count;
    }
}
