using System;
using System.Collections;
using System.Collections.Generic;
using Windows.AbilitiesWindow.AbilitiesTree;
using Windows.AbilitiesWindow.MovingTracker;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.AbilitiesWindow.Window
{
public class AbilitiesWindowView : MonoBehaviour, IAbilitiesWindowView
{
    private const float WarningMessageDuration = 1.5f;
    
    [SerializeField]
    private Button _earnButton;

    [SerializeField]
    private Button _forgetAllAbilitiesButton;

    [SerializeField]
    private Button _buyAbilityButton;

    [SerializeField]
    private Button _forgetSelectedAbilityButton;

    [SerializeField]
    private TMP_Text _currentAbilityPointsCount;

    [SerializeReference]
    private GameObject _abilitiesTreeView;

    [SerializeField]
    private Transform _abilitiesTreeParent;

    [SerializeField]
    private TreeMovingTracker _treeMovingTracker;

    [SerializeField]
    private TMP_Text _warningMessage;

    public IMovingTracker MovingTracker => _treeMovingTracker;

    private List<Action> _earnButtonPressedEvents;
    private List<Action> _forgetAllAbilitiesButtonPressedEvents;
    private List<Action> _buyAbilityButtonPressedEvents;
    private List<Action> _forgetSelectedAbilityButtonPressedEvents;
    private Coroutine _showWarningMessageRoutine;


    public void Initialize()
    {
        _earnButtonPressedEvents = new List<Action>();
        _forgetAllAbilitiesButtonPressedEvents = new List<Action>();
        _buyAbilityButtonPressedEvents = new List<Action>();
        _forgetSelectedAbilityButtonPressedEvents = new List<Action>();
        _warningMessage.alpha = 0;
        
        SubscribeOnButtonsEvent();
    }

    public void ShowWarningMessage(string message)
    {
        _warningMessage.text = message;

        if (_showWarningMessageRoutine != null)
        {
            StopCoroutine(_showWarningMessageRoutine);
        }
        
        _showWarningMessageRoutine = StartCoroutine(ShowWaningMessage());
    }

    public void UpdateCurrentAbilityUpgradePoints(float currentPointsCount)
    {
        _currentAbilityPointsCount.text = ((int)currentPointsCount).ToString();
    }

    public void SubscribeOnEarnAbilityPointsButton(Action onEarnButtonPressed)
    {
        _earnButtonPressedEvents.Add(onEarnButtonPressed);
    }

    public void UnsubscribeOnEarnAbilityPointsButton(Action onEarnButtonPressed)
    {
        _earnButtonPressedEvents.Remove(onEarnButtonPressed);
    }

    public void SubscribeOnForgetAllAbilitiesButton(Action onForgetAllAbilitiesButtonPressed)
    {
        _forgetAllAbilitiesButtonPressedEvents.Add(onForgetAllAbilitiesButtonPressed);
    }

    public void UnsubscribeOnForgetAllAbilitiesButton(Action onForgetAllAbilitiesButtonPressed)
    {
        _forgetAllAbilitiesButtonPressedEvents.Remove(onForgetAllAbilitiesButtonPressed);
    }

    public void SubscribeOnBuyAbilityButton(Action onBuyAbilityButtonPressed)
    {
        _buyAbilityButtonPressedEvents.Add(onBuyAbilityButtonPressed);
    }

    public void UnsubscribeOnBuyAbilityButton(Action onBuyAbilityButtonPressed)
    {
        _buyAbilityButtonPressedEvents.Remove(onBuyAbilityButtonPressed);
    }

    public void SubscribeOnForgetSelectedAbilityButton(Action onForgetSelectedAbilityButtonPressed)
    {
        _forgetSelectedAbilityButtonPressedEvents.Add(onForgetSelectedAbilityButtonPressed);
    }

    public void UnsubscribeOnForgetSelectedAbilityButton(Action onForgetSelectedAbilityButtonPressed)
    {
        _forgetSelectedAbilityButtonPressedEvents.Remove(onForgetSelectedAbilityButtonPressed);
    }

    public IAbilitiesTreeView CreateAbilityTree()
    {
        var abilitiesTreeViewObject = Instantiate(_abilitiesTreeView, _abilitiesTreeParent);

        if (abilitiesTreeViewObject.TryGetComponent(out IAbilitiesTreeView abilitiesTreeView))
        {
            return abilitiesTreeView;
        }

        throw new Exception($"Prefab does not have a component that inherits from {nameof(IAbilitiesTreeView)}");
    }
    
    public void Dispose()
    {
        UnsubscribeOnButtonsEvent();
        _treeMovingTracker.Dispose();
        if (_showWarningMessageRoutine != null)
        {
            StopCoroutine(_showWarningMessageRoutine);
        }
        Destroy(gameObject);
    }

    private void SubscribeOnButtonsEvent()
    {
        _earnButton.onClick.AddListener(OnEarnButtonPressed);
        _forgetAllAbilitiesButton.onClick.AddListener(OnForgetAllAbilitiesButtonPressed);
        _buyAbilityButton.onClick.AddListener(OnBuyAbilityButtonPressed);
        _forgetSelectedAbilityButton.onClick.AddListener(OnForgetSelectedAbility);
    }

    private void UnsubscribeOnButtonsEvent()
    {
        _earnButton.onClick.RemoveListener(OnEarnButtonPressed);
        _forgetAllAbilitiesButton.onClick.RemoveListener(OnForgetAllAbilitiesButtonPressed);
        _buyAbilityButton.onClick.RemoveListener(OnBuyAbilityButtonPressed);
        _forgetSelectedAbilityButton.onClick.RemoveListener(OnForgetSelectedAbility);
    }

    private void OnForgetSelectedAbility()
    {
        foreach (var forgetAbilityEvent in _forgetSelectedAbilityButtonPressedEvents)
        {
            forgetAbilityEvent?.Invoke();
        }
    }

    private void OnEarnButtonPressed()
    {
        foreach (var earnButtonPressedEvent in _earnButtonPressedEvents)
        {
            earnButtonPressedEvent?.Invoke();
        }
    }

    private void OnForgetAllAbilitiesButtonPressed()
    {
        foreach (var forgetAllAbilitiesEvent in _forgetAllAbilitiesButtonPressedEvents)
        {
            forgetAllAbilitiesEvent?.Invoke();
        }
    }

    private void OnBuyAbilityButtonPressed()
    {
        foreach (var buyAbilityButtonPressedEvent in _buyAbilityButtonPressedEvents)
        {
            buyAbilityButtonPressedEvent?.Invoke();
        }
    }

    private IEnumerator ShowWaningMessage()
    {
        _warningMessage.gameObject.SetActive(true);

        float timer;
        var showDuration = WarningMessageDuration / 3;
        var waitShowedTextDuration = WarningMessageDuration / 3;
        var hideDuration = WarningMessageDuration / 3;

        _warningMessage.alpha = 0f;
        var elapsedTime = 0f;
        timer = showDuration;
        while (timer > 0)
        {
            var deltaTime = Time.deltaTime;
            elapsedTime += deltaTime;
            var normalizedTime = Mathf.Clamp01(elapsedTime / showDuration);
            _warningMessage.alpha = normalizedTime;
            timer -= deltaTime;

            yield return null;
        }

        timer = waitShowedTextDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        _warningMessage.alpha = 1f;
        timer = hideDuration;
        var startTime = Time.time;
        while (timer > 0)
        {
            var timePassed = Time.time - startTime;
            var normalizedTime = Mathf.Clamp01(timePassed / showDuration);
            var newAlpha = Mathf.Lerp(1.0f, 0.0f, normalizedTime);
            _warningMessage.alpha = newAlpha;
            timer -= Time.deltaTime;
            
            yield return null;
        }
    }
}
}