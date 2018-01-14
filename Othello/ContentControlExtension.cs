using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Othello
{
    /*
     * https://stackoverflow.com/questions/10831965/begin-animation-when-contentcontrol-content-is-changed
     */
    public static class ContentControlExtensions
    {
        public static readonly DependencyProperty ContentChangedAnimationProperty = DependencyProperty.RegisterAttached("ContentChangedAnimation", typeof(Storyboard), typeof(ContentControlExtensions), new PropertyMetadata(default(Storyboard), ContentChangedAnimationPropertyChangedCallback));

        public static void SetContentChangedAnimation(DependencyObject _element, Storyboard _value)
        {
            _element.SetValue(ContentChangedAnimationProperty, _value);
        }

        public static Storyboard GetContentChangedAnimation(DependencyObject _element)
        {
            return (Storyboard)_element.GetValue(ContentChangedAnimationProperty);
        }

        private static void ContentChangedAnimationPropertyChangedCallback(DependencyObject _dependencyObject, DependencyPropertyChangedEventArgs _dependencyPropertyChangedEventArgs)
        {
            if (!(_dependencyObject is ContentControl contentControl))
                throw new Exception("Can only be applied to a ContentControl");

            var propertyDescriptor = DependencyPropertyDescriptor.FromProperty(ContentControl.ContentProperty,
                typeof(ContentControl));

            propertyDescriptor.RemoveValueChanged(contentControl, ContentChangedHandler);
            propertyDescriptor.AddValueChanged(contentControl, ContentChangedHandler);
        }

        private static void ContentChangedHandler(object _sender, EventArgs _eventArgs)
        {
            var animateObject = (FrameworkElement)_sender;
            var storyboard = GetContentChangedAnimation(animateObject);
            storyboard.Begin(animateObject);
        }
    }
}
