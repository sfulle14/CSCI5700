/* 
 * Copyright (C) 2015 Christoph Kutza
 * 
 * Please refer to the LICENSE file for license information
 */
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Byn.Awrtc;
using Byn.Awrtc.Unity;
using Byn.Unity.Examples;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// This class + prefab is a complete app allowing to call another app using a shared text or password
/// to meet online.
/// 
/// It supports Audio, Video and Text chat. Audio / Video can optionally turned on/off via toggles.
/// 
/// After the join button is pressed the (first) app will initialize a native webrtc plugin 
/// and contact a server to wait for incoming connections under the given string.
/// 
/// Another instance of the app can connect using the same string. (It will first try to
/// wait for incoming connections which will fail as another app is already waiting and after
/// that it will connect to the other side)
/// 
/// The important methods are "Setup" to initialize the call class (after join button is pressed) and
/// "Call_CallEvent" which reacts to events triggered by the class.
/// 
/// Also make sure to use your own servers for production (uSignalingUrl and uStunServer).
/// 
/// NOTE: Currently, only 1 to 1 connections are supported. This will change in the future.
/// </summary>
public class CallAppUi : MonoBehaviour
{

    /// <summary>
    /// Texture of the local video
    /// </summary>
    protected Texture2D mLocalVideoTexture = null;

    /// <summary>
    /// Texture of the remote video
    /// </summary>
    protected Texture2D mRemoteVideoTexture = null;


    [Header("Setup panel")]
    /// <summary>
    /// Panel with the join button. Will be hidden after setup
    /// </summary>
    public RectTransform uSetupPanel;
    public RectTransform uMainSetupPanel;

    /// <summary>
    /// Input field used to enter the room name.
    /// </summary>
    public InputField uRoomNameInputField;
    /// <summary>
    /// Join button to connect to a server.
    /// </summary>
    public Button uJoinButton;

    public Toggle uAudioToggle;
    public Toggle uVideoToggle;
    public Dropdown uVideoDropdown;


    [Header("Settings panel")]
    public RectTransform uSettingsPanel;
    public InputField uIdealWidth;
    public InputField uIdealHeight;
    public InputField uIdealFps;
    public Toggle uRejoinToggle;
    public Toggle uLocalVideoToggle;


    [Header("Video and Chat panel")]
    public RectTransform uInCallBase;
    public RectTransform uVideoPanel;
    public RectTransform uChatPanel;
    public RectTransform uVideoOverlay;

    //added student code
    //public RectTransform uInstructionWindow;

    [Header("Default positions/transformations")]
    public RectTransform uVideoBase;
    public RectTransform uChatBase;


    [Header("Fullscreen positions/transformations")]
    public RectTransform uFullscreenPanel;
    public RectTransform uVideoBaseFullscreen;
    public RectTransform uChatBaseFullscreen;




    [Header("Chat panel elements")]
    /// <summary>
    /// Input field to enter a new message.
    /// </summary>
    public InputField uMessageInputField;

    /// <summary>
    /// Output message list to show incoming and sent messages + output messages of the
    /// system itself.
    /// </summary>
    public MessageList uMessageOutput;


    /// <summary>
    /// Send button.
    /// </summary>
    public Button uSendMessageButton;

    /// <summary>
    /// Shutdown button. Disconnects all connections + shuts down the server if started.
    /// </summary>
    public Button uShutdownButton;

    /// <summary>
    /// Toggle to switch the microphone on / off. 
    /// </summary>
    public Toggle uMuteToggle;

    /// <summary>
    /// Toggle to switch the loudspeakers on / off. Only for mobile visible.
    /// </summary>
    public Toggle uLoudspeakerToggle;

    /// <summary>
    /// Slider to just the remote users volume.
    /// </summary>
    public Slider uVolumeSlider;

    /// <summary>
    /// Slider to just the remote users volume.
    /// </summary>
    public Text uOverlayInfo;

    [Header("Video panel elements")]
    /// <summary>
    /// Image of the local camera
    /// </summary>
    public RawImage uLocalVideoImage;

    /// <summary>
    /// Image of the remote camera
    /// </summary>
    public RawImage uRemoteVideoImage;

    [Header("Resources")]
    public Texture2D uNoCameraTexture;

    protected bool mFullscreen = false;


    protected CallApp mApp;

    //added student code
    
    //Timer for both leader and follower
    public Timer mTimer;

    //Follower Canvases to be activated when Leader starts exercise
    public FollowerCanvases mFollow;


    private float mVideoOverlayTimeout = 0;
    private static readonly float sDefaultOverlayTimeout = 8;

    private bool mHasLocalVideo = false;
    private int mLocalVideoWidth = -1;
    private int mLocalVideoHeight = -1;
    private int mLocalFps = 0;
    private int mLocalFrameCounter = 0;
    private int mLocalRotation = 0;

    private FramePixelFormat mLocalVideoFormat = FramePixelFormat.Invalid;

    private bool mHasRemoteVideo = false;
    private int mRemoteVideoWidth = -1;
    private int mRemoteVideoHeight = -1;
    private int mRemoteFps = 0;
    private int mRemoteRotation = 0;
    private int mRemoteFrameCounter = 0;
    private FramePixelFormat mRemoteVideoFormat = FramePixelFormat.Invalid;

    private float mFpsTimer = 0;

    private string mPrefix = "CallAppUI_";
    private static readonly string PREF_AUDIO = "audio";
    private static readonly string PREF_VIDEO = "video";
    private static readonly string PREF_VIDEODEVICE = "videodevice";
    private static readonly string PREF_ROOMNAME = "roomname";
    private static readonly string PREF_IDEALWIDTH = "idealwidth";
    private static readonly string PREF_IDEALHEIGHT = "idealheight";
    private static readonly string PREF_IDEALFPS = "idealfps";
    private static readonly string PREF_REJOIN = "rejoin";
    private static readonly string PREF_LOCALVIDEO = "localvideo";

    public bool uLoadSettings = true;

    protected virtual void Awake()
    {
        mApp = GetComponent<CallApp>();

        if (Application.isMobilePlatform == false)
            uLoudspeakerToggle.gameObject.SetActive(false);
        mPrefix += this.gameObject.name + "_";
        if (uLoadSettings)
        {
            LoadSettings();
        }
        CheckSettings();
    }

    protected virtual void Start()
    {
        if (this.uVideoOverlay != null)
        {
            this.uVideoOverlay.gameObject.SetActive(false);
        }
    }


    private void SaveSettings()
    {
        PlayerPrefsSetBool(mPrefix + PREF_AUDIO, uAudioToggle.isOn);
        PlayerPrefsSetBool(mPrefix + PREF_VIDEO, uVideoToggle.isOn);
        PlayerPrefs.SetString(mPrefix + PREF_VIDEODEVICE, GetSelectedVideoDevice());
        PlayerPrefs.SetString(mPrefix + PREF_ROOMNAME, uRoomNameInputField.text);
        PlayerPrefs.SetString(mPrefix + PREF_IDEALWIDTH, uIdealWidth.text);
        PlayerPrefs.SetString(mPrefix + PREF_IDEALHEIGHT, uIdealHeight.text);
        PlayerPrefs.SetString(mPrefix + PREF_IDEALFPS, uIdealFps.text);
        PlayerPrefsSetBool(mPrefix + PREF_REJOIN, uRejoinToggle.isOn);
        PlayerPrefsSetBool(mPrefix + PREF_LOCALVIDEO, uLocalVideoToggle.isOn);
        PlayerPrefs.Save();
    }

    private string mStoredVideoDevice = null;

    /// <summary>
    /// Loads the ui state from last use
    /// </summary>
    private void LoadSettings()
    {
        uAudioToggle.isOn = PlayerPrefsGetBool(mPrefix + PREF_AUDIO, true);
        uVideoToggle.isOn = PlayerPrefsGetBool(mPrefix + PREF_VIDEO, true);
        //can't select this immediately because we don't know if it is valid yet
        mStoredVideoDevice = PlayerPrefs.GetString(mPrefix + PREF_VIDEODEVICE, null);
        uRoomNameInputField.text = PlayerPrefs.GetString(mPrefix + PREF_ROOMNAME, Application.productName + "_address");
        uIdealWidth.text = PlayerPrefs.GetString(mPrefix + PREF_IDEALWIDTH, "320");
        uIdealHeight.text = PlayerPrefs.GetString(mPrefix + PREF_IDEALHEIGHT, "240");
        uIdealFps.text = PlayerPrefs.GetString(mPrefix + PREF_IDEALFPS, "30");
        uRejoinToggle.isOn = PlayerPrefsGetBool(mPrefix + PREF_REJOIN, false);
        uLocalVideoToggle.isOn = PlayerPrefsGetBool(mPrefix + PREF_LOCALVIDEO, true);
    }

    public void ResetSettings()
    {
        PlayerPrefs.DeleteKey(mPrefix + PREF_AUDIO);
        PlayerPrefs.DeleteKey(mPrefix + PREF_VIDEO);
        PlayerPrefs.DeleteKey(mPrefix + PREF_VIDEODEVICE);
        PlayerPrefs.DeleteKey(mPrefix + PREF_ROOMNAME);
        PlayerPrefs.DeleteKey(mPrefix + PREF_IDEALWIDTH);
        PlayerPrefs.DeleteKey(mPrefix + PREF_IDEALHEIGHT);
        PlayerPrefs.DeleteKey(mPrefix + PREF_IDEALFPS);
        PlayerPrefs.DeleteKey(mPrefix + PREF_REJOIN);
        PlayerPrefs.DeleteKey(mPrefix + PREF_LOCALVIDEO);
        LoadSettings();
        CheckSettings();
    }

    private void CheckSettings()
    {
        if (ExampleGlobals.HasAudioPermission() == false)
        {
            uAudioToggle.isOn = false;
        }
        if (ExampleGlobals.HasVideoPermission() == false)
        {
            uVideoToggle.isOn = false;
        }
    }


    public void OnAudioSettingsChanged()
    {
        if (uAudioToggle.isOn && ExampleGlobals.HasAudioPermission() == false)
        {
            StartCoroutine(RequestAudioPermissions());
        }
    }
    public void OnVideoSettingsChanged()
    {
        if (uVideoToggle.isOn && ExampleGlobals.HasVideoPermission() == false)
        {
            StartCoroutine(RequestVideoPermissions());
        }
    }

    IEnumerator RequestAudioPermissions()
    {
        yield return ExampleGlobals.RequestAudioPermission();
        uAudioToggle.isOn = ExampleGlobals.HasAudioPermission();
    }
    IEnumerator RequestVideoPermissions()
    {
        yield return ExampleGlobals.RequestVideoPermission();
        uVideoToggle.isOn = ExampleGlobals.HasVideoPermission();
    }

    private static bool PlayerPrefsGetBool(string name, bool defval)
    {
        int def = 0;
        if (defval)
            def = 1;
        return PlayerPrefs.GetInt(name, def) == 1 ? true : false;
    }

    private static void PlayerPrefsSetBool(string name, bool value)
    {
        PlayerPrefs.SetInt(name, value ? 1 : 0);
    }

    private string GetSelectedVideoDevice()
    {
        if (uVideoDropdown.value <= 0 || uVideoDropdown.value >= uVideoDropdown.options.Count)
        {
            //return null if the first element is selected ("Any") or the ui returns
            //invalid values. This will trigger the app to pick a default device
            return null;
        }
        else
        {
            string devname = uVideoDropdown.options[uVideoDropdown.value].text;
            return devname;
        }
    }

    private static int TryParseInt(string value, int defval)
    {
        int result;
        if (int.TryParse(value, out result) == false)
        {
            result = defval;
        }
        return result;
    }

    private void SetupCallApp()
    {
        mApp.SetVideoDevice(GetSelectedVideoDevice());
        mApp.SetAudio(uAudioToggle.isOn);
        mApp.SetVideo(uVideoToggle.isOn);

        int width = TryParseInt(uIdealWidth.text, 320);
        int height = TryParseInt(uIdealHeight.text, 240);
        int fps = TryParseInt(uIdealFps.text, 30);
        mApp.SetIdealResolution(width, height);
        mApp.SetIdealFps(fps);
        mApp.SetAutoRejoin(uRejoinToggle.isOn);
        mApp.SetShowLocalVideo(uLocalVideoToggle.isOn);
        mApp.SetupCall();
        EnsureLength();
        Append("Trying to listen on address " + uRoomNameInputField.text);
        mApp.Join(uRoomNameInputField.text);
    }

    public void ToggleSettings()
    {
        uMainSetupPanel.gameObject.SetActive(!uMainSetupPanel.gameObject.activeSelf);
        uSettingsPanel.gameObject.SetActive(!uSettingsPanel.gameObject.activeSelf);
    }

    public void ToggleSetup()
    {
        uSetupPanel.gameObject.SetActive(!uSetupPanel.gameObject.activeSelf);
    }



    /// <summary>
    /// Updates the local video. If the frame is null it will hide the video image
    /// </summary>
    /// <param name="frame"></param>
    public virtual void UpdateLocalTexture(IFrame frame, FramePixelFormat format)
    {
        if (uLocalVideoImage != null)
        {
            if (frame != null)
            {
                UnityMediaHelper.UpdateTexture(frame, ref mLocalVideoTexture);
                uLocalVideoImage.texture = mLocalVideoTexture;
                if (uLocalVideoImage.gameObject.activeSelf == false)
                {
                    uLocalVideoImage.gameObject.SetActive(true);
                }
                //apply rotation
                //watch out uLocalVideoImage should be scaled -1 X to make the local camera appear mirrored
                //it should also be scaled -1 Y because Unity reads the image from bottom to top
                uLocalVideoImage.transform.localRotation = Quaternion.Euler(0, 0, frame.Rotation);

                mHasLocalVideo = true;
                mLocalFrameCounter++;
                mLocalVideoWidth = frame.Width;
                mLocalVideoHeight = frame.Height;
                mLocalVideoFormat = format;
                mLocalRotation = frame.Rotation;
            }
            else
            {
                //app shutdown. reset values
                mHasLocalVideo = false;
                uLocalVideoImage.texture = null;
                uLocalVideoImage.transform.localRotation = Quaternion.Euler(0, 0, 0);
                uLocalVideoImage.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Updates the remote video. If the frame is null it will hide the video image.
    /// </summary>
    /// <param name="frame"></param>
    public virtual void UpdateRemoteTexture(IFrame frame, FramePixelFormat format)
    {
        if (uRemoteVideoImage != null)
        {
            if (frame != null)
            {
                UnityMediaHelper.UpdateTexture(frame, ref mRemoteVideoTexture);
                uRemoteVideoImage.texture = mRemoteVideoTexture;
                //watch out: due to conversion from WebRTC to Unity format the image is flipped (top to bottom)
                //this also inverts the rotation
                uRemoteVideoImage.transform.localRotation = Quaternion.Euler(0, 0, frame.Rotation * -1);
                mHasRemoteVideo = true;
                mRemoteVideoWidth = frame.Width;
                mRemoteVideoHeight = frame.Height;
                mRemoteVideoFormat = format;
                mRemoteRotation = frame.Rotation;
                mRemoteFrameCounter++;
            }
            else
            {
                mHasRemoteVideo = false;
                uRemoteVideoImage.texture = uNoCameraTexture;
                uRemoteVideoImage.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }


    /// <summary>
    /// Updates the dropdown menu based on the current video devices and toggle status
    /// </summary>
    public void UpdateVideoDropdown()
    {
        uVideoDropdown.ClearOptions();
        uVideoDropdown.AddOptions(new List<string>(mApp.GetVideoDevices()));
        uVideoDropdown.interactable = mApp.CanSelectVideoDevice();

        //restore the stored selection if possible
        if (uVideoDropdown.interactable && mStoredVideoDevice != null)
        {
            int index = 0;
            foreach (var opt in uVideoDropdown.options)
            {
                if (opt.text == mStoredVideoDevice)
                {
                    uVideoDropdown.value = index;
                }
                index++;
            }
        }
    }
    public void VideoDropdownOnValueChanged(int index)
    {
        //moved to SetupCallApp
    }


    /// <summary>
    /// Adds a new message to the message view
    /// </summary>
    /// <param name="text"></param>
    public void Append(string text)
    {
        if (uMessageOutput != null)
        {
            uMessageOutput.AddTextEntry(text);
        }
        Debug.Log("Chat output: " + text);
    }

    private void SetFullscreen(bool value)
    {
        mFullscreen = value;
        if (mFullscreen)
        {
            uVideoPanel.SetParent(uVideoBaseFullscreen, false);
            uChatPanel.SetParent(uChatBaseFullscreen, false);
            uInCallBase.gameObject.SetActive(false);
            uFullscreenPanel.gameObject.SetActive(true);
        }
        else
        {
            uVideoPanel.GetComponent<RectTransform>().SetParent(uVideoBase, false);
            uChatPanel.GetComponent<RectTransform>().SetParent(uChatBase, false);
            uInCallBase.gameObject.SetActive(true);
            uFullscreenPanel.gameObject.SetActive(false);
        }
    }
    public void Fullscreen()
    {

        bool newValues = !mFullscreen;

        //just in case: make sure fullscreen button is ignored if in setup mode
        if (newValues == true && uSetupPanel.gameObject.activeSelf)
            return;
        SetFullscreen(newValues);

        transform.SetAsLastSibling();
    }

    public void ShowOverlay()
    {
        if (this.uVideoOverlay == null)
        {
            Debug.LogError("VideoOverlay transform is missing.");
            return;
        }
        if (this.uVideoOverlay.gameObject.activeSelf)
        {
            this.uVideoOverlay.gameObject.SetActive(false);
            mVideoOverlayTimeout = 0;
        }
        else
        {
            this.uVideoOverlay.gameObject.SetActive(true);
            mVideoOverlayTimeout = sDefaultOverlayTimeout;
        }
    }
    /// <summary>
    /// Shows the setup screen or the chat + video
    /// </summary>
    /// <param name="showSetup">true Shows the setup. False hides it.</param>
    public void SetGuiState(bool showSetup)
    {
        if (showSetup)
        {
            //fill the video dropbox
            UpdateVideoDropdown();
        }
        uSetupPanel.gameObject.SetActive(showSetup);


        uSendMessageButton.interactable = !showSetup;
        uShutdownButton.interactable = !showSetup;
        uMessageInputField.interactable = !showSetup;

        //student added code
        //uInstructionWindow.interactable = !showSetup;

        //this is going to hide the textures until it is updated with a new frame update
        UpdateLocalTexture(null, FramePixelFormat.Invalid);
        UpdateRemoteTexture(null, FramePixelFormat.Invalid);
        SetFullscreen(false);
    }

    /// <summary>
    /// Join button pressed. Tries to join a room.
    /// </summary>
    public void JoinButtonPressed()
    {
        //change procedure to open a blank page 
        SaveSettings();
        SetupCallApp();
    }

    private void EnsureLength()
    {
        if (uRoomNameInputField.text.Length > CallApp.MAX_CODE_LENGTH)
        {
            uRoomNameInputField.text = uRoomNameInputField.text.Substring(0, CallApp.MAX_CODE_LENGTH);
        }
    }

    public string GetRoomname()
    {
        EnsureLength();
        return uRoomNameInputField.text;
    }

    /// <summary>
    /// This is called if the send button
    /// </summary>
    public void SendButtonPressed()
    {
        //get the message written into the text field
        string msg = uMessageInputField.text;
        SendMsg(msg);
    }

    /// <summary>
    /// User either pressed enter or left the text field
    /// -> if return key was pressed send the message
    /// </summary>
    public void InputOnEndEdit()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            string msg = uMessageInputField.text;
            SendMsg(msg);
        }
    }

    /// <summary>
    /// Sends a message to the other end
    /// </summary>
    /// <param name="msg"></param>
    private void SendMsg(string msg)
    {
        if (String.IsNullOrEmpty(msg))
        {
            //never send null or empty messages. webrtc can't deal with that
            return;
        }

        //Append(msg);
        //mApp.Send(msg);

        msg = "R1:" + msg;
        Interface_events(msg);

        //reset UI
        uMessageInputField.text = "";
        uMessageInputField.Select();
    }

    //Custom method added to the class by Forrest to handle the user-interface events.
    public void Interface_events(string input_string)
    {
        //all strings sent to this method should be of the following format.
        //"R1:string of information."
        //R1 represents the input_code which will determine what event will occur.
        //The rest of the string after the ':' will provide additional information depending on the event.
        string input_code = input_string.Substring(0, 2);
        bool leader_role = false; //if leader role is false, then follower was chosen.

        //with the given string code, determine what action to take in the UI
        switch (input_code)
        {
            case "R1":
                Append("CHAT:" + input_string.Substring(3));
                mApp.Send("CHAT:" + input_string.Substring(3));
                break;
            case "R2":
                if (leader_role == false)
                {
                    //leader is informed of mistake made by follower
                    mApp.Send("BOOL: Whoops, an error has been made.");
                    mApp.Send("Follower will now have to choice.");
                }
                break;
            case "R3":
                //Console.WriteLine("LOG:message has been recorded.");
                break;
            case "R4":
                //Leader ready button has been pressed.
                break;
            case "R5":
                //Follower ready button has been pressed.
                break;
            case "R6":
                //Console.WriteLine("Both players are ready. Activate the interfaces for follower and start the timer.");
                mTimer.startTimer();

                //activate interface for follower
                mFollow.begin_exercise();
                break;
            case "R7":
                //Console.WriteLine("Leader role has been chosen. navigate to leader scene and activate bool for user to be true");
                leader_role = true;
                SceneManager.LoadScene(3);
                break;
            case "R8":
                //Console.WriteLine("Follower role has been chosen. navigate to follower scene and activate bool for user to be false");
                leader_role = false;
                SceneManager.LoadScene(4);
                break;
            case "R9":
                //Console.WriteLine("Follower wants to reset everything.");
                if (leader_role == false)
                {
                    //might just move to scene again to see if that works
                    SceneManager.LoadScene(4);
                }
                break;
            case "T1":
                //Console.WriteLine("Follower wants to try and fix mistake. Allow game to continue and reset mistake indications.");
                mTimer.startTimer();
                break;
            case "T2":
                //Console.WriteLine("Step completed. Update progress bar.");
                break;
            case "T3":
                //Console.WriteLine("Time has run out. Move to results scene.");
                SceneManager.LoadScene(5);
                break;
            case "T4":
                //Console.WriteLine("The exercise has been completed. Pause timer and show results.");
                SceneManager.LoadScene(5);
                break;
            case "T5":
                //Console.WriteLine("Take a screenshot of the environment.");
                break;
            default:
                //Console.WriteLine("Error in switch statement.");
                //just in case.
                break;
        }
    }

    //event functions for scenes
    //main menu functions
    public void Start_button_pressed()
    {
        //move to scene role_selection
        SceneManager.LoadScene(2);
    }

    public void How_to_play_button_pressed()
    {
        //move to how to play scene
        SceneManager.LoadScene(1);
    }

    public void Option_button_pressed()
    {
        //options button event from main menu
    }

    public void Quit_button_pressed()
    {
        //end program event from main menu
    }

    //Leader has hit the start button to indicate beginning of exercise.
    public void Begin_exercise()
    {
        Interface_events("R6");
    }

    //role_menu buttons
    public void Leader_button_pressed()
    {
        //go to leader scene
        Interface_events("R7");
    }

    public void Follower_button_pressed()
    {
        //go to follower scene
        Interface_events("R8");
    }

    //Fix_mistake buttons
    public void Start_over_pressed()
    {
        Interface_events("R9");
    }

    public void Fix_mistake_pressed()
    {
        Interface_events("T1");
    }

    /// <summary>
    /// Shutdown button pressed. Shuts the network down.
    /// </summary>
    public void ShutdownButtonPressed()
    {
        mApp.ResetCall();
    }

    public void OnVolumeChanged(float value)
    {
        mApp.SetRemoteVolume(value);
    }

    public void OnLoudspeakerToggle()
    {
        //watch out the on state of the toggle means
        //the icon is crossed out thus
        //isOn == true means the speaker is off
        bool state = !uLoudspeakerToggle.isOn;
        mApp.SetLoudspeakerStatus(state);
        //read if the state actually changed
        RefreshLoudspeakerToggle();
    }
    private void RefreshLoudspeakerToggle()
    {
        bool state = mApp.GetLoudspeakerStatus();
        uLoudspeakerToggle.isOn = !state;
    }
    public void OnMuteToggle()
    {
        bool state = uMuteToggle.isOn;
        mApp.SetMute(state);
        //read if the state actually changed
        RefreshMuteToggle();
    }


    private void RefreshMuteToggle()
    {
        bool state = mApp.IsMute();
        uMuteToggle.isOn = state;
    }
    
    protected virtual void Update()
    {
        if(mVideoOverlayTimeout > 0)
        {
            string local = "Local:";
            if (mHasLocalVideo == false)
            {
                local += "no video";
            }
            else
            {
                local += mLocalVideoWidth + "x" + mLocalVideoHeight + Enum.GetName(typeof(FramePixelFormat), mLocalVideoFormat) + " r:" + mLocalRotation + " FPS:" + mLocalFps;
            }
            string remote = "Remote:";
            if (mHasRemoteVideo == false)
            {
                remote += "no video";
            }
            else
            {
                remote += mRemoteVideoWidth + "x" + mRemoteVideoHeight + Enum.GetName(typeof(FramePixelFormat), mRemoteVideoFormat)  + " r:" + mRemoteRotation + " FPS:" + mRemoteFps;
            }

            uOverlayInfo.text = local + "\n" + remote;
            mVideoOverlayTimeout -= Time.deltaTime;
            if(mVideoOverlayTimeout <= 0)
            {
                mVideoOverlayTimeout = 0;
                uVideoOverlay.gameObject.SetActive(false);
            }
        }

        float fpsTimeDif = Time.realtimeSinceStartup - mFpsTimer;
        if(fpsTimeDif > 1)
        {
            mLocalFps = Mathf.RoundToInt( mLocalFrameCounter / fpsTimeDif);
            mRemoteFps = Mathf.RoundToInt(mRemoteFrameCounter / fpsTimeDif);
            mFpsTimer = Time.realtimeSinceStartup;
            mLocalFrameCounter = 0;
            mRemoteFrameCounter = 0;
        }

        //work around for the loudspeaker button on mobile devices
        //loudspeaker state might be changed globally via
        //different APIs or multiple parallel callapps
        //we refresh the button every few frames to make sure it
        //shows the correct icon as this has confused users in the past
        if (uLoudspeakerToggle != null && uLoudspeakerToggle.IsActive())
        {
            if(Time.frameCount % 30 == 0)
                RefreshLoudspeakerToggle();
        }
    }
}
