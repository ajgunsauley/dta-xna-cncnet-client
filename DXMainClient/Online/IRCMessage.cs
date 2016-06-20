﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTAClient.Online
{
    public class IRCMessage
    {
        /// <summary>
        /// Creates a new IRCMessage instance.
        /// </summary>
        /// <param name="sender">The sender of the message. Use null for none (system messages).</param>
        /// <param name="color">The color of the message.</param>
        /// <param name="dateTime">The date and time of the message.</param>
        /// <param name="message">The message.</param>
        public IRCMessage(string sender, Color color, DateTime dateTime, string message)
        {
            Sender = sender;
            Color = color;
            DateTime = dateTime;
            Message = message;
        }

        public string Sender { get; private set; }
        public Color Color { get; private set; }
        public DateTime DateTime { get; private set; }
        public string Message { get; private set; }
    }
}