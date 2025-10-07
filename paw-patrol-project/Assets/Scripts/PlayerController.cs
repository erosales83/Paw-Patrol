using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionReference Primary;
    public InputActionReference Secondary;
    public InputActionReference Move;
    public float ManueveringSpeed = 0.1f;
    public GameObject TreatPrefab;
    public float PrimaryDelay = 1.0f;
    public float SecondaryDelay = 0.5f;
    private bool primaryDown_ = false;
    private bool secondaryDown_ = false;
    private float primaryNextTime_ = 0.0f;
    private float secondaryNextTime_ = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        primaryDown_ = false;
        secondaryDown_ = false;
        primaryNextTime_ = 0.0f;
        secondaryNextTime_ = 0.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = Move.action.ReadValue<Vector2>();
        transform.position += new Vector3(
            move.x * ManueveringSpeed * Time.deltaTime, 0.0f, move.y * ManueveringSpeed * Time.deltaTime);

        if (primaryDown_ == true && Time.time >= primaryNextTime_ || secondaryDown_ == true && Time.time >= secondaryNextTime_)
        {
            //Asked ChatGPT what Quaternion.identity is and asked what . shortcuts there were to allow me customize the visual rotation.
            //It advised me to use.Euler which helps me angle my object how I want it.
            Instantiate(TreatPrefab, transform.position, Quaternion.Euler(0, 90, 90));
            primaryNextTime_ = Time.time + PrimaryDelay;
            secondaryNextTime_ = Time.time + SecondaryDelay;
        }
    }

    public void OnPrimaryDown(InputAction.CallbackContext context)
    {
        primaryDown_ = true;
    }

    public void OnPrimaryUp(InputAction.CallbackContext context)
    {
        primaryDown_ = false;
    }

    public void OnSecondaryDown(InputAction.CallbackContext context)
    {
        secondaryDown_ = true;
    }

    public void OnSecondaryUp(InputAction.CallbackContext context)
    {
        secondaryDown_= false;
    }


    private void OnEnable()
    {
        Primary.action.performed += OnPrimaryDown;
        Primary.action.canceled += OnPrimaryUp;
        Secondary.action.performed += OnSecondaryDown;
        Secondary.action.canceled += OnSecondaryUp;
    }

    private void OnDisable()
    {
        Primary.action.performed -= OnPrimaryDown;
        Primary.action.canceled -= OnPrimaryUp;
        Secondary.action.performed -= OnSecondaryDown;
        Secondary.action.canceled -= OnSecondaryUp;
    }
}
