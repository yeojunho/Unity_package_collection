# Unity_package_collection
유니티에서 사용되는 유용한 패키지를 모아놨습니다.
___
## 1.First_Person_Player_Script
일인칭 플레이어 스크립트이며 wasd로 이동 마우스로 화면전환을 할 수 있고, 컨트롤키로 달릴 수 있습니다.
파라미터는 다음과 같이 있습니다.
- WalkSpeed : 걷기속도
- RunSpeed : 달리기속도
- JumpForce : 점프하는 힘
- LookSensitivity : 카메라 상하감도
- CameraRotationLimit : 카메라 좌우감도

##### 사용방법
1.PlayerController.cs파일을 다운로드한다.<br>
2.땅을 생성한다.<br>
3.플래이어로 쓸 오브젝트를 생성한다.<br>
4.땅과 플래이어에 리지드 바디를 추가한다.<br>
5.에셋 폴더에 PlayerController.cs를 집어넣는다.<br>
6.플래이어에 PlayerController.cs파일을 적용시킨다.<br>
7.카메라를 플래이어의 자식으로 변경한다.<br>
8.카메라의 위치를 플래이어 위로 옴긴다.<br>
9.플래이어의 리지드바디 속성중 Freeze Rotation에서 x와 z를 체크한다.<br>
___
