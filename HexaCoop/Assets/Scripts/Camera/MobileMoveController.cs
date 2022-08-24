using UnityEngine;

public class MobileMoveController : MonoBehaviour {

    // PUBLIC
    public SimpleTouchController leftController;
    public SimpleTouchController rightController;
    public Transform headTrans;

    private float speedMovements = 40f;
    private float speedProgressiveLook = 120f;
    private new Rigidbody rigidbody;

    void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody>();
    }  

    void FixedUpdate()
    {
        if (leftController.GetTouchPosition.IsEmpty() && rightController.GetTouchPosition.IsEmpty())
        {
            return;
        }

        UpdateMove(leftController.GetTouchPosition);
        UpdateAim(rightController.GetTouchPosition);

    }

    private void UpdateMove(Vector2 lTouchPosition)
    {
        // move
        var origPosRB = rigidbody.transform.position;
        rigidbody.MovePosition(transform.position + (transform.forward * lTouchPosition.y * Time.fixedDeltaTime * speedMovements) +
            (transform.right * lTouchPosition.x * Time.fixedDeltaTime * speedMovements));
        rigidbody.position = new Vector3(rigidbody.position.x, origPosRB.y, rigidbody.position.z);
    }

    void UpdateAim(Vector2 rTouchPosition)
    {
        if(headTrans != null)
        {
            Quaternion rot = Quaternion.Euler(0f,
                transform.localEulerAngles.y - rTouchPosition.x * Time.fixedDeltaTime * -speedProgressiveLook,
                0f);

            rigidbody.MoveRotation(rot);

            rot = Quaternion.Euler(headTrans.localEulerAngles.x - rTouchPosition.y * Time.fixedDeltaTime * speedProgressiveLook,
                0f,
                0f);
            headTrans.localRotation = rot;

        }
        else
        {

            Quaternion rot = Quaternion.Euler(transform.localEulerAngles.x - rTouchPosition.y * Time.fixedDeltaTime * speedProgressiveLook,
                transform.localEulerAngles.y + rTouchPosition.x * Time.fixedDeltaTime * speedProgressiveLook,
                0f);

            rigidbody.MoveRotation(rot);
        }
    }
}
