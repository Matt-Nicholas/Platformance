using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class PlayerController : MonoBehaviour
{
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    public float wallSlideSpeedMax = 10;
    public float wallStickTime = .35f;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public bool PlayerCanMove;
    
    private readonly float _baseMoveSpeed = 10;
    private readonly float _boostMultiplier = 1.9f;

    private readonly float accelerationTimeAirborne = .1f;
    private readonly float accelerationTimeGrounded = .1f;

    [SerializeField] private Controller2D _controller2D;

    private Vector2 directionalInput;
    private float gravity;
    private bool isboosting;
    private bool isInPhysicsVolume;
    private float maxJumpVelocity;
    private float minJumpVelocity;

    private float moveSpeed;
    private Vector2 physicsVolumeVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;
    private int wallDirX;
    private bool wallSliding;
    private float wallUnstickCounter;
    
    private void Start()
    {
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    private void Update()
    {
        if (!PlayerCanMove)
            return;
        
        CalculateVelocity();
        HandleWallSliding();

        if (isInPhysicsVolume) velocity.y = physicsVolumeVelocity.y;

        _controller2D.Move(velocity * Time.deltaTime, directionalInput);

        if (_controller2D.collisions.above || _controller2D.collisions.below)
        {
            if (_controller2D.collisions.slidingDownMaxSlope)
                velocity.y += _controller2D.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            else
                velocity.y = 0;
        }
    }

    public void ResetMovementValues()
    {
        isboosting = false;
        velocity = Vector3.zero;
    }

    public void SetDirectionalInput(Vector2 input, bool isboosting)
    {
        if (!PlayerCanMove)
            return;
        
        moveSpeed = isboosting ? _baseMoveSpeed * _boostMultiplier : _baseMoveSpeed;
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
        if (isInPhysicsVolume)
            return;

        if (wallSliding || ((_controller2D.collisions.left || _controller2D.collisions.right) &&
                            !_controller2D.collisions.below && velocity.y != 0))
        {
            if (wallDirX == directionalInput.x)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
            }
            else if (directionalInput.x == 0)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
            }
            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
            }
        }

        if (_controller2D.collisions.below)
        {
            if (_controller2D.collisions.slidingDownMaxSlope)
            {
                if (directionalInput.x != -Mathf.Sign(_controller2D.collisions.slopeNormal.x))
                {
                    // not jumping against max slope
                    velocity.y = maxJumpVelocity * _controller2D.collisions.slopeNormal.y;
                    velocity.x = maxJumpVelocity * _controller2D.collisions.slopeNormal.x;
                }
            }
            else
            {
                velocity.y = maxJumpVelocity;
            }
        }
    }

    public void OnJumpInputUp()
    {
        if (isInPhysicsVolume)
            return;

        if (velocity.y > minJumpVelocity) velocity.y = minJumpVelocity;
    }

    private bool SameSign(float num1, float num2)
    {
        return (num1 >= 0 && num2 >= 0) || (num1 < 0 && num2 < 0);
    }

    private void HandleWallSliding()
    {
        if (isInPhysicsVolume)
            return;

        wallDirX = _controller2D.collisions.left ? -1 : 1;
        wallSliding = false;
        if ((_controller2D.collisions.left || _controller2D.collisions.right) && !_controller2D.collisions.below &&
            velocity.y < 0)
        {
            wallSliding = true;

            // govern speed when sliding down a wall
            if (velocity.y < -wallSlideSpeedMax) velocity.y = -wallSlideSpeedMax;

            // Don't let the player release from wall immediately in case they hit the joystick before the jump button
            if (wallUnstickCounter > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (Mathf.Sign(directionalInput.x) != Mathf.Sign(wallDirX)) wallUnstickCounter -= Time.deltaTime;
            }
            else
            {
                wallUnstickCounter = wallStickTime;
            }
        }
    }

    public void EnterPhysicsVolume(Vector2 velocity)
    {
        isInPhysicsVolume = true;
        physicsVolumeVelocity = velocity;
    }

    public void ExitPhysicsVolume()
    {
        isInPhysicsVolume = false;
    }

    private void CalculateVelocity()
    {
        var targetVelocityX = directionalInput.x * moveSpeed;

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
            _controller2D.collisions.below ? accelerationTimeGrounded : accelerationTimeAirborne);

        velocity.y += gravity * Time.deltaTime;
    }
}