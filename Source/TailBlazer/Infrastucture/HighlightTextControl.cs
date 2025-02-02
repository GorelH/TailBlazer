﻿using System;
using System.Collections.Generic;
using System.Linq;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using TailBlazer.Views.Formatting;

namespace TailBlazer.Infrastucture
{

    public class HighlightTextControl : Control
    {
        static HighlightTextControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HighlightTextControl), new FrameworkPropertyMetadata(typeof(HighlightTextControl)));
        }


        public static readonly DependencyProperty HighlightBackgroundProperty = DependencyProperty.Register(
            "HighlightBackground", typeof (Brush), typeof (HighlightTextControl), new PropertyMetadata(default(Brush), UpdateControlCallBack));

        public Brush HighlightBackground
        {
            get { return (Brush) GetValue(HighlightBackgroundProperty); }
            set { SetValue(HighlightBackgroundProperty, value); }
        }

        public static readonly DependencyProperty HighlightForegroundProperty = DependencyProperty.Register(
            "HighlightForeground", typeof (Brush), typeof (HighlightTextControl), new PropertyMetadata(default(Brush), UpdateControlCallBack));

        public Brush HighlightForeground
        {
            get { return (Brush) GetValue(HighlightForegroundProperty); }
            set { SetValue(HighlightForegroundProperty, value); }
        }

        public static readonly DependencyProperty FormattedTextProperty = DependencyProperty.Register(
            "FormattedText", typeof (IEnumerable<DisplayText>), typeof (HighlightTextControl), new PropertyMetadata(default(IEnumerable<DisplayText>), UpdateControlCallBack));

        public IEnumerable<DisplayText> FormattedText
        {
            get { return (IEnumerable<DisplayText>) GetValue(FormattedTextProperty); }
            set { SetValue(FormattedTextProperty, value); }
        }

        public static readonly DependencyProperty HighlightEnabledProperty = DependencyProperty.Register(
            "HighlightEnabled", typeof (bool), typeof (HighlightTextControl), new PropertyMetadata(true, UpdateControlCallBack));

        public bool HighlightEnabled
        {
            get { return (bool) GetValue(HighlightEnabledProperty); }
            set { SetValue(HighlightEnabledProperty, value); }
        }

        private static void UpdateControlCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HighlightTextControl obj = d as HighlightTextControl;
            obj.InvalidateVisual();
        }

        private TextBlock _textBlock;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _textBlock = (TextBlock)this.Template.FindName("PART_TEXT", this);

        }

        protected override void OnRender(DrawingContext drawingContext)
        {

            if (FormattedText == null)
            {
                base.OnRender(drawingContext);
                return;
            }



            _textBlock.Inlines.Clear();
            _textBlock.Inlines.AddRange(FormattedText.Select(ft =>
            {
                var run = new Run(ft.Text);

                if (ft.Highlight && HighlightEnabled)
                {
                    if (HighlightBackground != null) run.Background = HighlightBackground;
                    if (HighlightForeground != null) run.Foreground = HighlightForeground;

                    run.FontWeight = FontWeights.Bold;
                }
                return run;
            }));

            base.OnRender(drawingContext);
        }
    }
}
