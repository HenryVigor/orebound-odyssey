using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///     <see cref="MonoBehaviour"/> singleton class for top-down 2D player
///     movement
/// </summary>
public class PlayerMovement : BehaviourFSM {
    static PlayerMovement _Instance;
    /// <summary>Singleton <see cref="PlayerMovement"/> instance</summary>
    /// <value>Set only if null</value>
    static PlayerMovement Instance {
        get => _Instance;
        set => _Instance ??= value;
    }
    
    /// External Speed Modifier
    /// <summary>
    ///     Serialized modifier for speed and acceleration calculations
    /// </summary>
    [SerializeField] float ExternalSpeedModifier;
    
    /// <summary>Lock status for movement input</summary>
    bool InputLocked = false;
    
    /// <summary>External control override status</summary>
    bool ExternalOverride = false;
    
    Rigidbody2D rb;
    PlayerInput pi;

    /// Player Animation Controllers
    public Animator playerAnimator;
    public Animator toolAnimator;
    
    // For player attack/mine range
    GameObject rangeObject;
    GameObject interactObject;

    private void Start() 
    {
        Transform toolAnimatorObject = transform.Find("Tool");
        if (toolAnimatorObject != null)
        {
        toolAnimator = toolAnimatorObject.GetComponent<Animator>();
        Debug.Log("FOUND");
        }
        else
        {
            Debug.LogError("oops");
        }
    }

    void Awake() {
        Instance = this;
        if (Instance == this) {
            // Get components and values
            rb = GetComponent<Rigidbody2D>();
            pi = GetComponent<PlayerInput>();
            rangeObject = GameObject.Find("PlayerRange");
            interactObject = GameObject.Find("InteractRange");

            if (ExternalSpeedModifier <= 0f) ExternalSpeedModifier = 1f;

            // Get animation components
            playerAnimator = GetComponent<Animator>();
            
            // Set starting state
            SetState(typeof(InputMovement));
        }
    }
    
    void OnDestroy() {
        _Instance = null;
    }
    
    /// Main Movement State
    /// <summary>
    ///     Main player movement state. Accepts input and calculates player
    ///     velocity accordingly
    /// </summary>
    class InputMovement : State {
        void Update() {
            // Get input
            int xDir = GetXAxisInput();
            int yDir = GetYAxisInput();

            Instance.toolAnimator.SetFloat("Horizontal Direction", xDir);
            Instance.toolAnimator.SetFloat("Vertical Direction", yDir);

            // Determine animation values
            if (xDir == 0) {
                Instance.playerAnimator.SetBool("Is Horizontal Zero", true);
                Instance.toolAnimator.SetBool("Is Horizontal Zero", true);
            } else {
                Instance.playerAnimator.SetBool("Is Horizontal Zero", false);
                Instance.toolAnimator.SetBool("Is Horizontal Zero", false);
                Instance.playerAnimator.SetFloat("Horizontal Speed", xDir);
            }
            if (yDir == 0) {
                Instance.playerAnimator.SetBool("Is Vertical Zero", true);
            } else {
                Instance.playerAnimator.SetBool("Is Vertical Zero", false);
                Instance.playerAnimator.SetFloat("Vertical Speed", yDir);
            }

            // Determine top and target velocities
            float topSpeed = GetTopSpeed(xDir, yDir);
            topSpeed *= Instance.ExternalSpeedModifier;  // For testing
            float xTarget = topSpeed * xDir;
            float yTarget = topSpeed * yDir;
            
            // Determine acceleration values
            float baseAcc = GetBaseAcceleration();
            baseAcc *= Instance.ExternalSpeedModifier;  // For testing
            float xAcc = baseAcc * Mathf.Sign(xTarget - Instance.rb.velocity.x);
            float yAcc = baseAcc * Mathf.Sign(yTarget - Instance.rb.velocity.y);
            
            // Set player velocity
            SetPlayerVelocity(xAcc, xTarget, yAcc, yTarget);

            // Set PlayerRange Rotation --- Bad temporary code, should improve on this later
            if (xDir != 0)
            {
                Instance.rangeObject.transform.SetLocalPositionAndRotation(new Vector3(0.65f*xDir, -0.12f, 0f), Quaternion.Euler(0f, 0f, 0f));
                Instance.interactObject.transform.SetLocalPositionAndRotation(new Vector3(0.65f * xDir, -0.12f, 0f), Quaternion.Euler(0f, 0f, 0f));
            }
            if (yDir != 0)
            {
                Instance.rangeObject.transform.SetLocalPositionAndRotation(new Vector3(0f, 0.5f*yDir, 0f), Quaternion.Euler(0f, 0f, 90f));
                Instance.interactObject.transform.SetLocalPositionAndRotation(new Vector3(0f, 0.5f * yDir, 0f), Quaternion.Euler(0f, 0f, 90f));
            }


        }
        
        protected override Type Transitions() {
            if (IsOverridden()) return typeof(DoNothing);
            else if (IsLocked()) return typeof(Decelerate);
            return null;
        }
    }
    
    /// Decelerate (Locked)
    /// <summary>
    ///     Locked player movement state. Decelerates player velocity to zeros
    /// </summary>
    class Decelerate : State {
        void Update() {
            // Determine acceleration values
            float baseAcc = GetBaseAcceleration();
            baseAcc *= Instance.ExternalSpeedModifier;  // For testing
            float xAcc = baseAcc * Mathf.Sign(-Instance.rb.velocity.x);
            float yAcc = baseAcc * Mathf.Sign(-Instance.rb.velocity.y);
            
            // Decelerate player velocity toward zero
            SetPlayerVelocity(xAcc, 0f, yAcc, 0f);
        }
        
        protected override Type Transitions() {
            if (IsOverridden()) return typeof(DoNothing);
            else if (!IsLocked()) return typeof(InputMovement);
            return null;
        }
    }
    
    /// Do Nothing (External Override)
    /// <summary>
    ///     Empty state used when player movement is externally overridden
    /// </summary>
    class DoNothing : State {
        protected override Type Transitions() {
            if (!IsOverridden()) {
                if (IsLocked()) return typeof(Decelerate);
                return typeof(InputMovement);
            }
            return null;
        }
    }
    
    /// <summary>Locks / unlocks input for player movement</summary>
    /// <param name="status">
    ///     If true, input is locked<br/>
    ///     If false, input is unlocked
    /// </param>
    public static void Lock(bool status = true) {
        Instance.InputLocked = status;
    }
    
    /// External Override Toggle
    /// <summary>
    ///     Prepares or ends external movement override for scripted movement
    /// </summary>
    /// <param name="status">
    ///     If true, stops accepting input and performing movement calculations
    ///     <br/>If false, returns to locked or unlocked movement state
    /// </param>
    /// <returns>Current player velocity</returns>
    public static Vector2 Override(bool status = true) {
        Instance.ExternalOverride = status;
        return Instance.rb.velocity;
    }
    
    /// Locked Movement Check
    /// <summary>
    ///     Gets whether player movement is in locked state
    /// </summary>
    /// <returns><see cref="Instance.InputLocked"/></returns>
    public static bool IsLocked() {
        return Instance.InputLocked;
    }
    
    /// Overridden Movement Check
    /// <summary>
    ///     Gets whether player movement is being externally overridden
    /// </summary>
    /// <returns><see cref="Instance.ExternalControl"/></returns>
    public static bool IsOverridden() {
        return Instance.ExternalOverride;
    }
    
    /// <summary>Gets movement input for the X axis</summary>
    /// <returns>-1 for left, 0 for still, and 1 for right</returns>
    static int GetXAxisInput() {
        int value = 0;
        if (Instance.pi.actions["Move Left"].IsPressed()) value--;
        if (Instance.pi.actions["Move Right"].IsPressed()) value++;
        return value;
    }
    
    /// <summary>Gets movement input for the Y axis</summary>
    /// <returns>-1 for down, 0 for still, and 1 for up</returns>
    static int GetYAxisInput() {
        int value = 0;
        if (Instance.pi.actions["Move Up"].IsPressed()) value++;
        if (Instance.pi.actions["Move Down"].IsPressed()) value--;
        return value;
    }
    
    /// <summary>Gets absolute target speed value from input</summary>
    /// <param name="xInput">Directional input along the X axis</param>
    /// <param name="yInput">Directional input along the Y axis</param>
    /// <returns>Current top speed value</returns>
    static float GetTopSpeed(int xInput, int yInput) {
        float value = 7f;
        
        // Adjust top speed when moving diagonally
        if (xInput != 0 && yInput != 0) value *= 0.7071068f;
        return value;
    }
    
    /// <summary>Gets absolute acceleration value</summary>
    /// <returns>Current acceleration value</returns>
    static float GetBaseAcceleration() {
        float value = Time.deltaTime * 84;
        return value;
    }
    
    /// Set Player Velocity
    /// <summary>
    ///     Sets player velocity given target velocity and acceleration values
    /// </summary>
    /// <param name="xa">Acceleration along the X axis</param>
    /// <param name="xt">Target X axis velocity</param>
    /// <param name="ya">Acceleration along the Y axis</param>
    /// <param name="yt">Target Y axis velocity</param>
    static void SetPlayerVelocity(float xa, float xt, float ya, float yt) {
        // Determine X velocity
        float xVelocity = Instance.rb.velocity.x;
        if (Mathf.Abs(xt - xVelocity) < xa) xVelocity = xt;
        else xVelocity += xa;
        
        // Determine Y velocity
        float yVelocity = Instance.rb.velocity.y;
        if (Mathf.Abs(yt - yVelocity) < ya) yVelocity = yt;
        else yVelocity += ya;
        
        // Set player velocity
        Instance.rb.velocity = new(xVelocity, yVelocity);
    }
}