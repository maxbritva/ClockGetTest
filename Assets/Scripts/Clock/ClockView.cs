using UnityEngine;

namespace Clock
{
    public class ClockView : MonoBehaviour
    {
        [Header("Arrows")]
        [SerializeField] private GameObject _hoursArrow;
        [SerializeField] private GameObject _minutesArrow;
        [SerializeField] private GameObject _secondsArrow;


        public GameObject HoursArrow => _hoursArrow;
        public GameObject MinutesArrow => _minutesArrow;
        public GameObject SecondsArrow => _secondsArrow;
    }
}