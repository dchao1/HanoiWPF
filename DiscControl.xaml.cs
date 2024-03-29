﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1 {
	/// <summary>
	/// Interaction logic for DiscControl.xaml
	/// </summary>
	public partial class DiscControl : UserControl {
		public DiscControl() {
			InitializeComponent();
		}

		public string Text {
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public static readonly DependencyProperty TextProperty =
			 DependencyProperty.Register("Text", typeof(string), typeof(DiscControl), new UIPropertyMetadata(null));

		
	}
}
