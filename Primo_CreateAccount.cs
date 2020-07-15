// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Security;
using OpenQA.Selenium;

namespace Microsoft.Dynamics365.UIAutomation.Sample.UCI
{
    [TestClass]
    public class CreateAccountUCI
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly SecureString _mfaSecretKey = System.Configuration.ConfigurationManager.AppSettings["MfaSecretKey"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void UCITestCreateAccount()
        {
            var client = new WebClient(TestSettings.Options);
            using (var xrmApp = new XrmApp(client))
            {
                xrmApp.OnlineLogin.Login(_xrmUri, _username, _password, _mfaSecretKey);

                xrmApp.Navigation.OpenApp(UCIAppName.Sales);

                xrmApp.ThinkTime(5000);

                xrmApp.Navigation.OpenSubArea("Sales", "Accounts");

                xrmApp.CommandBar.ClickCommand("New");

                xrmApp.Entity.SetValue("name", TestSettings.GetRandomString(5, 15));
                xrmApp.ThinkTime(5000);



                //client.Browser.Driver.FindElement(By.XPath("//input[@aria-label='Phone']")).SendKeys("1346345");
                //xrmApp.ThinkTime(5000);

                //client.Browser.Driver.FindElement(By.XPath("//*[@title='Details']")).Click();
                //xrmApp.ThinkTime(3000);

                //Select Customer Group Field
                client.Browser.Driver.FindElement(By.XPath("//input[@aria-label='Primo SKU']")).Click();
                xrmApp.ThinkTime(5000);

                client.Browser.Driver.FindElement(By.XPath("//input[@aria-label='Group Code, Lookup']")).Click();
                xrmApp.ThinkTime(5000);

                client.Browser.Driver.FindElement(By.XPath("//*[@aria-label='PG01']")).Click();
                xrmApp.ThinkTime(5000);

                //Select Ranking field
                client.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Ranking']")).Click();
                client.Browser.Driver.FindElement(By.XPath("//select[@aria-label='Ranking']/option[text() =  'Platinum']")).Click();
                xrmApp.ThinkTime(10000);

                //Select Price Group field
                client.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Price Group, Lookup']")).Click();
                client.Browser.Driver.FindElement(By.XPath("//*[@class='symbolFont SearchButton-symbol ck cf dk ']")).Click();
                xrmApp.ThinkTime(5000);
                client.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Test Price 1']")).Click();
                xrmApp.ThinkTime(10000);

                //Fill up ABN field
                client.Browser.Driver.FindElement(By.XPath("//input[@aria-label='ABN']")).SendKeys("53 004 085 616");
                xrmApp.ThinkTime(5000);

                xrmApp.Entity.Save();

                xrmApp.ThinkTime(15000);

                xrmApp.Navigation.SignOut();


            }

        }
    }
}