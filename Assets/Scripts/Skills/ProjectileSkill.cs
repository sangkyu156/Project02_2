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

    //������ ���� �ɷ�ġ ����
    abstract public void SetAbility();

    //���������� �߻�
    abstract public void RightShoot();

    //�������� �߻�
    abstract public void LeftShoot();

    //������Ʈ ��Ȱ��ȭ
    abstract public void OnTargetReached();
}
