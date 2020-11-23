using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomTag : MonoBehaviour
{
    [SerializeField]
    private List<string> tags = new List<string>();
    
    public bool HasTag(string tag)
    {
        return tags.Contains(tag);
    }

    public List<string> GetTags()
    {
        return tags;
    }

    public void Add(string tag)
    {
        tags.Add(tag);
    }

    public void Replace(string oldValue, string newValue)
    {
        tags[tags.IndexOf(oldValue)] = newValue;
    }
}
