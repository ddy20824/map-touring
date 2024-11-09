using System;
using System.Collections;
using UnityEngine;

public class Helper
{
    public static IEnumerator Delay(Action methodName, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        methodName();
    }
    public static GameObject FindParentWithTag(GameObject childObject, string tag)
    {
        Transform t = childObject.transform;
        while (t.parent != null)
        {
            if (t.parent.tag == tag)
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null; // Could not find a parent with given tag.
    }
}