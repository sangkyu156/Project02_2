using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5.0f;

    public GameObject player;

    private static CameraController instance = null;

    private void OnEnable()
    {
        // ��������Ʈ ü�� �߰�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static CameraController Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void OnDisable()
    {
        // ��������Ʈ ü�� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        transform.position = new Vector3(0, 0, -10);
    }

    private void FixedUpdate()
    {
        Vector3 dir = player.transform.position - this.transform.position;
        if (dir.x > 0)
        {
            Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.fixedDeltaTime, 0, 0.0f);
            this.transform.Translate(moveVector);
        }
    }
}