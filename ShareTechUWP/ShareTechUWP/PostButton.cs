#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;

namespace ShareTechUWP
{

    public class PostButton : Button
    {
        private const int buttonWidth = 2560;
        private const int buttonHeight = 80;
        private const int topMargin = buttonHeight + 5;

        // public UserInfo Author { get; set; }
        public PostData Post;
        private const int MaxBodyTextCount = 30;

        public PostButton(in PostData post, 
            RoutedEventHandler postButtonClickNavigate, int buttonsCount)
        {
            Post = post;

            Click += postButtonClickNavigate;
            Height = buttonHeight;
            Margin = new(0, topMargin * buttonsCount, 0, 0);
            VerticalAlignment = VerticalAlignment.Top;
            CornerRadius = new(10);

            Grid grid1 = new();
            Grid grid2 = new()
            {
                Height = 80,
                Width = 2560,
                Margin = new(-13, 0, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left
            };
            Grid grid3 = new()
            {
                Width = 65,
                Margin = new(10, 0, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left
            };
            TextBlock nickname = new()
            {
                FontSize = 15,
                Text = Post.User.NickName,
                Margin = new(0, 0, 0, 12),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                // FontFamily = new("Consolas"),
                FontWeight = FontWeights.Bold
            };
            grid3.Children.Add(nickname);
            grid2.Children.Add(grid3);
            TextBlock title = new()
            {
                FontSize = 17,
                Text = Post.Title,
                Margin = new(80, 8, 0, 44),
                Width = buttonWidth
            };
            TextBlock body = new()
            {
                FontSize = 15,
                Text = (Post.Body.Length > MaxBodyTextCount) ? Post.Body.Substring(0, 19) + "..." : Post.Body,
                Margin = new(80, 38, 0, 13),
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = buttonWidth,
                Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xA0, 0xA0, 0xA0))
            };

            grid2.Children.Add(title);
            grid2.Children.Add(body);

            grid1.Children.Add(grid2);
            Content = grid1;
        }
    }
}
