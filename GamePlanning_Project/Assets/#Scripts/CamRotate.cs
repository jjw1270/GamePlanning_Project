using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    float rx;
    float ry;
    public float rotSpeed = 400;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        if (Input.GetKeyDown (KeyCode.L)) {
			Cursor.lockState = CursorLockMode.Locked;
		}

        float mx = Input.GetAxis("Mouse X"); //����â����?���콺��?����?����������?�̵��Ҷ�?����?(��?-����?:?����?+���)
        float my = Input.GetAxis("Mouse Y"); //����â����?���콺��?����?����������?�̵��Ҷ�?����?(�Ʒ�?-����?:?��?+���)

        rx += rotSpeed * my * Time.deltaTime;
        ry += rotSpeed * mx * Time.deltaTime;

        //rx?ȸ��?����?����?(ȭ��?������?���콺��?��������?x��?ȸ��?����?�ϵ�?���?����?����?����)
        rx = Mathf.Clamp(rx, -45, 60);
        //x��?������?����?x����?�̵���?�ƴ϶�?x����?ȸ��?�ؼ�?���Ʒ�?����?������?x���̿���?�Ѵ�.

        //2.?ȸ����?�Ѵ�.
        transform.eulerAngles = new Vector3(-rx, ry, 0);
        //X����?ȸ����?�����?�����Ǹ�?�Ʒ�,?������?�����Ǹ�?����?���ư���.?(�׷���?x����?-��?�־���)
    }
}
