using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class PlayerController:MonoBehaviour {

  //public Color mColor;

  public float maxJumpHeight = 4;
  public float minJumpHeight = 1;
  public float timeToJumpApex = .4f;
  public float wallSlideSpeedMax = 6;
  public float wallStickTime = .25f;

  public Vector2 wallJumpClimb;
  public Vector2 wallJumpOff;
  public Vector2 wallLeap;

  private float accelerationTimeAirborne = .1f;
  private float accelerationTimeGrounded = .1f;
  private const float BaseMoveSpeed = 6;
  private float moveSpeed = 6;

  private float timeToWallUnstick = 1;
  private float gravity;
  private float maxJumpVelocity;
  private float minJumpVelocity;
  private Vector3 velocity;
  private float velocityXSmoothing;

  private Controller2D controller;

  private Vector2 directionalInput;
  private bool wallSliding;
  private int wallDirX;
  private bool isboosting;

  void Start() {
    
    controller = GetComponent<Controller2D>();
    
    gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
    maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

    foreach(string s in Input.GetJoystickNames()) {
      Debug.Log(s);

    }
  }

  void Update() {
    CalculateVelocity();
    HandleWallSliding();
    
    controller.Move(velocity * Time.deltaTime, directionalInput);

    if(controller.collisions.above || controller.collisions.below) {
      if(controller.collisions.slidingDownMaxSlope) {
        velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
      }
      else {
        velocity.y = 0;
      }
    }


  }

  public void SetDirectionalInput(Vector2 input, bool isboosting) {

    moveSpeed = (isboosting) ? BaseMoveSpeed * 2 : BaseMoveSpeed;

    directionalInput = input;
    
  }

  public void OnJumpInputDown() {

    //print("(x: " + directionalInput.x + ")  (y: " + directionalInput.y + ")");

    if(wallSliding || ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y != 0)) {
      if(wallDirX == directionalInput.x) {
        velocity.x = -wallDirX * wallJumpClimb.x;
        velocity.y = wallJumpClimb.y;
      }
      else if(directionalInput.x == 0) {
        velocity.x = -wallDirX * wallJumpOff.x;
        velocity.y = wallJumpOff.y;
      }
      else {
        velocity.x = -wallDirX * wallLeap.x;
        velocity.y = wallLeap.y;
      }
    }
    if(controller.collisions.below) {
      if(controller.collisions.slidingDownMaxSlope) {
        if(directionalInput.x != -Mathf.Sign(controller.collisions.slopeNormal.x)) { // not jumping against max slope
          velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
          velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
        }
      }
      else {
        velocity.y = maxJumpVelocity;
      }
    }
  }

  public void OnJumpInputUp() {
    if(velocity.y > minJumpVelocity) {
      velocity.y = minJumpVelocity;
    }
  }

  bool SameSign(float num1, float num2) {
    return num1 >= 0 && num2 >= 0 || num1 < 0 && num2 < 0;
  }

  void HandleWallSliding() {
    wallDirX = (controller.collisions.left) ? -1 : 1;
    wallSliding = false;
    if((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
      wallSliding = true;

      // govern speed when sliding down a wall
      if(velocity.y < -wallSlideSpeedMax) {
        velocity.y = -wallSlideSpeedMax;
      }

      // Don't let the player release from wall immediately in case they hit the joystick before the jump button
      if(timeToWallUnstick > 0) {
        velocityXSmoothing = 0;
        velocity.x = 0;
        
        if(Mathf.Sign(directionalInput.x) != Mathf.Sign(wallDirX)) {
          //if(directionalInput.x != wallDirX && directionalInput.x != 0) {
            //timeToWallUnstick = 0;
            timeToWallUnstick -= Time.deltaTime;
        }
        //else {
        //  timeToWallUnstick = wallStickTime;
        //}
      }
      else {
        timeToWallUnstick = wallStickTime;
      }
    }
  }

  void CalculateVelocity() {
    float targetVelocityX = directionalInput.x * moveSpeed;
    velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
    velocity.y += gravity * Time.deltaTime;
  }
}
