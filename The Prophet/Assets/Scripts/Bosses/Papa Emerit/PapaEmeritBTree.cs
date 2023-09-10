using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

public class PapaEmeritBTree : BTree
{
    [SerializeField] private Transform _transform;

    [SerializeField] private Transform _meleeAttackPoint;
    [SerializeField] private GameObject _sickle;
    [SerializeField] private GameObject _ray;
    [SerializeField] private float _meleeAttackDistance;
    [SerializeField] private float _meleeAttackCoolDown;
    [SerializeField] private float _rayOfLightAttackCoolDown;
    [SerializeField] private int _rayMaxCount;

    private BossHealthController bossHealthController;

    private void Awake()
    {
        bossHealthController = GetComponent<BossHealthController>();    
    }

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckStage(0, bossHealthController.currentStageNumber),
                new CheckPlayerBossDistance(_transform, _meleeAttackDistance),
                new CheckTime(_meleeAttackCoolDown, "LastMeleeAttack"),
                new MeleeAttack(_sickle, _meleeAttackPoint),
            }),

            new Sequence(new List<Node>
            {
                new CheckStage(0, bossHealthController.currentStageNumber),
                new CheckTime(_rayOfLightAttackCoolDown, "LastRayOfLightAttack"),
                new RayOfLightAttack(_rayMaxCount, _ray)

            }),

            new LookAtPlayer(_transform), 
            
        });

        root.SetData("LastRayOfLightAttack", 0f);
        root.SetData("LastMeleeAttack", 0f);
        root.SetData("IsMeleeAttacking", false);

        return root;
    }
}
