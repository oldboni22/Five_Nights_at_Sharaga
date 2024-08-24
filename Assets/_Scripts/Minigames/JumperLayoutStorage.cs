using minigames.jumper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "minigames/jumper/layoutStorage")]
public class JumperLayoutStorage : Storage<jumperLayout>
{
    [SerializeField] private jumperLayout[] _members;
    protected override jumperLayout[] Members => _members;
}

[System.Serializable]
public class JumperLayoutStorageRef : AssetReferenceT<JumperLayoutStorage>
{
    public JumperLayoutStorageRef(string guid) : base(guid)
    {
    }
}