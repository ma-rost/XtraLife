using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    private bool grounded = false;
    /// <summary>
    ///     Is the object grounded.
    /// </summary>
    public bool Grounded
    {
        get => grounded;
        private set
        {
            bool oldBool = grounded;
            grounded = value;
            if (oldBool != grounded) onGroundStateChange?.Invoke(grounded);
        }
    }

    private RaycastHit2D[] results = new RaycastHit2D[4];

    public event Action<bool> onGroundStateChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ContactFilter2D cf = new ContactFilter2D();
        cf.SetLayerMask(groundLayer);

        int ray = Physics2D.Raycast(groundCheck.position, Vector2.down, cf, results, 0.7f);
        if (ray == -1) return;

        Grounded = ray > 0;
    }
}
