﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Geo.Core.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ClientExecutor {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ClientExecutor() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Geo.Core.Resources.ClientExecutor", typeof(ClientExecutor).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} uri is invalid..
        /// </summary>
        internal static string Invalid_Uri {
            get {
                return ResourceManager.GetString("Invalid Uri", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} uri is null..
        /// </summary>
        internal static string Null_Uri {
            get {
                return ResourceManager.GetString("Null Uri", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to parse the {0} response properly..
        /// </summary>
        internal static string Reader_Failed_To_Parse {
            get {
                return ResourceManager.GetString("Reader Failed To Parse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} request was cancelled..
        /// </summary>
        internal static string Request_Cancelled {
            get {
                return ResourceManager.GetString("Request Cancelled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} request failed..
        /// </summary>
        internal static string Request_Failed {
            get {
                return ResourceManager.GetString("Request Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The call to {0} failed with an exception..
        /// </summary>
        internal static string Request_Failed_Exception {
            get {
                return ResourceManager.GetString("Request Failed Exception", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The call to {0} did not return a successful http status code. See the exception data for more information..
        /// </summary>
        internal static string Request_Failure {
            get {
                return ResourceManager.GetString("Request Failure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to parse the {0} response properly..
        /// </summary>
        internal static string Serializer_Failed_To_Parse {
            get {
                return ResourceManager.GetString("Serializer Failed To Parse", resourceCulture);
            }
        }
    }
}