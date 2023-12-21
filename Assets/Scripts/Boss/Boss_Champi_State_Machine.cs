using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChampiState
{
    THINKING,
    HUNTING,
    FLEEING,
    ATTACKING,
    HURT,
    DEAD,
}

public class Boss_Champi_State_Machine : MonoBehaviour
{
    [SerializeField] private ChampiState _currentState;
    private Animator _animator;
    private Boss_Champi_Controller _controller;
    private HitBox _hitbox;

    public ChampiState CurrentState { get => _currentState; private set => _currentState = value; }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<Boss_Champi_Controller>();
        _hitbox = GetComponentInChildren<HitBox>();
    }
    private void Start()
    {
        CurrentState = ChampiState.THINKING;
        OnEnterThinking();
    }
    private void Update()
    {
        OnStateUpdate(CurrentState);
    }
    private void FixedUpdate()
    {
        OnStateFixedUpdate(CurrentState);
    }

    private void OnStateEnter(ChampiState state)
    {
        switch (state)
        {
            case ChampiState.THINKING:
                OnEnterThinking();
                break;
            case ChampiState.HUNTING:
                OnEnterHunting();
                break;
            case ChampiState.FLEEING:
                OnEnterFleeing();
                break;
            case ChampiState.ATTACKING:
                OnEnterAttacking();
                break;
            case ChampiState.HURT:
                OnEnterHurt();
                break;
            case ChampiState.DEAD:
                OnEnterDead();
                break;
            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateUpdate(ChampiState state)
    {
        switch (state)
        {
            case ChampiState.THINKING:
                OnUpdateThinking();
                break;
            case ChampiState.HUNTING:
                OnUpdateHunting();
                break;
            case ChampiState.FLEEING:
                OnUpdateFleeing();
                break;
            case ChampiState.ATTACKING:
                OnUpdateAttacking();
                break;
            case ChampiState.HURT:
                OnUpdateHurt();
                break;
            case ChampiState.DEAD:
                OnUpdateDead();
                break;
            default:
                Debug.LogError("OnStateUpdate: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateFixedUpdate(ChampiState state)
    {
        switch (state)
        {
            case ChampiState.THINKING:
                OnFixedUpdateThinking();
                break;
            case ChampiState.HUNTING:
                OnFixedUpdateHunting();
                break;
            case ChampiState.FLEEING:
                OnFixedUpdateFleeing();
                break;
            case ChampiState.ATTACKING:
                OnFixedUpdateAttacking();
                break;
            case ChampiState.HURT:
                OnFixedUpdateHurt();
                break;
            case ChampiState.DEAD:
                OnFixedUpdateDead();
                break;
            default:
                Debug.LogError("OnStateFixedUpdate: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateExit(ChampiState state)
    {
        switch (state)
        {
            case ChampiState.THINKING:
                OnExitThinking();
                break;
            case ChampiState.HUNTING:
                OnExitHunting();
                break;
            case ChampiState.FLEEING:
                OnExitFleeing();
                break;
            case ChampiState.ATTACKING:
                OnExitAttacking();
                break;
            case ChampiState.HURT:
                OnExitHurt();
                break;
            case ChampiState.DEAD:
                OnExitDead();
                break;
            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }
    private void TransitionToState(ChampiState toState)
    {
        OnStateExit(CurrentState);
        CurrentState = toState;
        OnStateEnter(toState);
    }

    private void OnEnterThinking()
    {
        // On d�marre la r�flexion
        _controller.StartThinking();
        // On envoie les param�tres necessaires � l'Animator
        _animator.SetBool("IsIdle", true);
    }
    private void OnUpdateThinking()
    {
        // Si la Hitbox est touch�e
        if (_hitbox.IsHit())
        {
            TransitionToState(ChampiState.HURT);
            return;
        }
        // Si la r�flexion est termin�e
        if (_controller.IsThinkingEnded)
        {
            // Si la cible est � port�e
            if (_controller.IsTargetReachable)
            {
                TransitionToState(_controller.ThinkNear());
                return;
            }
            // Sinon
            else
            {
                TransitionToState(_controller.ThinkFar());
                return;
            }
        }
    }
    private void OnFixedUpdateThinking()
    {
        // On fige le personnage
        _controller.DoIdle();
    }
    private void OnExitThinking()
    {
        // On termine la r�flexion
        _controller.EndThinking();
        // On envoie les param�tres necessaires � l'Animator
        _animator.SetBool("IsIdle", false);
    }

    private void OnEnterHunting()
    {
        // On envoie les param�tres necessaires � l'Animator
        _animator.SetBool("IsRun", true);
    }
    private void OnUpdateHunting()
    {
        // Si la Hitbox est touch�e
        if (_hitbox.IsHit())
        {
            TransitionToState(ChampiState.HURT);
            return;
        }
        // Si la cible est � port�e ou que le d�lai entre deux r�flexions est termin�
        if (_controller.IsTargetReachable
            || _controller.IsDelayBetweenThoughtsEnded)
        {
            TransitionToState(ChampiState.THINKING);
            return;
        }

        // On fait se retourner le personnage
        _controller.DoTurnCharacter();
    }
    private void OnFixedUpdateHunting()
    {
        // On fait se d�placer le personnage vers la cible
        _controller.DoHunt();
    }
    private void OnExitHunting()
    {
        // On envoie les param�tres necessaires � l'Animator
        _animator.SetBool("IsRun", false);
    }

    private void OnEnterFleeing()
    {
        // On envoie les param�tres necessaires � l'Animator
        _animator.SetBool("IsRun", true);
    }
    private void OnUpdateFleeing()
    {
        // Si la Hitbox est touch�e
        if (_hitbox.IsHit())
        {
            TransitionToState(ChampiState.HURT);
            return;
        }
        // Si le d�lai entre deux r�flexions est termin�
        if (_controller.IsDelayBetweenThoughtsEnded)
        {
            TransitionToState(ChampiState.THINKING);
            return;
        }

        // On fait se retourner le personnage
        _controller.DoTurnCharacter();
    }
    private void OnFixedUpdateFleeing()
    {
        // On fait se d�placer le personnage dans la direction opppos�e � la cible
        _controller.DoFlee();
    }
    private void OnExitFleeing()
    {
        // On envoie les param�tres necessaires � l'Animator
        _animator.SetBool("IsRun", false);
    }

    private void OnEnterAttacking()
    {
        // On d�marre l'attaque
        _controller.StartAttack();
        // On envoie les param�tres necessaires � l'Animator
        _animator.SetBool("IsAttack", true);
    }
    private void OnUpdateAttacking()
    {
        // Si l'attaque est termin�e
        if (_controller.IsAttackEnded)
        {
            TransitionToState(ChampiState.THINKING);
            return;
        }

        // On continue l'attaque
        _controller.DoAttack();
    }
    private void OnFixedUpdateAttacking()
    {
    }
    private void OnExitAttacking()
    {
        // On termine l'attaque
        _controller.EndAttack();
        // On envoie les param�tres necessaires � l'Animator
        _animator.SetBool("IsAttack", false);
    }

    private void OnEnterHurt()
    {
        // On d�marre le recul
        _controller.StartKnockBack(_hitbox.GetHit());
        // On envoie les param�tres necessaires � l'Animator
        _animator.SetBool("IsHurt", true);
    }
    private void OnUpdateHurt()
    {
        // Si le recul est termin�
        if (_controller.IsKnockBackEnded)
        {
            // S'il reste des points de vie
            if (_controller.HealthPoints > 0)
            {
                TransitionToState(ChampiState.THINKING);
                return;
            }
            // Sinon
            else
            {
                TransitionToState(ChampiState.DEAD);
                return;
            }
        }

        // On continue le recul vertical
        _controller.DoVerticalKnockback();
    }
    private void OnFixedUpdateHurt()
    {
        // On continue le recul horizontal
        _controller.DoHorizontalKnockBack();
    }
    private void OnExitHurt()
    {
        // On termine le recul
        _controller.EndKnockBack();
        // On envoie les param�tres necessaires � l'Animator
        _animator.SetBool("IsHurt", false);
    }

    private void OnEnterDead()
    {
        // On d�marre la disparition du corps
        _controller.StartVanishing();
        // On envoie les param�tres necessaires � l'Animator
        _animator.SetBool("IsDead", true);
    }
    private void OnUpdateDead()
    {
        // On continue la disparition du corps
        _controller.DoVanishing();
    }
    private void OnFixedUpdateDead()
    {
        // On fige le personnage
        _controller.DoIdle();
    }
    private void OnExitDead()
    {
        // On envoie les param�tres necessaires � l'Animator
        _animator.SetBool("IsDead", false);
    }

}