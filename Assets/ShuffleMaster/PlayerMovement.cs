using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : Singleton<PlayerMovement>
{
    [SerializeField] private float forwardSpeed = 6f;
    [SerializeField] private float horizontalSpeed = 4f;
    [SerializeField] private float xConstraint = 4f;

    //Input variables
    //private float vInput;
    private float hInput;
    //private string verticalInputName = "Vertical";
    //private string horizontalInputName = "Horizontal";
    private TouchInput touchInput;

    private Rigidbody rb;
    private bool moving;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        touchInput = GetComponent<TouchInput>();
    }
    private void FixedUpdate()
    {
        if(moving)
            Move();
    }
    void SetMove(bool moving)
    {
        this.moving = moving;
    }
    public float GetForwardSpeed()
    {
        return forwardSpeed;
    }

    void Move()
    {
        Vector3 v = (Vector3.right * touchInput.horizontal * horizontalSpeed + Vector3.forward * forwardSpeed) * Time.deltaTime;
        v += rb.position;
        v.x = Mathf.Clamp(v.x, -xConstraint, xConstraint);
        rb.MovePosition(v);
        
    }
    private void OnStart()
    {
        SetMove(true);
    }
    private void OnGameOver()
    {
        SetMove(false);
    }
    private void OnGameWin()
    {
        SetMove(false);
    }
    private void OnEnable()
    {
        GameManager.OnGameStart += OnStart;
        GameManager.OnGameLose += OnGameOver;
        GameManager.OnGameWin += OnGameWin;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= OnStart;
        GameManager.OnGameLose -= OnGameOver;
        GameManager.OnGameWin -= OnGameWin;
    }
}
