using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapCharacter : MonoBehaviour
{
    private Vector3Int currentCell;
    [SerializeField] protected Transform target;
    [SerializeField] float speed = 1.0f;

    [SerializeField] LayerMask whatStopsMovement;

    // Start is called before the first frame update
    void Start()
    {
        currentCell = MapManager.Instance.GetClosestCell(transform.position);
        transform.position = MapManager.Instance.GetPosition(currentCell);

        target.parent = null;
    }

    protected virtual void Update() {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, target.position) <= .05f) MoveTarget();

    }

    private void MoveTarget() {
        Vector3Int nextDir = NextDirection();
        if(nextDir == Vector3Int.zero) return;

        Vector3Int nextCell = currentCell + nextDir;
        Vector3 nextPos = MapManager.Instance.GetPosition(nextCell);

        if(Physics2D.OverlapCircle(nextPos, .2f, whatStopsMovement)) return;

        target.position = nextPos;
        currentCell = nextCell;
    }

    protected abstract Vector3Int NextDirection();

}
