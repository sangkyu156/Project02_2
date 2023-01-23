using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ProjectileSkill : MonoBehaviour
{
    protected int curPower = 0;
    protected int nextPower = 0;
    protected float nextCooldown = 0;
    protected float deadTiem = 0;
    protected bool first = false;
    protected bool right = false;
    protected bool left = false;

    //레벨에 따라서 능력치 세팅
    abstract public void SetAbility();

    //오른쪽으로 발사
    abstract public void RightShoot();

    //왼쪽으로 발사
    abstract public void LeftShoot();

    //오브젝트 비활성화
    abstract public void OnTargetReached();
}
