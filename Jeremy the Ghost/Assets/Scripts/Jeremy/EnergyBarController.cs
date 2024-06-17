using UnityEngine;
using UnityEngine.UI;

public class EnergyBarController : MonoBehaviour
{
    public float CurrentEnergy { get; private set; }
    private float _maxEnergy; // Is set through the sliders MaxValue
    
    private Slider _energySlider; // Controls the displayed energy in the UI

    [SerializeField] private float _moveCost;
    public float MoveCost => _moveCost; // Cost per one move
    
    [SerializeField] private float _rechargeAmountPerUpdate; // Energy constantly recharges (see Update)

    private void Start()
    {
        _energySlider = gameObject.GetComponent<Slider>();
        _maxEnergy = _energySlider.maxValue;
        CurrentEnergy = _maxEnergy;
    }

    private void Update()
    {
        // Constantly recharging energy
        IncreaseEnergy();
        UpdateSlider();
    }

    public void DecreaseEnergy()
    {
        CurrentEnergy -= MoveCost;
        UpdateSlider();
    }
    
    private void IncreaseEnergy()
    {
        CurrentEnergy += _rechargeAmountPerUpdate * Time.deltaTime;
        if (CurrentEnergy > _maxEnergy) CurrentEnergy = _maxEnergy;
    }

    private void UpdateSlider()
    {
        _energySlider.value = CurrentEnergy;
    }

    public void ResetEnergy()
    {
        CurrentEnergy = _maxEnergy;
        UpdateSlider();
    }
    
}
