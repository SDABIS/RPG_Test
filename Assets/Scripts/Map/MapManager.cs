using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    private Grid _grid;
    public Grid Grid {get {return _grid;}}
    
    protected override void Awake() {
        base.Awake();
        
        _grid = GetComponent<Grid>();
    }

    public Vector3Int GetClosestCell(Vector2 position) {
        Vector3Int idx = _grid.WorldToCell(position);
        return idx;
    }

    public Vector3 GetPosition(Vector3Int cell) {
        Vector3 pos = _grid.GetCellCenterWorld(cell);
        return pos;
    }
}
