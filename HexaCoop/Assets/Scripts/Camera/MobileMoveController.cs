using UnityEngine;

public class MobileMoveController : HexaEventCallback {

    // PUBLIC
    public SimpleTouchController leftController;
    public SimpleTouchController rightController;
    public Transform headTrans;
    private float speedMovements = 100f;
    private float speedProgressiveLook = 400f;

    // PRIVATE
    [ComponentInject] private Rigidbody rigidbody;
    [SerializeField] bool continuousRightController = true;

    new void Awake()
    {
        base.Awake();
        rightController.TouchEvent += RightController_TouchEvent;
    }  

    public bool ContinuousRightController
    {
        set{continuousRightController = value;}
    }

    void RightController_TouchEvent (Vector2 value)
    {
        if(!continuousRightController)
        {
            UpdateAim(value);
        }
    }

    void Update()
    {
        if(leftController.GetTouchPosition.IsEmpty() && rightController.GetTouchPosition.IsEmpty())
        {
            return;
        }

        // move
        rigidbody.MovePosition(transform.position + (transform.forward * leftController.GetTouchPosition.y * Time.deltaTime * speedMovements) +
            (transform.right * leftController.GetTouchPosition.x * Time.deltaTime * speedMovements) );

        if(continuousRightController)
        {
            UpdateAim(rightController.GetTouchPosition);
        }
    }

    void UpdateAim(Vector2 value)
    {
        if(headTrans != null)
        {
            Quaternion rot = Quaternion.Euler(0f,
                transform.localEulerAngles.y - value.x * Time.deltaTime * -speedProgressiveLook,
                0f);

            rigidbody.MoveRotation(rot);

            rot = Quaternion.Euler(headTrans.localEulerAngles.x - value.y * Time.deltaTime * speedProgressiveLook,
                0f,
                0f);
            headTrans.localRotation = rot;

        }
        else
        {

            Quaternion rot = Quaternion.Euler(transform.localEulerAngles.x - value.y * Time.deltaTime * speedProgressiveLook,
                transform.localEulerAngles.y + value.x * Time.deltaTime * speedProgressiveLook,
                0f);

            rigidbody.MoveRotation(rot);
        }
    }

    void OnDestroy()
    {
        rightController.TouchEvent -= RightController_TouchEvent;
    }

}
