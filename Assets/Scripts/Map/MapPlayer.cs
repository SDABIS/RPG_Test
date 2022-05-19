using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayer : MapCharacter
{
    // Update is called once per frame
    protected override Vector3Int NextDirection()
    {
        
        Vector3Int direction = Vector3Int.zero;
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) {
            direction.x = (int)Input.GetAxisRaw("Horizontal");
        }
        else if(Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) {
            direction.y = (int)Input.GetAxisRaw("Vertical");
        }

        return direction;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            EventBroker.Instance.CallBattleStart();
        }
    }
}
