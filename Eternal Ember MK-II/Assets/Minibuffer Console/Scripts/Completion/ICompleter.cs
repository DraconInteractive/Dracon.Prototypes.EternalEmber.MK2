
/*
  Copyright (c) 2016 Seawisp Hunter, LLC

  Author: Shane Celis
*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace SeawispHunter.MinibufferConsole {
/**
   Provide tab completions for an input string.
*/
public interface ICompleter {
  /**
     Return a list of matching strings for the given input. They do not need to
     be in any particular order.
  */
  IEnumerable<string> Complete(string input);
}

}
