using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Hololux.Tello;
using Hololux.Tello.UI;

public class DroneHandler : MonoBehaviour
{
    private UDPClient_Tello telloClient;
    private TelloVideoRenderer _telloVideoRenderer;
    
    [SerializeField] private DroneUIView droneUIView;

    private void Start()
    {
        telloClient = new UDPClient_Tello();
        _telloVideoRenderer = FindObjectOfType<TelloVideoRenderer>(true);
    }
    
    private void OnEnable()
    {
        if (droneUIView != null)
        {
            droneUIView.OnVideoToggleButtonClicked += OnVideoToggleButtonClicked;
        }
    }
    
    private void OnDisable()
    {
        if (droneUIView != null)
        {
            droneUIView.OnVideoToggleButtonClicked -= OnVideoToggleButtonClicked;
        }
    }
    
    private void OnVideoToggleButtonClicked(object sender, bool toggle)
    {
        if (toggle)
        {
            StartStream();
        }
        else
        {
            StopStream();
        }
    }
    
    public void ConnectToTello()
    {
        telloClient.ConnectToTello("192.168.10.1", 8889);
        telloClient.IntitiateSDK();
    }

  
    public void CheckBattery()
    {
        telloClient.SendtoDrone("battery?");
        //Recieve();
    }

    public void TakeOff()
    {
        telloClient.SendtoDrone("takeoff");

    }
    public void MoveRight()
    {
        telloClient.SendtoDrone("right 50");

    }
    public void MoveRight(float distance)
    {
        telloClient.SendtoDrone("right " + ((int)distance).ToString());
    }


    public void MoveLeft()
    {
        telloClient.SendtoDrone("left 50");
    }

    public void MoveLeft(float distance)
    {
        telloClient.SendtoDrone("left " + ((int)distance).ToString());
    }


    public void Ascend()
    {
        telloClient.SendtoDrone("up 40");

    }

    public void Ascend(float distance)
    {
        telloClient.SendtoDrone("up " + ((int)distance).ToString());
    }

    public void Descend()
    {
        telloClient.SendtoDrone("down 40");

    }

    public void Descend(float distance)
    {
        telloClient.SendtoDrone("down " + ((int)distance).ToString());
    }


    public void MoveBack()
    {
        telloClient.SendtoDrone("back 50");

    }

    public void MoveBack(float distance)
    {
        telloClient.SendtoDrone("back " + ((int)distance).ToString());
    }
    public void MoveForward()
    {
        telloClient.SendtoDrone("forward 50");
    }

    public void MoveForward(float distance)
    {
        telloClient.SendtoDrone("forward " + ((int)distance).ToString());
    }


    public void land()
    {
        telloClient.SendtoDrone("land");
    }

    public void spin()
    {
        
        telloClient.SendtoDrone("cw 90");
        
        
    }
    public void Square()
    {
        telloClient.SendtoDrone("back 50");
        Sleep(8);
        telloClient.SendtoDrone("left 50");
        Sleep(8);
        telloClient.SendtoDrone("forward 50");
        Sleep(8);
        telloClient.SendtoDrone("right 50");
    }


    public void Bounce()
    {
        int verticalSpeed = 20;
        int distance = 60;
        int times = 2;
        int bounceDelay = distance / verticalSpeed;
        telloClient.SendtoDrone("up 90");
        for (int i = 0; i < times; i++)
        {
            telloClient.SendtoDrone("down" + " " + distance.ToString());
            Sleep(5);
            telloClient.SendtoDrone("up" + " " + distance.ToString());
            Sleep(5);
        }
    }

    public void Flip()
    {
        telloClient.SendtoDrone("flip r");
    }

    IEnumerator Sleep(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
    
    private void StartStream()
    {
        _telloVideoRenderer.StartVideo();
        telloClient.SendtoDrone("streamon");
    }
    
    private void StopStream()
    {
        _telloVideoRenderer.StopVideo();
        telloClient.SendtoDrone("streamoff");
    }
}
