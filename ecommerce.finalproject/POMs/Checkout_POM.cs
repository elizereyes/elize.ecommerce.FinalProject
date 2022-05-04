﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.finalproject.POMs
{
    public class Checkout_POM
    {
        IWebDriver driver;
        public Checkout_POM(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators
        public IWebElement proceedCheckout => driver.FindElement(By.LinkText("Proceed to checkout"));
        public IWebElement firstName => driver.FindElement(By.CssSelector("input#billing_first_name"));
        public IWebElement lastName => driver.FindElement(By.CssSelector("input#billing_last_name"));
        public IWebElement streetAddress => driver.FindElement(By.CssSelector("input[name='billing_address_1']"));
        public IWebElement city => driver.FindElement(By.CssSelector("input#billing_city"));
        public IWebElement postCode => driver.FindElement(By.CssSelector("input#billing_postcode"));
        public IWebElement phoneNo => driver.FindElement(By.CssSelector("input#billing_phone"));
        public IWebElement placeOrder => driver.FindElement(By.CssSelector("button#place_order"));
        public IWebElement orderNo => driver.FindElement(By.CssSelector(".order > strong"));


        //Service Method
        public Checkout_POM BillingDetails()
        {
            proceedCheckout.Click();
            firstName.Clear(); //makes sure the box is empty before continuing
            firstName.SendKeys(Environment.GetEnvironmentVariable("FirstName"));
            lastName.Clear();
            lastName.SendKeys(Environment.GetEnvironmentVariable("LastName"));
            streetAddress.Clear();
            streetAddress.SendKeys(Environment.GetEnvironmentVariable("StreetAddress"));
            city.Clear();
            city.SendKeys(Environment.GetEnvironmentVariable("City"));
            postCode.Clear();
            postCode.SendKeys(Environment.GetEnvironmentVariable("PostCode"));
            phoneNo.Clear();
            phoneNo.SendKeys(Environment.GetEnvironmentVariable("PhoneNo"));
            Thread.Sleep(1000);
            placeOrder.Click();
            return this;
        }

        public int GetOrderNo()
        {
           //waits for the driver url to change so that the Order number can be returned
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(drv => drv.Url.Contains("order-received"));


            Console.WriteLine("Order number: " + orderNo.Text);
            return int.Parse(orderNo.Text);
        }


    }
}