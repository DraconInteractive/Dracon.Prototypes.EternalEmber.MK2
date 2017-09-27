/*
  Copyright (c) 2016 Seawisp Hunter, LLC

  Author: Shane Celis
*/
using UnityEngine.Assertions;
using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using SeawispHunter.MinibufferConsole.Extensions;

namespace SeawispHunter.MinibufferConsole {

public class CommandInfo : ITaggable {
  private readonly Command command;
  public Delegate delegate_;
  public string name;

  public CommandInfo(Command command) {
    this.command = command;
  }

  public MethodInfo methodInfo {
    get { return delegate_ != null ? delegate_.Method : null; }
  }

  private string _group;
  public string group {
    get {
      if (_group == null) {
        if (command.group != null) {
          _group = command.group;
        } else if (methodInfo != null) {
          _group = Minibuffer.instance.GetGroup(methodInfo.DeclaringType, true).name;
            // .GetGroup(Minibuffer.instance.CanonizeCommand).name;
        } else {
          _group = Minibuffer.instance.anonymousGroup.name;
        }
      }
      Assert.IsNotNull(_group);
      return _group;
    }
  }

  public const int BRIEF_DESCRIPTION_MAX_LENGTH = 60;

  public static string GenerateBriefDescription(string desc) {
    if (desc == null)
      return null;
    // Remove everything after the first period like Doxygen.
    var briefDesc = Regex.Replace(desc, @"\..*$", "");
    if (briefDesc.Length > BRIEF_DESCRIPTION_MAX_LENGTH)
      briefDesc = briefDesc.Substring(0, BRIEF_DESCRIPTION_MAX_LENGTH - 3) + "...";
    return briefDesc;
  }

  public string description {
    get { return command.description; }
  }

  private string _briefDescription;
  /**
     The brief description will be generated from the first sentence of the
     description if available; otherwise it's null.
  */
  public string briefDescription {
    get {
      if (_briefDescription == null)
        _briefDescription = command.briefDescription
          ?? (command.description != null ? GenerateBriefDescription(command.description) : null);
      return _briefDescription;
    }
  }

  public string keymap {
    get { return command.keymap ?? group; }
  }


  // public string keyBinding {
  //   get { return command.keyBinding; }
  // }

  private string[] _keyBindings;
  public string[] keyBindings {
    get {
      if (_keyBindings == null)
        _keyBindings = TagUtil.Coalesce(command.keyBindings, command.keyBinding);
      return _keyBindings;
    }
  }

  public string definedIn {
    get { return command.definedIn
                 ?? "class {0}".Formatted(methodInfo.DeclaringType.PrettyName()); }
  }

  public string signature {
    get { return command.signature ?? methodInfo.PrettySignature(); }
  }

  public string[] parameterNames {
    get { return command.parameterNames; }
  }

  public Prompt[] prompts {
    get {
      if (command.prompts != null)
        return command.prompts;
      else {
        // XXX We should return the prompts that are attached to the method parameters.
        return null;
      }
    }
  }
  // public CommandInfo ToBoundCommand(object o) {
  //   MethodInfo z = methodInfo;
  //   var d = Delegate.CreateDelegate(z.CreateDelegateType(),
  //                                   o,
  //                                   z,
  //                                   true);
  //   return new CommandInfo(command) { name = name, delegate_ = d };
  // }

  // private object target {
  //   get { return null; }
  // }

  /*
    XXX Not sure about this one.
    XXX Rename to needsInstance
   */
  public bool instanceExists {
    get {
      if (methodInfo.IsStatic)
        return true;
      else {
        var promise = Minibuffer.instance
          .interpreter.FillInstance(methodInfo.DeclaringType,
                                    false);
        var completer = Minibuffer
          .instance.GetCompleterEntity(methodInfo.DeclaringType).completer;
        if (delegate_.Target != null
            || promise != null
            || (completer != null && completer.Complete("").Any()))
          return true;
        else
          return false;
      }
    }
  }

  private string[] _tags = null;
  public string[] tags {
    get {
      if (_tags == null)
        _tags = TagUtil.Coalesce(command.tags, command.tag);
      return _tags;
    }
  }

  public override string ToString() {
    return "CommandInfo " + name;
  }
}

}
