using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEnemy : MapCharacter
{
    [SerializeField] float delayTime;
    private bool canMove = true;
    private Vector3Int[] possibleDirections = new Vector3Int[] 
        { Vector3Int.left, Vector3Int.right, Vector3Int.up, Vector3Int.down };


    protected override Vector3Int NextDirection()
    {
        Vector3Int direction = Vector3Int.zero;
        if(canMove) {

            direction = possibleDirections[Random.Range(0, possibleDirections.Length)];

            canMove = false;
            Invoke("AllowMove", delayTime);
        }

        return direction;
    }

    private void AllowMove() {
        canMove = true;
    }
}
