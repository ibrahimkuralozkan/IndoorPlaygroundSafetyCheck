using IndoorPlaygroundSafetyCheck.Data;
using IndoorPlaygroundSafetyCheck.Models;
using IndoorPlaygroundSafetyCheck.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IndoorPlaygroundSafetyCheck.Services
{
    public class MessagingService
    {
        private readonly Data.SafetyCheckContext _context;
        private readonly int _loggedInUserIdent; // Adjusted to use 'Ident'

        public MessagingService(Data.SafetyCheckContext context, int loggedInUserIdent)
        {
            _context = context;
            _loggedInUserIdent = loggedInUserIdent; // Store the logged-in user's Ident
        }

        public void SendMessage(Message message, int receiverIdent)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            // Set the sender to the logged-in user using Ident
            message.SenderIdent = _loggedInUserIdent;

            // Ensure the Notes field isn't null
            message.Notes = message.Notes ?? string.Empty;

            // Set receiver using Ident
            message.ReceiverIdent = receiverIdent;

            // Ensure DateTime fields are within SQL Server's acceptable range
            message.InsertTimeStamp = SqlDateTimeConverter.EnsureSqlDateTime(DateTime.Now);
            message.UpdateTimeStamp = SqlDateTimeConverter.EnsureSqlDateTime(DateTime.Now);

            // Validate sender existence (optional, based on your business logic)
            var senderExists = _context.Employees.Any(e => e.Ident == _loggedInUserIdent);
            if (!senderExists)
            {
                throw new ArgumentException("Sender does not exist.");
            }

            // Validate receiver existence and position using Ident
            var receiverExists = _context.Employees.Any(e => e.Ident == receiverIdent && e.Position == 1);
            if (!receiverExists)
            {
                throw new ArgumentException("Receiver does not exist or is not eligible to receive messages.");
            }

            _context.Messages.Add(message);
            _context.SaveChanges();
        }

        public IEnumerable<Message> GetMessagesForUser(int userIdent)
        {
            // Fetch messages for a specific user using Ident; ensure it aligns with your business rules
            return _context.Messages
                           .Where(m => m.ReceiverIdent == userIdent)
                           .OrderByDescending(m => m.InsertTimeStamp)
                           .ToList();
        }

        // Add other methods as needed...
    }
}
