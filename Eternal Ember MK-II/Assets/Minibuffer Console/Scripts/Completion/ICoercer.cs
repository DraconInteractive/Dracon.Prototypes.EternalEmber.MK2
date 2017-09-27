
/*
  Copyright (c) 2016 Seawisp Hunter, LLC

  Author: Shane Celis
*/

using System;

namespace SeawispHunter.MinibufferConsole {
/**
   Coerce a completion string to a desired type.
*/
public interface ICoercer {

  /**
     What is the default or preferred type for this Coercer.  This
     does not mean that the Coerce method cannot produce a radically
     different type.
  */
  Type defaultType { get; }

  /**
     Coerce or lookup an input string to a particular type.

     \note Not strictly parsing or coercision because it can do
     lookups.
  */
  object Coerce(string input, Type desiredType);

  //string message { get; }
}
}
