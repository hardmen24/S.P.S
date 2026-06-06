using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor : MonoBehaviour
{
       [Header("Layer")]
    [Tooltip("Objects on this layer can be grabbed or pushed.")]
    [SerializeField] private LayerMask interactLayer;
 
    [Header("Push collider")]
    [Tooltip("Radius of the invisible push circle (world units).")]
    [SerializeField] private float pushRadius = 0.15f;
 
    [Header("Grab settings")]
    [Tooltip("How strongly the grabbed object follows the mouse. Higher = snappier.")]
    [SerializeField] private float grabStiffness = 20f;
 
    [Tooltip("Multiplier applied to mouse velocity when throwing on release.")]
    [SerializeField] private float throwScale = 1f;
 
    // ── private state ──────────────────────────────────────────────────────
    private Camera       _cam;
    private Rigidbody2D  _pushRb;
    private Collider2D   _pushCol;
 
    private Rigidbody2D  _grabbed;
    private Vector2      _grabOffset;    // world-space offset: click point minus rb.position
 
    private Vector2 _prevMouseWorld;
    private Vector2 _mouseVelocity;
    private bool    _isPushing;
 
    private InputAction _clickAction;
    private InputAction _pointAction;
    
    public Stats stat_script;
    private float timeSinceLastChange = 0f;
    private float changeInterval = 1f; 

    void Update_happiness()
    {
        timeSinceLastChange += Time.deltaTime;
        if (timeSinceLastChange >= changeInterval)
        {
            timeSinceLastChange = 0f;
            stat_script.happiness.Change(0.33433f);
        }
    }




    // ── Unity messages ─────────────────────────────────────────────────────
    private void Awake()
    {
        _cam = Camera.main;
        BuildPushCollider();
 
        _pointAction = new InputAction("MousePosition", binding: "<Mouse>/position");
        _clickAction = new InputAction("LeftClick",     binding: "<Mouse>/leftButton");
 
        _clickAction.started  += _ => OnPress();
        _clickAction.canceled += _ => OnRelease();
 
        _pointAction.Enable();
        _clickAction.Enable();
    }
 
    private void OnDestroy()
    {
        _clickAction.started  -= _ => OnPress();
        _clickAction.canceled -= _ => OnRelease();
        _pointAction.Disable();
        _clickAction.Disable();
    }
 
    private void Update()
    {
        Vector2 mouse      = MouseWorld();
        _mouseVelocity     = (mouse - _prevMouseWorld) / Time.deltaTime;
        _prevMouseWorld    = mouse;
 
        if (_isPushing)
        {
            _pushRb.position = mouse;
            _pushRb.MovePosition(mouse);
            
        }
    }
 
    private void FixedUpdate()
    {
        if (_grabbed == null) return;

        Update_happiness();
        Vector2 target = MouseWorld() - _grabOffset;
        _grabbed.MovePosition(Vector2.Lerp(_grabbed.position, target, grabStiffness * Time.fixedDeltaTime));
    }
 
    // ── input handlers ─────────────────────────────────────────────────────
 
    /// <summary>Called the frame the left mouse button is pressed.</summary>
    private void OnPress()
    {
        Vector2    mouse = MouseWorld();
        Collider2D hit   = Physics2D.OverlapPoint(mouse, interactLayer);
 
        if (hit != null && hit.attachedRigidbody != null)
        {
            // Grab — push collider stays completely inactive
            _grabbed          = hit.attachedRigidbody;
            _grabbed.bodyType = RigidbodyType2D.Kinematic;
            _grabbed.linearVelocity  = Vector2.zero;
 
            // Offset = where on the object we clicked, relative to its pivot
            _grabOffset = mouse - _grabbed.position;
        }
        else
        {
            // Empty space — push collider appears FAR from any object, then moves
            // We teleport it off-screen first so it never overlaps anything on spawn
            _pushRb.position = new Vector2(-9999f, -9999f);
            _pushCol.enabled = true;
            _isPushing       = true;
        }
    }
 
    /// <summary>Called the frame the left mouse button is released.</summary>
    private void OnRelease()
    {
        if (_grabbed != null)
        {
            _grabbed.bodyType       = RigidbodyType2D.Dynamic;
            _grabbed.linearVelocity = _mouseVelocity * throwScale;
            _grabbed                = null;
        }
 
        _pushCol.enabled = false;
        _isPushing       = false;
    }
 
    // ── helpers ────────────────────────────────────────────────────────────
 
    private Vector2 MouseWorld()
    {
        Vector2 screen = _pointAction.ReadValue<Vector2>();
        return _cam.ScreenToWorldPoint(screen);
    }
 
    /// <summary>Creates the push collider object at runtime — no prefab needed.</summary>
    private void BuildPushCollider()
    {
        GameObject go = new GameObject("_PushCollider");
        go.tag ="cursor";
        go.transform.SetParent(transform);
        go.layer = gameObject.layer;
 
        _pushRb                       = go.AddComponent<Rigidbody2D>();
        _pushRb.bodyType              = RigidbodyType2D.Kinematic;
        _pushRb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _pushRb.position              = new Vector2(-9999f, -9999f);
 
        _pushCol         = go.AddComponent<CircleCollider2D>();
        ((CircleCollider2D)_pushCol).radius = pushRadius;
        _pushCol.enabled = false;
    }


}