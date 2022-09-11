using UnityEngine;

public class MobileMoveController : MonoBehaviour {

    // PUBLIC
    public SimpleTouchController leftController;
    public SimpleTouchController rightController;
    public Transform headTrans;

    private float speedMovements = 30f;
    private float speedProgressiveLook = 100f;
    private Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }  

    void FixedUpdate()
    {
        if (leftController.GetTouchPosition.IsEmpty() && rightController.GetTouchPosition.IsEmpty())
        {
            return;
        }

        UpdateMove(leftController.GetTouchPosition, rightController.GetTouchPosition);
        //UpdateAim(rightController.GetTouchPosition);

    }

    private void UpdateMove(Vector2 lTouchPosition, Vector2 rTouchPosition)
    {
        // move
        var origPosRB = _rigidbody.transform.position;

        // left controller
        _rigidbody.MovePosition(transform.position + (transform.forward * lTouchPosition.y * Time.fixedDeltaTime * speedMovements) +
            (transform.right * lTouchPosition.x * Time.fixedDeltaTime * speedMovements));

        var resx = _rigidbody.position.x;
        var resz = _rigidbody.position.z;

        _rigidbody.transform.position = origPosRB;

        // Right controller
        _rigidbody.MovePosition(transform.position + (transform.forward * rTouchPosition.y * Time.fixedDeltaTime * speedMovements) +
            (transform.right * rTouchPosition.x * Time.fixedDeltaTime * speedMovements));


        // combined result
        _rigidbody.position = new Vector3(resx, _rigidbody.position.y, resz);
    }   

    void UpdateAim(Vector2 rTouchPosition)
    {
        if(headTrans != null)
        {
            Quaternion rot = Quaternion.Euler(0f,
                transform.localEulerAngles.y - rTouchPosition.x * Time.fixedDeltaTime * -speedProgressiveLook,
                0f);

            _rigidbody.MoveRotation(rot);

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

            _rigidbody.MoveRotation(rot);
        }
    }
}
