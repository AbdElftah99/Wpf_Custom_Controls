﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfProject.CustomControl
{
    public class AnalogClock : Control
    {
        static AnalogClock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnalogClock),
                new FrameworkPropertyMetadata(typeof(AnalogClock)));
        }
    }
}
