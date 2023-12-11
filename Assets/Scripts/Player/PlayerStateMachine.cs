using UnityEngine;

// On déclare les quatre états du personnage joueur
public enum PlayerState
{
    IDLE,
    WALKING,
    SPRINTING,
    ROLLING,
}


public class PlayerStateMachine : MonoBehaviour
{
    // On stocke l'état courant dans une variable globale
    private PlayerState _currentState;

    // Les composants
    private PlayerInput _playerInput;
    private PlayerController _playerController;
    private Animator _animator;

    private void Awake()
    {
        // On mets en cache nos composants
        _playerInput = GetComponent<PlayerInput>();
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        // Update de l'état en cours
        OnStateUpdate(_currentState);
    }
    private void FixedUpdate()
    {
        // FixedUpdate de l'état en cours
        OnStateFixedUpdate(_currentState);
    }

    // Méthode appelée lorsque l'on entre dans un état
    private void OnStateEnter(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.IDLE:
                OnEnterIdle();
                break;
            case PlayerState.WALKING:
                OnEnterWalking();
                break;
            case PlayerState.ROLLING:
                OnEnterRolling();
                break;
            case PlayerState.SPRINTING:
                OnEnterSprinting();
                break;
            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }
    // Méthode appelée à chaque frame dans un état
    private void OnStateUpdate(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.IDLE:
                OnUpdateIdle();
                break;
            case PlayerState.WALKING:
                OnUpdateWalking();
                break;
            case PlayerState.ROLLING:
                OnUpdateRolling();
                break;
            case PlayerState.SPRINTING:
                OnUpdateSprinting();
                break;
            default:
                Debug.LogError("OnStateUpdate: Invalid state " + state.ToString());
                break;
        }
    }
    // Méthode appelée à chaque "frame physique" dans un état
    private void OnStateFixedUpdate(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.IDLE:
                OnFixedUpdateIdle();
                break;
            case PlayerState.WALKING:
                OnFixedUpdateWalking();
                break;
            case PlayerState.ROLLING:
                OnFixedUpdateRolling();
                break;
            case PlayerState.SPRINTING:
                OnFixedUpdateSprinting();
                break;
            default:
                Debug.LogError("OnStateFixedUpdate: Invalid state " + state.ToString());
                break;
        }
    }
    // Méthode appelée lorsque l'on sort d'un état
    private void OnStateExit(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.IDLE:
                OnExitIdle();
                break;
            case PlayerState.WALKING:
                OnExitWalking();
                break;
            case PlayerState.ROLLING:
                OnExitRolling();
                break;
            case PlayerState.SPRINTING:
                OnExitSprinting();
                break;
            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }
    // Méthode appelée pour passer d'un état à un autre
    private void TransitionToState(PlayerState toState)
    {
        OnStateExit(_currentState);
        _currentState = toState;
        OnStateEnter(toState);
    }

    // Ce qu'il se passe lorsque l'on entre dans l'état IDLE
    private void OnEnterIdle()
    {
        // On déclenche le paramètre de l'animator
        _animator.SetTrigger("Idle");
    }
    // Ce qu'il se passe à chaque frame dans l'état IDLE
    private void OnUpdateIdle()
    {
        // Si il y a un mouvement
        if (_playerInput.HasMovement)
        {
            // Si la touche Roll est enfoncée
            if (_playerInput.Roll)
            {
                // On passe en SPRINTING
                TransitionToState(PlayerState.SPRINTING);
            }
            // Sinon
            else
            {
                // On passe en RUNNING
                TransitionToState(PlayerState.WALKING);
            }
        }
        // Sinon si on appuie la touche Roll
        else if (_playerInput.RollDown)
        {
            TransitionToState(PlayerState.ROLLING);
        }
    }
    // Ce qu'il se passe à chaque "frame physique" dans l'état IDLE
    private void OnFixedUpdateIdle()
    {
        // On commande au joueur de rester immobile
        _playerController.DoIdle();
    }
    // Ce qu'il se passe lorsque l'on sort de l'état IDLE
    private void OnExitIdle()
    {

    }

    // Ce qu'il se passe lorsque l'on entre dans l'état WALKING
    private void OnEnterWalking()
    {
        // On déclenche le paramètre de l'animator
        _animator.SetTrigger("Walk");
    }
    // Ce qu'il se passe à chaque frame dans l'état WALKING
    private void OnUpdateWalking()
    {
        // Si il n'y a pas de mouvement
        if (!_playerInput.HasMovement)
        {
            // On passe en IDLE
            TransitionToState(PlayerState.IDLE);
        }
        // Sinon si on appuie la touche Roll
        else if (_playerInput.RollDown)
        {
            // On passe en ROLLING
            TransitionToState(PlayerState.ROLLING);
        }
    }
    // Ce qu'il se passe à chaque "frame physique" dans l'état WALKING
    private void OnFixedUpdateWalking()
    {
        // On commande au joueur de marcher
        _playerController.DoWalk();
    }
    // Ce qu'il se passe lorsque l'on sort de l'état WALKING
    private void OnExitWalking()
    {

    }

    // Ce qu'il se passe lorsque l'on entre dans l'état SPRINTING
    private void OnEnterSprinting()
    {
        // On déclenche le paramètre de l'animator
        _animator.SetTrigger("Sprint");
    }
    // Ce qu'il se passe à chaque frame dans l'état SPRINTING
    private void OnUpdateSprinting()
    {
        // Si il n'y a pas de mouvement
        if (!_playerInput.HasMovement)
        {
            // On passe en IDLE
            TransitionToState(PlayerState.IDLE);
        }
        // Sinon (il y a mouvement) si la touche Roll n'est plus enfoncée
        else if (!_playerInput.Roll)
        {
            // On passe en RUNNING
            TransitionToState(PlayerState.WALKING);
        }
    }
    // Ce qu'il se passe à chaque "frame physique" dans l'état SPRINTING
    private void OnFixedUpdateSprinting()
    {
        // On commande au joueur de courir
        _playerController.DoSprint();
    }
    // Ce qu'il se passe lorsque l'on sort de l'état SPRINTING
    private void OnExitSprinting()
    {

    }

    // Ce qu'il se passe lorsque l'on entre dans l'état ROLLING
    private void OnEnterRolling()
    {
        // On commande au joueur de démarrer sa roulade
        _playerController.StartRoll();
        // On déclenche le paramètre de l'animator
        _animator.SetTrigger("Roll");
    }
    // Ce qu'il se passe à chaque frame dans l'état ROLLING
    private void OnUpdateRolling()
    {
        // Si la roulade est terminée
        if (_playerController.IsRollEnded)
        {
            // Si il y a mouvement et que la touche ROLL est enfoncée
            if (_playerInput.HasMovement && _playerInput.Roll)
            {
                // On passe en SPRINTING
                TransitionToState(PlayerState.SPRINTING);
            }
            // Si il y a mouvement et que la touche ROLL n'est pas enfoncée
            else if (_playerInput.HasMovement && !_playerInput.Roll)
            {
                // On passe en RUNNING
                TransitionToState(PlayerState.WALKING);
            }
            // Si il n'y a pas de mouvement
            else if (!_playerInput.HasMovement)
            {
                // On passe en IDLE
                TransitionToState(PlayerState.IDLE);
            }
        }
    }
    // Ce qu'il se passe à chaque "frame physique" dans l'état ROLLING
    private void OnFixedUpdateRolling()
    {
        // On commande au joueur de continuer sa roulade
        _playerController.DoRoll();
    }
    // Ce qu'il se passe lorsque l'on sort de l'état ROLLING
    private void OnExitRolling()
    {

    }

    /*private void OnGUI()
    {
        // On affiche l'état en cours pour le debug
        GUIStyle style = new GUIStyle() { fontSize = 50, fontStyle = FontStyle.Bold };
        style.normal.textColor = Color.white;
        GUI.Label(new Rect(50, 50, 100, 100), _currentState.ToString(), style);
    }*/
}
