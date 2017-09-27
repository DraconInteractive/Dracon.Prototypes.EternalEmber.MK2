/* QuadrupusCommands.cs

   A more advanced example of integrating Minibuffer. Used in the demo scenes.

   See HelloCommands.cs for simpler, step-by-step instructions how to add commands.
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SeawispHunter.MinibufferConsole.Extensions;

/* STEP 1. Reference the MinibufferConsole namespace. */
using SeawispHunter.MinibufferConsole;

namespace SeawispHunter.MinibufferConsole.Examples {

/**
   An example of Minibuffer commands and variables with a "quadrupus", a
   four-legged robot. Used in the scenes:

   - 0 Quadrapus
   - 1 Snake Fight

   It show cases how to add commands, variables, and key bindings to Minibuffer
   including some advanced features like dynamically generated commands and
   variables. Notes are also provided on instances where one of Minibuffer's
   components like MiniAction or MiniToggler could be used instead of the coded
   command.

   \see HelloCommands for simpler, step-by-step instructions how to add commands.
 */
public class QuadrapusCommands : MonoBehaviour {

  /* These four fields should be set up in the inspector. */
  public GameObject quadrapus;
  public Rigidbody quadrapusRootBody;
  public Transform cubePrefab;
  public Text _quote;

  /* Expose a field as a _variable_ in Minibuffer. */
  [Variable]
  public int score = 0;

  /* Expose a property as a _variable_ in Minibuffer. */
  [Variable]
  public string quote {
    get { return _quote.text; }
    set { _quote.text = value; }
  }

  /* This will show all the commands, variables, and key bindings known at
     compile-time in the inspector.  It does not need to be instantiated. */
  public MinibufferListing minibufferExtensions;

  void Start () {
/* STEP 2. Register all commands, variables, and key bindings with Minibuffer. */
    Minibuffer.Register(this);

    /* If you want to setup some other things at the start, using
       Minibuffer.With() is recommended. */

    // Minibuffer.With(minibuffer => {
    //     Keymap keymap = minibuffer.GetKeymap("user", true);
    //     keymap["C-d"]         = "cube-drop";
    //     /* We could setup some key bindings here, but this can also be done in
    //        the [Command("cube-drop", keyBinding = "C-d"] tag.  See below. */
    //
    //     /* Add a new variable at runtime. This variable would actually read-only. */
    //     minibuffer
    //     .RegisterVariable<string>(new Variable("dynamic-var"),
    //                               () => "my var",
    //                               (s) => { Minibuffer.instance.Message("Set " + s + " but not really."); });
    //   });
  }

/* STEP 3. Add [Command] attributes to any public method. */
  [Command(description = "Cheat code with ad hoc completions.")]
  public static string GoodCheatCode(/* Add custom ad hoc completions. */
                                     [Prompt(completions = new []
                                         { "GodMode", "KonamiCode" })]
                                     string str) {
    // ... your cheat implementation here ...
    return str + " activated.";
  }

  /* Or instead of custom completions, just use an enumeration. */
  public enum Cheat { GodMode, KonamiCode };

  /* Command methods may be static or not. */
  [Command(description = "Cheat code with completions from enumeration.")]
  public string BetterCheatCode(Cheat c) {
    // ... your cheat implementation here ...
    return c + " activated.";
  }

  /** Expose methods as commands in Minibuffer. */
  [Command("quadrapus-detach",
           description = "Drop the quadrapus from its hook")]
  public string Detach() {
    quadrapusRootBody.isKinematic = false;
    /* For convenience, if we return a string from a command, it'll be
       reported as a message. */
    return "Detached.";
    /* This is equivalent to:

    Minibuffer.instance.Message("Detached.");
    */

    /* Note that quadrapus-detach's functionality could be easily replicated by
       a MiniAction component with no code. */
  }

  /* The quadrapus has two scripts attached to each body part, we toggle them on
     and off. */
  [Command("quadrapus-twitch",
           description = "Toggle quadrapus twitching")]
  public void Twitch() {
    quadrapus.GetComponentsInChildren<Twitchy>().Each(x => x.enabled = !x.enabled);
    quadrapus.GetComponentsInChildren<Snakey>().Each(x => x.enabled = false);

    /* Each() is an extension method that takes an action. It is purely for
       convenience. It is equivalent to this code:

    foreach(var x in quadrapus.GetComponentsInChildren<Snakey>()) {
      x.enabled = false;
    }
    */
  }

  [Command("quadrapus-snake",
           description = "Toggle sinusoidal movement")]
  public void Snake() {
    quadrapus.GetComponentsInChildren<Snakey>().Each(x => x.enabled = !x.enabled);
    quadrapus.GetComponentsInChildren<Twitchy>().Each(x => x.enabled = false);
  }

  [Command(description = "Set material of quadrapus. Shows preview in editor.")]
  public string QuadrapusMaterial(Material m) {
    foreach(var renderer in quadrapus
            .GetComponentsInChildren<MeshRenderer>())
      renderer.material = m;
    return "Changed material to " + m.name + ".";
  }

  [Command(description = "Set color of quadrapus")]
  // requireMatch is false so that coercion may be used.
  public string QuadrapusColor([Prompt(requireMatch = false)]
                               Color c) {
    var renderers = quadrapus.GetComponentsInChildren<MeshRenderer>();
    foreach(var renderer in renderers) {
      renderer.material.SetColor("_Color", c);
    }
    return "Changed color to " + c + ".";
  }

  /* Minibuffer comes with its own built-in game! Can you guess the number? */
  [Command("guess-a-number")]
  public string GuessANumber(int guess) {
    if (guess > 42)
      return "Too high.";
    else if (guess < 42)
      return "Too low.";
    else
      return "You got it! 42, the answer to life, the universe and everything!";
  }

  [Command("cube-drop",
           description = "Drop a cube into the scene",
           /* Either standard or Emacs key sequence notation can be used to set
              key bindings. */
           // keyBinding = "C-d",
           keyBinding = "ctrl-d")]
  public IEnumerator CubeDrop([UniversalArgument]
                              int count) {
    for (int i = 0; i < count; i++) {
      var pos = UnityEngine.Random.insideUnitSphere;
      Instantiate(cubePrefab,
                  quadrapusRootBody.transform.position + pos * 5f + Vector3.up * 5f,
                  Quaternion.identity);
      yield return null;
    }
    if (count == 1 && ! seenCubeDropTip) {
      Minibuffer.instance.Message("Try C-u 1000 M-x cube-drop for some #Cubefetti!");
      seenCubeDropTip = true;
    }
  }
  private bool seenCubeDropTip = false;

  [Command(description = "Go to seawisphunter.com")]
  /** This is used by the Logo Button, but we can easily make it an
      interactive command too. */
  public void GotoSeawispHunter() {
    Application.OpenURL("http://seawisphunter.com");
  }

  [Command(description = "Randomly pick a cheerful color",
           keyBinding = "ctrl-c b")]
  public void RandomizeBackgroundColor() {
    Camera.main.backgroundColor = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
  }

  [Command]
  public string AddDynamicCommands() {
    var minibuffer = Minibuffer.instance;
    minibuffer
      .RegisterCommand("my-dynamic-command", () => {
          minibuffer.Message("Make commands on the fly!");
        });

    minibuffer
      .RegisterCommand("my-dynamic-command-with-args", (string s) => {
          minibuffer.Message("They can take arguments too!  Like '{0}'.", s);
        });
    return "Added my-dynamic-command and my-dynamic-command-with-args.";
  }

  /* This command is implemented in UnityCommands. If there was a particular
     game object, however, that you wanted to toggle then the MiniToggler
     component might be easier. */
  // [Command]
  // public void ToggleGameObject(GameObject go) {
  //   go.SetActive(! go.activeSelf);
  // }

  /* This functionality actually works now, but it's in TwitterCommands. */
  // [Command]
  // public string TweetScreenshot([Prompt("To twitter account: ",
  //                                       completions = new []
  //                                           { "@shanecelis",
  //                                             "@stupidmassive",
  //                                             "@unormal" })]
  //                               string str) {
  //   Minibuffer.instance.ExecuteCommand("capture-screenshot");
  //   // ... Magic twitter code goes here ...
  //   return "Tweeted screenshot to " + str + "."; // Not really.
  //                                                // Good idea though!
  // }

/* STEP 4. Unregister if necessary. */
  void OnDestroy() {
    /* Unregister from Minibuffer. It's undefined what happens if you run
       commands whose objects have been destroyed. */
    Minibuffer.Unregister(this);

    /* If you've added some things at runtime, you may want to have a block like
       this: */
    // Minibuffer.With(m => {
    //     m.UnregisterCommand("my-dynamic-command");
    //     m.UnregisterCommand("my-dynamic-command-with-args");
    //     // m.UnregisterVariable("dynamic-var");
    //   });
  }

}
}
