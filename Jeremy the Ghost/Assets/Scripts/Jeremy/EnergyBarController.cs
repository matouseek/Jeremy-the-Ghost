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
    [SerializeField] private float _slowRechargeSubtractionConstant; // Used when player holds S,
                                                                     // the recharge amount is decreased

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

    /// <summary>
    /// Slows the energy recharge rate. Used when player presses the S key.
    /// </summary>
    public void SlowEnergyRecharge()
    {
        _rechargeAmountPerUpdate -= _slowRechargeSubtractionConstant;
    }

    /// <summary>
    /// Puts the recharge rate back to normal. Used when player lets go of S key.
    /// </summary>
    public void PutEnergyRechargeToNormal()
    {
        _rechargeAmountPerUpdate += _slowRechargeSubtractionConstant;
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
