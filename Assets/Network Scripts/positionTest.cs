using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PositionTest : MonoBehaviour
{
    string projectName = "";
    Vector3 targetPosition; // 目標位置
    bool isChecking = false; // 防止多次檢查執行

    void Update()
    {
        if (projectTP.flag1)
        {
            ChangePosition("project1", new Vector3(-93.3f, 1.1f, 39));
        }
        if (projectTP.flag2)
        {
            ChangePosition("project2", new Vector3(-87.3f, 1.1f, 39));
        }
        if (projectTP.flag4)
        {
            ChangePosition("project4", new Vector3(-106.5f, 1.1f, 39));
        }
        if (projectTP.flag5)
        {
            ChangePosition("project5", new Vector3(-113f, 1.1f, 39));
        }
    }

    void ChangePosition(string project, Vector3 position)
    {
        if (GetComponent<NetworkObject>().HasStateAuthority)
        {
            projectName = project;
            targetPosition = position;
            transform.position = position;
            Debug.Log($"切換至: {project}, 位置: {transform.position}");
            StartCheck();
        }
    }

    void StartCheck()
    {
        if (!isChecking)
        {
            isChecking = true;
            InvokeRepeating(nameof(CheckPosition), 0.01f, 0.01f);
        }
    }

    void CheckPosition()
    {
        float tolerance = 0.1f; // 容許誤差範圍
        if (Vector3.Distance(transform.position, targetPosition) > tolerance)
        {
            transform.position = targetPosition;
        }
        else
        {
            Debug.Log($"檢查完成，位置穩定於: {transform.position}");
            CancelInvoke(nameof(CheckPosition));
            ResetFlags();
            isChecking = false;
        }
    }

    void ResetFlags()
    {
        if (projectName == "project1") projectTP.flag1 = false;
        if (projectName == "project2") projectTP.flag2 = false;
        if (projectName == "project4") projectTP.flag4 = false;
        if (projectName == "project5") projectTP.flag5 = false;
    }
}
