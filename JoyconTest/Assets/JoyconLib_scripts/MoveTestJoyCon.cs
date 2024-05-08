//            if (Input.GetKeyDown(KeyCode.Joystick1Button0)) botton_name = "A";  //A
//            if (Input.GetKeyDown(KeyCode.Joystick1Button2)) botton_name = "B";  //B
//            if (Input.GetKeyDown(KeyCode.Joystick1Button1)) botton_name = "X";  //X
//            if (Input.GetKeyDown(KeyCode.Joystick1Button3)) botton_name = "Y";  //Y

//            if (Input.GetKeyDown(KeyCode.Joystick2Button2)) botton_name = "十字↑";  //十字↑
//            if (Input.GetKeyDown(KeyCode.Joystick2Button3)) botton_name = "十字→";  //十字→
//            if (Input.GetKeyDown(KeyCode.Joystick2Button1)) botton_name = "十字↓";  //十字↓
//            if (Input.GetKeyDown(KeyCode.Joystick2Button0)) botton_name = "十字←";  //十字←

//            if (Input.GetKeyDown(KeyCode.Joystick2Button10)) botton_name = "Lスティック";  //Lスティック
//            if (Input.GetKeyDown(KeyCode.Joystick1Button11)) botton_name = "Rスティック";  //Rスティック

//            if (Input.GetKeyDown(KeyCode.Joystick2Button14)) botton_name = "L";  //L
//            if (Input.GetKeyDown(KeyCode.Joystick1Button14)) botton_name = "R";  //R
//            if (Input.GetKeyDown(KeyCode.Joystick2Button15)) botton_name = "ZL";  //ZL
//            if (Input.GetKeyDown(KeyCode.Joystick1Button15)) botton_name = "ZR";  //ZR

//            if (Input.GetKeyDown(KeyCode.Joystick2Button4)) botton_name = "左SL";  //左SL
//            if (Input.GetKeyDown(KeyCode.Joystick2Button5)) botton_name = "左SR";  //左SR
//            if (Input.GetKeyDown(KeyCode.Joystick1Button5)) botton_name = "右SL";  //右SL
//            if (Input.GetKeyDown(KeyCode.Joystick1Button4)) botton_name = "右SR";  //右SR

//            if (Input.GetKeyDown(KeyCode.Joystick2Button8)) botton_name = "-";  //-
//            if (Input.GetKeyDown(KeyCode.Joystick1Button9)) botton_name = "+";  //+
//            if (Input.GetKeyDown(KeyCode.Joystick1Button12)) botton_name = "HOME";  //HOME
//            if (Input.GetKeyDown(KeyCode.Joystick2Button13)) botton_name = "キャプチャ";  //キャプチャ

//            if (Input.GetKeyDown(KeyCode.Joystick1Button0)) botton_name = "A";  //A
//            if (Input.GetKeyDown(KeyCode.Joystick1Button2)) botton_name = "B";  //B
//            if (Input.GetKeyDown(KeyCode.Joystick1Button1)) botton_name = "X";  //X
//            if (Input.GetKeyDown(KeyCode.Joystick1Button3)) botton_name = "Y";  //Y

//            if (Input.GetKeyDown(KeyCode.Joystick2Button2)) botton_name = "十字↑";  //十字↑
//            if (Input.GetKeyDown(KeyCode.Joystick2Button3)) botton_name = "十字→";  //十字→
//            if (Input.GetKeyDown(KeyCode.Joystick2Button1)) botton_name = "十字↓";  //十字↓
//            if (Input.GetKeyDown(KeyCode.Joystick2Button0)) botton_name = "十字←";  //十字←

//            if (Input.GetKeyDown(KeyCode.Joystick2Button10)) botton_name = "Lスティック";  //Lスティック
//            if (Input.GetKeyDown(KeyCode.Joystick1Button11)) botton_name = "Rスティック";  //Rスティック

//            if (Input.GetKeyDown(KeyCode.Joystick2Button14)) botton_name = "L";  //L
//            if (Input.GetKeyDown(KeyCode.Joystick1Button14)) botton_name = "R";  //R
//            if (Input.GetKeyDown(KeyCode.Joystick2Button15)) botton_name = "ZL";  //ZL
//            if (Input.GetKeyDown(KeyCode.Joystick1Button15)) botton_name = "ZR";  //ZR

//            if (Input.GetKeyDown(KeyCode.Joystick2Button4)) botton_name = "左SL";  //左SL
//            if (Input.GetKeyDown(KeyCode.Joystick2Button5)) botton_name = "左SR";  //左SR
//            if (Input.GetKeyDown(KeyCode.Joystick1Button5)) botton_name = "右SL";  //右SL
//            if (Input.GetKeyDown(KeyCode.Joystick1Button4)) botton_name = "右SR";  //右SR

//            if (Input.GetKeyDown(KeyCode.Joystick2Button8)) botton_name = "-";  //-
//            if (Input.GetKeyDown(KeyCode.Joystick1Button9)) botton_name = "+";  //+
//            if (Input.GetKeyDown(KeyCode.Joystick1Button12)) botton_name = "HOME";  //HOME
//            if (Input.GetKeyDown(KeyCode.Joystick2Button13)) botton_name = "キャプチャ";  //キャプチャ

using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveTestJoyCon : MonoBehaviour
{
    private static readonly Joycon.Button[] m_buttons =
        Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;
    private Joycon.Button? m_pressedButtonL;
    private Joycon.Button? m_pressedButtonR;

    //Transform mypos;
    //Vector3 pos;
    Vector3 spd;
    Vector3 max_spd;
    Vector3 force;

    Rigidbody rb; 

    private void Start()
    {
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);
        m_joyconR = m_joycons.Find(c => !c.isLeft);

        spd = new Vector3(25.0f, 100.0f, 25.0f);
        //max_spd = new Vector3(30.0f, 30.0f, 30.0f);
        rb = this.GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.81f / 100.0f, 0);
    }

    private void FixedUpdate()
    {
        m_pressedButtonL = null;
        m_pressedButtonR = null;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        foreach (var button in m_buttons)
        {
            if (m_joyconL.GetButton(button))
            {
                m_pressedButtonL = button;
            }
            if (m_joyconR.GetButton(button))
            {
                m_pressedButtonR = button;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_joyconL.SetRumble(160, 320, 0.6f, 200);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            m_joyconR.SetRumble(160, 320, 0.6f, 200);
        }

        // transformを取得
        //mypos = this.transform;
        // 座標を取得
        //pos = mypos.position;

        foreach (var joycon in m_joycons)
        {
            var isLeft = joycon.isLeft;
            //var name = isLeft ? "Joy-Con (L)" : "Joy-Con (R)";
            //var key = isLeft ? "Z キー" : "X キー";
            var button = isLeft ? m_pressedButtonL : m_pressedButtonR;
            var stick = joycon.GetStick();
            //var gyro = joycon.GetGyro();
            var accel = joycon.GetAccel();
            //var orientation = joycon.GetVector();

            //if (isLeft == true && stick[0] != 0)
            //{
            //    pos.z += stick[0] * Time.deltaTime;
            //}
            //if (isLeft == true && stick[1] != 0)
            //{
            //    pos.x -= stick[1] * Time.deltaTime;
            //}

            //if (isLeft == true && accel.x != 0)
            //{
            //    pos.x += spd.x * accel.x * Time.deltaTime;
            //}
            //if (isLeft == true && accel.z != 0)
            //{
            //    pos.y += spd.y * accel.z * Time.deltaTime;
            //}
            //if (isLeft == true && accel.y != 0)
            //{
            //    pos.z -= spd.z * accel.y * Time.deltaTime;
            //}
            //mypos.position = pos;  // 座標を設定


            if (isLeft == true && accel.x != 0)
            {
                // Unity上のx座標を動かす
                force.x = spd.x * accel.x;
            }
            if (isLeft == true && accel.z != 0)
            {
                // Unity上のy座標を動かす
                force.y = spd.y * accel.z;
            }
            if (isLeft == true && accel.y != 0)
            {
                // Unity上のz座標を動かす
                force.z = -spd.z * accel.y;
            }
            //if (Mathf.Sqrt(spd.x * spd.x) > max_spd.x)
            //{
            //    if (spd.x > 0)
            //    {
            //        force.x = max_spd.x;
            //    }
            //    if (spd.x < 0)
            //    {
            //        force.x = -max_spd.x;
            //    }
            //}
            //if (Mathf.Sqrt(spd.z * spd.z) > max_spd.z)
            //{
            //    if (spd.z > 0)
            //    {
            //        force.x = -max_spd.x;
            //    }
            //    if (spd.x < 0)
            //    {
            //        force.x = max_spd.x;
            //    }
            //}
            //rb.AddForce(force);  // 力を加える
        }
    }
}