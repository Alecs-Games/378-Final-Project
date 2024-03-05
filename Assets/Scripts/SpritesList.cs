using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewList", menuName = "SpritesList", order = 1)]
public class SpritesList : ScriptableObject
{
    public Sprite[] sprites;
    public float animationSpeed;
}
