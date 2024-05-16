﻿using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace CropCare.Models
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Custom map used to display a farm location
    public class CustomMap : Map
    {
        [Obsolete]
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsVisible))
            {
                if (IsVisible)
                {
                    Task.Run(async () =>
                    {
                        // Introduce a small delay before invoking MoveToRegion
                        await Task.Delay(1500);

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            var center = new Location(36.9628066, -122.0194722); // Example center point
                            var span = new MapSpan(center, 0.01, 0.01); // Example span

                            // Move to the specified MapSpan
                            MoveToRegion(span);
                        });
                    });
                }
            }
        }
    }
}