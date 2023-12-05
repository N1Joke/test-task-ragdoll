using RootMotion.Demos;
using UnityEngine;

public class UserControlThirdPersonTouch : UserControlThirdPerson
{
    [HideInInspector]
    public Joystick joystick;

    protected override void Start()
    {
        // get the transform of the main camera
        cam = Camera.main.transform;
        state.crouch = false;
    }

    protected override void Update()
    {
        float h = 0f;
        float v = 0f;
        if (joystick != null)
        {
            h = joystick.Horizontal;
            v = joystick.Vertical;
        }
        // calculate move direction
        Vector3 move = cam.rotation * new Vector3(h, 0f, v).normalized;

        // Flatten move vector to the character.up plane
        if (move != Vector3.zero)
        {
            Vector3 normal = transform.up;
            Vector3.OrthoNormalize(ref normal, ref move);
            state.move = move;
        }
        else state.move = Vector3.zero;

        bool walkToggle = Input.GetKey(KeyCode.LeftShift);

        // We select appropriate speed based on whether we're walking by default, and whether the walk/run toggle button is pressed:
        float walkMultiplier = (walkByDefault ? walkToggle ? 1 : 0.5f : walkToggle ? 0.5f : 1);

        state.move *= walkMultiplier;

        // calculate the head look target position
        state.lookPos = transform.position + cam.forward * 100f;
    }

    public void DoJump()
    {
        state.jump = canJump;
    }
}
