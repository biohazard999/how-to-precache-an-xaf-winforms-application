﻿using how_to_precache_an_xaf_winforms_application.Win;
using Scissors.Xaf.CacheWarmup.Attributes;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("how-to-precache-an-xaf-winforms-application.Win")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

[assembly: XafCacheWarmup(typeof(how_to_precache_an_xaf_winforms_applicationWindowsFormsApplication))]