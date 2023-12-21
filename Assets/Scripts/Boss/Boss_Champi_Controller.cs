using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Boss_Champi_Controller : MonoBehaviour
{
    //Reference settings
    [Header("R�f�rences Settings")]
    [SerializeField]
    [Tooltip("R�f�rence � l'enfant Sprite contenant le composant SpriteRenderer")]
    private Transform _graphics;
    [SerializeField]
    [Tooltip("R�f�rence � l'AttackBox")]
    private GameObject _attackBox;

    //Health settings
    [Header("Health Settings")]
    [Min(0f)]
    [SerializeField]
    [Tooltip("Delai avant disparition du corps")]
    private float _vanishingDuration = 3f;
    [Min(0f)]
    [SerializeField]
    [Tooltip("Points de vie")]
    private int _healthPoints = 10;

    //Speed settings
    [Header("Speed Settings")]
    [Min(0f)]
    [SerializeField]
    [Tooltip("Vitesse de marche")]
    private float _speed = 2.5f;

    //Attack settings
    [Header("Attack Settings")]
    [Min(0f)]
    [SerializeField]
    [Tooltip("Dur�e de l'attaque")]
    private float _attackDuration = 0.1f;
    [Min(0f)]
    [SerializeField]
    [Tooltip("D�lai minimum entre deux coups pour les combos")]
    private float _attackComboDuration = 0.5f;

    //KnockBack settings
    [Header("Knockback Settings")]
    [Min(0f)]
    [SerializeField]
    [Tooltip("Dur�e du knockback")]
    private float _knockBackDuration = 0.5f;
    [Min(0f)]
    [SerializeField]
    [Tooltip("Puissance horizontale du knockback")]
    private float _knockBackHorizontalForce = 1f;
    [Min(0f)]
    [SerializeField]
    [Tooltip("Puissance verticale du knockback")]
    private float _knockBackVerticalForce = 0.5f;
    [SerializeField]
    [Tooltip("Easing horizontal du knockback")]
    private AnimationCurve _knockBackHorizontalEasing;
    [SerializeField]
    [Tooltip("Easing vertical du knockback")]
    private AnimationCurve _knockBackVerticalEasing;

    //IA settings
    [Header("IA Settings")]
    [Min(0.1f)]
    [SerializeField]
    [Tooltip("Fourchette de dur�e de la reflexion")]
    private Vector2 _thinkDuration = new Vector2(1f, 4f);
    [Min(0.1f)]
    [SerializeField]
    [Tooltip("Fourchette de dur�e entre deux reflexions")]
    private Vector2 _delayBetweenThoughts = new Vector2(1f, 4f);
    [Min(0.1f)]
    [SerializeField]
    [Tooltip("Distance maximum � partir de laquelle la cible est � port�e")]
    private float _reachableDistance = 1.5f;

    // R�f�rences de composants
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    // Variables de timer
    private float _attackEndTime;                               // heure de fin d'attaque coup de poing
    private float _attackComboEndTime;                          // heure de fin du combo d'attaque
    private float _knockBackEndTime;                            // heure de fin de knockBack
    private float _thinkEndTime;                                // heure de fin de reflexion
    private float _delayBetweenThoughtsEndTime;                 // heure de finde d�lai entre deux reflexions
    private float _vanishingEndTime;                            // heure de disparition du corps

    // Variables d'attaques
    private Transform _target;                                  // cible
    private Vector2 _knockBackDirection;                        // direction du knockBack en cours
    private int _combo;                                         // combo actuel

    // Propri�t�s
    public int HealthPoints { get => _healthPoints; }
    public bool IsAttackEnded { get => Time.time >= _attackEndTime; }
    public bool IsKnockBackEnded { get => Time.time >= _knockBackEndTime; }
    public bool IsThinkingEnded { get => Time.time >= _thinkEndTime; }
    public bool IsDelayBetweenThoughtsEnded { get => Time.time >= _delayBetweenThoughtsEndTime; }
    public bool IsTargetReachable { get => Vector2.Distance(transform.position, _target.position) < _reachableDistance; }

    private void Awake()
    {
        // On r�cup�re les composants
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        // On trouve le Player sur la sc�ne
        _target = FindObjectOfType<PlayerController>().transform;
    }

    public void StartThinking()
    {
        // On d�termine � quelle heure la r�flexion sera termin�e
        _thinkEndTime = Time.time + Random.Range(_thinkDuration.x, _thinkDuration.y);
    }
    public EnemyState ThinkNear()
    {
        float thought = Random.value;
        if (thought < 0.15f)
        {
            return EnemyState.THINKING;
        }
        else if (thought < 0.3f)
        {
            return EnemyState.FLEEING;
        }
        else
        {
            return EnemyState.ATTACKING;
        }
    }
    public EnemyState ThinkFar()
    {
        float thought = Random.value;
        if (thought < 0.15f)
        {
            return EnemyState.THINKING;
        }
        else if (thought < 0.3f)
        {
            return EnemyState.FLEEING;
        }
        else
        {
            return EnemyState.HUNTING;
        }
    }
    public void EndThinking()
    {
        // On d�termine � quelle heure la prochaine r�flexion pourra avoir lieu
        _delayBetweenThoughtsEndTime = Time.time + Random.Range(_delayBetweenThoughts.x, _delayBetweenThoughts.y);
    }


    public void DoTurnCharacter()
    {
        // On tourne le personnage dans le sens de son d�placement
        _transform.right = _rigidbody.velocity.x < 0 ? Vector2.left : Vector2.right;
    }
    public void DoIdle()
    {
        // On fige le personnage
        _rigidbody.velocity = Vector2.zero;
    }
    public void DoHunt()
    {
        // On d�termine la direction vers la cible
        Vector2 direction = (_target.position - transform.position).normalized;
        // On applique le d�placement
        _rigidbody.velocity = direction * _speed;
    }
    public void DoFlee()
    {
        // On d�termine la direction oppos�e � la cible
        Vector2 direction = (transform.position - _target.position).normalized;
        // On applique le d�placement
        _rigidbody.velocity = direction * _speed;
    }

    public void StartAttack()
    {
        // On active l'AttackBox
        _attackBox.SetActive(true);
        // On d�termine l'heure o� l'attaque prendra fin
        _attackEndTime = Time.time + _attackDuration;

        // Si la derni�re attaque a eu lieu apr�s le d�lai imparti pour d�clencher un combo
        if (Time.time > _attackComboEndTime)
        {
            // On r�initialise le compteur de combo
            _combo = 0;
        }
        // Sinon
        else
        {
            // On incr�mente le compteur de combo
            _combo = _combo + 1 == 2 ? 0 : _combo + 1;
        }

        // On determine l'heure avant laquelle doit avoir lieu la prochaine attaque pour d�clencher un combo
        _attackComboEndTime = Time.time + _attackComboDuration;
        // On envoie le num�ro de combo � l'Animator
        _animator.SetInteger("AttackCombo", _combo);
    }
    public void DoAttack()
    {
        // On calcule la progression de l'attaque
        float attackProgress = (_attackDuration - (_attackEndTime - Time.time)) / _attackDuration;
        // On d�finit le param�tre d'animator concernant la progression de l'attaque
        _animator.SetFloat("AttackProgress", attackProgress);
    }
    public void EndAttack()
    {
        // On d�sactive l'AttackBox
        _attackBox.SetActive(false);
        // On d�finit le param�tre d'animator concernant la progression de l'attaque � 0
        _animator.SetFloat("AttackProgress", 0f);
    }

    public void StartKnockBack(Vector2 knockBackDirection)
    {
        // On stocke la direction du knockback en cours
        _knockBackDirection = knockBackDirection;
        // On retranche des points de vie au personnage
        _healthPoints = Mathf.Max(0, _healthPoints - 1);
        // On d�termine l'heure o� le knockback sera termin�
        _knockBackEndTime = Time.time + (_knockBackDuration);
    }
    public void DoVerticalKnockback()
    {
        // On calcule la progression du knockback
        float knockBackFreezeProgress = (_knockBackDuration - (_knockBackEndTime - Time.time)) / _knockBackDuration;
        // On calcule le easing gr�ce � la courbe, la progression et la force
        float curvedAmount = _knockBackVerticalEasing.Evaluate(knockBackFreezeProgress) * _knockBackVerticalForce;
        // On calcule la nouvelle position Y du sprite
        float newYPos = curvedAmount * _knockBackVerticalForce;
        // On applique le mouvement
        _graphics.localPosition = new Vector3(0, newYPos, _graphics.localPosition.z);
    }
    public void DoHorizontalKnockBack()
    {
        // On calcule la progression du knockback
        float knockBackFreezeProgress = (_knockBackDuration - (_knockBackEndTime - Time.time)) / _knockBackDuration;
        // On calcule le easing gr�ce � la courbe, la progression et la force
        float curvedAmount = _knockBackHorizontalEasing.Evaluate(knockBackFreezeProgress) * _knockBackHorizontalForce;
        // On cr�e un vecteur de mouvement gr�ce � la direction et au easing
        Vector2 motion = _knockBackDirection * curvedAmount;
        // On applique le mouvement
        _rigidbody.velocity = motion;
    }
    public void EndKnockBack()
    {
        // On snap la position Y du sprite � 0
        _graphics.localPosition = new Vector3(0, 0, _graphics.localPosition.z);
    }

    public void StartVanishing()
    {
        // On d�termine l'heure � laquelle le corps disparaitra
        _vanishingEndTime = Time.time + _vanishingDuration;
    }
    public void DoVanishing()
    {
        // Si le d�lai de disparition du corps est �coul�
        if (Time.time > _vanishingEndTime)
        {
            // On d�truit le personnage
            Destroy(gameObject);
        }
    }
}