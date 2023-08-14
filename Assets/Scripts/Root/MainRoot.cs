using System;
using Windows.AbilitiesWindow.Window;
using Config.AbilitiesConfig.FakeAbilitiesConfig;
using Config.PlayerConfig.FakePlayerConfig;
using UnityEngine;

namespace Root
{
public class MainRoot : MonoBehaviour, IDisposable
{
    [SerializeField]
    private AbilitiesWindowView _abilitiesWindowViewPrefab;

    [SerializeReference]
    private FakePlayerConfig _playerConfig;

    [SerializeReference]
    private FakeAbilitiesConfig _abilitiesConfig;

    [SerializeField]
    private Transform _windowsParent;

    private AbilitiesWindowPresenter _abilitiesWindowPresenter;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        var abilitiesWindowView = Instantiate(_abilitiesWindowViewPrefab, _windowsParent);
        var abilitiesWindowModel = new AbilitiesWindowModel(_playerConfig, _abilitiesConfig);
        _abilitiesWindowPresenter = new AbilitiesWindowPresenter(abilitiesWindowModel, abilitiesWindowView);
    }

    public void Dispose()
    {
        _abilitiesWindowPresenter.Dispose();
    }
}
}
