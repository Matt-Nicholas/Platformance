using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulseText : MonoBehaviour
{

    private Text _text;
    private Vector3 _baseScale;

    void Start()
    {
        _text = GetComponent<Text>();
        _baseScale = _text.transform.localScale;
    }

    private void Update()
    {
        var newScale = Mathf.PingPong(Time.time / 5, 0.1f);
        var pulseScale = new Vector3(newScale, newScale, _baseScale.z);

        var x = _baseScale.x + pulseScale.x;
        var y = _baseScale.y + pulseScale.y;

        _text.transform.localScale = new Vector3(x, y, _baseScale.z);
    }
}
