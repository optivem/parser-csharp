﻿using OpenQA.Selenium;
using Optivem.Core.Common.WebAutomation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Optivem.Infrastructure.Selenium
{
    public class Driver : IDriver<Element, TextBox, CheckBox, ComboBox, Button, RadioGroup, CheckBoxGroup>, IDriver
    {
        private static Dictionary<FindType, Func<string, By>> findTypeMap
            = new Dictionary<FindType, Func<string, By>>
            {
                { FindType.ClassName, e => By.ClassName(e) },
                { FindType.CssSelector, e => By.CssSelector(e) },
                { FindType.Id, e => By.Id(e) },
                { FindType.LinkText, e => By.LinkText(e) },
                { FindType.Name, e => By.Name(e) },
                { FindType.PartialLinkText, e => By.PartialLinkText(e) },
                { FindType.TagName, e => By.TagName(e) },
                { FindType.XPath, e => By.XPath(e) },
            };

        public Driver(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public IWebDriver WebDriver { get; private set; }

        public string Url
        {
            get { return WebDriver.Url; }
            set { WebDriver.Url = value; }
        }

        public CheckBox FindCheckBox(FindType findType, string findBy)
        {
            var element = FindWebElement(findType, findBy);
            return new CheckBox(element);
        }

        public TextBox FindTextBox(FindType findType, string findBy)
        {
            var element = FindWebElement(findType, findBy);
            return new TextBox(element);
        }

        public RadioGroup FindRadioGroup(FindType findType, string findBy)
        {
            var elements = FindWebElements(findType, findBy);
            return new RadioGroup(elements);
        }


        public CheckBoxGroup FindCheckBoxGroup(FindType findType, string findBy)
        {
            var elements = FindWebElements(findType, findBy);
            return new CheckBoxGroup(elements);
        }



        public ComboBox FindComboBox(FindType findType, string findBy)
        {
            var element = FindWebElement(findType, findBy);
            return new ComboBox(element);
        }



        public Button FindButton(FindType findType, string findBy)
        {
            var element = FindWebElement(findType, findBy);
            return new Button(element);
        }

        public Element FindElement(FindType findType, string findBy)
        {
            var element = FindWebElement(findType, findBy);
            return new Element(element);
        }

        public void Dispose()
        {
            WebDriver.Dispose();
        }


        #region Helper

        private ReadOnlyCollection<IWebElement> FindWebElements(FindType findType, string findBy)
        {
            var byGetter = findTypeMap[findType];
            var by = byGetter(findBy);
            return WebDriver.FindElements(by);
        }

        private IWebElement FindWebElement(FindType findType, string findBy)
        {
            var elements = FindWebElements(findType, findBy);
            return elements.Single();
        }

        IElement IDriver<IElement, ITextBox, ICheckBox, IComboBox, IButton, IRadioButtonGroup, ICheckBoxGroup>.FindElement(FindType findType, string findBy)
        {
            return FindElement(findType, findBy);
        }

        ITextBox IDriver<IElement, ITextBox, ICheckBox, IComboBox, IButton, IRadioButtonGroup, ICheckBoxGroup>.FindTextBox(FindType findType, string findBy)
        {
            return FindTextBox(findType, findBy);
        }

        ICheckBox IDriver<IElement, ITextBox, ICheckBox, IComboBox, IButton, IRadioButtonGroup, ICheckBoxGroup>.FindCheckBox(FindType findType, string findBy)
        {
            return FindCheckBox(findType, findBy);
        }

        IComboBox IDriver<IElement, ITextBox, ICheckBox, IComboBox, IButton, IRadioButtonGroup, ICheckBoxGroup>.FindComboBox(FindType findType, string findBy)
        {
            return FindComboBox(findType, findBy);
        }

        IButton IDriver<IElement, ITextBox, ICheckBox, IComboBox, IButton, IRadioButtonGroup, ICheckBoxGroup>.FindButton(FindType findType, string findBy)
        {
            return FindButton(findType, findBy);
        }

        IRadioButtonGroup IDriver<IElement, ITextBox, ICheckBox, IComboBox, IButton, IRadioButtonGroup, ICheckBoxGroup>.FindRadioGroup(FindType findType, string findBy)
        {
            return FindRadioGroup(findType, findBy);
        }

        ICheckBoxGroup IDriver<IElement, ITextBox, ICheckBox, IComboBox, IButton, IRadioButtonGroup, ICheckBoxGroup>.FindCheckBoxGroup(FindType findType, string findBy)
        {
            return FindCheckBoxGroup(findType, findBy);
        }








        #endregion Helper
    }

    // TODO: VC: DELETE

    /*
     * 
     * 
        public RadioGroup<T> FindRadioGroup<T>(FindType findType, string findBy, Dictionary<string, T> values)
        {
            var elements = FindWebElements(findType, findBy);
            return new RadioGroup<T>(elements, values);
        }
     * 
     * 
        public ICheckBoxGroup<T> FindCheckBoxGroup<T>(FindType findType, string findBy, Dictionary<string, T> values)
        {
            var elements = FindWebElements(findType, findBy);
            return new CheckBoxGroup<T>(elements, values);
        }

        public IComboBox<T> FindComboBox<T>(FindType findType, string findBy, Dictionary<string, T> values)
        {
            var element = FindWebElement(findType, findBy);
            return new ComboBox<T>(element, values);
        }

     * 
     * 
     */

}