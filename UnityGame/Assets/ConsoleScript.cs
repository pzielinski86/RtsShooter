using UnityEngine;
using System.Collections;
using Game.Core.Controls.CommandsLine;
using Game.Core.Logs;
using Game.Core.Unity;

public class ConsoleScript : MonoBehaviour
{

    private CommandWindow _cmd;
    // Use this for initialization
    void Start()
    {

        var logger = new UnityLogger();
        var commandsLoader = new CommandsLoader(logger);
        var commands = commandsLoader.Load();

        _cmd = new CommandWindow(new UnityInput(), new UnityGuiAdapter(), new CommandLineExecuter(logger, commands),
            logger);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        _cmd.UpdateGui(Screen.width, Screen.height);
    }
}
