using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class SettingsBinder : MonoBehaviour
{
    public RenderPipelineAsset rp;
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown AADropdown;
    public TMP_Dropdown TexDropdown;
    public TMP_Dropdown ShadowsDropdown;

    public int qualityLevel = 0;
    public int AALevel = 0;
    public int TexLevel = 0;
    public int ShadowLevel = 0;

    bool loadedQuality = false;

    private void Awake()
    {
        if (QualitySettings.GetQualityLevel() != 4)
        {
            qualityLevel = QualitySettings.GetQualityLevel();
            AALevel = QualitySettings.antiAliasing;
            TexLevel = QualitySettings.masterTextureLimit;
            ShadowLevel = (int)QualitySettings.shadowResolution;
        }
        qualityDropdown.value = qualityLevel;
        AADropdown.value = AALevel;
        TexDropdown.value = TexLevel;
        ShadowsDropdown.value = ShadowLevel;
        loadedQuality = true;
    }

    //public void SwitchPage(int page)
    //{ }

    public void SetQuality(int level)
    {
        if  (level == 6)
        {
            GraphicsSettings.renderPipelineAsset = rp;
        }
        else
        {
            GraphicsSettings.renderPipelineAsset = default;
        }
        if (!loadedQuality) return;
        qualityLevel = level;
        QualitySettings.SetQualityLevel(qualityLevel, true);
        AALevel = QualitySettings.antiAliasing;
        TexLevel = QualitySettings.masterTextureLimit;
        ShadowLevel = (int)QualitySettings.shadowResolution;
        //qualityDropdown.value = qualityLevel;
        //AADropdown.value = AALevel;
        //TexDropdown.value = TexLevel;
        //ShadowsDropdown.value = ShadowLevel;
        Debug.Log("Updated Quality field");
    }
    public void SetQuality(int level, bool updateFields)
    {
        qualityLevel = level;
        qualityDropdown.value = qualityLevel;
        if (updateFields)
        {
            AALevel = QualitySettings.antiAliasing;
            TexLevel = QualitySettings.masterTextureLimit;
            ShadowLevel = (int)QualitySettings.shadowResolution;
            AADropdown.value = AALevel;
            TexDropdown.value = TexLevel;
            ShadowsDropdown.value = ShadowLevel;
            Debug.Log("Updated Quality field");
        }
        QualitySettings.SetQualityLevel(qualityLevel, true);
    }
    public void SetMSAA(int level)
    { 
        if (!loadedQuality) return;
        if (QualitySettings.GetQualityLevel() != 5) SetQuality(5, false);
        AALevel = level;
        QualitySettings.antiAliasing = AALevel;
        Debug.Log("Updated AA field");
    }
    public void SetTex(int level)
    {  
        if (!loadedQuality) return;
        if (QualitySettings.GetQualityLevel() != 5) SetQuality(5, false);
        TexLevel = level;
        QualitySettings.masterTextureLimit = UIToTex(TexLevel + 1);
        Debug.Log("Updated Tex field");
    }
    public void SetShadows(int level)
    { 
        if (!loadedQuality) return;
        if (QualitySettings.GetQualityLevel() != 5) SetQuality(5, false);
        ShadowLevel = level;
        QualitySettings.shadowResolution = (ShadowResolution)ShadowLevel;
        Debug.Log("Updated Shadow field");
    }

    public void RemoveData()
    {
        PlayerPrefs.DeleteAll();
    }

    //Yed no
    int UIToTex(int put)
    {
        int res = 0;
        switch (put)
        { 
            case 0:
                res = 4;
                break;
            case 1:
                res = 3;
                break;
            case 2:
                res = 2;
                break;
            case 3:
                res = 1;
                break;
            case 4:
                res = 0;
                break;
            default:
                break;
        }
        return res;
    }

}
