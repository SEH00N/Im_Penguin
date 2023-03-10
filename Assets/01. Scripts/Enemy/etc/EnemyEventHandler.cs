using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyEventHandler : MonoBehaviour
{
    [HideInInspector] public Action attackAction;
    [HideInInspector] public Action DieAction;

    private EnemyController controller;
    private Animator animator;
    private PlayerWallet playerWallet;

    private void Start() 
    {
        controller = GetComponent<EnemyController>();
        animator = GetComponent<Animator>();
        playerWallet = FindObjectOfType<PlayerWallet>();

        SetAttackEvent();
        SetDieEvent();
    }

    #region  attack
    private void SetAttackEvent()
    {
        attackAction += AttackAnimation;
    }

    private void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
    #endregion

    #region die
    public void DieAnimEvent()
    {
        GameObject.FindObjectOfType<StageManager>().DescreaseEnemyCount();
        Destroy(gameObject);
    }

    private void SetDieEvent()
    {
        DieAction += controller.ChangeState<DeadState>;
        DieAction += DieAnimation;
        DieAction += AddPlayerMoney;
    }

    private void DieAnimation()
    {
        animator.SetTrigger("Death");
    }

    private void AddPlayerMoney()
    {
        playerWallet.AddMoney(controller.info.killReward);
    }
    #endregion
}
