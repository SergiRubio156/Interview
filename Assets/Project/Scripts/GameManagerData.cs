using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameManagerData ", menuName = "ScriptableObjects/GameManagerData")]
public class GameManagerData : ScriptableObject
{
    public List<string> objectList = new List<string>();

    public Transform positionPlayer;
}