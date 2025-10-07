using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionReference Primary;
    public InputActionReference Move;
    public float ManueveringSpeed = 0.1f;
    public GameObject TreatPrefab;
    public float PrimaryDelay = 1.0f;
    private bool primaryDown_ = false;
    private float primaryNextTime_ = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        primaryDown_ = false;
        primaryNextTime_ = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = Move.action.ReadValue<Vector2>();
        transform.position += new Vector3(
            move.x * ManueveringSpeed * Time.deltaTime, 0.0f, move.y * ManueveringSpeed * Time.deltaTime);

        if (primaryDown_ == true && Time.time >= primaryNextTime_)
        {
            //Asked ChatGPT what Quaternion.identity is and asked what . shortcuts there were to allow me customize the visual rotation.
            //It advised me to use.Euler which helps me angle my object how I want it.
            Instantiate(TreatPrefab, transform.position, Quaternion.Euler(0,90,90));
            primaryNextTime_ = Time.time + PrimaryDelay; 
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


    private void OnEnable()
    {
        Primary.action.performed += OnPrimaryDown;
        Primary.action.canceled += OnPrimaryUp;
    }

    private void OnDisable()
    {
        Primary.action.performed -= OnPrimaryDown;
        Primary.action.canceled -= OnPrimaryUp;
    }
}
