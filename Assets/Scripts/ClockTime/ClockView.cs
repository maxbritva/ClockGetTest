using Data;
using DG.Tweening;
using TMPro;
using UnityEngine;
using VContainer;

namespace ClockTime
{
    public class ClockView : MonoBehaviour
    {
        [Header("Arrows")]
        [SerializeField] private GameObject _hoursArrow;
        [SerializeField] private GameObject _minutesArrow;
        [SerializeField] private GameObject _secondsArrow;

        [SerializeField] private TMP_Text _clockText;
        
        private IUpdateTime _updateTime;

        private void OnEnable()
        {
            _updateTime.OnSecondsChanged += SetSecondsArrow;
            _updateTime.OnSecondsChanged += SetTextClock;
            _updateTime.OnMinutesChanged += SetMinutesArrow;
            _updateTime.OnHoursChanged += SetHoursArrow;
            _updateTime.OnUpdatedTime += SetCurrentTime;
        }

        private void OnDisable()
        {
            _updateTime.OnSecondsChanged -= SetSecondsArrow;
            _updateTime.OnSecondsChanged -= SetTextClock;
            _updateTime.OnSecondsChanged -= SetMinutesArrow;
            _updateTime.OnHoursChanged -= SetHoursArrow;
        }

        private void SetCurrentTime()
        {
            SetTextClock();
            SetSecondsArrow();
            SetMinutesArrow();
            SetHoursArrow();
        }

        private void SetSecondsArrow() => 
            AnimateArrow(_secondsArrow.transform, new Vector3(0f, 0f, - _updateTime.GetCurrentSeconds() * 360f / 60f));

        private void SetMinutesArrow() => 
            AnimateArrow(_minutesArrow.transform,  new Vector3(0f,0f, - _updateTime.GetCurrentMinutes() * 360f / 60f));
        
        private void SetHoursArrow() => 
            AnimateArrow(_hoursArrow.transform, new Vector3(0f, 0f, -_updateTime.GetCurrentHours() * 360f / 12f));

        private void SetTextClock() => _clockText.text = $"{_updateTime.GetCurrentHours()} : {_updateTime.GetCurrentMinutes()} : {_updateTime.GetCurrentSeconds()}";

        private void AnimateArrow(Transform target,Vector3 position) => 
            target.DORotate(position, 1f).SetEase(Ease.OutBounce);

        [Inject] private void Construct(IUpdateTime updateTime) => _updateTime = updateTime;
    }
}