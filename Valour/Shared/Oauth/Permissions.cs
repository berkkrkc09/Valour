﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

/*  Valour - A free and secure chat client
 *  Copyright (C) 2020 Vooper Media LLC
 *  This program is subject to the GNU Affero General Public license
 *  A copy of the license should be included - if not, see <http://www.gnu.org/licenses/>
 */

namespace Valour.Server.Oauth
{
    /// <summary>
    /// Permissions are basic flags used to denote if actions are allowed
    /// to be taken on one's behalf
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// The master permission which is used for multiple means
        /// </summary>
        public static readonly Permission FullControl = new Permission(0x00, "Full Control", "Control every part of your account.");

        /// <summary>
        /// The name of this permission
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of this permission
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The value of this permission
        /// </summary>
        public ulong Value { get; set; }

        /// <summary>
        /// Initializes the permission
        /// </summary>
        public Permission(ulong value, string name, string description)
        {
            this.Name = name;
            this.Description = description;
            this.Value = value; 
        }

        /// <summary>
        /// Returns whether the given code includes the given permission
        /// </summary>
        public static bool HasPermission(ulong code, Permission permission)
        {
            // Case if full control is granted
            if (code == FullControl.Value) return true;

            // Otherwise check for specific permission
            return (code & permission.Value) == permission.Value;
        }

        /// <summary>
        /// Creates and returns a permission code from given permissions 
        /// </summary>
        public static ulong CreateCode(params Permission[] permissions)
        {
            ulong code = 0x00;

            foreach (Permission permission in permissions)
            {
                code |= permission.Value;
            }

            return code;
        }
    }

    /// <summary>
    /// This class contains all user permissions and helper methods for working
    /// with them.
    /// </summary>
    public class UserPermissions
    {
        // Use shared full control definition
        public static readonly Permission FullControl = Permission.FullControl;

        // Every subsequent permission has double the value (the next bit)
        // An update should NEVER change the order or value of old permissions
        // As that would be a massive security issue
        public static readonly Permission None = new Permission(0x01, "None", "Only view your account ID when authorized.");
        public static readonly Permission View = new Permission(0x02, "View", "Access basic information about your account.");
        public static readonly Permission Membership = new Permission(0x04, "Membership", "View the planets you are a member of.");
    }
}
