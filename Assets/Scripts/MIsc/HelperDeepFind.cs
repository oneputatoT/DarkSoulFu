using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperDeepFind
{
    public static Transform DeepFind(this Transform parent, string targetname)
    {
        Transform temp = null;

        foreach (Transform child in parent)
        {
            if (child.name == targetname)
            {
                return child;
            }
            else
            {
                temp = DeepFind(child, targetname);
                if (temp!= null)
                {
                    return temp;
                }
            }
        }
        return null;    
    }
}
