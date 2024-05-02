using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeremyController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    
    [SerializeField] 
    private float movementSpeed;

    /// <summary>
    /// These values cap the max velocity the player can move, it is to prevent him from stacking up force which
    /// will make his velocity huge and boost him to infinity (Well not literally).
    ///
    /// Choosing 1 means the player can move in the direction again only if his velocity is less than what he gets
    /// immediately after one move. (Basically allows him to move twice in one direction before having to wait).
    ///
    /// If you set new values, you have to restart the game as thr/thl/thu (which are thresholds in the velocity language)
    /// are calculated at Start().
    /// </summary>
    [SerializeField] private float thresholdRightMove;
    [SerializeField] private float thresholdLeftMove;
    [SerializeField] private float thresholdUpMove;
    private float thr;
    private float thl;
    private float thu;
    private float upMult = 1.4f; // Because we're using RB2D, and we need to fight gravity to make
                                 // controls tighter we multiply the upwards movement

    private bool keyPressed;
    private Vector2 normalizedInput;

    private MoveController moveController;

    private Transform puzzleRespawn;

    private void Start()
    {
        moveController = GameObject.Find("MoveController").GetComponent<MoveController>();
        puzzleRespawn = GameObject.Find("PuzzleRespawn").transform;
        thr = movementSpeed * Time.fixedDeltaTime * thresholdRightMove;
        thl = movementSpeed * Time.fixedDeltaTime * -thresholdLeftMove;
        thu = movementSpeed * Time.fixedDeltaTime * thresholdUpMove;
    }
    
    void Update()
    {
        Vector2 normalizedInputTemp = GetInputNormalized();
        if (normalizedInputTemp == Vector2.zero) return;
        if (moveController.Active && !moveController.DecreaseMoves())
        {
            Reset();
            return;
        }
        keyPressed = true;
        normalizedInput = normalizedInputTemp;
    }

    private void FixedUpdate()
    {
        if (!keyPressed) return;
        Move();
        keyPressed = false;
    }

    Vector2 GetInputNormalized()
    {
        var input = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.D) && rb.velocity.x <= thr) { input += Vector2.right; }
        if (Input.GetKeyDown(KeyCode.A) && rb.velocity.x >= thl) { input += Vector2.left; }
        if (Input.GetKeyDown(KeyCode.W) && rb.velocity.y <= thu) { input += upMult*Vector2.up; }
        return input;
    }

    void Move()
    {
        rb.AddForce(normalizedInput * movementSpeed);
    }

    void Reset()
    {
        rb.position= puzzleRespawn.position;
        rb.velocity = Vector2.zero;
        moveController.ResetMoves();
    }
}
