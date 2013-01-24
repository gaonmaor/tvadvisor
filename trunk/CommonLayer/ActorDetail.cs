using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLayer
{
    /// <summary>
    /// Information about an actor.
    /// </summary>
    public class ActorDetail
    {
        /// <summary>
        /// the id of the actor.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The name of the actor.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The biography of the actor.
        /// </summary>
        public string Biography { get; set; }
    }
}
