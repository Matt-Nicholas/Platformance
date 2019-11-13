using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager {
  //static float deadzone = 0.1f;

  // -- Axis --
  public static float MainHorizontal(int playerNumber) {
    if(playerNumber != 1 && playerNumber != 2) return 0;
    float r = 0.0f;
    r += Input.GetAxisRaw("J_MainHorizontal" + playerNumber);
    //r += Input.GetAxis("K_MainHorizontal" + playerNumber);
    return Mathf.Clamp(r, -1.0f, 1.0f);

  }
  public static float MainVertical(int playerNumber) {
    if(playerNumber != 1 && playerNumber != 2) return 0;
    float r = 0.0f;
    r += Input.GetAxisRaw("J_MainVertical" + playerNumber);
    //r += Input.GetAxis("K_MainVertical" + playerNumber);
    return Mathf.Clamp(r, -1.0f, 1.0f);
  }
  //public static Vector3 MainJoystick(int playerNumber) {
  //  return new Vector3(MainHorizontal(playerNumber), 0.0f, MainVertical());
  //}

  // -- Joysticks As Buttons --
  //public static bool MainJoystickUp() {
  //  return MainVertical() > deadzone;
  //}
  //public static bool MainJoystickDown() {
  //  return MainVertical() < -deadzone;
  //}

  // A Button
  public static bool AButton(int playerNumber) {
    return Input.GetButton("A_Button" + playerNumber);
  }
  public static bool AButtonDown() {
    return Input.GetButtonDown("A_Button" + 1);
  }
  public static bool AButtonDown(int playerNumber) {
    
      return Input.GetButtonDown("A_Button" + playerNumber);
  }
  public static bool AButtonUp(int playerNumber) {
      return Input.GetButtonUp("A_Button" + playerNumber);
  }
  // B Button
  public static bool BButton(int playerNumber) {
    return Input.GetButton("B_Button" + playerNumber);
  }
  public static bool BButtonDown(int playerNumber) {
    return Input.GetButtonDown("B_Button" + playerNumber);
  }
  public static bool BButtonUp(int playerNumber) {
    return Input.GetButtonUp("B_Button" + playerNumber);
  }
  // X Button
  public static bool XButton(int playerNumber) {
    return Input.GetButton("X_Button" + playerNumber);
  }
  public static bool XButtonDown(int playerNumber) {
    return Input.GetButtonDown("X_Button" + playerNumber);
  }
  public static bool XButtonUp(int playerNumber) {
    return Input.GetButtonUp("X_Button" + playerNumber);
  }
  // Y Button
  public static bool YButton(int playerNumber) {
    return Input.GetButton("Y_Button" + playerNumber);
  }
  public static bool YButtonDown(int playerNumber) {
    return Input.GetButtonDown("Y_Button" + playerNumber);
  }
  public static bool YButtonUp(int playerNumber) {
    return Input.GetButtonUp("Y_Button" + playerNumber);
  }

  // Right Bumber
  public static bool RightBumper(int playerNumber) {
    return Input.GetButton("Right_Bumper" + playerNumber);
  }
  public static bool RightBumperDown(int playerNumber) {
    return Input.GetButtonDown("Right_Bumper" + playerNumber);
  }
  public static bool RightBumperUp(int playerNumber) {
    return Input.GetButtonUp("Right_Bumper" + playerNumber);
  }


  // Start
  public static bool StartButton(int playerNumber) {
    return Input.GetButtonDown("Start_Button" + playerNumber);
  }
  // Select
  public static bool SelectButton(int playerNumber) {
    return Input.GetButtonDown("Select_Button" + playerNumber);
  }

}
