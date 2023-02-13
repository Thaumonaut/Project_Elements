using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PlayerAttack();

public class Sword_Attack : MonoBehaviour
{

    float forceAmount = 6000f;
    public bool isAttacking = false;
    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag != "Enemy") return;

        if(isAttacking)
        {
            var forwardForce = new Vector2(transform.parent.up.x, transform.parent.up.y) * forceAmount;
            other.attachedRigidbody.AddForce(forwardForce);
        }

    }

    public void AttackEvent(int attackingStatus) {

        var Collider = GetComponent<PolygonCollider2D>();

        isAttacking = attackingStatus != 0;
        //setAttacking.Invoke();
        if(attackingStatus != 0) {
            Collider.enabled = true;
        } else {
            Collider.enabled = false;
            transform.parent.GetComponent<PlayerMovement>().isAttacking = false;
        }
    }

    public event PlayerAttack setAttacking;
}
