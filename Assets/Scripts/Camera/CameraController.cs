using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5.0f;

    public GameObject player;

    private void OnEnable()
    {
        // ��������Ʈ ü�� �߰�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // ��������Ʈ ü�� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        try
        {
            Vector3 dir = player.transform.position - this.transform.position;
            if (dir.x > 0)
            {
                Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.fixedDeltaTime, 0, 0.0f);
                this.transform.Translate(moveVector);
            }
        }
        catch (MissingReferenceException e)
        {
            player = GameObject.Find("Player");
            Debug.Log($"{e}������ ���µ� �÷��̾� �ٽ� ã�Ƽ� ����");
        }
    }
}