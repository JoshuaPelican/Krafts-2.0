using UnityEngine;
using UnityEngine.Events;

public class ToolManager : MonoBehaviour
{
    public static Tool selectedTool;

    [SerializeField] Tool[] allTools;

    private void Start()
    {
        //Defaults the selected tool to be the manipulate tool
        Swap(allTools[0]);
    }

    private void Update()
    {
        //Uses the selected tool's left click capabilities 
        if (Input.GetMouseButtonDown(0))
        {
            selectedTool.LeftMouseDown();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            selectedTool.LeftMouseUp();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            selectedTool.RightMouseDown();
        }

        ToolSwapInput();
    }

    //Checks if numkeys are pressed to swap tools
    void ToolSwapInput()
    {
        for (int i = 0; i < allTools.Length; i++)
        {

            if (Input.GetKeyDown((i + 1).ToString()))
            {
                Swap(allTools[i]);
            }
        }
    }

    //Swaps the currently selected tool with a new one, if it is not the same one
    public void Swap(Tool tool)
    {
        if(tool != selectedTool)
        {
            selectedTool?.SwappedFrom();

            selectedTool = tool;

            selectedTool.SwappedTo();
        }
    }
}
