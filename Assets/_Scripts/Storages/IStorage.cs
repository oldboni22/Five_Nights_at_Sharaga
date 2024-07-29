using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IStoreable 
{
    public string Id { get; }
}

public abstract class Storage<T> : ScriptableObject where T : IStoreable
{
    protected abstract T[] Members { get; }
    public T GetMemberById(string id)
    {
        return Members.Where(member => member.Id == id).Single();
    }
    public bool Exists(string id)
    {
        return Members.Any(member => member.Id == id);
    }
}
