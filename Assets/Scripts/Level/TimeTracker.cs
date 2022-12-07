using System.Diagnostics;
using TMPro;
using UnityEngine;

namespace Level
{
    public class TimeTracker : MonoBehaviour
    {
        [SerializeField] private TMP_Text timeText;
        
        private static readonly Stopwatch Stopwatch = new(); 
        private readonly string _format = @"hh\:mm\:ss\.ff";

        public void StartTimeTracker() => 
            Stopwatch.Start();

        public void StopTimeTracker()
        {
            Stopwatch.Stop();
            Stopwatch.Reset();
        }

        private void Update() => 
            timeText.text = Stopwatch.Elapsed.ToString(_format);
    }
}

