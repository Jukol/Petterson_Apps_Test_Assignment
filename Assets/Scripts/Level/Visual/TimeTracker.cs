using System.Diagnostics;
using TMPro;
using UnityEngine;

namespace Level.Visual
{
    public class TimeTracker : MonoBehaviour
    {
        [SerializeField] private TMP_Text timeText;
        
        private static readonly Stopwatch Stopwatch = new(); 
        private readonly string _format = @"hh\:mm\:ss\.ff";

        public void StartTimeTracker()
        {
            Stopwatch.Reset();
            Stopwatch.Start();
        }
        
        public void StopTimeTracker()
        {
            Stopwatch.Stop();
        }

        private void Update() => 
            timeText.text = Stopwatch.Elapsed.ToString(_format);
    }
}

