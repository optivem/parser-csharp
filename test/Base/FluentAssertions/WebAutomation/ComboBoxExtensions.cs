﻿using FluentAssertions;
using Atomiv.Core.Common.WebAutomation;

namespace Atomiv.Test.FluentAssertions.WebAutomation
{
    public static class ComboBoxExtensions
    {
        public static void ShouldNotHaveSelection(this IComboBox comboBox)
        {
            comboBox.HasSelected().Should().BeFalse();
        }

        public static void ShouldHaveSelectedValue(this IComboBox comboBox, string key)
        {
            var selected = comboBox.ReadSelectedValue();
            selected.Should().Be(key);
        }
    }
}