using System;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.AbilitiesWindow.AbilitiesTree.AbilityNode
{
public class AbilityNodeView : MonoBehaviour, IAbilityNodeView
{
    [SerializeField]
    private string _abilityId;

    [SerializeField]
    private Image _abilityIcon;

    [SerializeField]
    private Image _selectedCircle;

    [SerializeField]
    private Button _abilityButton;

    private Action _onAbilityPressed;
    private readonly Color _initialIconColor = Color.white;

    public string AbilityId => _abilityId;

    public void Initialize(Sprite abilityIcon, Action onAbilityPressed)
    {
        _onAbilityPressed = onAbilityPressed;
        _abilityIcon.sprite = abilityIcon;
        SubscribeOnAbilityButton();
    }

    public void UpdateOwnState(bool isOwn)
    {
        _abilityIcon.color = isOwn ? _initialIconColor : Color.grey;
    }

    public void UpdateSelectedState(bool isSelected)
    {
        _selectedCircle.enabled = isSelected;
    }

    public void Dispose()
    {
        UnsubscribeOnAbilityButton();
        Destroy(gameObject);
    }

    private void SubscribeOnAbilityButton()
    {
        _abilityButton.onClick.AddListener(OnAbilityButtonPressed);
    }

    private void UnsubscribeOnAbilityButton()
    {
        _abilityButton.onClick.RemoveListener(OnAbilityButtonPressed);
    }

    private void OnAbilityButtonPressed()
    {
        _onAbilityPressed?.Invoke();
    }
}
}