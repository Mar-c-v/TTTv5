using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : Tile
{
    [SerializeField] private Color _baseColor, _offsetColor;
    public override void Init(int x, int y)
    {
        var isOffset = ((x + y) % 2 == 1);
        _renderer.color = (x + y) % 2 == 0 ? _baseColor : _offsetColor;

    }
}
