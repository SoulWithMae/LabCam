﻿namespace LabCam.Scripts;

// giggity
[RegisterTypeInIl2Cpp]
public class Quagmire : MonoBehaviour
{
    public static Quagmire Instance;

    public GameObject giggity;
    public GameObject giggityPreview;
    public MeshRenderer giggityRenderer;
    public AudioSource giggityAudio;
    
    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        SetFields();
        SetQuality();
    }

    private void SetFields()
    {
        giggity = transform.Find("Scale").gameObject;
        giggityPreview = transform.Find("Preview").gameObject;
        giggityRenderer = giggityPreview.GetComponent<MeshRenderer>();
        giggityAudio = transform.Find("TriggerSound").GetComponent<AudioSource>();
    }

    public void SetQuality()
    {
        switch (Preferences.Quality.Value)
        {
            case ImageQuality.Low:
                giggityRenderer.material = Assets.Materials.LowQuality;
                break;
            case ImageQuality.Medium:
                giggityRenderer.material = Assets.Materials.MediumQuality;
                break;
            case ImageQuality.High:
                giggityRenderer.material = Assets.Materials.HighQuality;
                break;
            default:
                ModConsole.Error("Invalid quality setting!");
                break;
        }
    }
	
    public void SendCapture()
    {
        if (LabCamera.Instance == null) return;
        giggityAudio.Play();
        LabCamera.Instance.Capture();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public Quagmire(IntPtr ptr) : base(ptr) { }
}