using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLayer
{
    /// <summary>
    /// The user details from the database.
    /// </summary>
    public class UserDetail
    {
        /// <summary>
        /// The user id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Indicate if the user is an admin.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// The full constructor.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <param name="name">The user name.</param>
        /// <param name="isAdmin">Indicate if the user is an admin.</param>
        public UserDetail(int id, string name, bool isAdmin)
        {
            Id = id;
            Name = name;
            IsAdmin = isAdmin;
        }
    }
}
