using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Global;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GlobalVars.ElementType currentElement;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject sword;
    [SerializeField] private Animator sword_animator;
    [SerializeField] private float speed = 1f;
    Vector2 velocity;
    //public bool isAttacking = false;
    bool canChangeElement = true;
    public bool isAttacking = false;

    private void Start() {

        //print(GlobalVars.ElementColors[currentElement]);

        sword.GetComponent<SpriteRenderer>().color = GlobalVars.ElementColors[currentElement];
        sword.GetComponent<TrailRenderer>().startColor = GlobalVars.ElementColors[currentElement];

    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        var value = ctx.ReadValue<Vector2>();
        velocity = value * speed;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(velocity.x, velocity.y);

        var mouse_position = Mouse.current.position.ReadValue();
        mouse_position = Camera.main.ScreenToWorldPoint(mouse_position);

        var targetOffset = new Vector2(mouse_position.x - rb.position.x, mouse_position.y - rb.position.y);

        var angle = Mathf.Atan2(targetOffset.y, targetOffset.x) * Mathf.Rad2Deg - 90;

        rb.rotation = angle;

    }

    public void SwingSword(InputAction.CallbackContext ctx)
    {
        if(isAttacking) return;
        isAttacking = true;
        if (Mouse.current.leftButton.isPressed)
        {
            sword_animator.SetTrigger("attack");
            StartCoroutine(AttackCoroutine());
        }
    }

    public void SwordBlock(InputAction.CallbackContext ctx) {
        var val = ctx.performed;

        sword_animator.SetBool("isBlocking", val);
    }

    public IEnumerator<WaitForSeconds> AttackCoroutine() {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    public void changeElementType(InputAction.CallbackContext ctx) {
        var val = ctx.ReadValue<float>();

        //if(val == 0) return;
        if(!canChangeElement) {
            // Print Text to screen warning player they can't change element right now
            return;
        }

        canChangeElement = false;

        if((int)currentElement + val > 5) {
            currentElement = (GlobalVars.ElementType)0;
        } else if((int)currentElement + val < 0) {
            currentElement = (GlobalVars.ElementType)5;
        } else {
            currentElement = (GlobalVars.ElementType)((int)currentElement + val);
        }

        StartCoroutine(ChangeElementTimer()); 

        sword.GetComponent<SpriteRenderer>().color = GlobalVars.ElementColors[currentElement];
        sword.GetComponent<TrailRenderer>().startColor = GlobalVars.ElementColors[currentElement];
    }

    public IEnumerator<WaitForSeconds> ChangeElementTimer() {
        yield return new WaitForSeconds(1f);
        canChangeElement = true;
    }
}
