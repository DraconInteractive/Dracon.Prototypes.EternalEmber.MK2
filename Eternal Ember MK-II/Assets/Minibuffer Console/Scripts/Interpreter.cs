/*
  Copyright (c) 2016 Seawisp Hunter, LLC

  Author: Shane Celis
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using RSG;
using SeawispHunter.MinibufferConsole.Extensions;

namespace SeawispHunter.MinibufferConsole {
/*
  Minibuffer essentially has a kind of user driven eval/apply
  interpreter when it evaluates a command.

define eval(expr, environment):
   if is_literal(expr): return literal_value(expr)
   if is_symbol(expr):  return lookup_symbol(expr, environment)
   ;; other similar cases here
   ;; remaining (and commonest) case: function application
   function  = extract_function(expr)
   arguments = extract_arguments(expr)
   apply(eval(function, environment), eval_list(arguments, environment))

 define apply(function, arguments):
   if is_primitive(function): return apply_primitive(function, arguments)
   environment = augment(function_environment(function),
                         formal_args(function), arguments)
   return eval(function_body(function), environment)

 def eval_list(items, environment):
   return map( { x -> eval(x, environment) }, items)

Commands are essentially our primitive functions.  Instances and
parameters can be thought of as symbol lookups or determined by user
selection.

  http://c2.com/cgi/wiki?EvalApply
*/
public class Interpreter {
  private Minibuffer minibuffer;
  public struct Applied {
    public MethodInfo methodInfo;
    public object instance;
    public object[] arguments;
  }
  internal Applied lastApplied;

  public Interpreter(Minibuffer minibuffer) {
    this.minibuffer = minibuffer;
  }

  // public abstract object LookupSymbol(string name);

  // public abstract object LookupByType(Type t);

  public IPromise<object> Apply(Delegate delegate_,
                                string[] parameterNames = null,
                                Prompt[] prompts = null) {
    return Apply(delegate_.Method, delegate_.Target, parameterNames, prompts);
  }

  public IPromise<object> Apply(MethodInfo methodInfo,
                                object givenInstance = null,
                                string[] parameterNames = null,
                                Prompt[] prompts = null) {
    bool makePromises = false;
    //bool makePromises = true;
    object instance = null;
    var nearPromises = new List<Func<IPromise<object>>>();
    if (givenInstance != null) {
      instance = givenInstance;
      nearPromises.Add(() => Promise<object>.Resolved(instance));
    } else if (methodInfo.IsStatic) {
      instance = null;
      nearPromises.Add(() => Promise<object>.Resolved(null));
    } else {
      var p = FillInstance(methodInfo.DeclaringType, false);
      if (p == null) {
        makePromises = true;
        nearPromises.Add(() => FillInstance(methodInfo.DeclaringType));
      } else {
        // The promise should be resolved already.
        p.Then(o => instance = o);
        nearPromises.Add(() => p);
      }
    }

    ParameterInfo[] theParams = methodInfo.GetParameters();
    object[] parameters = new object[theParams.Length];
    int i = 0;
    if (methodInfo.IsStatic && givenInstance != null) {
      // We have a first parameter.
      parameters[0] = givenInstance;
      nearPromises.Add(() => Promise<object>.Resolved(givenInstance));
      i = 1;
    }
    // Do we need to make promises in general?  Yes, if we have
    // non-optional parameters.
    makePromises = makePromises || theParams.Where(x => !x.IsOptional).Any();
    for (; i < parameters.Length; ++i) {
      bool hasPromptValue = (prompts != null && prompts[i] != null && prompts[i].defaultValue != null);
      if (hasPromptValue || theParams[i].IsOptional) {
        var value = hasPromptValue ? prompts[i].defaultValue
                                   : theParams[i].DefaultValue;
        if (makePromises)
          nearPromises.Add(() => Promise<object>.Resolved(value));
        else
          parameters[i] = value;
      } else {
        var myParam = theParams[i];
        var myParamName = (parameterNames == null) ? null : parameterNames[i];
        var myPrompt = prompts == null ? null : prompts[i];
        nearPromises.Add(() => FillParam(myPrompt, myParam, myParamName));
      }
    }
    if (makePromises) {
      // XXX I have two execution paths. I should just have one.
      // They do the same thing. I'm just optimizing.
      //print("making big promise for " + command + " with " + nearPromises.Count + " promises");
      return Promise
        .Sequence<object>(nearPromises)
        .Then((objects) =>
            {
              var q = objects.ToQueue();
              var inst = q.Dequeue();
              var args = q.ToArray();
              //print("invoking " + command);
              try {
                lastApplied.methodInfo = methodInfo;
                lastApplied.instance = inst;
                lastApplied.arguments = args;
                object result = methodInfo.Invoke(inst,
                                                  args);
                return result; //object
              } catch (TargetInvocationException tie) {
                Debug.Log("Command invocation exception thrown.");
                Debug.LogException(tie.InnerException);
                throw tie;
              } catch (Exception ex) {
                throw ex;
              }
            })
        .Catch(exception => {
            if (exception is AbortException) {
              Debug.Log("Minibuffer aborted.");
            } else {
              Debug.Log("Command invocation exception thrown.");
              Debug.Log("An exception occured while getting params! "
                        + exception.Message);
              Debug.LogException(exception);
              minibuffer.Message("Error: {0}", exception.Message);
            }
          });
    } else {
      //print("running now " + command);
      try {
        lastApplied.methodInfo = methodInfo;
        lastApplied.instance = instance;
        lastApplied.arguments = parameters;
        var result = methodInfo.Invoke(instance,
                                       parameters);
        //print("invoking " + command);

        //minibuffer.PostCommand(command);
        return Promise<object>.Resolved(result);
      } catch (TargetInvocationException tie) {
        Debug.Log("Command invocation exception thrown.");
        Debug.LogException(tie.InnerException);
        return Promise<object>.Rejected(tie.InnerException);
      }
    }
  }

  /*

   */
  internal IPromise<object> FillInstance(System.Type type,
                                         bool fillType = true) {
    var name = type.PrettyName();
    var ce = minibuffer.GetCompleterEntity(type);
    var completer = ce.completer;
    List<string> list;
    if (minibuffer.instances.ContainsKey(name)) {
      // This is like a lookup_symbol.
      return Promise<object>.Resolved(minibuffer.instances[name]);
    } else if (completer != null
               //&& completer as ICoercer != null
               && (list = completer.Complete("").ToList()).Count == 1
               && ce.coercer != null) {
      // If there's just one completion, provide that.
      //Debug.Log("FillInstance for " + name + " auto resolved.");
      return Promise<object>.Resolved(ce.coercer.Coerce(list[0], type));
    } else if (type == typeof(Minibuffer)) {
      // This is like a lookup_by_type.
      return Promise<object>.Resolved(this);
    } else if (type == typeof(InputField)) {
      return Promise<object>.Resolved(minibuffer.gui.input);
    // } else if (type == typeof(TappedInputField)) {
    //     // Might want to have a focus that actually moves around.
    //   return Promise<object>.Resolved(minibuffer.gui.minibufferPrompt);
    } else if (typeof(IBuffer).IsAssignableFrom(type)) {
      return Promise<object>.Resolved(minibuffer.gui.main.buffer);
    } else if (type == typeof(Window)) {
      // This is like a primitive_function.
      return Promise<object>.Resolved(minibuffer.showAutocomplete
                                      ? minibuffer.gui.autocomplete
                                      : minibuffer.gui.main);
    } else {
      var prompt = new PromptInfo();
      prompt.prompt = "Instance " + name + ": ";

      return fillType ? FillType(prompt, type) : null;
    }
  }

  public IPromise<object> FillParam(Prompt promptSettings, ParameterInfo paramInfo, string paramName = null) {
    PromptInfo prompt;
    object [] attrs;
    if (promptSettings == null) {
      attrs = paramInfo.GetCustomAttributes (typeof(Prompt), false);
      prompt = PromptInfo.CopyOrNew((Prompt) attrs.FirstOrDefault());
    } else {
      prompt = new PromptInfo(promptSettings);
    }
    if (prompt.prompt == null) {
      prompt.prompt = paramInfo.ParameterType.PrettyName() + " "
        + (paramName ?? paramInfo.Name) + ": ";
    }
    attrs = paramInfo.GetCustomAttributes(typeof(UniversalArgument), false);
    if (attrs.Any()) {
      if (paramInfo.ParameterType == typeof(int?)) {
        return Promise<object>.Resolved(minibuffer.currentPrefixArg);
      } else if (paramInfo.ParameterType == typeof(int)) {
        return Promise<object>.Resolved(minibuffer.currentPrefixArg.HasValue
                                        ? minibuffer.currentPrefixArg
                                        : 1); // default value of 0 or 1?
      } else if (paramInfo.ParameterType == typeof(bool)) {
        return Promise<object>.Resolved(minibuffer.currentPrefixArg.HasValue);
      } else {
        return Promise<object>
          .Rejected(new MinibufferException(
            "UniversalArgument can only be int, int?, or bool not "
            + paramInfo.ParameterType.PrettyName()));
      }
    }
    attrs = paramInfo.GetCustomAttributes(typeof(Current), false);
    if (attrs.Any()) {
      var currentAttr = (Current) attrs.First();
      ICurrentProvider provider = null;
      foreach(var cp in Minibuffer.instance.currentProviders) {
        if (cp.CanProvideType(paramInfo.ParameterType)) {
          if (provider == null) {
            provider = cp;
          } else {
            return Promise<object>
              .Rejected(new MinibufferException(
                 "Ambiguity. Multiple ICurrentProviders can provide type"
                 + paramInfo.ParameterType.PrettyName()
                 + ". In this case both "
                 + cp.canonicalType.PrettyName()
                 + " and "
                 + provider.canonicalType.PrettyName() + "."));
          }
        }
      }

      if (provider != null) {
        var obj = provider.CurrentObject();
        if (obj != null
            || (obj == null && currentAttr.acceptNull))
          return Promise<object>.Resolved(obj);
        else {
          return Promise<object>.Rejected(new MinibufferException(
            "The parameter [Current] {0} {1} can not be null."
            .Formatted(paramInfo.ParameterType.PrettyName(),
                       paramInfo.Name)));
        }
      } else {
        return Promise<object>
          .Rejected(new MinibufferException(
             "No ICurrentProvider found for type "
             + paramInfo.ParameterType.PrettyName()
             + "; these are the registered types: "
             + String.Join(", ",
                           Minibuffer.instance.currentProviders
                             .Select(c => c.canonicalType.PrettyName())
                             .ToArray()) + "."));
      }
    }
    return FillType(prompt, paramInfo.ParameterType);
  }

  internal IPromise<object> FillField(FieldInfo fieldInfo) {
    // XXX Since when do fields have any Prompt attributes?
    object[] attrs = fieldInfo.GetCustomAttributes(typeof(Prompt), false);
    PromptInfo prompt = PromptInfo.CopyOrNew((Prompt) attrs.FirstOrDefault());
    if (prompt.prompt == null)
      prompt.prompt = fieldInfo.FieldType.PrettyName() + " "
        + fieldInfo.Name + ": ";

    return FillType(prompt, fieldInfo.FieldType);
  }

  public IPromise<object> FillType(PromptInfo prompt, Type t) {
    Type promptResultType = null;
    if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(PromptResult<>)) {
      promptResultType = t;
      t = promptResultType.GetGenericArguments()[0];
    } else if (t == typeof(PromptResult)) {
      promptResultType = t;
      t = typeof(object);
    }
    prompt.desiredType = t;
    if (prompt.ignore) {
      return Promise<object>.Resolved(null);
    } else if (prompt.filler == "keybinding") {
      return FillKeyBinding(prompt).Then(s => (object) s);
    } else if (t == typeof(bool)) {
      // Handle booleans
      return minibuffer.ReadTrueOrFalse(prompt.prompt)
        .Then<object>((bool b) => (object) b);
    } else if (t.IsArray) {
      // Handle arrays
      var promptp = new PromptInfo(prompt);
      if (promptp.prompt == null)
        promptp.prompt = t.PrettyName() + ": ";
      return FillArray(promptp, t.GetElementType());
    } else if (t.IsGenericType
               && t.GetGenericTypeDefinition() == typeof(List<>)) {
      // Handle lists
      var promptp = new PromptInfo(prompt);
      if (promptp.prompt == null)
        promptp.prompt = t.PrettyName() + ": ";
      return FillList(promptp, t.GetGenericArguments()[0]);
    } else if (t == typeof(Vector3)) {
      // XXX Should have a better way of dealing with generic constructors.
      return FillVector3(prompt).Then(v => (object) v);
    } else if (t == typeof(Vector2)) {
      return FillVector2(prompt).Then(v => (object) v);
    } else if (t == typeof(Vector4)) {
      // ExecuteCommand can turn any command into a "filler".

      // Eval("MakeVector4")
      return minibuffer.ExecuteCommand("MakeVector4");
    } else if (t == typeof(Quaternion)) {
      return minibuffer.ExecuteCommand("MakeQuaternion");
    } else if (t == typeof(Matrix4x4)) {
      return FillMatrix4x4(prompt).Then(m => (object) m);
    // } else if (t == typeof(InputField) || t == typeof(TappedInputField)) {
    //   return Promise<object>.Resolved(minibuffer.gui.input);
    } else {
      // Handle whatever
      // var promise = new Promise<object>();

      if (prompt.completerEntity.Any()) {
        // This takes precedence.
      } else if (prompt.completions != null && prompt.completer != null) {
        CompleterEntity ce;
        if (minibuffer.completers.TryGetValue(prompt.completer, out ce)) {
          prompt.completerEntity
            = new ListCompleter(prompt.completions).ToEntity().Combine(ce);
        } else {
          prompt.completerEntity
            = new ListCompleter(prompt.completions).ToEntity();
          minibuffer.MessageAlert("No such completer {0}.", prompt.completer);
        }
      } else if (prompt.completions != null) {
        prompt.completerEntity
          = new ListCompleter(prompt.completions).ToEntity();
      } else if (prompt.completer != null) {
        CompleterEntity ce;
        if (minibuffer.completers.TryGetValue(prompt.completer, out ce)) {
          prompt.completerEntity = ce;
        } else {
          // Maybe it's a dynamically generated completer.
          prompt.completerEntity = minibuffer.GetCompleterEntity(t, true);
          if (! prompt.completerEntity.Any())
            return Promise<object>
              .Rejected(new MinibufferException("No such completer {0}."
                                                .Formatted(prompt.completer)));
        }
      }
      if (! prompt.completerEntity.Any()) {
        prompt.completerEntity = minibuffer.GetCompleterEntity(t);
      }
      if (prompt.history == null)
        prompt.history = t.PrettyName();
      if (prompt.completerEntity.coercer == null
          && t != typeof(string)) {
        var msg = string.Format("No coercer for type {0}.", t.PrettyName());
        return Promise<object>.Rejected(new MinibufferException(msg));
      }
      if (t.IsNumericType()) {
        prompt.requireCoerce = true;
        prompt.requireMatch = false;
      }
      prompt.desiredType = t;
      var p = minibuffer.MinibufferEdit(prompt);
      if (promptResultType == typeof(PromptResult))
        return p.Then<object>(pr => (object) pr);
      else if (promptResultType != null) {
        return p.Then(promptResult => {
            string input = promptResult.str;
            object obj = promptResult.obj;
            // We were asked for a PromptResult<T>, give them back
            // that.
            object pr = Activator.CreateInstance(promptResultType);
            promptResultType.GetField("str").SetValue(pr, input);
            promptResultType.GetField("obj")
            .SetValue(pr,
                      obj != null && t.IsAssignableFrom(obj.GetType())
                      ? obj
                      : null);
            //Debug.Log("input = " + input + " obj " + obj);
            if (obj != null) {
              if (t.IsAssignableFrom(obj.GetType()) || t == typeof(string)) {
                return pr;
              } else {
                var msg = string.Format("Unable to assign expected type {0} to given type {1}.",
                                        t.PrettyName(),
                                        obj.GetType().PrettyName());
                throw new MinibufferException(msg);
              }
            } else {
              return pr;
            }
          });
      } else {
        return p.Then(promptResult => {
            string input = promptResult.str;
            object obj = promptResult.obj;
            //Debug.Log("input = " + input + " obj " + obj);
            if (obj != null) {
              if (t.IsAssignableFrom(obj.GetType())) {
                return obj;
              } else if (t == typeof(string)) {
                // Choice here. Do we give them the input string, or the
                // obj.ToString()? We give them the object string. If they want
                // the input string, they can ask for a PromptResult<T>.

                // return input;
                return obj.ToString();
              } else {
                var msg = string.Format("Unable to assign expected type {0} to given type {1}.",
                                        t.PrettyName(),
                                        obj.GetType().PrettyName());
                throw new MinibufferException(msg);
              }
            } else if (t == typeof(string)) {
              return input;
            } else {
              var msg = string.Format("Unable to coerce '{1}' to type {0}.",
                                      t.PrettyName(),
                                      input);
              throw new MinibufferException(msg);
            }
          });
      }
    }
  }

  /*
    Fill a List (not a List<T>) with objects of the given Type T. This
    can be then used to create Arrays (T[]) or Lists (List<T>) or
    other composite types like Vector3.
   */
  public IPromise<IEnumerable<object>>
    FillObjectList(PromptInfo prompt, Type t, int? elementCount = null) {
    var promise = new Promise<IEnumerable<object>>();
    var elementType = t;
    if (prompt.prompt == null)
      prompt.prompt = elementType.PrettyName();
    IPromise<int> promisedCount;
    if (! elementCount.HasValue) {
      promisedCount = minibuffer.Read<int>(new PromptInfo(prompt)
          { prompt = "Count of " + prompt.prompt });
    } else
      promisedCount = Promise<int>.Resolved((int) elementCount);
    promisedCount
      .Then(count => {
          // We got a count.
          var promises = new List<Func<IPromise<object>>>();
          for (int i = 0; i < count; i++) {
            var j = i;
            promises.Add(() => FillType(new PromptInfo(prompt) {
                  prompt = string.Format("Item {0} of {1} for {2}",
                                         j, count, prompt.prompt)
                },
                elementType));
          }
          Promise
          .Sequence<object>(promises)
          .Then((objects) => promise.Resolve(objects))
          .Catch(ex => promise.Reject(ex));
        })
      .Catch(ex => promise.Reject(ex));
    return promise;
  }

  public IPromise<object> FillArray(PromptInfo prompt, Type t) {
    var elementType = t;
    return FillObjectList(prompt, t)
      .Then((objects) => {
          var list = objects.ToList();
          var a = Array.CreateInstance(elementType, list.Count);
          Array arr = (Array) a;
          for (int i = 0; i < list.Count; i++)
            arr.SetValue(list[i], i);
          return (object) a;
        });
  }

  public IPromise<object> FillList(PromptInfo prompt, Type t) {
    var elementType = t;
    return FillObjectList(prompt, t)
      .Then((objects) =>
          {
            var listType = typeof(List<>);
            var l = (IList) Activator.CreateInstance(listType.MakeGenericType(elementType));
            var list = objects.ToList();
            for (int i = 0; i < list.Count; i++)
              l.Add(list[i]);
            return (object) l;
          });
  }

  public IPromise<Vector3> FillVector3(PromptInfo prompt) {
    // XXX Prompt is totally ignored here.
    return minibuffer.ExecuteCommand<Vector3>("MakeVector3");
    // return FillObjectList(prompt, typeof(float), 3)
    //   .Then(objects =>
    //       {
    //         var list = objects.ToList();
    //         return new Vector3((float) list[0], (float) list[1], (float) list[2]);
    //       });
  }

  public IPromise<Matrix4x4> FillMatrix4x4(PromptInfo prompt) {
    return FillObjectList(prompt, typeof(float), 16)
      .Then(objects =>
          {
            var list = objects.ToList();
            var m = Matrix4x4.zero;
            for (int i = 0; i < 16; i++)
              m[i] = (float) list[i];
            return m;
          });
  }

  public IPromise<Vector2> FillVector2(PromptInfo prompt) {
    return minibuffer.ExecuteCommand<Vector2>("MakeVector2");
    // return FillObjectList(prompt, typeof(float), 2)
    //   .Then(objects =>
    //       {
    //         var list = objects.ToList();
    //         return new Vector2((float) list[0], (float) list[1]);
    //       });
  }

  internal IPromise<object> FillProperty(PropertyInfo propertyInfo) {
    object[] attrs = propertyInfo.GetCustomAttributes(typeof(Prompt), false);
    PromptInfo prompt = PromptInfo.CopyOrNew((Prompt) attrs.FirstOrDefault());
    if (prompt.prompt == null)
      prompt.prompt
        = propertyInfo.PropertyType.PrettyName() + " "
        + propertyInfo.Name + ": ";

    return FillType(prompt, propertyInfo.PropertyType);
  }

  //[Filler("keybinding")]
  /**
    Returns an existing keybinding from the user.
   */
  public IPromise<string> FillKeyBinding(PromptInfo prompt,
                                         List<string> keyAccum = null,
                                         Promise<string> promise = null) {
    if (keyAccum == null)
      keyAccum = new List<string>();
    if (promise == null)
      promise = new Promise<string>();
    minibuffer.visible = true;
    minibuffer.Message(prompt.prompt + String.Join(" ", keyAccum.ToArray()));
    minibuffer.ReadKeyChord()
      .Then(k => {
          var key = k.ToString();
          keyAccum.Add(key);
          key = String.Join(" ", keyAccum.ToArray());
          var command = minibuffer.Lookup(key);
          if (command != null) {
            promise.Resolve(key);
          } else if (minibuffer.prefixes.Contains(key)) {
            // There's more.
            FillKeyBinding(prompt, keyAccum, promise);
          } else {
            // There's nothing.
            if (prompt.requireMatch)
              promise.Reject(new MinibufferException(key + " is not bound to any command."));
            else
              promise.Resolve(key);
          }
        })
      .Catch(ex => promise.Reject(ex));
    return promise;
  }

}
}
