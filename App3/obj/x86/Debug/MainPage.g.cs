﻿#pragma checksum "D:\Проект\MiBand (2) (3)\MiBand\App3\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6D750E7191AC11456C20938E4DB4C1D9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App3
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // MainPage.xaml line 14
                {
                    this.btnStart = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnStart).Click += this.Button_Click;
                }
                break;
            case 3: // MainPage.xaml line 17
                {
                    this.tbCount = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 4: // MainPage.xaml line 20
                {
                    this.tbHr = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5: // MainPage.xaml line 22
                {
                    this.LineChart = (global::WinRTXamlToolkit.Controls.DataVisualization.Charting.Chart)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

