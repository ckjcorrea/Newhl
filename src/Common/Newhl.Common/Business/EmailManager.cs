/**
 * Copyright (c) 2009 Arthur Correa.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Common Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/cpl1.0.php
 *
 * Contributors:
 *    Arthur Correa – initial contribution
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using Newhl.Common.Configuration;
using Newhl.Common.Utilities;

namespace Newhl.Common.Business
{
    /// <summary>
    /// This class drives all email sending from the system.
    /// </summary>
    public class EmailManager
    {
        public static bool IsValidEmail(string emailString)
        {
            return Regex.IsMatch(emailString, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }   

        private EmailConfiguration emailConfig = null;

        /// <summary>
        /// The constructor forces the caller to define the email configuration options necessary to
        /// connect to the smtp server.
        /// </summary>
        /// <param name="emailConfig"></param>
        public EmailManager(EmailConfiguration emailConfig)
        {
            this.emailConfig = emailConfig;
        }
        /// <summary>
        /// Send an email to a specific address.
        /// </summary>
        /// <param name="fromAddress">What address is the email sent from</param>
        /// <param name="toAddress">Where is the email going</param>
        /// <param name="emailSubject">The email subject line</param>
        /// <param name="emailBody">The email message</param>
        public void SendEmail(string fromAddress, string toAddress, string emailSubject, string emailBody)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(this.emailConfig.FromAddress);
            mail.To.Add(toAddress);
            mail.Subject = emailSubject;
            mail.Body = emailBody;

            try
            {
                SmtpClient smtpClient = new SmtpClient(this.emailConfig.SmtpServer, this.emailConfig.SmtpPort);
                smtpClient.Send(mail);
            }
            catch (Exception e)
            {
                LogManager.GetLogger().Error(e);
            }
        }
    }
}
