// Updated by XamlIntelliSenseFileGenerator 27.05.2024 9:32:39
#pragma checksum "..\..\..\View\Profile.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9CA223AE62682E502CB76F6D39E65E2D8ACB1F1E35CFA62C8130758BD59B4C3D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using CURSACH.View;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace CURSACH.View
{


    /// <summary>
    /// ProfileVip
    /// </summary>
    public partial class ProfileVip : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {


#line 41 "..\..\..\View\Profile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMinimize;

#line default
#line hidden


#line 72 "..\..\..\View\Profile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMaximaze;

#line default
#line hidden


#line 103 "..\..\..\View\Profile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;

#line default
#line hidden


#line 161 "..\..\..\View\Profile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnHome;

#line default
#line hidden


#line 271 "..\..\..\View\Profile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnProfile;

#line default
#line hidden


#line 496 "..\..\..\View\Profile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnExit;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CURSACH;component/view/profile.xaml", System.UriKind.Relative);

#line 1 "..\..\..\View\Profile.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.btnMinimize = ((System.Windows.Controls.Button)(target));

#line 49 "..\..\..\View\Profile.xaml"
                    this.btnMinimize.Click += new System.Windows.RoutedEventHandler(this.btnMinimize_Click);

#line default
#line hidden
                    return;
                case 2:
                    this.btnMaximaze = ((System.Windows.Controls.Button)(target));

#line 80 "..\..\..\View\Profile.xaml"
                    this.btnMaximaze.Click += new System.Windows.RoutedEventHandler(this.btnMaximaze_Click);

#line default
#line hidden
                    return;
                case 3:
                    this.btnClose = ((System.Windows.Controls.Button)(target));

#line 111 "..\..\..\View\Profile.xaml"
                    this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);

#line default
#line hidden
                    return;
                case 4:
                    this.btnHome = ((System.Windows.Controls.Button)(target));

#line 170 "..\..\..\View\Profile.xaml"
                    this.btnHome.Click += new System.Windows.RoutedEventHandler(this.btnHome_Click);

#line default
#line hidden
                    return;
                case 5:
                    this.btnTeam = ((System.Windows.Controls.Button)(target));

#line 208 "..\..\..\View\Profile.xaml"
                    this.btnTeam.Click += new System.Windows.RoutedEventHandler(this.btnTeam_Click);

#line default
#line hidden
                    return;
                case 6:
                    this.btnProjects = ((System.Windows.Controls.Button)(target));

#line 244 "..\..\..\View\Profile.xaml"
                    this.btnProjects.Click += new System.Windows.RoutedEventHandler(this.btnProjects_Click);

#line default
#line hidden
                    return;
                case 7:
                    this.btnProfile = ((System.Windows.Controls.Button)(target));
                    return;
                case 8:
                    this.UserName = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 9:
                    this.UserEmail = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 10:
                    this.UserPhone = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 11:
                    this.UserPassword = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 12:
                    this.btnSave = ((System.Windows.Controls.Button)(target));

#line 471 "..\..\..\View\Profile.xaml"
                    this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnApplyUserDataChane_Click);

#line default
#line hidden
                    return;
                case 13:
                    this.btnExit = ((System.Windows.Controls.Button)(target));

#line 505 "..\..\..\View\Profile.xaml"
                    this.btnExit.Click += new System.Windows.RoutedEventHandler(this.btnExit_Click);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.TextBlock UserName;
        internal System.Windows.Controls.TextBlock UserRole;
        internal System.Windows.Controls.TextBlock UserPhone;
        internal System.Windows.Controls.TextBlock UserPassword;
        internal System.Windows.Controls.TextBlock UserEmail;
    }
}

