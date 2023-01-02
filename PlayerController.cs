using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float WalkSpeed;  //기본 이동속도
    [SerializeField]
    private float RunSpeed;  //달리기 속도
    private float ApplySpeed;  //속도 대입용
    private bool isRun;  //뛰고 있는가?
    [SerializeField]
    private float JumpForce;
    private bool isGround = true;

    [SerializeField]
    private float LookSensitivity;  //카메라 감도
    [SerializeField]
    private float CameraRotationLimit;  //카메라 각도 한게
    private float CurrentCameraRotationX = 0f;  //카메라 최초 각도

    [SerializeField]
    private Camera Camera;  //카메라 불러오기
    private Rigidbody MyRigid;  //리지드 바디 저장변수 생성
    private CapsuleCollider CapsuleCollider;

    void Start()
    {
        MyRigid = GetComponent<Rigidbody>();  //리지드 바디 불러오기
        CapsuleCollider = GetComponent<CapsuleCollider>();
        ApplySpeed = WalkSpeed;
    }

    void Update()
    {
        IsGround();
        TryRun();
        Move();
        TryJump();
        CameraRotation();
        CharacterRotation();
        Cursor.visible = false;
    }

    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, CapsuleCollider.bounds.extents.y + 0.1f);  //0.1은 오차보정
    }

    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }

    private void Jump()
    {
        MyRigid.velocity = transform.up * JumpForce;
    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            RunningCancel();
        }
    }

    private void Running()
    {
        isRun = true;
        ApplySpeed = RunSpeed;
    }

    private void RunningCancel()
    {
        isRun = false;
        ApplySpeed = WalkSpeed;
    }

    private void Move()  //이동
    {
        float MoveDirX = Input.GetAxisRaw("Horizontal");  //좌우
        float MoveDirZ = Input.GetAxisRaw("Vertical");  //전후

        Vector3 MoveHorizontal = transform.right * MoveDirX;
        Vector3 MoveVertical = transform.forward * MoveDirZ;

        Vector3 Velocity = (MoveHorizontal + MoveVertical).normalized * ApplySpeed;
        MyRigid.MovePosition(transform.position + Velocity * Time.deltaTime);
    }

    private void CameraRotation()  //카메라 상하이동
    {
        float XRotation = Input.GetAxisRaw("Mouse Y");  //마우스 상하이동 감지
        float CameraRotationX = XRotation * LookSensitivity;  //감도에 따라 마우스 이동 저장
        CurrentCameraRotationX -= CameraRotationX;  //카메라 움직이기(-를 +로 바꾸면 Y축 반전
        CameraRotationX = Mathf.Clamp(CameraRotationX, -CameraRotationLimit, CameraRotationLimit);  //카메라 각도 한게 적용

        Camera.transform.localEulerAngles = new Vector3(CurrentCameraRotationX, 0f, 0f);  //백터 적용
    }

    private void CharacterRotation()  //캐릭터 좌우이동
    {
        float YRotation = Input.GetAxisRaw("Mouse X");  //마우스 좌우이동 감지
        Vector3 CharacterRotationY = new Vector3(0f, YRotation, 0f) * LookSensitivity;  //감도에 따라 마우스 이동 저장
        MyRigid.MoveRotation(MyRigid.rotation * Quaternion.Euler(CharacterRotationY));  //값을 사원수로 변환 후 저장
    }
}