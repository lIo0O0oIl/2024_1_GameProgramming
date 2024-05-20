using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public AIAgent aiAgent;
    public AIAnimation aiAnimation;

    public TestAI testAI;

    private void Awake()
    {
        Instance = this;
    }
}
