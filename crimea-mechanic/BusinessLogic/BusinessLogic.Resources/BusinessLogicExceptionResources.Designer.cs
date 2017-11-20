﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessLogic.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class BusinessLogicExceptionResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal BusinessLogicExceptionResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BusinessLogic.Resources.BusinessLogicExceptionResources", typeof(BusinessLogicExceptionResources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Заявка не принадлежит автосервису.
        /// </summary>
        public static string ApplicationDoesNotBelongToCarService {
            get {
                return ResourceManager.GetString("ApplicationDoesNotBelongToCarService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Заявка не принадлежит пользователю.
        /// </summary>
        public static string ApplicationDoesNotBelongToUser {
            get {
                return ResourceManager.GetString("ApplicationDoesNotBelongToUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Состояние заявки не позволяет выполнить данное действие.
        /// </summary>
        public static string ApplicationIncorrectState {
            get {
                return ResourceManager.GetString("ApplicationIncorrectState", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Заявка не найдена.
        /// </summary>
        public static string ApplicationNotFound {
            get {
                return ResourceManager.GetString("ApplicationNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Для данной заявки предложение уже было отправлено.
        /// </summary>
        public static string ApplicationOfferAlreadyContains {
            get {
                return ResourceManager.GetString("ApplicationOfferAlreadyContains", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Предложение не принадлежит автосервису.
        /// </summary>
        public static string ApplicationOfferDoesNotBelongToCarService {
            get {
                return ResourceManager.GetString("ApplicationOfferDoesNotBelongToCarService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Предложение на заявку не найдено.
        /// </summary>
        public static string ApplicationOfferNotFound {
            get {
                return ResourceManager.GetString("ApplicationOfferNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Машина не принадлежит пользователю.
        /// </summary>
        public static string CarDoesNotBelongToUser {
            get {
                return ResourceManager.GetString("CarDoesNotBelongToUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Марка автомобиля не найдена.
        /// </summary>
        public static string CarMarkNotFound {
            get {
                return ResourceManager.GetString("CarMarkNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Машина не может быть удалёна пока не буду удалены либо завершены все заявки.
        /// </summary>
        public static string UserCarCanNotBeRemove {
            get {
                return ResourceManager.GetString("UserCarCanNotBeRemove", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Машина пользователя не найдена.
        /// </summary>
        public static string UserCarNotFound {
            get {
                return ResourceManager.GetString("UserCarNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невозможно выполнить данное действие, пользователь находится в другой роли.
        /// </summary>
        public static string UserHasDifferentRole {
            get {
                return ResourceManager.GetString("UserHasDifferentRole", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Пользователь не найден.
        /// </summary>
        public static string UserNotFound {
            get {
                return ResourceManager.GetString("UserNotFound", resourceCulture);
            }
        }
    }
}
