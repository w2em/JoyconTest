//            if (Input.GetKeyDown(KeyCode.Joystick1Button0)) botton_name = "A";  //A
//            if (Input.GetKeyDown(KeyCode.Joystick1Button2)) botton_name = "B";  //B
//            if (Input.GetKeyDown(KeyCode.Joystick1Button1)) botton_name = "X";  //X
//            if (Input.GetKeyDown(KeyCode.Joystick1Button3)) botton_name = "Y";  //Y

//            if (Input.GetKeyDown(KeyCode.Joystick2Button2)) botton_name = "�\����";  //�\����
//            if (Input.GetKeyDown(KeyCode.Joystick2Button3)) botton_name = "�\����";  //�\����
//            if (Input.GetKeyDown(KeyCode.Joystick2Button1)) botton_name = "�\����";  //�\����
//            if (Input.GetKeyDown(KeyCode.Joystick2Button0)) botton_name = "�\����";  //�\����

//            if (Input.GetKeyDown(KeyCode.Joystick2Button10)) botton_name = "L�X�e�B�b�N";  //L�X�e�B�b�N
//            if (Input.GetKeyDown(KeyCode.Joystick1Button11)) botton_name = "R�X�e�B�b�N";  //R�X�e�B�b�N

//            if (Input.GetKeyDown(KeyCode.Joystick2Button14)) botton_name = "L";  //L
//            if (Input.GetKeyDown(KeyCode.Joystick1Button14)) botton_name = "R";  //R
//            if (Input.GetKeyDown(KeyCode.Joystick2Button15)) botton_name = "ZL";  //ZL
//            if (Input.GetKeyDown(KeyCode.Joystick1Button15)) botton_name = "ZR";  //ZR

//            if (Input.GetKeyDown(KeyCode.Joystick2Button4)) botton_name = "��SL";  //��SL
//            if (Input.GetKeyDown(KeyCode.Joystick2Button5)) botton_name = "��SR";  //��SR
//            if (Input.GetKeyDown(KeyCode.Joystick1Button5)) botton_name = "�ESL";  //�ESL
//            if (Input.GetKeyDown(KeyCode.Joystick1Button4)) botton_name = "�ESR";  //�ESR

//            if (Input.GetKeyDown(KeyCode.Joystick2Button8)) botton_name = "-";  //-
//            if (Input.GetKeyDown(KeyCode.Joystick1Button9)) botton_name = "+";  //+
//            if (Input.GetKeyDown(KeyCode.Joystick1Button12)) botton_name = "HOME";  //HOME
//            if (Input.GetKeyDown(KeyCode.Joystick2Button13)) botton_name = "�L���v�`��";  //�L���v�`��

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JoyConSample : MonoBehaviour
{
    private static readonly Joycon.Button[] m_buttons =
        Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;
    private Joycon.Button? m_pressedButtonL;
    private Joycon.Button? m_pressedButtonR;

    private void Start()
    {
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);
        m_joyconR = m_joycons.Find(c => !c.isLeft);
    }

    private void Update()
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
    }

    private void OnGUI()
    {
        var style = GUI.skin.GetStyle("label");
        style.fontSize = 24;

        if (m_joycons == null || m_joycons.Count <= 0)
        {
            GUILayout.Label("Joy-Con ���ڑ�����Ă��܂���");
            return;
        }

        if (!m_joycons.Any(c => c.isLeft))
        {
            GUILayout.Label("Joy-Con (L) ���ڑ�����Ă��܂���");
            return;
        }

        if (!m_joycons.Any(c => !c.isLeft))
        {
            GUILayout.Label("Joy-Con (R) ���ڑ�����Ă��܂���");
            return;
        }

        GUILayout.BeginHorizontal(GUILayout.Width(960));

        foreach (var joycon in m_joycons)
        {
            var isLeft = joycon.isLeft;
            var name = isLeft ? "Joy-Con (L)" : "Joy-Con (R)";
            var key = isLeft ? "Z �L�[" : "X �L�[";
            var button = isLeft ? m_pressedButtonL : m_pressedButtonR;
            var stick = joycon.GetStick();
            var gyro = joycon.GetGyro();
            var accel = joycon.GetAccel();
            var orientation = joycon.GetVector();

            var mypos = this.transform.position;

            GUILayout.BeginVertical(GUILayout.Width(480));
            GUILayout.Label(name);
            GUILayout.Label(key + "�F�U��");
            GUILayout.Label("������Ă���{�^���F" + button);
            GUILayout.Label(string.Format("�X�e�B�b�N�F({0}, {1})", stick[0], stick[1]));
            GUILayout.Label("�W���C���F" + gyro);
            GUILayout.Label("�����x�F" + accel);
            GUILayout.Label("�X���F" + orientation);

            GUILayout.Label("x, y, z�F" + mypos);
            GUILayout.EndVertical();
        }

        GUILayout.EndHorizontal();
    }
}