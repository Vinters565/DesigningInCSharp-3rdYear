﻿using System.Windows;
using System.Windows.Controls;
using UI.Windows;

namespace UI.UserControls
{
    /// <summary>
    /// Логика взаимодействия для EmptyBlock.xaml
    /// </summary>
    public partial class EmptyBlock : UserControl
    {
        public bool IsPublic { get; set; } = false;
        private DateTime currentDateTime;

        public EmptyBlock(bool isPublic, DateTime dateTime)
        {
            InitializeComponent();
            this.IsPublic = isPublic;
            this.currentDateTime = dateTime;
        }

        private void CreateNewCalendarEvent_Click(object sender, RoutedEventArgs e)
        {
            var eventWindow = new NewEventWindow(currentDateTime, IsPublic);
            eventWindow.Show();
        }
    }
}
