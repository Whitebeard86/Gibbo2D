﻿#pragma checksum "..\..\..\Windows\NewProjectWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D26F40F5A8C8760D640B8789947AE61C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
<<<<<<< HEAD
//     Runtime Version:4.0.30319.18051
=======
//     Runtime Version:4.0.30319.34003
>>>>>>> adab60d19e1a01c7784b43c84c3c8dbabfe955f5
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Gibbo.Editor.WPF {
    
    
    /// <summary>
    /// NewProjectWindow
    /// </summary>
    public partial class NewProjectWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\Windows\NewProjectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox pathTxt;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\Windows\NewProjectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button pathBtn;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Windows\NewProjectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button createBtn;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Windows\NewProjectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancelBtn;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Windows\NewProjectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nameTxt;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Windows\NewProjectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button samplesBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Gibbo.Editor.WPF;component/windows/newprojectwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\NewProjectWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 4 "..\..\..\Windows\NewProjectWindow.xaml"
            ((Gibbo.Editor.WPF.NewProjectWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 6 "..\..\..\Windows\NewProjectWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Border_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.pathTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.pathBtn = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\..\Windows\NewProjectWindow.xaml"
            this.pathBtn.Click += new System.Windows.RoutedEventHandler(this.pathBtn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.createBtn = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\..\Windows\NewProjectWindow.xaml"
            this.createBtn.Click += new System.Windows.RoutedEventHandler(this.createBtn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cancelBtn = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\Windows\NewProjectWindow.xaml"
            this.cancelBtn.Click += new System.Windows.RoutedEventHandler(this.cancelBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.nameTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.samplesBtn = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\Windows\NewProjectWindow.xaml"
            this.samplesBtn.Click += new System.Windows.RoutedEventHandler(this.samplesBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

