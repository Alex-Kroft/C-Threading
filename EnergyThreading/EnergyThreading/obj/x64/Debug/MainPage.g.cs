﻿#pragma checksum "C:\Users\dakro\Documents\GitHub\C-Threading\EnergyThreading\EnergyThreading\MainPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "56B0EBF933618AD1047651A7236AB17AFD4B540436060C62DC3A7645E2A0D195"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EnergyThreading
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // MainPage.xaml line 16
                {
                    this.multiThreadingButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.multiThreadingButton).Click += this.Button_Click_Multithread;
                }
                break;
            case 3: // MainPage.xaml line 17
                {
                    this.singleThreadingButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.singleThreadingButton).Click += this.Button_Click_Singlethread;
                }
                break;
            case 4: // MainPage.xaml line 37
                {
                    this.MyFrame = (global::Windows.UI.Xaml.Controls.Frame)(target);
                    ((global::Windows.UI.Xaml.Controls.Frame)this.MyFrame).Navigated += this.MyFrame_Navigated;
                }
                break;
            case 5: // MainPage.xaml line 39
                {
                    this.resultBox = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBlock)this.resultBox).SelectionChanged += this.TextBlock_SelectionChanged;
                }
                break;
            case 6: // MainPage.xaml line 40
                {
                    this.timeOfDayText = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBlock)this.timeOfDayText).SelectionChanged += this.TextBlock_SelectionChanged;
                }
                break;
            case 7: // MainPage.xaml line 41
                {
                    this.timeOfDay = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBlock)this.timeOfDay).SelectionChanged += this.TextBlock_SelectionChanged;
                }
                break;
            case 8: // MainPage.xaml line 42
                {
                    this.TotalDemand = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBlock)this.TotalDemand).SelectionChanged += this.TextBlock_SelectionChanged;
                }
                break;
            case 9: // MainPage.xaml line 34
                {
                    this.timePicker = (global::Windows.UI.Xaml.Controls.TimePicker)(target);
                }
                break;
            case 10: // MainPage.xaml line 20
                {
                    this.houses = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.houses).SelectionChanged += this.amountOfHouses_SelectionChanged;
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

