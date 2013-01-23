using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLayer
{
    /// <summary>
    /// Contains the info of an order to be save/restore from the database.
    /// </summary>
    public class OrderDetail
    {
        /// <summary>
        /// the user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The channel id (from xmltv)
        /// </summary>
        public string ChanId { get; set; }

        /// <summary>
        /// The program starting time.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Indicate if the order as olready been saved.
        /// </summary>
        public bool Saved { get; set; }

        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="userId">the user id.</param>
        /// <param name="chanId">The channel id (from xmltv)</param>
        /// <param name="start">The program starting time.</param>
        /// <param name="saved">Indicate if the order as olready been saved.</param>
        public OrderDetail(int userId, string chanId, DateTime start, bool saved)
        {
            UserId = userId;
            ChanId = chanId;
            Start = new DateTime(start.Year, start.Month, start.Day, start.Hour, start.Minute, start.Second, DateTimeKind.Local);
            Saved = saved;
        }
    }
}
