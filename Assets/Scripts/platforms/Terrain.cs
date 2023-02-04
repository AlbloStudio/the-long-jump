using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] private List<Sprite> _variants;
    [SerializeField] private int _amountToAdd;

    public List<Sprite> Variants => _variants;
    public int AmountToAdd => _amountToAdd;
}
