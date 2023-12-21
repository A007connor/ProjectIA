using UnityEngine;

public enum EnemyState
{
    THINKING,
    HUNTING,
    FLEEING,
    ATTACKING,
    HURT,
    DEAD,
}

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyState _currentState;
    private Animator _animator;
    private EnemyController _controller;
    private HitBox _hitbox;

    public EnemyState CurrentState { get => _currentState; private set => _currentState = value; }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<EnemyController>();
        _hitbox = GetComponentInChildren<HitBox>();
    }
    private void Start()
    {
        CurrentState = EnemyState.THINKING;
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

    private void OnStateEnter(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.THINKING:
                OnEnterThinking();
                break;
            case EnemyState.HUNTING:
                OnEnterHunting();
                break;
            case EnemyState.FLEEING:
                OnEnterFleeing();
                break;
            case EnemyState.ATTACKING:
                OnEnterAttacking();
                break;
            case EnemyState.HURT:
                OnEnterHurt();
                break;
            case EnemyState.DEAD:
                OnEnterDead();
                break;
            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateUpdate(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.THINKING:
                OnUpdateThinking();
                break;
            case EnemyState.HUNTING:
                OnUpdateHunting();
                break;
            case EnemyState.FLEEING:
                OnUpdateFleeing();
                break;
            case EnemyState.ATTACKING:
                OnUpdateAttacking();
                break;
            case EnemyState.HURT:
                OnUpdateHurt();
                break;
            case EnemyState.DEAD:
                OnUpdateDead();
                break;
            default:
                Debug.LogError("OnStateUpdate: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateFixedUpdate(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.THINKING:
                OnFixedUpdateThinking();
                break;
            case EnemyState.HUNTING:
                OnFixedUpdateHunting();
                break;
            case EnemyState.FLEEING:
                OnFixedUpdateFleeing();
                break;
            case EnemyState.ATTACKING:
                OnFixedUpdateAttacking();
                break;
            case EnemyState.HURT:
                OnFixedUpdateHurt();
                break;
            case EnemyState.DEAD:
                OnFixedUpdateDead();
                break;
            default:
                Debug.LogError("OnStateFixedUpdate: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateExit(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.THINKING:
                OnExitThinking();
                break;
            case EnemyState.HUNTING:
                OnExitHunting();
                break;
            case EnemyState.FLEEING:
                OnExitFleeing();
                break;
            case EnemyState.ATTACKING:
                OnExitAttacking();
                break;
            case EnemyState.HURT:
                OnExitHurt();
                break;
            case EnemyState.DEAD:
                OnExitDead();
                break;
            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }
    private void TransitionToState(EnemyState toState)
    {
        OnStateExit(CurrentState);
        CurrentState = toState;
        OnStateEnter(toState);
    }

    private void OnEnterThinking()
    {
        // On démarre la réflexion
        _controller.StartThinking();
        // On envoie les paramètres necessaires à l'Animator
        _animator.SetBool("IsIdle", true);
    }
    private void OnUpdateThinking()
    {
        // Si la Hitbox est touchée
        if (_hitbox.IsHit())
        {
            TransitionToState(EnemyState.HURT);
            return;
        }
        // Si la réflexion est terminée
        if (_controller.IsThinkingEnded)
        {
            // Si la cible est à portée
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
        // On termine la réflexion
        _controller.EndThinking();
        // On envoie les paramètres necessaires à l'Animator
        _animator.SetBool("IsIdle", false);
    }

    private void OnEnterHunting()
    {
        // On envoie les paramètres necessaires à l'Animator
        _animator.SetBool("IsWalking", true);
    }
    private void OnUpdateHunting()
    {
        // Si la Hitbox est touchée
        if (_hitbox.IsHit())
        {
            TransitionToState(EnemyState.HURT);
            return;
        }
        // Si la cible est à portée ou que le délai entre deux réflexions est terminé
        if (_controller.IsTargetReachable
            || _controller.IsDelayBetweenThoughtsEnded)
        {
            TransitionToState(EnemyState.THINKING);
            return;
        }

        // On fait se retourner le personnage
        _controller.DoTurnCharacter();
    }
    private void OnFixedUpdateHunting()
    {
        // On fait se déplacer le personnage vers la cible
        _controller.DoHunt();
    }
    private void OnExitHunting()
    {
        // On envoie les paramètres necessaires à l'Animator
        _animator.SetBool("IsWalking", false);
    }

    private void OnEnterFleeing()
    {
        // On envoie les paramètres necessaires à l'Animator
        _animator.SetBool("IsWalking", true);
    }
    private void OnUpdateFleeing()
    {
        // Si la Hitbox est touchée
        if (_hitbox.IsHit())
        {
            TransitionToState(EnemyState.HURT);
            return;
        }
        // Si le délai entre deux réflexions est terminé
        if (_controller.IsDelayBetweenThoughtsEnded)
        {
            TransitionToState(EnemyState.THINKING);
            return;
        }

        // On fait se retourner le personnage
        _controller.DoTurnCharacter();
    }
    private void OnFixedUpdateFleeing()
    {
        // On fait se déplacer le personnage dans la direction oppposée à la cible
        _controller.DoFlee();
    }
    private void OnExitFleeing()
    {
        // On envoie les paramètres necessaires à l'Animator
        _animator.SetBool("IsWalking", false);
    }

    private void OnEnterAttacking()
    {
        // On démarre l'attaque
        _controller.StartAttack();
        // On envoie les paramètres necessaires à l'Animator
        _animator.SetBool("IsAttacking", true);
    }
    private void OnUpdateAttacking()
    {
        // Si l'attaque est terminée
        if (_controller.IsAttackEnded)
        {
            TransitionToState(EnemyState.THINKING);
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
        // On envoie les paramètres necessaires à l'Animator
        _animator.SetBool("IsAttacking", false);
    }

    private void OnEnterHurt()
    {
        // On démarre le recul
        _controller.StartKnockBack(_hitbox.GetHit());
        // On envoie les paramètres necessaires à l'Animator
        _animator.SetBool("IsHurt", true);
    }
    private void OnUpdateHurt()
    {
        // Si le recul est terminé
        if (_controller.IsKnockBackEnded)
        {
            // S'il reste des points de vie
            if (_controller.HealthPoints > 0)
            {
                TransitionToState(EnemyState.THINKING);
                return;
            }
            // Sinon
            else
            {
                TransitionToState(EnemyState.DEAD);
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
        // On envoie les paramètres necessaires à l'Animator
        _animator.SetBool("IsHurt", false);
    }

    private void OnEnterDead()
    {
        // On démarre la disparition du corps
        _controller.StartVanishing();
        // On envoie les paramètres necessaires à l'Animator
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
        // On envoie les paramètres necessaires à l'Animator
        _animator.SetBool("IsDead", false);
    }

}
